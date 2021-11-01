using Godot;
using System;

// in the case of the inverted graft, numTaps really means numInjects of BSS
public class Inverted : Graft {
  bool injectAreaEntered = false;

  protected override void SetObjectives() {
    currentConfirmation = "Inverted";
    numTapsComplete = rng.Next(3,5);
    var levelSwitcher = GetNode<LevelSwitcher>("/root/LevelSwitcher");
		// isTutorialMode = levelSwitcher.tutorialMode();
		// setUpHitboxes(isTutorialMode);
		GD.Print("you have to inject " + numTapsComplete + " times!");
  }

  protected override void CheckObjectives() {
    if(numTaps >= numTapsComplete) {
      numTaps = 0;
      isFinished = true;
      GD.Print("injecting complete.");
    }

    // you want to be injecting perpendicular to the graft
    // so this will have to be checked for
    if(injectAreaEntered) {
      if((lCannula.injecting && lCannula.inArea) || (rCannula.injecting && rCannula.inArea)) {
        numTaps += 1;
        GD.Print("inject registered.");
      }
    }
  }

  public void _OnInjectAreaEntered(Area2D area) {
    injectAreaEntered = true;
    Cannula2D currentCannula = area.GetParent() as Cannula2D;
    currentCannula.inArea = true;
    GD.Print("inject area entered.");
  }

  public void _OnInjectAreaExited(Area2D area) {
    injectAreaEntered = false;
    Cannula2D currentCannula = area.GetParent() as Cannula2D;
    currentCannula.inArea = false;
  }
}
