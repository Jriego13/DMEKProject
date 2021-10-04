using Godot;
using System;
using System.Collections.Generic;
/*
 *  EdgeFold also has only one way to be unfolded being the dirisamer tech.
 *	It can unfold into:
 *  a. Done!
 */
public class EdgeFold : Graft {
	Cannula lCannula;
	Cannula rCannula;
	PackedScene scene1;
	MeshInstance objectMesh;
	List<Mesh> inbetweens = new List<Mesh>();
	bool tapAreaEntered = false;
	bool holdAreaEntered = false;
	bool holding = false;

	public override void _Ready() {
		numTapsComplete = rng.Next(3,6);
		scene1 = GD.Load<PackedScene>("res://Finished.tscn");
		lCannula = GetNode("../../Cannulas/CannulaLMesh") as Cannula;
		rCannula = GetNode("../../Cannulas/CannulaRMesh") as Cannula;
		objectMesh = GetNode("./EdgeFoldMesh") as MeshInstance;
		LoadInbetweens();
		GD.Print("you have to tap " + numTapsComplete + " times!");
	}

	private void LoadInbetweens() {
		Mesh tempMesh;
		for(int i = 1; i <= 3; i++) {
			tempMesh = GD.Load<Mesh>("res://models/new_models/EdgeFold" + i + ".obj");
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
			if(holding) {
				if(lCannula.tapped || rCannula.tapped) {
					numTaps += 1;
					lCannula.tapped = false;
					rCannula.tapped = false;
					if(numTaps <= 3)
						objectMesh.SetMesh(inbetweens[numTaps-1]);
					GD.Print("tap registered");
				}
			}
		}

		if(holdAreaEntered) {
			if(lCannula.locked || rCannula.locked) {
				holding = true;
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
		holdAreaEntered = true;
	}

	private void _on_HoldArea_area_exited(object area) {
		holdAreaEntered = false;
	}
}
