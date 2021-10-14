using Godot;
using System;

public class CannulaMain2D : Node2D {
	Cannula2D lCannula;
	Cannula2D rCannula;
	AudioStreamPlayer audio;
	bool lHeld = false;
	bool rHeld = false;
	bool lrRotating = false; // false defualts to rotating left cannula, true to right cannula
	float timer = 0;

	public override void _Ready() {
		Input.SetMouseMode((Godot.Input.MouseMode)1); // hide mouse
		lCannula = GetNode("./CannulaLSprite") as Cannula2D;
		rCannula = GetNode("./CannulaRSprite") as Cannula2D;
		audio = GetNode("./CannulaLSprite/AudioStreamPlayer") as AudioStreamPlayer;
	}

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
				if(lHeld){
					lCannula.tapped = true;
					audio.Play();
				}
				else{
					rCannula.tapped = true;
					audio.Play();
				}
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
				lCannula.Rotate(-0.1f);
			else
				rCannula.Rotate(-0.1f);
		}
		if(Input.IsActionPressed("cann_counterclock")) {
			if(!lrRotating)
				lCannula.Rotate(0.1f);
			else
				rCannula.Rotate(0.1f);
		}
		if(Input.IsActionJustPressed("cann_reset")) {
			if(!lrRotating)
				lCannula.SetRotation(0);
			else
				rCannula.SetRotation(0);
		}
	}

	public override void _PhysicsProcess(float delta) {
		Vector2 localMousePos = this.GetLocalMousePosition(); // local pos of the mouse
		Vector2 leftPos = new Vector2((localMousePos.x - 28.5f), localMousePos.y);
		Vector2 rightPos = new Vector2((localMousePos.x + 28.5f), localMousePos.y);

		// enabling and disabling cannula movement
		if(!lCannula.locked)
			lCannula.SetPosition(leftPos);
		if(!rCannula.locked)
			rCannula.SetPosition(rightPos);
		if(lCannula.locked && rCannula.locked)
			Input.SetMouseMode((Godot.Input.MouseMode)0); // displays the mouse
		else
			Input.SetMouseMode((Godot.Input.MouseMode)1); // hides the mouse
	}
}
