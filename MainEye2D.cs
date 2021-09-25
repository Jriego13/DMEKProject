using Godot;
using System;

public class MainEye2D : Node2D
{
    public override void _Ready()
    {
        // Load the singleton:
        var levelSwitcher = GetNode<LevelSwitcher>("/root/LevelSwitcher");
        // Get the levelName from the levelSwitcher:
        var levelName = levelSwitcher.getLevelName();
        GD.Print("instancing "+ levelName);
        // Load the specified level/fold, instance it as a Node2D, then place it in the tree:
        var confirmation = GD.Load<PackedScene>(levelName);
        var confirmationNode = (Node2D)confirmation.Instance();
        GetNode("/root/Main").AddChild(confirmationNode);
    }
}
