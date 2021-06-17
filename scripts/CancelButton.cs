using Godot;
using System;

public class CancelButton : Button
{
	private MenuButton sprite ;

	public override void _Ready()
	{
		sprite = GetNode("/root/Spatial/MenuButton") as MenuButton;
		GD.Print(GetParent().GetName());
	}

	private void _on_CancelButton_pressed()
	{
		sprite.toggleVisibility();
	}
}
