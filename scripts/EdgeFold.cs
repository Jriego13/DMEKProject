using Godot;
using System;
using System.Collections.Generic;

/*
 *  EdgeFold also has only one way to be unfolded being the dirisamer tech.
 *	It can unfold into:
 *  a. Done!
 */
public class EdgeFold : Graft
{
	bool tapAreaEntered = false;
	bool holdAreaEntered = false;
	bool holding = false;
	Cannula cannulaL;
	Cannula cannulaR;
	PackedScene scene1;
	MeshInstance objectMesh;
	List<Mesh> inbetweens = new List<Mesh>();

	public override void _Ready() {
		numTapsComplete = rng.Next(3,6);
		GD.Print("you have to tap " + numTapsComplete + " times!");
		scene1 = GD.Load<PackedScene>("res://Finished.tscn");
		cannulaL = GetNode("../../Cannulas/CannulaLMesh") as Cannula;
		cannulaR = GetNode("../../Cannulas/CannulaRMesh") as Cannula;
		objectMesh = GetNode("./EdgeFoldMesh") as MeshInstance;
		LoadInbetweens();
	}

	private void LoadInbetweens() {
		Mesh tempMesh;
		for(int i = 1; i <= 3; i++) {
			tempMesh = GD.Load<Mesh>("res://models/new_models/EdgeFold" + i + ".obj");
			inbetweens.Add(tempMesh);
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

	public override void _Process(float delta) {
		if(numTaps >= numTapsComplete) {
			numTaps = 0;
			Node sceneNode = scene1.Instance();
			GetNode("/root/Spatial/MainEye").AddChild(sceneNode);
      GetParent().RemoveChild(this);
		}

		if(tapAreaEntered) {
			//GD.Print("in your tap area!");
			if(holding) {
				if(cannulaL.tapped || cannulaR.tapped) {
					GD.Print("tap registered");
					numTaps += 1;
					cannulaL.tapped = false;
					cannulaR.tapped = false;
					if(numTaps <= 3)
						objectMesh.SetMesh(inbetweens[numTaps-1]);
				}
			}
		}

		if(holdAreaEntered) {
			//GD.Print("in your hold area!");
			if(cannulaL.locked || cannulaR.locked) {
				//GD.Print("holding is true");
				holding = true;
			}
		}
	}
}
