using Godot;
using System;

public class DoubleScroll2D : Graft {
  PackedScene scene1;
  PackedScene scene2;
  bool areaEntered = false;
  protected override void SetObjectives()
  {
    numTapsComplete = rng.Next(3,6);
    GD.Print("you have to tap the bottom " + numTapsComplete + " times!");
  }
  protected override void CheckObjectives()
  {
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

    if(!areaEntered){
			if(rCannula.tapped || lCannula.tapped){
				registerMisclick();
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
