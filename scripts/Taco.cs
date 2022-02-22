using Godot;
using System;

public class Taco : Graft {
  bool injectAreaEntered = false;
  int areaState = 0; // 0-no cannulas, 1-left cannula, 2-right cannula, 3-both
  float lastInject = 0;
  float timePassed = 0;

  protected override void SetObjectives() {
    currentConfirmation = "Taco";
    numTapsComplete = 2;
    var levelSwitcher = GetNode<LevelSwitcher>("/root/LevelSwitcher");
		// isTutorialMode = levelSwitcher.tutorialMode();
		// setUpHitboxes(isTutorialMode);
		GD.Print("you have to inject " + numTapsComplete + " times!");
  }

  protected override void CheckObjectives(float delta) {
    timePassed += delta;
    if(numTaps >= numTapsComplete) {
      numTaps = 0;
      isFinished = true;
      GD.Print("injecting complete.");
    }

    // you want to be injecting perpendicular to the graft
    // so this will have to be checked for
    if(areaState != 0) {
      if(((lCannula.injecting && lCannula.numAreasIn != 0) || (rCannula.injecting && rCannula.numAreasIn != 0)) && (((timePassed - lastInject) > 0.5f) || numTaps == 0) && eye.getWaterLevel() >= 125) {
        lastInject = timePassed;
        numTaps += 1;
        if(numTaps < graftTextures.Count && numTaps >= 0)
          SetTexture(graftTextures[numTaps]);

        GD.Print("inject registered.");
      }
    }
  }

  public void _OnInjectAreaEntered(Area2D area) {
    int nextState = Helper.getNextHitboxState(area, true, areaState);
    if (nextState != -1)
      areaState = nextState;
    GD.Print("inject area entered.");
  }

  public void _OnInjectAreaExited(Area2D area) {
    int nextState = Helper.getNextHitboxState(area, false, areaState);
		if (nextState != -1)
			areaState = nextState;
  }

  // public void setUpHitboxes(bool setup) {
	//     tapHitBox = GetNode("TapArea/TapHitbox/TapHitboxColorRect") as ColorRect;
  // 	  holdHitBox = GetNode("HoldArea/HoldHitbox/HoldHitboxColorRect") as ColorRect;
  //     Color tapHitBoxColor = new Color( 0.98f, 0.5f, 0.45f, .5f );
  //     Color holdHitBoxColor = new Color( 0.5f, 1f, 0f, .5f );
	//     tapHitBox.Color = tapHitBoxColor;
	//     holdHitBox.Color = holdHitBoxColor;
  //     if (setup) {
  //       tapHitBox.SetVisible(true);
  //       holdHitBox.SetVisible(true);
  //     }
  //     else {
  //       tapHitBox.SetVisible(false);
  //       holdHitBox.SetVisible(false);
  //     }
  // }
}
