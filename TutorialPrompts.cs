using Godot;
using System;

public class TutorialPrompts : RichTextLabel
{   
    private String levelName = "";
    private Boolean shownWelcomeMessage;

    //ok will get the level, then based on the level offer the correct prompts
    public override void _Ready()
    {
        var levelSwitcher = GetNode<LevelSwitcher>("/root/LevelSwitcher");
        // Get the levelName from the levelSwitcher:
        levelName = levelSwitcher.getLevelName();    
        shownWelcomeMessage = false; 
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(float delta)
  {
    if (levelName == "res://Scroll2D.tscn") {
        if (!shownWelcomeMessage) {
        WelcomeMessage();
        }
       else {
           SetText("This is a scroll \n\n" +
                    "You need to tap the bottom and top about 5 times each");
       } 

        if (Input.IsActionJustPressed("continue")) {shownWelcomeMessage = true;}
    }
  }

  public async void WelcomeMessage() {
      SetText("Welcome to the tutorial! \n\n" +
                "Press h to view the controls. \n\n" +
                "The tutorial will walk you through " + 
                "how to tap each cornea transplant confirmation correctly. \n\n" +
                "Press enter to continue!");
  }
}
