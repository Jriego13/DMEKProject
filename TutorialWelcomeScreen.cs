using Godot;
using System;

public class TutorialWelcomeScreen : Node2D
{
    private RichTextLabel prompt;
    private Node2D mainMenu;
    private ColorRect tap;
    private ColorRect hold;
    private int enterCount = 0;
    private bool showHelpPrompt;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        prompt = GetNode("Prompt") as RichTextLabel;
        tap = GetNode("Background/Eyeball/DemoGraft/Tap") as ColorRect;
        hold = GetNode("Background/Eyeball/DemoGraft/Hold") as ColorRect;
        Color tapColor = new Color( 0.98f, 0.5f, 0.45f, .5f );
        Color holdColor = new Color( 0.5f, 1f, 0f, .5f );
        tap.Color = tapColor;
        hold.Color = holdColor;
        showHelpPrompt = false;

    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        var levelSwitcher = GetNode<LevelSwitcher>("/root/LevelSwitcher");
        
        if(Input.IsActionJustPressed("toggle_help")) {
            showHelpPrompt = !showHelpPrompt;
        }

        if (!showHelpPrompt) {
            if (Input.IsActionJustPressed("ui_left")) {
                if (enterCount == 1) {
                enterCount--;
                }
            }
            if (Input.IsActionJustPressed("continue")) {
                enterCount++;
            }

            if (enterCount == 0) {
            prompt.SetText("Welcome to the tutorial! \n\n" +
                    "Press h to view the controls. \n\n" +
                    "The tutorial will walk you through " + 
                    "how to tap each cornea transplant confirmation correctly.\n\n" + 
                    "TAP on PINK areas and HOLD the cannula on GREEN ones." +
                    "The purple dots are incisions that you hold on to let in liquid \n\n" +
                    "Press enter to continue!");
            }
            if (enterCount == 1) {
            prompt.SetText("Above is the fluid level in the eye\n" +
            "For most grafts, keep it between 10-30.\n\nFor flipped grafts " + 
            "like taco and inverted it needs to be deep (>50). \n\nFluid is" +
            " inserted by holding on an incision point in purple.\n\nHit enter to test your skills"
            +"\nPress left arrow to see previous message");
            }
            if (enterCount == 2) {
                levelSwitcher.setWelcomeMessage(true);
                Input.SetMouseMode((Godot.Input.MouseMode)0);
                GetTree().ChangeScene(Helper.toFileName("TutorialSelect"));
            }
        }
        else {
            prompt.SetText("Press esc to return to menu \n" 
              +"Rotate cannula clockwise: s ccw: w" + "\n" + "\n"
              + "swap cannula control: r" + "\n"
              + "reset cannula: e" + "\n" 
              + "inject liquid: hold cannula on incision marks \n" 
              + "extract liquod: o \n \n"
              + "tap l/r cannula: l/r mouse" + "\n"+ "\n"
              + "hold mouse to keep cannula down"
              + "press h to minimze this message");
        }

    }  
}
