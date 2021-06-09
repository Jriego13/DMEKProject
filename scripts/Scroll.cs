using Godot;
using System;

public class Scroll : MeshInstance
{
	// Declare member variables here. Examples:
	private PackedScene nextScene;
	private Node sceneNode;
	public bool topCircle = false;
	public bool midCircle = false;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		nextScene = GD.Load<PackedScene>("res://SimpleFold.tscn");
		sceneNode = nextScene.Instance();
	}
	
	public override void _Process(float delta)
	{
		if(topCircle && midCircle)
		{
			// change scene being displayed. not mesh.
			GetNode("/root/Spatial/MainEye").AddChild(sceneNode);
			GetParent().RemoveChild(this);
		}
	}
}
