using Godot;
using System;

public class Bouquet : Graft {
  bool topAreaEntered = false;
  bool midAreaEntered = false;
  int topTaps = 0;
  int topTapsComplete = 0;

  protected override void SetObjectives()
  {
    numTapsComplete = rng.Next(4,6);
		topTapsComplete = numTapsComplete;
    GD.Print("you have to tap the bottom " + numTapsComplete + " and the top " + topTapsComplete + " times!");
  }
  protected override void CheckObjectives()
  {
    if(numTaps >= numTapsComplete && topTaps >= topTapsComplete) {
      numTaps = 0;
      topTaps = 0;
      isFinished = true;
      GD.Print("tapping complete.");
    }

    if(topAreaEntered) {
      if(lCannula.CheckCannulaRotation(this.GetRotation(), 1.396f, 1.745f) || rCannula.CheckCannulaRotation(this.GetRotation(), 1.396f, 1.745f)) {
        if(lCannula.tapped || rCannula.tapped) {
          topTaps += 1;
          lCannula.tapped = false;
          rCannula.tapped = false;
          GD.Print("top tap registered");
        }
      }
    }

    if(midAreaEntered) {
      if(lCannula.CheckCannulaRotation(this.GetRotation(), 0f, 0.52f) || rCannula.CheckCannulaRotation(this.GetRotation(), 0f, 0.52f)) {
        if(lCannula.tapped || rCannula.tapped) {
          numTaps += 1;
          lCannula.tapped = false;
          rCannula.tapped = false;
          GD.Print("bot tap registered");
        }
      }
    }
  }

  private void _OnTopAreaEntered(object area) {
    topAreaEntered = true;
    GD.Print("top area entered.");
  }

  private void _OnTopAreaExited(object area) {
    topAreaEntered = false;
  }

  private void _OnMidAreaEntered(object area) {
    midAreaEntered = true;
    GD.Print("mid area entered.");
  }

  private void _OnMidAreaExited(object area) {
    midAreaEntered = false;
  }
}
