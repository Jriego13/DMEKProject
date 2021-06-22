using Godot;
using System;

public class CameraButton : Sprite3D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	
	private Camera cam1;
	private Camera cam2;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		cam1 = (Camera)GetNode("../Camera");
		cam2 = (Camera)GetNode("../Camera2");
		cam1.Current = true;
		cam2.Current = false;	
		
	}




private void _on_Area_input_event(object camera, object @event, Vector3 click_position, Vector3 click_normal, int shape_idx)
{
	// Replace with function body.
	if(@event is InputEventMouseButton mouseEvent && mouseEvent.Pressed )
			{
				if ((ButtonList)mouseEvent.ButtonIndex == ButtonList.Left)
				{
					GD.Print("Hello");
					if(cam1.Current == true){
						cam1.Current = false;
						cam2.Current = true;
					}
					else{
						cam1.Current = true;
						cam2.Current = false;
					}
				}
			}
}

}
