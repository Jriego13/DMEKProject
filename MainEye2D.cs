using Godot;
using System;

public class MainEye2D : Node2D
{
    public override void _Ready()
    {
        var levelSwitcher = GetNode<LevelSwitcher>("/root/LevelSwitcher");
        var foldName = levelSwitcher.getFoldName();
        GD.Print("instancing "+ foldName);
        var confirmation = GD.Load<PackedScene>(foldName);
        var node = confirmation.Instance();
        GetNode("/root/Main").AddChild(node);
    }
}
