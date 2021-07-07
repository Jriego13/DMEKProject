using Godot;
using System;
/*
 *  SimpleFold has only one way to be unfolded being the dirisamer tech.
 *	It can unfold into:
 *  a. Edge Fold (most likely situation as one will full unravel)
 */
public class SimpleFold : Graft
{
	bool tapAreaEntered = false;
	bool holdAreaEntered = false;
	bool holding = false;
	Cannula cannulaL;
	Cannula cannulaR;
	PackedScene scene1;

	public override void _Ready() {
		numTapsComplete = rng.Next(3,6);
		GD.Print("you have to tap " + numTapsComplete + " times!");
		scene1 = GD.Load<PackedScene>("res://EdgeFold.tscn");
		cannulaL = GetNode("../../Cannulas/CannulaLMesh") as Cannula;
		cannulaR = GetNode("../../Cannulas/CannulaRMesh") as Cannula;
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
