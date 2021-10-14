using Godot;
using System;

public class EdgeFold : Graft {
  bool tapAreaEntered = false;
  bool holdAreaEntered = false;
  bool heldDown = false;

  protected override void SetObjectives() {
    currentConfirmation = "EdgeFold";
    numTapsComplete = rng.Next(3,6);
    GD.Print("you have to tap " + numTapsComplete + " times!");
  }

  protected override void CheckObjectives() {
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
          if(numTaps < graftTextures.Count)
            SetTexture(graftTextures[numTaps]);
            
          GD.Print("tap registered");
        }
      }
    }

    if(holdAreaEntered) {
      if(lCannula.locked || rCannula.locked) {
        heldDown = true;
      }
    }

    if(!tapAreaEntered){
      if(rCannula.tapped || lCannula.tapped){
        numTaps -= 1;
        SetTexture(graftTextures[numTaps]);
        registerMisclick();
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
