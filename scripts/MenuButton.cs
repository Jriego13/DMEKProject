using Godot;
using System;

// three bar menu button that is in the top left of the screen
public class MenuButton : Sprite3D
{
	private PackedScene nextScene;
	private Node sceneNode;
	private MarginContainer menuView;
	private Button cancelButton;

	public override void _Ready()
	{
		nextScene = GD.Load<PackedScene>("res://Menu.tscn");
		sceneNode = nextScene.Instance();
		menuView = GetChild(1).GetChild(0) as MarginContainer;
		cancelButton = GetChild(1).GetChild(1) as Button;
	}

	private void _on_Area_input_event(object camera, object @event, Vector3 click_position, Vector3 click_normal, int shape_idx)
	{
		if(@event is InputEventMouseButton mouseEvent && mouseEvent.Pressed )
			{
				if ((ButtonList)mouseEvent.ButtonIndex == ButtonList.Left)
				{
					this.Visible = false;
					menuView.Visible = true;
					cancelButton.Visible = true;
				}
			}
	}

	public void toggleVisibility()
	{
		menuView.Visible = false;
		cancelButton.Visible = false;
		this.Visible = true;
	}
}
