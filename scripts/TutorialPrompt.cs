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

    if (levelName == "res://Scroll.tscn") {
        if (!shownWelcomeMessage) {
        WelcomeMessage();
        }
       else {
           SetText("This confirmation is called a scroll \n\n" +
                    "Tap one cannula 3-5 times horizontally on the top end of " +  
                    "it to send fluid waves into the fold to open it \n" +
                    "Tap another cannula 4-6 times vertically on the center of the fold");
       } 
    }

    if (levelName == "res://SimpleFold.tscn") {
      if (!shownWelcomeMessage) {
        WelcomeMessage();
        }
       else {
           SetText("This confirmation is called a simple fold \n\n" +
                    "Tap one cannula in the middle 3-6 times \n" +
                    "Hold the another cannula on the right portion of the graft");
       } 
    }

     if (levelName == "res://DoubleScroll.tscn") {
      if (!shownWelcomeMessage) {
        WelcomeMessage();
        }
       else {
           SetText("This confirmation is called a double scroll \n\n" +
                    "Tap one cannula in the middle 3-6 times");
       } 
    }

    if (levelName == "res://EdgeFold.tscn") {
      if (!shownWelcomeMessage) {
        WelcomeMessage();
        }
       else {
           SetText("This confirmation is called an edge fold \n\n" +
                    "Tap one cannula in the middle 3-6 times \n" +
                    "Hold the another cannula on the right portion of the graft");
       }
    } 
  }

  public void WelcomeMessage() {
      SetText("Welcome to the tutorial! \n\n" +
                "Press h to view the controls. \n\n" +
                "The tutorial will walk you through " + 
                "how to tap each cornea transplant confirmation correctly.\n" + 
                "TAP on PINK areas and HOLD the cannula on GREEN ones \n\n" +
                "Press enter to continue!");
      if (Input.IsActionJustPressed("continue") && !shownWelcomeMessage) {
          shownWelcomeMessage = true;
          }
  }
}