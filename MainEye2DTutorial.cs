using Godot;
using System;

// This class should only contain code that is specific to the tutorial game
// Otherwise, it should go in MainGame.cs
public class MainEye2DTutorial : MainGame
{
    RichTextLabel tutorialPrompt;
    private RichTextLabel helpPrompt;
    private RichTextLabel waterUIPrompt;
    private MarginContainer waterLevelUI;
    private int curNumTaps;
    private int curTopTaps;
    private bool levelComplete;

    protected override void CheckObjectives()
    {
        if (Input.IsActionPressed("continue") && levelComplete) {
            GD.Print("u pressed enter, here we go!");
            confirmation.QueueFree();
            Input.SetMouseMode((Godot.Input.MouseMode)0);
            GetTree().ChangeScene(Helper.toFileName("TutorialSuccessScreen"));
        }
        
        if (curNumTaps < confirmation.getNumTaps()) {
            showSuccessfulTapPrompt();
            curNumTaps = curNumTaps + 1;
        }

        if (curTopTaps < confirmation.getTopTaps()) {
            showSuccessfulTapPrompt();
            curTopTaps = curTopTaps + 1;
        }
    }
    // Sets up the tutorial
    protected override void SetUp()
    {
        GD.Print("here inside maineye2dtut");
        tutorialPrompt = GetNode("Overlay/TutorialPrompt") as RichTextLabel;
        tutorialPrompt.Visible = true;
        confirmation.misclicksOn = false;
        curNumTaps = 0;
        curTopTaps = 0;
        levelComplete = false;
        
        helpPrompt = GetNode("Overlay/HelpPrompt") as RichTextLabel;
        helpPrompt.Visible = true;
        
        waterUIPrompt = GetNode("Overlay/WaterUIPrompt") as RichTextLabel;
        waterLevelUI = GetNode("UI") as MarginContainer;
        waterLevelUI.Visible = true;
    }    
    protected override void OnConfirmationFinished()
    {
        successMode();
    }

    private async void showSuccessfulTapPrompt() {
        GD.Print("successful tap");
        successfulTapPrompt.Visible = true;
        await ToSignal(GetTree().CreateTimer(1), "timeout");
        successfulTapPrompt.Visible = false;
    }

    private void successMode() {
        successfulTapPrompt.Visible = false;
        tutorialPrompt.Visible = false;
        waterLevelUI.Visible = false;
        helpPrompt.Visible = false;
        levelCompletePrompt.Visible = true;
        waterLevelUI.Visible = false;
        levelComplete = true;
    }
}