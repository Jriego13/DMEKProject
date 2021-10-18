using Godot;
using System;

public class DoubleScroll : Graft {
  PackedScene scene1;
  PackedScene scene2;
  ColorRect midHitBox;
  bool areaEntered = false;

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

    if(areaEntered) {
      if(lCannula.tapped || rCannula.tapped) {
        numTaps += 1;
        lCannula.tapped = false;
        rCannula.tapped = false;
        // if(numTaps < graftTextures.Count)
        //   SetTexture(graftTextures[numTaps]);

        GD.Print("tap registered");
      }
    }

    if(!areaEntered){
      if(rCannula.tapped || lCannula.tapped){
        // numTaps -= 1;
        // if(numTaps >= 0)
        //   SetTexture(graftTextures[numTaps]);

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
