using Godot;
using System;

public class SimpleDirisamerT : Spatial
{
	SimpleFold node;
	Mesh mesh;

	public override void _Ready()
	{
		node = GetNode("..") as SimpleFold;
	}

	private void _on_Area_input_event(object camera, object @event, Vector3 click_position, Vector3 click_normal, int shape_idx)
	{

		if(@event is InputEventMouseButton mouseEvent && mouseEvent.Pressed )
		{
			if ((ButtonList)mouseEvent.ButtonIndex == ButtonList.Left)
			{
				//GD.Print("Changing mesh.");
				//node.SetMesh(mesh);
				//node.tapCircle = true;
			}
		}
	}
}
