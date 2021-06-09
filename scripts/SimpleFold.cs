using Godot;
using System;

public class SimpleFold : MeshInstance
{
	// Declare member variables here. Examples:
	private PackedScene nextScene;
	private Node sceneNode;
	public bool tapCircle = false;
	public bool holdCircle = false;
	
	public override void _Ready()
	{
		nextScene = GD.Load<PackedScene>("res://EdgeFold.tscn");
		sceneNode = nextScene.Instance();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		if(tapCircle && holdCircle)
		{
			// change scene being displayed. not mesh.
			GetNode("/root/Spatial/MainEye").AddChild(sceneNode);
			GetParent().RemoveChild(this);
		}   
	}
}
