using Godot;
using System;

public class CameraController : Camera
{
	private Vector3 movement = new Vector3();
	const int cameraSpeed = 10;
	
	public override void _PhysicsProcess(float delta)
	{
		movement.x = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");
		movement.z = Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up");
		movement = movement.Normalized();
		this.GlobalTranslate(movement * delta * cameraSpeed);
	}
}
