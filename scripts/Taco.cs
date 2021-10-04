using Godot;
using System;

public class Taco : Graft {
  Cannula2D lCannula;
  Cannula2D rCannula;
  bool tapAreaEntered = false;
  bool holdAreaEntered = false;
  bool heldDown = false;

  public override void _Ready() {
    lCannula = GetNode("../Cannulas/CannulaLSprite") as Cannula2D;
    rCannula = GetNode("../Cannulas/CannulaRSprite") as Cannula2D;
  }

  public void _OnTapAreaEntered(object area) {
    tapAreaEntered = true;
  }

  public void _OnTapAreaExited(object area) {
    tapAreaEntered = false;
  }

  public void _OnHoldAreaEntered(object area) {
    holdAreaEntered = true;
    GD.Print("hold area entered");
  }

  public void _OnHoldAreaExited(object area) {
    holdAreaEntered = false;
  }
}
