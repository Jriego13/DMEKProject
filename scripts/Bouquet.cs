using Godot;
using System;

public class Bouquet : Graft {
  int topAreaState = 0; // 0-no cannulas, 1-left cannula, 2-right cannula, 3-both
  int midAreaState = 0;
  int topTaps = 0;
  int topTapsComplete = 0;

  protected override void SetObjectives()
  {
    currentConfirmation = "Bouquet";
    numTapsComplete = 2;
		topTapsComplete = numTapsComplete;
    GD.Print("you have to tap the bottom " + numTapsComplete + " and the top " + topTapsComplete + " times!");
  }

  protected override void CheckObjectives(float delta)
  {
    if(numTaps >= numTapsComplete && topTaps >= topTapsComplete) {
      numTaps = 0;
      topTaps = 0;
      isFinished = true;
      GD.Print("tapping complete.");
    }

    if(topAreaState != 0) {
      if((lCannula.tapped && topAreaState != 2 && lCannula.CheckCannulaRotationRelative(Rotation, 1.396f, 1.745f))
       || (rCannula.tapped && topAreaState !=1 && rCannula.CheckCannulaRotationRelative(Rotation, 1.396f, 1.745f))) {
          topTaps += 1;
          lCannula.tapped = false;
          rCannula.tapped = false;
          if(topTaps + numTaps < graftTextures.Count && topTaps >= 0)
            SetTexture(graftTextures[numTaps + topTaps]);

          GD.Print("top tap registered");
      }
    }

    if(midAreaState != 0) {
      if((lCannula.tapped && midAreaState != 2 && lCannula.CheckCannulaRotationRelative(Rotation, 0f, 0.52f))
       || (rCannula.tapped && midAreaState != 1 && rCannula.CheckCannulaRotationRelative(Rotation, 0f, 0.52f))) {
          numTaps += 1;
          lCannula.tapped = false;
          rCannula.tapped = false;
          if(topTaps + numTaps < graftTextures.Count && numTaps >= 0)
            SetTexture(graftTextures[numTaps + topTaps]);

          GD.Print("mid tap registered");
      }
    }
  }

  private void _OnTopAreaEntered(Area2D area) {
    int nextState = Helper.getNextHitboxState(area, true, topAreaState);
		if (nextState != -1)
			topAreaState = nextState;
		GD.Print("top area entered.");
	}

	private void _OnTopAreaExited(Area2D area) {
		int nextState = Helper.getNextHitboxState(area, false, topAreaState);
		if (nextState != -1)
			topAreaState = nextState;
		GD.Print("top area exited");
	}

	private void _OnMidAreaEntered(Area2D area) {
		int nextState = Helper.getNextHitboxState(area, true, midAreaState);
		if (nextState != -1)
			midAreaState = nextState;
		GD.Print("mid area entered.");
	}

	private void _OnMidAreaExited(Area2D area) {
		int nextState = Helper.getNextHitboxState(area, false, midAreaState);
		if (nextState != -1)
			midAreaState = nextState;
		GD.Print("mid area exited");
	}
}
