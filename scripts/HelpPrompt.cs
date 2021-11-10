using Godot;
using System;
using System.Threading;

// text at the front which tells you which confirmation you are on
public class HelpPrompt : RichTextLabel {
  bool showHelp;

  public override void _Ready() {
	   showHelp = false;
  }

  public override void _Process(float delta) {
    if(Input.IsActionJustPressed("toggle_help")) {
      showHelp = !showHelp;
    }

    if (!showHelp) {
        SetBbcode("[right]Press 'h' to show controls[/right]");
    }
    else {
      SetBbcode("[right] Press esc to return to menu \n" 
              +"Rotate cannula clockwise: s ccw: w" + "\n" + "\n"
              + "swap cannula control: r" + "\n"
              + "reset cannula: e" + "\n" 
              + "inject liquid in: p out: o \n \n"
              + "tap l/r cannula: l/r mouse" + "\n"+ "\n"
              + "hold mouse to keep cannula down[/right]");
    }
  }

  public bool showingHelp() {
      return showHelp;
  }

  public void setShowHelp(bool input) {
      this.showHelp = input;
  }
}
