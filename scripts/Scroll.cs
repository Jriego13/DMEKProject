using Godot;
using System;
using System.Collections.Generic;

public class Scroll : Graft {
	bool topAreaEntered = false;
	bool bottomAreaEntered = false;
	Cannula cannulaL; // left cannula reference
	Cannula cannulaR; // right cannula reference
	PackedScene scene1; // one graft that a scroll can turn into
	PackedScene scene2; // second graft that a scroll can turn into
	MeshInstance objectMesh;
	List<Mesh> inbetweens = new List<Mesh>(); // inbetweens (animation frames) list
	int topTaps = 0;
	int topTapsComplete = 0;

	public override void _Ready() {
		numTapsComplete = rng.Next(4,6);
		topTapsComplete = rng.Next(3,5);
    GD.Print("you have to tap the bottom " + numTapsComplete + " and the top " + topTapsComplete + " times!");
    scene1 = GD.Load<PackedScene>("res://SimpleFold.tscn");
    scene2 = GD.Load<PackedScene>("res://DoubleScroll.tscn");
    cannulaL = GetNode("../../Cannulas/CannulaLMesh") as Cannula;
    cannulaR = GetNode("../../Cannulas/CannulaRMesh") as Cannula;
		objectMesh = GetNode("./ScrollMesh") as MeshInstance;
		LoadInbetweens();
	}

	// given how the "animations" are made the inbetween models have to be loaded and this is where that is done
	private void LoadInbetweens() {
		Mesh tempMesh;
		for(int i = 1; i <= 4; i++) {
			tempMesh = GD.Load<Mesh>("res://models/new_models/Scroll" + i + ".obj"); // load a mesh given its particular naming convention and stoire it in our list
			inbetweens.Add(tempMesh);
		}
	}

	// each graft has a collection of these functions that check if particular areas are being
	// interacted with (the cannulas are inside them). only when these areas are entered
	// or a particular signal is sent can steps be taken to proceed
	private void _on_BottomArea_area_entered(object area) {
		bottomAreaEntered = true;
	}

	private void _on_BottomArea_area_exited(object area) {
		bottomAreaEntered = false;
	}

	private void _on_TopArea_area_entered(object area) {
		topAreaEntered = true;
	}

	private void _on_TopArea_area_exited(object area) {
		topAreaEntered = false;
	}

	public override void _Process(float delta) {
		if(numTaps >= numTapsComplete && topTaps >= topTapsComplete) { // moves onto the next graft
			numTaps = 0;
			topTaps = 0;
			Node sceneNode = scene1.Instance(); // instancing the next scene is necessary to use it in the way that follows
			GetNode("res:///root/Spatial/MainEye").AddChild(sceneNode); // add new graft
      GetParent().RemoveChild(this); // remove itself
		}

		// bottom hitbox
		if(bottomAreaEntered) {
			if(Math.Abs(cannulaL.GetRotation().y) <= 0.34f && Math.Abs(cannulaL.GetRotation().y) >= 0f) { // given the correct rotation
				if(cannulaL.tapped) { // and it has been tapped
					GD.Print("bot tap registered");
					numTaps += 1; // add a tap towards completion
					cannulaL.tapped = false; // reset these values
					cannulaR.tapped = false;
					if(numTaps <= 4)
						objectMesh.SetMesh(inbetweens[numTaps-1]); // change models to an inbetween frame
				}
			}

			// this if can just be combined into the previous one as a big OR
			if(Math.Abs(cannulaR.GetRotation().y) <= 0.34f && Math.Abs(cannulaR.GetRotation().y) >= 0f) {
				if(cannulaR.tapped) {
					GD.Print("bot tap registered");
					numTaps += 1;
					cannulaL.tapped = false;
					cannulaR.tapped = false;
					if(numTaps <= 4)
						objectMesh.SetMesh(inbetweens[numTaps-1]);
				}
			}
		}

		// top hitbox
		if(topAreaEntered) {
			if(Math.Abs(cannulaL.GetRotation().y) >= 1.39f && Math.Abs(cannulaL.GetRotation().y) <= 1.74f) {
				if(cannulaL.tapped) {
					GD.Print("top tap registered");
					topTaps += 1;
					cannulaL.tapped = false;
					cannulaR.tapped = false;
				}
			}
			if(Math.Abs(cannulaR.GetRotation().y) >= 1.39f && Math.Abs(cannulaR.GetRotation().y) <= 1.74f) {
				if(cannulaR.tapped) {
					GD.Print("top tap registered");
					topTaps += 1;
					cannulaL.tapped = false;
					cannulaR.tapped = false;
				}
			}
		}
	}
}
