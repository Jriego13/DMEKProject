using Godot;
using System;
using System.Collections.Generic;

public class Scroll : Graft {
	bool topAreaEntered = false;
	bool bottomAreaEntered = false;
	Cannula cannulaL;
	Cannula cannulaR;
	PackedScene scene1;
	PackedScene scene2;
	MeshInstance objectMesh;
	List<Mesh> inbetweens = new List<Mesh>();
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

	private void LoadInbetweens() {
		Mesh tempMesh;
		for(int i = 1; i <= 4; i++) {
			tempMesh = GD.Load<Mesh>("res://models/new_models/Scroll" + i + ".obj");
			inbetweens.Add(tempMesh);
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

	public override void _Process(float delta) {
		if(numTaps >= numTapsComplete && topTaps >= topTapsComplete) {
			numTaps = 0;
			topTaps = 0;
			Node sceneNode = scene1.Instance();
			GetNode("/root/Spatial/MainEye").AddChild(sceneNode);
      GetParent().RemoveChild(this);
		}

		if(bottomAreaEntered) {
			if(Math.Abs(cannulaL.GetRotation().y) <= 0.34f && Math.Abs(cannulaL.GetRotation().y) >= 0f) {
				if(cannulaL.tapped) {
					GD.Print("bot tap registered");
					numTaps += 1;
					cannulaL.tapped = false;
					cannulaR.tapped = false;
					if(numTaps <= 4)
						objectMesh.SetMesh(inbetweens[numTaps-1]);
				}
			}
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
