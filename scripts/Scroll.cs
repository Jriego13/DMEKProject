using Godot;
using System;
using System.Collections.Generic;

public class Scroll : Graft {
	Cannula lCannula;
	Cannula rCannula;
	PackedScene scene1;
	PackedScene scene2;
	MeshInstance objectMesh;
	List<Mesh> inbetweens = new List<Mesh>();
	bool topAreaEntered = false;
	bool bottomAreaEntered = false;
	int topTaps = 0;
	int topTapsComplete = 0;

	public override void _Ready() {
		numTapsComplete = rng.Next(4,6);
		topTapsComplete = rng.Next(3,5);
    scene1 = GD.Load<PackedScene>("res://SimpleFold.tscn");
    scene2 = GD.Load<PackedScene>("res://DoubleScroll.tscn");
    lCannula = GetNode("../../Cannulas/CannulaLMesh") as Cannula;
    rCannula = GetNode("../../Cannulas/CannulaRMesh") as Cannula;
		objectMesh = GetNode("./ScrollMesh") as MeshInstance;
		LoadInbetweens();
		GD.Print("you have to tap the bottom " + numTapsComplete + " and the top " + topTapsComplete + " times!");
	}

	// given how the "animations" are made the inbetween models have to be loaded and this is where that is done
	private void LoadInbetweens() {
		Mesh tempMesh;
		for(int i = 1; i <= 4; i++) {
			tempMesh = GD.Load<Mesh>("res://models/new_models/Scroll" + i + ".obj"); // load a mesh given its particular naming convention and stoire it in our list
			inbetweens.Add(tempMesh);
		}
	}

	public override void _Process(float delta) {
		// i want to make a function that encapsulates this behavior
		if(numTaps >= numTapsComplete && topTaps >= topTapsComplete) {
			numTaps = 0;
			topTaps = 0;
			Node sceneNode = scene1.Instance(); // instancing the next scene is necessary to use it in the way that follows
			GetNode("/root/Spatial/MainEye").AddChild(sceneNode); // add new graft
	  GetParent().RemoveChild(this); // remove itself
		}

		// bottom hitbox
		if(bottomAreaEntered) {
			if(lCannula.CheckCannulaRotation(0f, 0.34f) || rCannula.CheckCannulaRotation(0f, 0.34f)) {
				if(lCannula.tapped || rCannula.tapped) {
					numTaps += 1;
					lCannula.tapped = false;
					rCannula.tapped = false;
					if(numTaps <= 4)
						objectMesh.SetMesh(inbetweens[numTaps-1]);
					GD.Print("bot tap registered");
				}
			}
		}

		// top hitbox
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
	}

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
}
