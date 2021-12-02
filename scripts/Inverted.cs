using Godot;
using System;

// in the case of the inverted graft, numTaps really means numInjects of BSS
public class Inverted : Graft {
  int injectAreaState = 0;
  float lastInject = 0;
  float timePassed = 0;

  protected override void SetObjectives() {
    currentConfirmation = "Inverted";
    numTapsComplete = 2;
    var levelSwitcher = GetNode<LevelSwitcher>("/root/LevelSwitcher");
		isTutorialMode = levelSwitcher.tutorialMode();
		//setUpHitboxes(isTutorialMode);
		GD.Print("you have to inject " + numTapsComplete + " times!");
  }

  protected override void CheckObjectives(float delta) {
    GD.Print(timePassed);
    GD.Print(lastInject);
    timePassed += delta;
    if(numTaps >= numTapsComplete) {
      numTaps = 0;
      isFinished = true;
      GD.Print("injecting complete.");
    }

    // you want to be injecting perpendicular to the graft
    // so this will have to be checked for
    if(injectAreaState != 0) {
      if(((lCannula.injecting && injectAreaState != 2) || (rCannula.injecting && injectAreaState != 1)) && ((timePassed - lastInject) > 1f || numTaps == 0)) {
        lastInject = timePassed;
        numTaps += 1;
        if(numTaps < graftTextures.Count && numTaps >= 0)
          SetTexture(graftTextures[numTaps]);

        GD.Print("inject registered.");
      }
    }
  }

  public void _OnInjectAreaEntered(Area2D area) {
    int nextState = Helper.getNextHitboxState(area, true, injectAreaState);
		if (nextState != -1)
			injectAreaState = nextState;
		GD.Print("inject area entered");
  }

  public void _OnInjectAreaExited(Area2D area) {
    int nextState = Helper.getNextHitboxState(area, false, injectAreaState);
		if (nextState != -1)
			injectAreaState = nextState;
		GD.Print("inject area exited");
  }
}
