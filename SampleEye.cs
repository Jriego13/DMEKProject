using Godot;
using System;

public class SampleEye : Spatial
{

    public override void _Ready()
    {
        var levelSwitcher = GetNode<LevelSwitcher>("/root/LevelSwitcher");
        var foldName = levelSwitcher.getLevelName();
        GD.Print(foldName);
        var confirmation = GD.Load<PackedScene>(foldName);
        var node = confirmation.Instance();
        GetNode("/root/Spatial/MainEye").AddChild(node);
    }
}
