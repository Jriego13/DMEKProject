using Godot;
using System;

public class MainEye2D : Node2D
{
    public override void _Ready()
    {
        var levelSwitcher = GetNode<LevelSwitcher>("/root/LevelSwitcher");
        var foldName = levelSwitcher.getFoldName();
        GD.Print("instancing "+ foldName);
        // Load the specified fold, instance it as a Node2D, then place it in the tree:
        var confirmation = GD.Load<PackedScene>(foldName);
        var confirmationNode = (Node2D)confirmation.Instance();
        GetNode("/root/Main").AddChild(confirmationNode);
        // Scale the confirmation up:
        confirmationNode.Scale = new Vector2(0.04f, 0.04f);
    }
}
