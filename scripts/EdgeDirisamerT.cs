using Godot;
using System;

// !no longer being used but kept for reference!
public class EdgeDirisamerT : Node
{
	EdgeFold node;
	Mesh mesh;

	public override void _Ready()
	{
		//node = GetNode("..") as EdgeFold;
	}

	private void _on_Area_input_event(object camera, object @event, Vector3 click_position, Vector3 click_normal, int shape_idx)
	{

		if(@event is InputEventMouseButton mouseEvent && mouseEvent.Pressed )
		{
			if ((ButtonList)mouseEvent.ButtonIndex == ButtonList.Left)
			{
				//node.tapCircle = true;
			}
		}
	}
}
