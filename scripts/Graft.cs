using Godot;
using System;
using System.Collections.Generic;

// base graft class that I would like to use for all derivative grafts
public class Graft : Sprite {
  protected List<PackedScene> nextConfirmations = new List<PackedScene>();
  protected Random rng = new Random();
  protected int numTaps;
  protected int numTapsComplete;
  protected bool isFinished;
  protected double rotationalVelocity = 0;

  public bool getIsFinished() {
		return isFinished;
	}
  public override void _Input(InputEvent @event)
    {
        base._Input(@event);
        if (@event.IsActionPressed("left_mouse") &&
          GetRect().HasPoint(ToLocal(GetViewport().GetMousePosition())))
        {
          RotateFromTap();
        }
    }
  public override void _Process(float delta)
  {
    Input.SetMouseMode((Godot.Input.MouseMode)0);
    CheckObjectives();
    if (Math.Abs(rotationalVelocity) > 0)
      Deaccelerate();
  }

  // This is where each graft will check for their specific objectives.
  // This separation allows for a universal _Process function.
  protected virtual void CheckObjectives()
  {

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
    Vector2 mousePos = GetViewport().GetMousePosition();

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
      var distance = ToGlobal(point).DistanceTo(mousePos);
      if (distance < minDistance)
      {
        minDistance = distance;
        closestAxisPoint = ToGlobal(point);
      }
    }
    // 3. Find slope of line going from where the user clicked to center of the graft:
    var centerLine = GetNode("Center") as Line2D;
    var centerPoint = ToGlobal(centerLine.Points[0]);
    var slope = (mousePos.y - centerPoint.y)/(mousePos.x - centerPoint.x);

    // 4. Calculate r and θ:
    var r = Math.Abs(centerPoint.DistanceTo(closestAxisPoint));
    var theta = Math.Atan((slope - axisSlope)/(1+slope * axisSlope)); // tanθ = (m1-m2)/(1+m1*m2)

    // 5. Calculate torque, T = F*r*sinθ:
    const float tapForce = 0.001f;
    var torque = tapForce * r * Math.Sin(theta);
    rotationalVelocity = torque; // Make rotational velocity the torque because things are complicated enough

    GD.Print("axis slope ", axisSlope);
    GD.Print("mouse pos ", mousePos);
    GD.Print("slope of line from mouse to center ", slope);
    GD.Print("r ", r);
    GD.Print("theta ", theta);
    GD.Print("torque ", torque);
  }
}
