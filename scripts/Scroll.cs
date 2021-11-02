using Godot;
using System;
using System.Collections.Generic;

public class Scroll : Graft {
	bool topAreaEntered = false;
	bool bottomAreaEntered = false;
	ColorRect topHitBox;
	ColorRect midHitBox;

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

		if(topAreaEntered) {
			if(lCannula.CheckCannulaRotation(1.39f, 1.74f) || rCannula.CheckCannulaRotation(1.39f, 1.74f)) {
				if(lCannula.tapped || rCannula.tapped) {
					topTaps += 1;
					lCannula.tapped = false;
					rCannula.tapped = false;
					GD.Print(topTaps);
					GD.Print(graftTexturesOther.Count);
					if(topTaps <= graftTexturesOther.Count && topTaps >= 0)
						SetTexture(graftTexturesOther[topTaps-1]);
					GD.Print("top tap registered");
				}
			}
		}

		if(bottomAreaEntered) {
			if(lCannula.CheckCannulaRotation(0f, 0.34f) || rCannula.CheckCannulaRotation(0f, 0.34f)) {
				if(lCannula.tapped || rCannula.tapped) {
					numTaps += 1;
					lCannula.tapped = false;
					rCannula.tapped = false;
					if(numTaps < graftTextures.Count && numTaps >= 0)
						SetTexture(graftTextures[numTaps]);

					GD.Print("bot tap registered");
				}
			}
		}

		if(!topAreaEntered && !bottomAreaEntered){
			if(rCannula.tapped || lCannula.tapped){
				if(numTaps > 0)
					numTaps -= 1;
				if(numTaps >= 0)
					SetTexture(graftTextures[numTaps]);

				registerMisclick();
			}
		}
	}

  private void _OnTopAreaEntered(object area) {
		topAreaEntered = true;
	GD.Print("top area entered.");
	}

	private void _OnTopAreaExited(object area) {
		topAreaEntered = false;
	}

	private void _OnMidAreaEntered(object area) {
		bottomAreaEntered = true;
	GD.Print("mid area entered.");
	}

	private void _OnMidAreaExited(object area) {
		bottomAreaEntered = false;
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
