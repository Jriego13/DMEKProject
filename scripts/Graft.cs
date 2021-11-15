using Godot;
using System;
using System.Collections.Generic;

// base graft class that I would like to use for all derivative grafts
public class Graft : Sprite {
  protected List<PackedScene> nextConfirmations = new List<PackedScene>();
  protected List<Texture> graftTextures = new List<Texture>();
  protected List<Texture> graftTexturesOther = new List<Texture>();
  protected Random rng = new Random();
  protected int numTaps;
  protected int numTapsComplete;
  protected int numTapsWrong;
  protected int topTaps = 0;
  protected int topTapsComplete = 0;
  protected bool isFinished;
  protected bool isNextLevelSet;

  protected String previousConfirmation;
  protected String currentConfirmation;
  protected String nextConfirmation;
  protected Cannula2D lCannula;
	protected Cannula2D rCannula;
  protected RichTextLabel misclickText;
	protected bool isTutorialMode;
  protected double rotationalVelocity;
  protected Area2D interactionBox;
  protected bool interactable = true;
  public bool gamePaused = false;
  public bool misclicksOn = true;
  public override void _Ready()
  {
    SetObjectives();
    LoadTextures();
    lCannula = GetNode("../Cannulas/CannulaLSprite") as Cannula2D;
    rCannula = GetNode("../Cannulas/CannulaRSprite") as Cannula2D;
    misclickText = GetNode("../Overlay/MisclickCounter") as RichTextLabel;
  	var levelSwitcher = GetNode<LevelSwitcher>("/root/LevelSwitcher");
  	isTutorialMode = levelSwitcher.tutorialMode();
  }

  // This is where each graft will check for their specific objectives.
  // This separation allows for a universal _Process function.
  protected virtual void CheckObjectives() {}

  // This is where each graft sets up their specific objectives.
  // This separation allows for a universal _Ready function.
  protected virtual void SetObjectives() {}
  protected virtual void RegisterClick() {}

  protected void LoadTextures() {
    Texture currImg;
    GD.Print("Loading Textures...");

    if(currentConfirmation == "SimpleFold") {
      graftTextures.Add(Texture);
      for(int i = 0; i < 3; i++) {
        currImg = GD.Load("res://sprites/SimpleEdge" + (i+1) + ".png") as Texture;
        graftTextures.Add(currImg);
      }
    }
    else if(currentConfirmation == "EdgeFold") {
      graftTextures.Add(Texture);
      for(int i = 0; i < 3; i++) {
        currImg = GD.Load("res://sprites/EdgeDone" + (i+1) + ".png") as Texture;
        graftTextures.Add(currImg);
      }
    }
    else if(currentConfirmation == "Scroll") {
      graftTextures.Add(Texture);
      for(int i = 0; i < 3; i++) {
        currImg = GD.Load("res://sprites/ScrollSimple" + (i+1) + ".png") as Texture;
        graftTextures.Add(currImg);
      }
      for(int i = 0; i < 3; i++) {
        currImg = GD.Load("res://sprites/ScrollDouble" + (i+1) + ".png") as Texture;
        graftTexturesOther.Add(currImg);
      }
    }
    else if(currentConfirmation == "Bouquet") {
      graftTextures.Add(Texture);
      for(int i = 0; i < 6; i++) {
        currImg = GD.Load("res://sprites/BouquetSimple" + (i+1) + ".png") as Texture;
        graftTextures.Add(currImg);
      }
    }
    else if(currentConfirmation == "DoubleScroll") {
      graftTextures.Add(Texture);
      for(int i = 0; i < 3; i++) {
        currImg = GD.Load("res://sprites/DoubleSimple" + (i+1) + ".png") as Texture;
        graftTextures.Add(currImg);
      }
    }
  }

  // What will happen when the player clicks outside of the correct areas:
  protected void registerMisclick()
  {
    if (gamePaused || !misclicksOn)
      return;
    if(numTapsWrong < 3){
					GD.Print("You clicked outside of the correct areas");
          if (lCannula.tapped)
          {
            lCannula.ShowMisclickCircle();
          }
          if (rCannula.tapped)
          {
            rCannula.ShowMisclickCircle();
          }
					++numTapsWrong;
          misclickText.Text = "Misclicks: " + numTapsWrong + "/3";
				}
				else{
					// GD.Print("Misclicked too many times. You fail!");
					// lCannula.locked = true;
					// rCannula.locked = true;
					// previousConfirmation = "Scroll";
          // numTapsWrong = 0;
          // Input.SetMouseMode((Godot.Input.MouseMode)0);
					// GetTree().ChangeScene("res://FailScreen.tscn");
				}
  }
  public bool getIsFinished() {
		return isFinished;
	}

  public void onInteractionBoxEntered()
	{
		interactable = true;
	}

  public void onInteractionBoxExited()
  {
    interactable = false;
  }

  public override void _Input(InputEvent @event)
    {
        base._Input(@event);

        if (@event.IsActionPressed("toggle_rotation"))
        {
          interactable = !interactable;
        }
    }

  public override void _Process(float delta)
  {
    //Input.SetMouseMode((Godot.Input.MouseMode)0);
    if (!gamePaused)
    {
      CheckObjectives();
      if (Math.Abs(rotationalVelocity) > 0)
        Deaccelerate();
    }
    if ((lCannula.tapped || rCannula.tapped) &&
      this.GetRect().HasPoint(ToLocal(GetViewport().GetMousePosition())) && interactable)
    {
      RotateFromTap();
    }
      // if the player taps outside of a hitbox:
			if((lCannula.tapped && lCannula.numAreasIn == 0)||(rCannula.tapped && rCannula.numAreasIn == 0))
			{
				registerMisclick();
			}
  }

  protected void Deaccelerate()
  {
    const float deacceleration = 0.01f;
    this.Rotate((float)rotationalVelocity);

    // Bring rotationalVelocity gradually to 0.
    if (rotationalVelocity > 0)
    {
      rotationalVelocity -= deacceleration;
      if (rotationalVelocity < 0)
        rotationalVelocity = 0;
    }
    else
    {
      rotationalVelocity += deacceleration;
      if (rotationalVelocity > 0)
        rotationalVelocity = 0;
    }
  }

  private void RotateFromTap()
  {
    Vector2 tapPos = new Vector2(0.0f,0.0f);
    if (lCannula.tapped)
      tapPos = lCannula.GlobalPosition;
    else if (rCannula.tapped)
      tapPos = rCannula.GlobalPosition;
    var axis = GetNode("Axis") as Line2D;
    var axisPoint0 = GlobalTransform.BasisXform(axis.Points[0]);
    var axisPoint1 = GlobalTransform.BasisXform(axis.Points[1]);

    // 1. Get the slope of the axis line. GlobalTransform.BasisXform makes the point coordinates update after rotation.
    // m = (y1-y2)/(x1-x2)
    var axisSlope = (axisPoint0.y - axisPoint1.y)/(axisPoint0.x - axisPoint1.x + 0.0001);// add 0.0001 to prevent div by 0.

    // 2. Find closest point on rotational axis to where the user clicked:
    var minDistance = 100000.0; //start min distance impossibly large.
    var closestAxisPoint = new Vector2();
    foreach (Vector2 point in axis.Points)
    {
      var distance = ToGlobal(point).DistanceTo(tapPos);
      if (distance < minDistance)
      {
        minDistance = distance;
        closestAxisPoint = ToGlobal(point);
      }
    }
    // 3. Find slope of line going from where the user clicked to center of the graft:
    var centerLine = GetNode("Center") as Line2D;
    var centerPoint = ToGlobal(centerLine.Points[0]);
    var slope = (tapPos.y - centerPoint.y)/(tapPos.x - centerPoint.x);

    // 4. Calculate r and θ:
    var r = Math.Abs(centerPoint.DistanceTo(closestAxisPoint));
    var theta = Math.Atan((slope - axisSlope)/(1+slope * axisSlope)); // tanθ = (m1-m2)/(1+m1*m2)

    // 5. Calculate torque, T = F*r*sinθ:
    const float tapForce = 0.001f;
    var torque = tapForce * r * Math.Sin(theta);
    rotationalVelocity = torque; // Make rotational velocity the torque because things are complicated enough

    // GD.Print("axis slope ", axisSlope);
    // GD.Print("mouse pos ", mousePos);
    // GD.Print("slope of line from mouse to center ", slope);
    // GD.Print("r ", r);
    // GD.Print("theta ", theta);
    // GD.Print("torque ", torque);
  }

  public bool getIsNextLevelSet() {
    return isNextLevelSet;
  }

  public String getSetConfirmation() {
    return nextConfirmation;
  }

  public int getNumTaps() {
    return numTaps;
  }

  public int getTopTaps() {
    return topTaps;
  }
}
