using Godot;
using System;
using System.Collections.Generic;

public class Scroll : Graft {
	bool topAreaEntered = false;
	bool bottomAreaEntered = false;
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

		if(topAreaEntered) {
			if(lCannula.CheckCannulaRotation(this.GetRotation(), 1.396f, 1.745f) || rCannula.CheckCannulaRotation(this.GetRotation(), 1.396f, 1.745f)) {
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
			if(lCannula.CheckCannulaRotation(this.GetRotation(), 0f, 0.52f) || rCannula.CheckCannulaRotation(this.GetRotation(), 0f, 0.52f)) {
				if((lCannula.inArea && lCannula.tapped) || (rCannula.inArea && rCannula.tapped)) {
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
				// if(numTaps > 0)
				// 	numTaps -= 1;
				// if(numTaps >= 0)
				// 	SetTexture(graftTextures[numTaps]);

				registerMisclick();
			}
		}
	}

  private void _OnTopAreaEntered(Area2D area) {
		topAreaEntered = true;
		Cannula2D currentCannula = area.GetParent() as Cannula2D;
		currentCannula.inArea = true;
		GD.Print("top area entered.");
	}

	private void _OnTopAreaExited(Area2D area) {
		topAreaEntered = false;
		Cannula2D currentCannula = area.GetParent() as Cannula2D;
		currentCannula.inArea = false;
	}

	private void _OnMidAreaEntered(Area2D area) {
		bottomAreaEntered = true;
		Cannula2D currentCannula = area.GetParent() as Cannula2D;
		currentCannula.inArea = true;
		GD.Print("mid area entered.");
	}

	private void _OnMidAreaExited(Area2D area) {
		bottomAreaEntered = false;
		Cannula2D currentCannula = area.GetParent() as Cannula2D;
		currentCannula.inArea = false;
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
