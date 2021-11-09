using Godot;
using System;

public class TutorialPrompt : RichTextLabel
{   
    private String levelName = "";
    private Boolean shownWelcomeMessage = false;
    private Boolean shownWaterUIMessage = false;
    private Boolean isTutorialMode;
    private RichTextLabel waterUIPrompt;
    protected Cannula2D lCannula;
	  protected Cannula2D rCannula;
    
    

    //ok will get the level, then based on the level offer the correct prompts
    public override void _Ready()
    { 
        lCannula = GetNode("../Cannulas/CannulaLSprite") as Cannula2D;
        rCannula = GetNode("../Cannulas/CannulaRSprite") as Cannula2D;
        var levelSwitcher = GetNode<LevelSwitcher>("/root/LevelSwitcher");
        waterUIPrompt = GetNode("../WaterUIPrompt") as RichTextLabel;
        waterUIPrompt.SetVisible(false);
        this.SetVisible(true);
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
            SetText("This confirmation is a scroll \n\n" +
                        "Tap one cannula 3-5 times horizontally on the top end of " +  
                        "it to send fluid waves into the fold to open it \n" +
                        "Tap another cannula 4-6 times vertically on the center of the fold");
          
        }

        if (levelName == "res://SimpleFold.tscn") {
              SetText("This confirmation is a simple fold \n\n" +
                        "Tap one cannula in the middle 3-6 times \n" +
                        "Hold the another cannula on the right portion of the graft");
          
        }

        if (levelName == "res://DoubleScroll.tscn") {
              SetText("This confirmation is a double scroll \n\n" +
                        "Tap one cannula in the middle 3-6 times");
          
        }

        if (levelName == "res://EdgeFold.tscn") {
              SetText("This confirmation is an edge fold \n\n" +
                        "Tap one cannula in the middle 3-6 times \n" +
                        "Hold the another cannula on the right portion of the graft");
          
        }

        if (levelName == "res://Taco.tscn") {
              SetText("This confirmation is called a taco \n\n");
          
        }

        if (levelName == "res://Inverted.tscn") {
              SetText("This confirmation is an inverted graft \n\n" +
                      "You need to inject fluid into the inverted section");
          
        }
      }
    } 
  }

  public void WelcomeMessage() {
      var levelSwitcher = GetNode<LevelSwitcher>("/root/LevelSwitcher");
      
      if (!shownWelcomeMessage && !shownWaterUIMessage) {
        this.SetVisible(true);
        SetText("Welcome to the tutorial! \n\n" +
                "Press h to view the controls. \n\n" +
                "The tutorial will walk you through " + 
                "how to tap each cornea transplant confirmation correctly.\n" + 
                "TAP on PINK areas and HOLD the cannula on GREEN ones." +
                "The purple dots are incisions that you hold on to let in liquid \n\n" +
                "Press enter to continue!");
          if (Input.IsActionJustPressed("continue")) {
          shownWelcomeMessage = true;
          }
      }
      else if (shownWelcomeMessage && !shownWaterUIMessage) {
        this.SetVisible(false);
        waterUIPrompt.SetVisible(true);
        waterUIPrompt.SetText("Above is the fluid level in the eye\n" +
         "For most grafts, keep it between 10-30.\n For flipped grafts " + 
         "like taco and inverted it needs to be deep (>50). \n Fluid is" +
         " inserted by holding on an incision point in purple.\n Hit enter to continue");
        if (Input.IsActionJustPressed("continue")) {
          levelSwitcher.setWelcomeMessage(true);
          waterUIPrompt.SetVisible(false);
          this.SetVisible(true);
          }
      }
      
  }
}
