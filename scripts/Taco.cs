using Godot;
using System;

public class Taco : Graft {
  bool tapAreaEntered = false;
  bool holdAreaEntered = false;
  bool heldDown = false;
  ColorRect tapHitBox;
	ColorRect holdHitBox;
  protected override void SetObjectives()
  {
    var levelSwitcher = GetNode<LevelSwitcher>("/root/LevelSwitcher");
	  isTutorialMode = levelSwitcher.tutorialMode();
    setUpHitboxes(isTutorialMode);
  }
  protected override void CheckObjectives()
  {

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

  public void setUpHitboxes(bool setup) {
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
