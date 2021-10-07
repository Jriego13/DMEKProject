using Godot;
using System;

public class TutorialPrompt : RichTextLabel
{   
    private String levelName = "";
    private Boolean shownWelcomeMessage = false;

    //ok will get the level, then based on the level offer the correct prompts
    public override void _Ready()
    {
        var levelSwitcher = GetNode<LevelSwitcher>("/root/LevelSwitcher");
        // Get the levelName from the levelSwitcher:
        levelName = levelSwitcher.getLevelName(); 
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(float delta)
  {
      
    if (levelName == "res://Scroll2D.tscn") {
        if (!shownWelcomeMessage) {
        WelcomeMessage();
        }
       else {
           SetText("This confirmation is called a scroll \n\n" +
                    "Tap one cannula horizontally on the top end of " +  
                    "it to send fluid waves into the fold to open it \n" +
                    "Tap another cannula vertically on the center of the fold");
       } 

        if (Input.IsActionJustPressed("continue")) {shownWelcomeMessage = true;}
    }
  }

  public async void WelcomeMessage() {
      SetText("Welcome to the tutorial! \n\n" +
                "Press h to view the controls. \n\n" +
                "The tutorial will walk you through " + 
                "how to tap each cornea transplant confirmation correctly.\n" + 
                "The target areas are highlighted pink \n\n" +
                "Press enter to continue!");
  }
}