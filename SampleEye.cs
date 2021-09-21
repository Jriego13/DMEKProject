using Godot;
using System;

public class SampleEye : Spatial
{

    public override void _Ready()
    {
        var levelSwitcher = GetNode<LevelSwitcher>("/root/LevelSwitcher");
        var foldName = levelSwitcher.getFoldName();
        GD.Print(foldName);
        var confirmation = GD.Load<PackedScene>(foldName);
        var node = confirmation.Instance();
        GetNode("/root/Spatial/MainEye").AddChild(node);
    }
}
