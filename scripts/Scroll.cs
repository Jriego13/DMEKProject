using Godot;
using System;
using System.Collections.Generic;

public class Scroll : Graft {
	int topAreaState = 0; // 0-no cannulas, 1-left cannula, 2-right cannula, 3-both
	int bottomAreaState = 0;
	ColorRect topHitBox;
	ColorRect midHitBox;

	public override void _Process(float delta) {
		base._Process(delta);
		// GD.Print("top: " + topAreaEntered);
		// GD.Print("mid: " + bottomAreaEntered);
	}

	protected override void SetObjectives() {
		currentConfirmation = "Scroll";
		numTapsComplete = rng.Next(4,6);
		topTapsComplete = rng.Next(3,5);
		var levelSwitcher = GetNode<LevelSwitcher>("/root/LevelSwitcher");
		isTutorialMode = levelSwitcher.tutorialMode();
		setUpHitboxes(isTutorialMode);
		GD.Print("you have to tap " + numTapsComplete + " times!");
	}

	protected override void CheckObjectives() {
		// i want to make a function that encapsulates this behavior
		if(numTaps >= numTapsComplete && topTaps >= topTapsComplete) {
			numTaps = 0;
			topTaps = 0;
	  	isFinished = true;
			GD.Print("tapping complete.");
		}
		else if(topTaps >= topTapsComplete && numTaps == 0) {
			numTaps = 0;
			topTaps = 0;
			isFinished = true;
			isNextLevelSet = true;
			nextConfirmation = "DoubleScroll";
			GD.Print("tapping complete.");
		}
		else if(topTaps > 0 && (numTaps >= numTapsComplete)) {
			numTaps = 0;
			topTaps = 0;
			isFinished = true;
			isNextLevelSet = true;
			nextConfirmation = "SimpleFold";
			GD.Print("tapping complete");
		}

		// Only bother checking all this stuff is something was tapped:
		if (lCannula.tapped || rCannula.tapped)
		{
			if(topAreaState != 0) {
				if((lCannula.CheckCannulaRotation(Rotation, 1.396f, 1.745f)  && lCannula.tapped && topAreaState != 2) ||
				(rCannula.CheckCannulaRotation(Rotation, 1.396f, 1.745f)  && rCannula.tapped && topAreaState != 1)) {
					topTaps += 1;
					lCannula.tapped = false;
					rCannula.tapped = false;
					if(topTaps <= graftTexturesOther.Count && topTaps >= 0)
						SetTexture(graftTexturesOther[topTaps-1]);
						
					GD.Print("top tap registered");
				}
			}

			if(bottomAreaState != 0) {
				if ((lCannula.CheckCannulaRotation(Rotation, 0f, 0.52f) && lCannula.tapped && bottomAreaState != 2)||
				(rCannula.CheckCannulaRotation(Rotation, 0f, 0.52f) && rCannula.tapped && bottomAreaState != 1)){
						if(topTaps > 0) {
							numTaps += 1;
							lCannula.tapped = false;
							rCannula.tapped = false;
							if(numTaps < graftTextures.Count && numTaps >= 0)
								SetTexture(graftTextures[numTaps]);

							GD.Print("bot tap registered");
						}
					}
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
		int nextState = Helper.getNextHitboxState(area, true, bottomAreaState);
		if (nextState != -1)
			bottomAreaState = nextState;
		GD.Print("mid area entered.");
	}

	private void _OnMidAreaExited(Area2D area) {
		int nextState = Helper.getNextHitboxState(area, false, bottomAreaState);
		if (nextState != -1)
			bottomAreaState = nextState;
		GD.Print("mid area exited");
	}

	private void setUpHitboxes(bool setup) {
		topHitBox = GetNode("TopArea/TopHitbox/TopHitboxColorRect") as ColorRect;
		midHitBox = GetNode("MidArea/MidHitbox/MidHitboxColorRect") as ColorRect;
		Color hitBoxColor = new Color( 0.98f, 0.5f, 0.45f, .5f );
		topHitBox.Color = hitBoxColor;
		midHitBox.Color = hitBoxColor;
		if (setup) {
			topHitBox.SetVisible(true);
			midHitBox.SetVisible(true);
		}
		else {
			topHitBox.SetVisible(false);
			midHitBox.SetVisible(false);
		}
	}

}
