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
    private int waterLevel;
    private int curNumTaps;
    private int curTopTaps;
    private bool levelComplete;

    public override void _Ready()
    {
        GD.Print("here inside maineye2dtut");
        tutorialPrompt = GetNode("Overlay/TutorialPrompt") as RichTextLabel;
        tutorialPrompt.SetVisible(true);
        var levelSwitcher = GetNode<LevelSwitcher>("/root/LevelSwitcher");
        levelName = levelSwitcher.getLevelName();
        loadConfirmation(levelName);
        waterLevelUI = GetNode("UI") as MarginContainer;
        waterLevelUI.SetVisible(true);
        curNumTaps = 0;
        curTopTaps = 0;
        successfulTapPrompt = GetNode("Overlay/SuccessfulTapPrompt") as RichTextLabel;
        successfulTapPrompt.SetVisible(false);
        helpPrompt = GetNode("Overlay/HelpPrompt") as RichTextLabel;
        helpPrompt.SetVisible(true);
        levelCompletePrompt = GetNode("Overlay/LevelCompletePrompt") as RichTextLabel;
        levelCompletePrompt.SetVisible(false);
        waterUIPrompt = GetNode("Overlay/WaterUIPrompt") as RichTextLabel;
        levelComplete = false;
    }
    public override void _Process(float delta)
    {
        //base._Process(delta);
        // If confirmation is complete, load the next confirmation:
        if (confirmation.getIsFinished())
        {
            successMode();
        }
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
    protected override void SetUp()
    {
        GD.Print("here inside maineye2dtut");
        tutorialPrompt = GetNode("Overlay/TutorialPrompt") as RichTextLabel;
        confirmation.misclicksOn = false;
    }    
    protected override void OnConfirmationFinished()
    {
        successMode();
    }

    private async void showSuccessfulTapPrompt() {
        GD.Print("successfil tap");
        successfulTapPrompt.SetVisible(true);
        await ToSignal(GetTree().CreateTimer(1), "timeout");
        successfulTapPrompt.SetVisible(false);
    }

    private void successMode() {
        successfulTapPrompt.SetVisible(false);
        tutorialPrompt.SetVisible(false);
        waterLevelUI.SetVisible(false);
        helpPrompt.SetVisible(false);
        levelCompletePrompt.SetVisible(true);
        waterLevelUI.SetVisible(false);
        levelComplete = true;
    }
}