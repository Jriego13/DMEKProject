using Godot;
using System;

public class SimpleFold : Graft {
// 0-no cannulas, 1-left cannula, 2-right cannula, 3-both
  int tapAreaState = 0;
  int holdAreaState = 0;
  bool heldDown = false;
  RectangleShape2D tapRect;
  CollisionShape2D tapCollider;
  ColorRect tapHitBox;
  ColorRect holdHitBox;

  protected override void SetObjectives()
  {
    currentConfirmation = "SimpleFold";
    numTapsComplete = rng.Next(3,6);
    var levelSwitcher = GetNode<LevelSwitcher>("/root/LevelSwitcher");
    tapCollider = GetNode("./TapArea/TapHitbox") as CollisionShape2D;
    tapRect = tapCollider.GetShape() as RectangleShape2D;
    isTutorialMode = levelSwitcher.tutorialMode();
    setUpHitboxes(isTutorialMode);
    GD.Print("you have to tap " + numTapsComplete + " times!");
  }

  protected override void CheckObjectives(float delta)
  {
    if(numTaps >= numTapsComplete) {
      numTaps = 0;
      isFinished = true;
      GD.Print("tapping complete.");
    }

    // if at least one cannula is in tap area:
    if(tapAreaState != 0 && heldDown) {
      // if cannula is being tapped and is not the state where only the other cannula is there:
        if((lCannula.tapped && tapAreaState != 2) || (rCannula.tapped && tapAreaState != 1)) {
          numTaps += 1;
          lCannula.tapped = false;
          rCannula.tapped = false;
          if(numTaps < graftTextures.Count && numTaps >= 0) {
            SetTexture(graftTextures[numTaps]);
          }

          Vector2 rect = tapRect.GetExtents();
          tapRect.SetExtents(new Vector2(rect.x + (14.113f  / numTapsComplete), rect.y));
          tapCollider.Translate(new Vector2(-13.358f / numTapsComplete, 0));
          GD.Print("tap registered");
        }
    }

    // if at least one cannula is in hold area:
    if((holdAreaState != 0)) {
      // if cannula is being held and is not the state where only the other cannula is there:
      if((lCannula.locked && holdAreaState != 2) || (rCannula.locked && holdAreaState != 1)) {
        heldDown = true;
      }
      else {
        heldDown = false;
      }
    }
  }

  public void _OnTapAreaEntered(Area2D area) {
    int nextState = Helper.getNextHitboxState(area, true, tapAreaState);
		if (nextState != -1)
			tapAreaState = nextState;
		GD.Print("tap area entered.");
  }

  public void _OnTapAreaExited(Area2D area) {
    int nextState = Helper.getNextHitboxState(area, false, tapAreaState);
		if (nextState != -1)
			tapAreaState = nextState;
		GD.Print("tap area exited.");
  }

  public void _OnHoldAreaEntered(Area2D area) {
    int nextState = Helper.getNextHitboxState(area, true, holdAreaState);
		if (nextState != -1)
			holdAreaState = nextState;
		GD.Print("hold area entered.");
  }

  public void _OnHoldAreaExited(Area2D area) {
    int nextState = Helper.getNextHitboxState(area, false, holdAreaState);
		if (nextState != -1)
			holdAreaState = nextState;
		GD.Print("hold area exited.");
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
