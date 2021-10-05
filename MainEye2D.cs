using Godot;
using System;

public class MainEye2D : Node2D
{
    // levelName is random by default so it can be loaded without levelSelect
    private String levelName = Helper.getRandomConfirmation();
    private Graft confirmation;

    public override void _Ready()
    {
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
            String nextLevel = Helper.getNextConfirmation(levelName);
            loadConfirmation(nextLevel);
        }
    }

    // Load the specified level/fold, instance it as a Node2D, then place it in the tree:
    private void loadConfirmation(String next)
    {
        GD.Print(next);
        if(next == "res://Done.tscn") {
          GetTree().ChangeScene("res://Done.tscn");
          return;
        }

        levelName = next;
        GD.Print("instancing "+ levelName);
        var confirmationScene = GD.Load<PackedScene>(levelName);
        confirmation = (Graft)confirmationScene.Instance();
        GetNode("/root/Main").AddChild(confirmation);
    }
}
