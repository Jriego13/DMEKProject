using Godot;
using System;

public class NextButton : Sprite3D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	private MarginContainer infoBox;
	private RichTextLabel title;
	private RichTextLabel body;
	private MeshInstance eye;
	private MeshInstance graft;
	private int counter = 0;
	private Mesh simpleFold;
	private Mesh edgeFold; 
//	private Camera camera;
//	private Vector3 movement = new Vector3();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// get all the nodes and resources we need
		infoBox = (MarginContainer)GetNode("../InformationBox");
		title = (RichTextLabel)GetNode("../InformationBox/ColorRect/Title");
		body = (RichTextLabel)GetNode("../InformationBox/ColorRect/Body");
		eye = (MeshInstance)GetNode("../MainEye");
		graft = (MeshInstance)GetNode("../Grafts/MeshInstance");
		simpleFold = (Mesh)ResourceLoader.Load("res://models/Simple Fold.obj");
		edgeFold = (Mesh)ResourceLoader.Load("res://models/Edge Fold.obj");
		
//		camera = (Camera)GetNode("../Camera");
		// init values
		title.Text = "DMEK Surgery Overview";
		body.Text = "DMEK Surgery Information will go here";
		graft.Visible = false;
		
	}


private void _on_Area_input_event(object camera, object @event, Vector3 click_position, Vector3 click_normal, int shape_idx)
{
   		if(@event is InputEventMouseButton mouseEvent && mouseEvent.Pressed )
			{
				if ((ButtonList)mouseEvent.ButtonIndex == ButtonList.Left)
				{
					switch(counter){
						case 0:

							title.Text = "Scroll";
							body.Text = "Information about the Scroll configuration. Where to tap and hold, what configuration comes next/before, etc. ";
							eye.Visible = false;
							graft.Visible = true;
							counter++;
							break;
						case 1: 
							title.Text = "Simple Fold";
							body.Text = "Information about the Simple Fold configuration. Where to tap and hold, what configuration comes next/before, etc. ";
							graft.Mesh = simpleFold;
							counter++;
							break;
						case 2:
							title.Text = "Edge Fold";
							body.Text = "Information about the Edge Fold configuration. Where to tap and hold, what configuration comes next/before, etc. ";
							graft.Mesh = edgeFold;
							break;
							
						
					}
				
			
				}
			}
}
}
