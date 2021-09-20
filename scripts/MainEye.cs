using Godot;
using System;

public class MainEye : Node2D {
    public override void _Ready() {
      GD.Print("eye ready.");
    }

    private void _OnAreaEntered(object area) {
      GD.Print("we are in your area.");
    }

    private void _OnMouseEntered() {
      GD.Print("mouse is in your area.");
    }
}
