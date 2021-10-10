using Godot;
using System;
using System.Collections.Generic;

public class Scroll2D : Graft {
	PackedScene scene1;
	PackedScene scene2;
	bool topAreaEntered = false;
	bool bottomAreaEntered = false;
	int topTaps = 0;
	int topTapsComplete = 0;

	protected override void SetObjectives()
	{
		numTapsComplete = rng.Next(4,6);
		topTapsComplete = rng.Next(3,5);
		GD.Print("you have to tap the bottom " + numTapsComplete + " and the top " + topTapsComplete + " times!");
	}

	protected override void CheckObjectives()
	{
		if(numTaps >= numTapsComplete && topTaps >= topTapsComplete) {
			numTaps = 0;
			topTaps = 0;
	  		isFinished = true;
			GD.Print("tapping complete.");
		}

		if(bottomAreaEntered) {
			if(lCannula.CheckCannulaRotation(0f, 0.34f) || rCannula.CheckCannulaRotation(0f, 0.34f)) {
				if(lCannula.tapped || rCannula.tapped) {
					numTaps += 1;
					lCannula.tapped = false;
					rCannula.tapped = false;
					GD.Print("bot tap registered");
				}
			}
		}

		if(topAreaEntered) {
			if(lCannula.CheckCannulaRotation(1.39f, 1.74f) || rCannula.CheckCannulaRotation(1.39f, 1.74f)) {
				if(lCannula.tapped || rCannula.tapped) {
					topTaps += 1;
					lCannula.tapped = false;
					rCannula.tapped = false;
					GD.Print("top tap registered");
				}
			}
		}

		if(!topAreaEntered && !bottomAreaEntered){
			if(rCannula.tapped || lCannula.tapped){
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
}
