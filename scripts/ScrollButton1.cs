using Godot;
using System;

// these buttons should be changing the scene on the screen.
public class ScrollButton1 : Spatial
{
	Scroll node;
	Mesh mesh;
	
	public override void _Ready()
	{
		node = GetNode("..") as Scroll;
		mesh = (Mesh)GD.Load("res://models/Simple Fold.obj");
	}

	private void _on_Area_input_event(object camera, object @event, Vector3 click_position, Vector3 click_normal, int shape_idx)
	{
		if(@event is InputEventMouseButton mouseEvent && mouseEvent.Pressed )
		{
			if ((ButtonList)mouseEvent.ButtonIndex == ButtonList.Left)
			{
				//GD.Print("Changing mesh.");
				//node.SetMesh(mesh);
				node.midCircle = true;
			}
		}
	}
}
