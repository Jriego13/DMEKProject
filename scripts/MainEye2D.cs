using Godot;
using System;

public class MainEye2D : MainGame
{
    // levelName is random by default so it can be loaded without levelSelect

    public override void _Ready()
    {
        // PrintTreePretty();

        // Load the singleton:
        var levelSwitcher = GetNode<LevelSwitcher>("/root/LevelSwitcher");
        // Get the levelName from the levelSwitcher:
        levelName = levelSwitcher.getLevelName();
        Helper.startLevel = levelName;
        loadConfirmation(levelName);

        // _tween = GetNode("UI/Tween") as Tween;
        successfulTapPrompt = GetNode("Overlay/SuccessfulTapPrompt") as RichTextLabel;
        successfulTapPrompt.SetVisible(false);
        levelCompletePrompt = GetNode("Overlay/LevelCompletePrompt") as RichTextLabel;
        levelCompletePrompt.SetVisible(false);
    }

     public override void _Process(float delta)
    {
        //base._Process(delta);
        
        if (confirmation.getIsFinished())
        {
            // Delete the current node:
            confirmation.QueueFree();
            // Add new node to the tree:
            Input.SetMouseMode((Godot.Input.MouseMode)0);
            OnConfirmationFinished();
        }
    }
    protected override void OnConfirmationFinished()
    {
        // Add new node to the tree:
        String nextLevel = Helper.getNextConfirmation(levelName);
        loadConfirmation(nextLevel);
    }
    protected override void SetUp()
    {
        
    }
}
