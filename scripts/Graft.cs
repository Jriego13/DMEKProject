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
        if (@event.IsActionPressed("left_mouse")&&
          GetRect().HasPoint(ToLocal(GetViewport().GetMousePosition())))
        {
          RotateFromTap();
        }
    }
  public override void _Process(float delta)
  {
    //this.Rotate(0.1f);
    Input.SetMouseMode((Godot.Input.MouseMode)0);
    CheckObjectives();
    if (Math.Abs(rotationalVelocity) > 0)
      Deaccelerate();
  }

  // This is where each graft will check for their specific objectives.
  // This separation allows for a universal _Process function.
  protected async virtual void CheckObjectives()
  {

  }
  protected void Deaccelerate()
  {
    this.Rotate((float)rotationalVelocity);
    if (rotationalVelocity > 0)
    {
      rotationalVelocity -= .01;
      if (rotationalVelocity < 0)
        rotationalVelocity = 0;
    }
    else
    {
      rotationalVelocity += .01;
      if (rotationalVelocity > 0)
        rotationalVelocity = 0;
    }
      
  }
  private void RotateFromTap()
  {
    Vector2 mousePos = GetViewport().GetMousePosition();
    Vector2 graftPos = GlobalPosition;
    
    // T = F*r*sinθ, force is constant for now
    var axis = GetNode("Axis") as Line2D;
    var axisSlope = (GlobalTransform.BasisXform(axis.Points[0]).y - GlobalTransform.BasisXform(axis.Points[1]).y)/
    (GlobalTransform.BasisXform(axis.Points[0]).x - GlobalTransform.BasisXform(axis.Points[1]).x + 0.0001);
    GD.Print("axis slope", axisSlope);
    //GD.Print(polygon.Polygon);
    var minDistance = 10000.0;
    var closestAxisPoint = new Vector2();
    foreach (Vector2 vertex in axis.Points)
    {
      var distance = ToGlobal(vertex).DistanceTo(mousePos);
      //GD.Print(distance);
      if (distance < minDistance)
      {

        minDistance = distance;
        closestAxisPoint = ToGlobal(vertex);
      }
    }
    GD.Print("mouse pos ", mousePos);
    const float tapForce = 0.001f;
    var spriteCenter = GetNode("Center") as Line2D;
    var centerPoint = ToGlobal(spriteCenter.Points[0]);
    GD.Print("sprite center ", centerPoint);
    var slope = (mousePos.y - centerPoint.y)/(mousePos.x - centerPoint.x);
    var y_intercept = mousePos.y - (slope * mousePos.x);
    var r = Math.Abs(centerPoint.DistanceTo(closestAxisPoint));
    GD.Print("slope ", slope);
    var theta = Math.Atan((slope - axisSlope)/(1+slope * axisSlope));
    GD.Print("theta", theta);
    var torque = tapForce * r * Math.Sin(theta);
    rotationalVelocity = torque;
    GD.Print(torque);

    //var xTorque = tapForce * Math.Abs(mousePos.x - graftPos.x) * 
  }
}
