using Godot;
using System;

public class MainEye2DTutorial : Node
{
    private String levelName = "";
    private Graft confirmation;
    RichTextLabel tutorialPrompt;
    private RichTextLabel helpPrompt;
    private TextureProgress bar;
    private RichTextLabel waterLevelCounter;
    private RichTextLabel successfulTapPrompt;
    private RichTextLabel levelCompletePrompt;
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
        // Load the singleton:
        var levelSwitcher = GetNode<LevelSwitcher>("/root/LevelSwitcher");
        // Get the levelName from the levelSwitcher:
        levelName = levelSwitcher.getLevelName();
        loadConfirmation(levelName);
        bar = GetNode("UI/TextureProgress") as TextureProgress;
        waterLevelUI = GetNode("UI") as MarginContainer;
        waterLevelUI.SetVisible(true);
        waterLevelCounter = GetNode("UI/NinePatchRect/WaterLevel") as RichTextLabel;
        waterLevel = 100;
        bar.Value = waterLevel;
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
        base._Process(delta);
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

         if(Input.IsActionPressed("addLiquid")){
            GD.Print("trying to add liquid");
            if(waterLevel <= 99){
               waterLevel += 1;
               bar.Value = waterLevel;
               waterLevelCounter.Text = waterLevel.ToString();
            }

        }
        if(Input.IsActionPressed("removeLiquid")){
            GD.Print("trying to remove liquid");
            if(waterLevel >= 1){
                waterLevel -= 1;
                bar.Value = waterLevel;
                waterLevelCounter.Text = waterLevel.ToString();

            }
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
    
    // Load the specified level/fold, instance it as a Node2D, then place it in the tree:
    private void loadConfirmation(String next)
    {
        levelName = next;
        GD.Print("instancing "+ levelName);
        var confirmationScene = GD.Load<PackedScene>(levelName);
        confirmation = (Graft)confirmationScene.Instance();
        GetNode("/root/Main").AddChild(confirmation);
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