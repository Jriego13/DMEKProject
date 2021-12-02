using Godot;
using System;

public class CannulaMain2D : Node2D {
	Cannula2D lCannula;
	Cannula2D rCannula;
	bool inject = false;
	bool lHeld = false;
	bool rHeld = false;
	bool lrRotating = false; // false defualts to rotating left cannula, true to right cannula
	float timer = 0;
	bool menuOpen;

	public override void _Ready() {
		Input.SetMouseMode((Godot.Input.MouseMode)1); // hide mouse
		lCannula = GetNode("./CannulaLSprite") as Cannula2D;
		rCannula = GetNode("./CannulaRSprite") as Cannula2D;
		menuOpen = false;
	}

	public override void _Process(float delta) {
		if (!menuOpen) {
			if(Input.IsActionPressed("left_mouse") && !lCannula.locked)	{
				if(lHeld) {
					timer += delta; // if cannula continues being held then keep adding to timer
					lCannula.SetScale(new Vector2(0.28f, 0.28f));
				}
				if(timer >= 0.4f) { // once held for longer than whatever time
					lCannula.LockCannula();
					timer = -1;
				}

				lHeld = true;
			}
			else if(Input.IsActionJustReleased("left_mouse")) {
				if(lCannula.locked)
					lCannula.LockCannula();
			}
			else if(Input.IsActionPressed("right_mouse") && !rCannula.locked) {
				if(rHeld) {
					timer += delta;
					rCannula.SetScale(new Vector2(0.28f, 0.28f));
				}
				if(timer >= 0.4f) {
					rCannula.LockCannula();
					timer = -1;
				}

				rHeld = true;
			}
			else if(Input.IsActionJustReleased("right_mouse")) {
				if(rCannula.locked)
					rCannula.LockCannula();
			}
			else {
				if((timer < 1 && timer > 0) && (lHeld || rHeld)) { // left or right cannula was held and released in under a second
					if(lHeld){
						lCannula.tapped = true;
					}
					else{
						rCannula.tapped = true;
					}
				}
				else {
					lCannula.tapped = false;
					rCannula.tapped = false;
				}

				// reset their states
				lCannula.SetScale(new Vector2(0.3f, 0.3f));
				rCannula.SetScale(new Vector2(0.3f, 0.3f));
				lHeld = false;
				rHeld = false;
				timer = 0;
			}

			if(Input.IsActionPressed("cann_inject")) {
				if(!lrRotating) {
					lCannula.injecting = true;
					lCannula.Inject();
				}
				else {
					rCannula.injecting = true;
					rCannula.Inject();
				}
			}
			else if(Input.IsActionJustReleased("cann_inject")){
				if(!lrRotating) {
					lCannula.injecting = false;
					lCannula.Inject();
				}
				else {
					rCannula.injecting = false;
					rCannula.Inject();
				}
			}

			if(Input.IsActionJustPressed("cann_swap")) {
				lrRotating = !lrRotating;
				invertSelectedSprites();
				GD.Print("cannula control swap.");
			}

			// !lrRotating = lCannula | lrRotating = rCannula
			if(Input.IsActionPressed("cann_clock")) {
				if(!lrRotating)
					lCannula.Rotate(-0.06f);
				else
					rCannula.Rotate(-0.06f);
			}
			if(Input.IsActionPressed("cann_counterclock")) {
				if(!lrRotating)
					lCannula.Rotate(0.06f);
				else
					rCannula.Rotate(0.06f);
			}
			if(Input.IsActionJustPressed("cann_reset")) {
				if(!lrRotating)
					lCannula.SetRotation(0);
				else
					rCannula.SetRotation(0);
			}
		}
	}

	public override void _PhysicsProcess(float delta) {
		Vector2 localMousePos = this.GetLocalMousePosition(); // local pos of the mouse
		Vector2 leftPos = new Vector2((localMousePos.x - 28.5f), localMousePos.y);
		Vector2 rightPos = new Vector2((localMousePos.x + 28.5f), localMousePos.y);
		if (!menuOpen) {
			localMousePos = this.GetLocalMousePosition(); // local pos of the mouse
			leftPos = new Vector2((localMousePos.x - 28.5f), localMousePos.y);
			rightPos = new Vector2((localMousePos.x + 28.5f), localMousePos.y);

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

	public void invertSelectedSprites() {
		Sprite lSprite = GetNode("./CannulaLSprite/Selected") as Sprite;
		Sprite rSprite = GetNode("./CannulaRSprite/Selected") as Sprite;
		lSprite.SetVisible(!lSprite.IsVisible());
		rSprite.SetVisible(!rSprite.IsVisible());
	}

	public Cannula2D getLCannula() {
		return lCannula;
	}

	public Cannula2D getRCannula() {
		return rCannula;
	}

	public bool getLHeld(){
		return lHeld;
	}

	public bool getRHeld(){
		return rHeld;
	}

	public bool getLLocked(){
		return lCannula.locked;
	}

	public bool getRLocked(){
		return rCannula.locked;
	}

	public void setMenuOpen(bool input) {
		menuOpen = input;
	}
}
