using Godot;
using System;
using System.Collections.Generic;

public class Scroll : Graft {
	Cannula2D lCannula;
	Cannula2D rCannula;
	PackedScene scene1;
	PackedScene scene2;
	PackedScene failScene;
	bool topAreaEntered = false;
	bool bottomAreaEntered = false;
	int topTaps = 0;
	int topTapsComplete = 0;
	int numTapsWrong = 0;
	Texture circleTexture;
	Texture hitBoxTexture;
	Boolean isTutorialMode;
	ColorRect topHitBox;
	ColorRect midHitBox;

	public override void _Ready() {
		numTapsComplete = rng.Next(4,6);
		topTapsComplete = rng.Next(3,5);
    // scene1 = GD.Load<PackedScene>("res://SimpleFold.tscn");
    // scene2 = GD.Load<PackedScene>("res://DoubleScroll.tscn");
	failScene = GD.Load<PackedScene>("res://FailScreen.tscn");
    lCannula = GetNode("../Cannulas/CannulaLSprite") as Cannula2D;
    rCannula = GetNode("../Cannulas/CannulaRSprite") as Cannula2D;
	topHitBox = GetNode("TopArea/TopHitbox/TopHitboxColorRect") as ColorRect;
	midHitBox = GetNode("MidArea/MidHitbox/MidHitboxColorRect") as ColorRect;
	circleTexture = GD.Load("res://images/circle.png") as Texture; 
	Color hitBoxColor = new Color( 0.98f, 0.5f, 0.45f, .5f );
	topHitBox.Color = hitBoxColor;
	midHitBox.Color = hitBoxColor;
	var levelSwitcher = GetNode<LevelSwitcher>("/root/LevelSwitcher");
	isTutorialMode = levelSwitcher.tutorialMode();
	GD.Print("tut mode = " + isTutorialMode);
	if (isTutorialMode) {
		topHitBox.SetVisible(true);
		midHitBox.SetVisible(true);
	}
	else {
		topHitBox.SetVisible(false);
		midHitBox.SetVisible(false);
	}
	GD.Print("you have to tap the bottom " + numTapsComplete + " and the top " + topTapsComplete + " times!");
	}

	public async override void _Process(float delta) {

		// i want to make a function that encapsulates this behavior
		if(numTaps >= numTapsComplete && topTaps >= topTapsComplete) {
			numTaps = 0;
			topTaps = 0;
			//Node sceneNode = scene1.Instance();
			// GetNode("/root/Spatial/MainEye").AddChild(sceneNode);
      // GetParent().RemoveChild(this);
      GD.Print("tapping complete.");
	  isFinished = true;
		}

		if(bottomAreaEntered) {
			if(lCannula.CheckCannulaRotation(0f, 0.34f) || rCannula.CheckCannulaRotation(0f, 0.34f)) {
				if(lCannula.tapped || rCannula.tapped) {
					numTaps += 1;
					lCannula.tapped = false;
					rCannula.tapped = false;
					// if(numTaps <= 4)
					// 	objectMesh.SetMesh(inbetweens[numTaps-1]);
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
				if(numTapsWrong < 3){
					GD.Print("You clicked outside of the correct areas");
					Vector2 mousePos = GetViewport().GetMousePosition();
					GD.Print(mousePos);
					Sprite misclickCircle = new Sprite();
					misclickCircle.Texture = circleTexture;
					misclickCircle.Scale = new Vector2(0.1f , 0.1f);
					misclickCircle.Position = mousePos;
					misclickCircle.Modulate = new Color(1, 0 , 0);

					GetTree().GetRoot().AddChild(misclickCircle);
					await ToSignal(GetTree().CreateTimer(0.25f), "timeout");
					misclickCircle.QueueFree();
					numTapsWrong++;
				}
				else{
					GD.Print("Misclicked too many times. You fail!");
					Node sceneNode = failScene.Instance();
					GetNode("/root").AddChild(sceneNode);
				}
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
