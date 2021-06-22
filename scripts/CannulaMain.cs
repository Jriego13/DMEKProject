using Godot;
using System;

public class CannulaMain : Spatial
{	
	Camera mainCam;
	
	public override void _Ready()
	{
		mainCam = GetNode("../Camera") as Camera;
	}

//	public override void _Process(float delta)
//	{
//
//	}

	public override void _PhysicsProcess(float delta)
	{
		Vector3 mousePos = mainCam.ProjectPosition(GetViewport().GetMousePosition(), 10);
		mousePos.y = 12;
		SetTranslation(mousePos);
	}
}
