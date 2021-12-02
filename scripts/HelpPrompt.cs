using Godot;
using System;
using System.Threading;


// text at the front which tells you which confirmation you are on
public class HelpPrompt : RichTextLabel {
  bool showHelp;
  private HelpPopup helpPopUp;
  private Sprite anteriorChamber;
  private Boolean isTutorial;

  public override void _Ready() {
    var levelSwitcher = GetNode<LevelSwitcher>("/root/LevelSwitcher");
    isTutorial = levelSwitcher.tutorialMode();
	  showHelp = false;
    helpPopUp = GetNode("../../HelpPopup") as HelpPopup;
    SetBbcode("[right]Press 'h' to show controls[/right]");
	  showHelp = false;
    anteriorChamber = GetNode("../AnteriorChamber") as Sprite;
  }

  public override void _Process(float delta) {
    if(Input.IsActionJustPressed("toggle_help")) {
      showHelp = !showHelp;
      helpPopUp.changeVisiblity(showHelp);
    }

    if (showHelp) {
      SetBbcode("[right]Press 'h' to close window![/right]");
      anteriorChamber.SetVisible(!showHelp);
    }

    if (!showHelp) {
      if (isTutorial) {
        SetBbcode("[right]Press 'h' to show controls[/right]\n" +
               "[right]'k' for tutorial help[/right]");
      }
      else {
        SetBbcode("[right]Press 'h' to show controls[/right]");
      }
    }

  
  }

  public bool showingHelp() {
      return showHelp;
  }

  public void setShowHelp(bool input) {
      this.showHelp = input;
  }
}
