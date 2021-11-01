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

    if(midAreaEntered) {
      if(lCannula.CheckCannulaRotation(0f, 0.34f) || rCannula.CheckCannulaRotation(0f, 0.34f)) {
        if((lCannula.tapped && lCannula.inArea) || (rCannula.tapped && rCannula.inArea)) {
          numTaps += 1;
          lCannula.tapped = false;
          rCannula.tapped = false;
          GD.Print("bot tap registered");
        }
      }
    }

    if(topAreaEntered) {
      if(lCannula.CheckCannulaRotation(1.39f, 1.74f) || rCannula.CheckCannulaRotation(1.39f, 1.74f)) {
        if((lCannula.tapped && lCannula.inArea) || (rCannula.tapped && rCannula.inArea)) {
          topTaps += 1;
          lCannula.tapped = false;
          rCannula.tapped = false;
          GD.Print("top tap registered");
        }
      }
    }
  }

  private void _OnTopAreaEntered(Area2D area) {
    topAreaEntered = true;
    Cannula2D currentCannula = area.GetParent() as Cannula2D;
    currentCannula.inArea = true;
    GD.Print("top area entered.");
  }

  private void _OnTopAreaExited(Area2D area) {
    topAreaEntered = false;
    Cannula2D currentCannula = area.GetParent() as Cannula2D;
    currentCannula.inArea = false;
  }

  private void _OnMidAreaEntered(Area2D area) {
    midAreaEntered = true;
    Cannula2D currentCannula = area.GetParent() as Cannula2D;
    currentCannula.inArea = true;
    GD.Print("mid area entered.");
  }

  private void _OnMidAreaExited(Area2D area) {
    midAreaEntered = false;
		Cannula2D currentCannula = area.GetParent() as Cannula2D;
		currentCannula.inArea = false;
  }
}
