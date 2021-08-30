using Godot;
using System;

// parent cannula class that handles both the left and right cannulas.
public class CannulaMain : Spatial {
	Camera mainCam;
	Cannula lCannula; // left cannula
	Cannula rCannula; // right cannula

	bool rotating = false; // false defualts to rotating left cannula, true to rotate right cannula
	bool lHeld = false; // if the LMB (left mouse button) is being held down
	bool rHeld = false; // same for RMB
	bool lLocked = false; // left cannula locked (duplicate variables for no reason here but it works)
	bool rLocked = false; // right cannula locked
	float timer = 0; // tracks how long a mouse button is being held

	public override void _Ready() {
		Input.SetMouseMode((Godot.Input.MouseMode)1);
		mainCam = GetNode("../Camera") as Camera;
		lCannula = GetNode("./CannulaLMesh") as Cannula;
		rCannula = GetNode("./CannulaRMesh") as Cannula;
	}

	// _Process checks for either mouse button to be held for a substantial amount of time (1 second)
	// and lock the respective cannula that is being held. this also checks for regular input (<1 second)
	// and registers it as a tap.
	public override void _Process(float delta) {
		if(Input.IsActionPressed("left_mouse"))	{
			if(lHeld) {
				timer += delta;
			}
			if(timer >= 1) {
				MeshInstance lCannulaMesh = GetChild(0) as MeshInstance;
				SpatialMaterial currentMat = lCannulaMesh.GetActiveMaterial(0) as SpatialMaterial;

				if(!lLocked) { // this means the user wants to hold the cannula in place
					currentMat.SetAlbedo(new Color(1,0,0,1));
					lCannula.locked = true;
					lLocked = true;
				}
				else { // this unlocks the cannula and resets the flag
					currentMat.SetAlbedo(new Color(0,1,0,1));
					lCannula.locked = false;
					lLocked = false;
				}

				timer = -1;
			}

			lHeld = true;
		}
		else if(Input.IsActionPressed("right_mouse")) {
			if(rHeld) {
				timer += delta;
			}
			if(timer >= 1) {
				MeshInstance rCannulaMesh = GetChild(1) as MeshInstance;
				SpatialMaterial currentMat = rCannulaMesh.GetActiveMaterial(0) as SpatialMaterial;

				if(!rLocked) {
					currentMat.SetAlbedo(new Color(1,0,0,1));
					rCannula.locked = true;
					rLocked = true;
				}
				else {
					currentMat.SetAlbedo(new Color(0,1,0,1));
					rCannula.locked = false;
					rLocked = false;
				}

				timer = -1;
			}

			rHeld = true;
		}
		else {
			if((timer < 1 && timer > 0) && (lHeld || rHeld)) {
				if(lHeld)
					lCannula.tapped = true;
				else
					rCannula.tapped = true;
			}
			else {
				lCannula.tapped = false;
				rCannula.tapped = false;
			}

			lHeld = false;
			rHeld = false;
			timer = 0;
		}

		// handles rotation of the cannula
		if(Input.IsActionPressed("cann_counterclock")) {
			if(!rotating)
				lCannula.GlobalRotate(new Vector3(0,1,0), 0.1f);
			else
				rCannula.GlobalRotate(new Vector3(0,1,0), 0.1f);
		}
		else if(Input.IsActionPressed("cann_clock")) {
			if(!rotating)
				lCannula.GlobalRotate(new Vector3(0,1,0), -0.1f);
			else
				rCannula.GlobalRotate(new Vector3(0,1,0), -0.1f);
		}
		else if(Input.IsActionPressed("cann_reset")) {
			if(!rotating)
				lCannula.SetRotation(new Vector3(0,0,0));
			else
				rCannula.SetRotation(new Vector3(0,0,0));
		}

		if(Input.IsActionPressed("cann_swap"))
			rotating = !rotating;
	}

	// cannula translation occurs here
	public override void _PhysicsProcess(float delta) {
		Vector3 mousePos = mainCam.ProjectPosition(GetViewport().GetMousePosition(), 10);
		Vector3 leftPos = new Vector3(mousePos.x-1.7f, 0, mousePos.z);
		Vector3 rightPos = new Vector3(mousePos.x+1.7f, 0, mousePos.z);

		// if the cannula are not locked then translate them as the mouse is being moved
		if(!lLocked)
			lCannula.SetTranslation(leftPos);
		if(!rLocked)
			rCannula.SetTranslation(rightPos);
		if(lLocked && rLocked)
			Input.SetMouseMode((Godot.Input.MouseMode)0);
		else
			Input.SetMouseMode((Godot.Input.MouseMode)1);
	}
}
