using Godot;
using System;

public class TutorialPrompt : RichTextLabel
{   
    private String levelName = "";
    private Boolean shownWelcomeMessage = false;
    private Boolean isTutorialMode;

    //ok will get the level, then based on the level offer the correct prompts
    public override void _Ready()
    {
        var levelSwitcher = GetNode<LevelSwitcher>("/root/LevelSwitcher");
        // Get the levelName from the levelSwitcher:
        levelName = levelSwitcher.getLevelName(); 
        isTutorialMode = levelSwitcher.tutorialMode();
    }

  //  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(float delta)
  {
    var levelSwitcher = GetNode<LevelSwitcher>("/root/LevelSwitcher");

    if (isTutorialMode) {

      if (!levelSwitcher.welcomeMessage()) {
        WelcomeMessage();
      }
      else {
        if (levelName == "res://Scroll.tscn") {
            SetText("This confirmation is called a scroll \n\n" +
                        "Tap one cannula 3-5 times horizontally on the top end of " +  
                        "it to send fluid waves into the fold to open it \n" +
                        "Tap another cannula 4-6 times vertically on the center of the fold");
          
        }

        if (levelName == "res://SimpleFold.tscn") {
              SetText("This confirmation is called a simple fold \n\n" +
                        "Tap one cannula in the middle 3-6 times \n" +
                        "Hold the another cannula on the right portion of the graft");
          
        }

        if (levelName == "res://DoubleScroll.tscn") {
              SetText("This confirmation is called a double scroll \n\n" +
                        "Tap one cannula in the middle 3-6 times");
          
        }

        if (levelName == "res://EdgeFold.tscn") {
              SetText("This confirmation is called an edge fold \n\n" +
                        "Tap one cannula in the middle 3-6 times \n" +
                        "Hold the another cannula on the right portion of the graft");
          
        }
      }
    } 
  }

  public void WelcomeMessage() {
      var levelSwitcher = GetNode<LevelSwitcher>("/root/LevelSwitcher");

      SetText("Welcome to the tutorial! \n\n" +
                "Press h to view the controls. \n\n" +
                "The tutorial will walk you through " + 
                "how to tap each cornea transplant confirmation correctly.\n" + 
                "TAP on PINK areas and HOLD the cannula on GREEN ones \n\n" +
                "Press enter to continue!");
      if (Input.IsActionJustPressed("continue")) {
          levelSwitcher.setWelcomeMessage(true);
          }
  }
}
