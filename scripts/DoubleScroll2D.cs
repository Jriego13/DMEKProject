using Godot;
using System;

public class DoubleScroll2D : Graft {
  Cannula2D lCannula;
  Cannula2D rCannula;
  PackedScene scene1;
  PackedScene scene2;
  bool areaEntered = false;

  public override void _Ready() {
    numTapsComplete = rng.Next(3,6);
    lCannula = GetNode("../Cannulas/CannulaLSprite") as Cannula2D;
    rCannula = GetNode("../Cannulas/CannulaRSprite") as Cannula2D;
    GD.Print("you have to tap the bottom " + numTapsComplete + " times!");
  }

  public override void _Process(float delta) {
    if(numTaps >= numTapsComplete) {
      numTaps = 0;
      GD.Print("tapping complete.");
      isFinished = true;
    }

    if(areaEntered) {
      if(lCannula.tapped || rCannula.tapped) {
        numTaps += 1;
        lCannula.tapped = false;
        rCannula.tapped = false;
        GD.Print("tap registered");
      }
    }
  }

  private void _OnAreaEntered(object area) {
    areaEntered = true;
  }

  private void _OnAreaExited(object area) {
    areaEntered = false;
  }
}
