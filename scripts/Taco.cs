using Godot;
using System;

public class Taco : Graft {
  bool tapAreaEntered = false;
  bool holdAreaEntered = false;
  bool heldDown = false;
  protected override void SetObjectives()
  {

  }
  protected override void CheckObjectives()
  {

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
