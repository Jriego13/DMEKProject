using Godot;
using System;

public class EdgeFold : Graft {
  bool tapAreaEntered = false;
  bool holdAreaEntered = false;
  bool heldDown = false;
  ColorRect tapHitBox;
  ColorRect holdHitBox;

  protected override void SetObjectives()
  {
    currentConfirmation = "EdgeFold";
    numTapsComplete = rng.Next(3,6);
    var levelSwitcher = GetNode<LevelSwitcher>("/root/LevelSwitcher");
    isTutorialMode = levelSwitcher.tutorialMode();
    setUpHitboxes(isTutorialMode);
    GD.Print("you have to tap " + numTapsComplete + " times!");
  }

  protected override void CheckObjectives()
  {
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
          if(numTaps < graftTextures.Count && numTaps >= 0)
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
        if(numTaps > 0)
          numTaps -= 1;
        if(numTaps >= 0)
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

  public void setUpHitboxes(bool setup ) {
    GD.Print("setup = " + setup);
    tapHitBox = GetNode("TapArea/TapHitbox/TapHitboxColorRect") as ColorRect;
    holdHitBox = GetNode("HoldArea/HoldHitbox/HoldHitboxColorRect") as ColorRect;
    Color tapHitBoxColor = new Color( 0.98f, 0.5f, 0.45f, .5f );
    Color holdHitBoxColor = new Color( 0.5f, 1f, 0f, .5f );
    tapHitBox.Color = tapHitBoxColor;
    holdHitBox.Color = holdHitBoxColor;
    if (setup) {
      tapHitBox.SetVisible(true);
      holdHitBox.SetVisible(true);
    }
    else {
      tapHitBox.SetVisible(false);
      holdHitBox.SetVisible(false);
    }
  }
}
