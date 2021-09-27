using Godot;
using System;

public class Bouquet : Graft {
  Cannula2D lCannula;
  Cannula2D rCannula;
  bool topAreaEntered = false;
  bool midAreaEntered = false;
  int topTaps = 0;
  int topTapsComplete = 0;

  public override void _Ready() {
    numTapsComplete = rng.Next(4,6);
		topTapsComplete = numTapsComplete;
    lCannula = GetNode("../Cannulas/CannulaLSprite") as Cannula2D;
    rCannula = GetNode("../Cannulas/CannulaRSprite") as Cannula2D;
		GD.Print("you have to tap the bottom " + numTapsComplete + " and the top " + topTapsComplete + " times!");
  }

  public override void _Process(float delta) {
    if(numTaps >= numTapsComplete && topTaps >= topTapsComplete) {
      numTaps = 0;
      topTaps = 0;
      isFinished = true;
      GD.Print("tapping complete.");
    }

    if(midAreaEntered) {
      if(lCannula.CheckCannulaRotation(0f, 0.34f) || rCannula.CheckCannulaRotation(0f, 0.34f)) {
        if(lCannula.tapped || rCannula.tapped) {
          numTaps += 1;
          lCannula.tapped = false;
          rCannula.tapped = false;
          GD.Print("bot tap registered");
        }
      }
    }

    if(topAreaEntered) {
      if(lCannula.CheckCannulaRotation(1.39f, 1.74f) || rCannula.CheckCannulaRotation(1.39f, 1.74f)) {
        if(lCannula.tapped || rCannula.tapped) {
          topTaps += 1;
          lCannula.tapped = false;
          rCannula.tapped = false;
          GD.Print("top tap registered");
        }
      }
    }
  }

  private void _OnTopAreaEntered(object area) {
    topAreaEntered = true;
  }

  private void _OnTopAreaExited(object area) {
    topAreaEntered = false;
  }

  private void _OnMidAreaEntered(object area) {
    midAreaEntered = true;
  }

  private void _OnMidAreaExited(object area) {
    midAreaEntered = false;
  }
}
