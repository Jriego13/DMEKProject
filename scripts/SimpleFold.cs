using Godot;
using System;
using System.Collections.Generic;
/*
 *  SimpleFold has only one way to be unfolded being the dirisamer tech.
 *	It can unfold into:
 *  a. Edge Fold (most likely situation as one will full unravel)
 */
public class SimpleFold : Graft {
	Cannula lCannula;
	Cannula rCannula;
	PackedScene scene1;
	MeshInstance objectMesh;
	List<Mesh> inbetweens = new List<Mesh>();
	bool tapAreaEntered = false;
	bool holdAreaEntered = false;
	bool heldDown = false;

	public override void _Ready() {
		numTapsComplete = rng.Next(3,6);
		scene1 = GD.Load<PackedScene>("res://EdgeFold.tscn");
		lCannula = GetNode("../../Cannulas/CannulaLMesh") as Cannula;
		rCannula = GetNode("../../Cannulas/CannulaRMesh") as Cannula;
		objectMesh = GetNode("./SimpleFoldMesh") as MeshInstance;
		LoadInbetweens();
		GD.Print("you have to tap " + numTapsComplete + " times!");
	}

	private void LoadInbetweens() {
		Mesh tempMesh;
		for(int i = 1; i <= 3; i++) {
			tempMesh = GD.Load<Mesh>("res://models/new_models/SimpleFold" + i + ".obj");
			inbetweens.Add(tempMesh);
		}
	}

	public override void _Process(float delta) {
		if(numTaps >= numTapsComplete) {
			numTaps = 0;
			Node sceneNode = scene1.Instance();
			GetNode("/root/Spatial/MainEye").AddChild(sceneNode);
      GetParent().RemoveChild(this);
		}

		if(tapAreaEntered) {
			if(heldDown) {
				if(lCannula.tapped || rCannula.tapped) {
					GD.Print("tap registered");
					numTaps += 1;
					lCannula.tapped = false;
					rCannula.tapped = false;
					if(numTaps <= 3)
						objectMesh.SetMesh(inbetweens[numTaps-1]);
				}
			}
		}

		if(holdAreaEntered) {
			if(lCannula.locked || rCannula.locked) {
				heldDown = true;
			}
		}
	}

	private void _on_TapArea_area_entered(object area) {
		tapAreaEntered = true;
	}

	private void _on_TapArea_area_exited(object area) {
		tapAreaEntered = false;
	}

	private void _on_HoldArea_area_entered(object area) {
		GD.Print("hold area entered");
		holdAreaEntered = true;
	}

	private void _on_HoldArea_area_exited(object area) {
		holdAreaEntered = false;
	}
}
