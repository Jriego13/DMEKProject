using Godot;
using System;

// toggles visibility of the X button that appears when the menu button is tapped
public class CancelButton : Button
{
	private MenuButton sprite ;

	public override void _Ready()
	{
		sprite = GetNode("/root/Spatial/MenuButton") as MenuButton;
		//GD.Print(GetParent().GetName());
	}

	private void _on_CancelButton_pressed()
	{
		sprite.toggleVisibility();
	}
}
