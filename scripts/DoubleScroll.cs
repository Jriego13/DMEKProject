using Godot;
using System;

public class DoubleScroll : Graft {
  PackedScene scene1;
  PackedScene scene2;
  ColorRect midHitBox;
  int areaState = 0; // 0-no cannulas, 1-left cannula, 2-right cannula, 3-both

  protected override void SetObjectives()
  {
    currentConfirmation = "DoubleScroll";
    numTapsComplete = rng.Next(3,6);
    var levelSwitcher = GetNode<LevelSwitcher>("/root/LevelSwitcher");
    isTutorialMode = levelSwitcher.tutorialMode();
    setUpHitboxes(isTutorialMode);
    GD.Print("you have to tap the bottom " + numTapsComplete + " times!");
  }

  protected override void CheckObjectives()
  {
    if(numTaps >= numTapsComplete) {
      numTaps = 0;
      GD.Print("tapping complete.");
      isFinished = true;
    }

    // if at least one cannula in the area:
    if(areaState != 0) {
      // if cannula tapped and it isn't the other cannula in the area:
      if((lCannula.tapped && areaState != 2) || (rCannula.tapped && areaState != 1)) {
        numTaps += 1;
        lCannula.tapped = false;
        rCannula.tapped = false;
        // if(numTaps < graftTextures.Count)
        //   SetTexture(graftTextures[numTaps]);

        GD.Print("tap registered");
      }
    }

  }

  private void _OnAreaEntered(Area2D area) {
    int nextState = Helper.getNextHitboxState(area, true, areaState);
		if (nextState != -1)
			areaState = nextState;
		GD.Print("area entered.");
  }

  private void _OnAreaExited(Area2D area) {
    int nextState = Helper.getNextHitboxState(area, false, areaState);
		if (nextState != -1)
			areaState = nextState;
		GD.Print("area entered.");
  }

  private void setUpHitboxes(bool setup) {
    midHitBox = GetNode("MidArea/MidHitbox/MidHitboxColorRect") as ColorRect;
    Color midHitBoxColor = new Color( 0.98f, 0.5f, 0.45f, .5f );
    midHitBox.Color = midHitBoxColor;
    if (setup) {
      midHitBox.SetVisible(true);
    }
    else {
      midHitBox.SetVisible(false);
    }
  }
}
