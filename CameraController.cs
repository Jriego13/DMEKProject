using Godot;
using System;

public class CameraController : Camera
{
	// Declare member variables here. 
	private Vector3 movement = new Vector3();
	const int cameraSpeed = 2;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}
	
	public override void _PhysicsProcess(float delta)
	{
		movement.x = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");
		movement.z = Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up");
		movement = movement.Normalized();
		this.GlobalTranslate(movement * delta * cameraSpeed);
	}
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
