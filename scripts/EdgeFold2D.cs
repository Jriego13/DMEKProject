using Godot;
using System;

public class EdgeFold2D : Graft {
  Cannula2D lCannula;
  Cannula2D rCannula;
  bool tapAreaEntered = false;
  bool holdAreaEntered = false;
  bool heldDown = false;

  public override void _Ready() {
    numTapsComplete = rng.Next(3,6);
    lCannula = GetNode("../Cannulas/CannulaLSprite") as Cannula2D;
    rCannula = GetNode("../Cannulas/CannulaRSprite") as Cannula2D;
    GD.Print("you have to tap " + numTapsComplete + " times!");
  }

  public override void _Process(float delta) {
    if(numTaps >= numTapsComplete) {
      numTaps = 0;
      isFinished = true;
      GD.Print("tapping complete.");
    }

    if(tapAreaEntered) {
      if(heldDown) {
        if(lCannula.tapped || rCannula.tapped) {
          numTaps += 1;
          lCannula.tapped = false;
          rCannula.tapped = false;
          GD.Print("tap registered");
        }
      }
    }

    if(holdAreaEntered) {
      if(lCannula.locked || rCannula.locked) {
        heldDown = true;
      }
    }
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
