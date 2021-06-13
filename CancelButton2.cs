using Godot;
using System;

public class CancelButton2 : Button
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	private MenuButton sprite ;
	
	

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		sprite = GetNode("/root/Spatial/MenuButton") as MenuButton;
		GD.Print(GetParent().GetName());
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }



private void _on_CancelButton_pressed()
{

	sprite.toggleVisibility();
}
}
