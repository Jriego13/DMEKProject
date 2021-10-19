using Godot;
using System;

public class MainEye2DTutorial : Node
{
    private String levelName = "";
    private Graft confirmation;
    RichTextLabel tutorialPrompt;

    public override void _Ready()
    {
        GD.Print("here inside maineye2dtut");
        tutorialPrompt = GetNode("Overlay/TutorialPrompt") as RichTextLabel;
        // Load the singleton:
        var levelSwitcher = GetNode<LevelSwitcher>("/root/LevelSwitcher");
        // Get the levelName from the levelSwitcher:
        levelName = levelSwitcher.getLevelName();
        loadConfirmation(levelName);
    }
    public override void _Process(float delta)
    {
        base._Process(delta);
        // If confirmation is complete, load the next confirmation:
        if (confirmation.getIsFinished())
        {

            // Delete the current node:
            confirmation.QueueFree();
            // Add new node to the tree:
            Input.SetMouseMode((Godot.Input.MouseMode)0);
            GetTree().ChangeScene(Helper.toFileName("TutorialSuccessScreen"));
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
}