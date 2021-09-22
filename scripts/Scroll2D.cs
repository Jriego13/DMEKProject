using Godot;
using System;
using System.Collections.Generic;

public class Scroll2D : Graft {
	Cannula2D lCannula;
	Cannula2D rCannula;
	PackedScene scene1;
	PackedScene scene2;
	bool topAreaEntered = false;
	bool bottomAreaEntered = false;
	int topTaps = 0;
	int topTapsComplete = 0;
	int numTapsWrong = 0;
	Texture circleTexture;

	public override void _Ready() {
		numTapsComplete = rng.Next(4,6);
		topTapsComplete = rng.Next(3,5);
    // scene1 = GD.Load<PackedScene>("res://SimpleFold.tscn");
    // scene2 = GD.Load<PackedScene>("res://DoubleScroll.tscn");
    lCannula = GetNode("../Cannulas/CannulaLSprite") as Cannula2D;
    rCannula = GetNode("../Cannulas/CannulaRSprite") as Cannula2D;
	circleTexture = GD.Load("res://images/circle.png") as Texture;
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
					misclickCircle.Scale = new Vector2(0.025f , 0.025f);
					misclickCircle.Position = mousePos;
					misclickCircle.Modulate = new Color(1, 0 , 0);
					
					GetTree().GetRoot().AddChild(misclickCircle);
					await ToSignal(GetTree().CreateTimer(0.25f), "timeout");
					misclickCircle.QueueFree();
					numTapsWrong++;
				}
				else{
					GD.Print("Misclicked too many times. You fail!");
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
