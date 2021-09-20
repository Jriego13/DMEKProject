using Godot;
using System;

// parent cannula class that handles both the left and right cannulas.
public class CannulaMain : Spatial {
	Camera mainCam;
	Cannula lCannula;
	Cannula rCannula;
	bool lHeld = false;
	bool rHeld = false;
	bool lrRotating = false; // false defualts to rotating left cannula, true to right cannula
	float timer = 0;

	public override void _Ready() {
		Input.SetMouseMode((Godot.Input.MouseMode)1); // hide mouse
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
				timer += delta; // if cannula continues being held then keep adding to timer
			}
			if(timer >= 1) { // once held for longer than a second
				lCannula.LockCannula();
				timer = -1;
			}

			lHeld = true;
		}
		else if(Input.IsActionPressed("right_mouse")) {
			if(rHeld) {
				timer += delta;
			}
			if(timer >= 1) {
				rCannula.LockCannula();
				timer = -1;
			}

			rHeld = true;
		}
		else {
			if((timer < 1 && timer > 0) && (lHeld || rHeld)) { // left or right cannula was held and released in under a second
				if(lHeld)
					lCannula.tapped = true;
				else
					rCannula.tapped = true;
			}
			else {
				lCannula.tapped = false;
				rCannula.tapped = false;
			}

			// reset their states
			lHeld = false;
			rHeld = false;
			timer = 0;
		}

		if(Input.IsActionJustPressed("cann_swap")) {
			lrRotating = !lrRotating;
			GD.Print("cannula control swap.");
		}

		// !lrRotating = lCannula | lrRotating = rCannula
		if(Input.IsActionPressed("cann_clock")) {
			if(!lrRotating)
				lCannula.GlobalRotate(new Vector3(0,1,0), -0.1f);
			else
				rCannula.GlobalRotate(new Vector3(0,1,0), -0.1f);
		}
		if(Input.IsActionPressed("cann_counterclock")) {
			if(!lrRotating)
				lCannula.GlobalRotate(new Vector3(0,1,0), 0.1f);
			else
				rCannula.GlobalRotate(new Vector3(0,1,0), 0.1f);
		}
		if(Input.IsActionJustPressed("cann_reset")) {
			if(!lrRotating)
				lCannula.SetRotation(new Vector3(0,0,0));
			else
				rCannula.SetRotation(new Vector3(0,0,0));
		}
	}

	// cannula translation occurs here
	public override void _PhysicsProcess(float delta) {
		Vector3 mousePos = mainCam.ProjectPosition(GetViewport().GetMousePosition(), 10); // used to get the position of the mouse
		Vector3 leftPos = new Vector3(mousePos.x-1.7f, 0, mousePos.z);
		Vector3 rightPos = new Vector3(mousePos.x+1.7f, 0, mousePos.z);

		// enabling and disabling cannula movement
		if(!lCannula.locked)
			lCannula.SetTranslation(leftPos);
		if(!rCannula.locked)
			rCannula.SetTranslation(rightPos);
		if(lCannula.locked && rCannula.locked)
			Input.SetMouseMode((Godot.Input.MouseMode)0); // displays the mouse
		else
			Input.SetMouseMode((Godot.Input.MouseMode)1); // hides the mouse
	}
}
