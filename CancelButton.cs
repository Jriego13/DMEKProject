using Godot;
using System;

public class CancelButton : Button
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print("Correl");
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

private void _on_CancelButton_pressed()
{
	// Replace with function body.
	GD.Print("Fuckkkkkkkk");
}
}
