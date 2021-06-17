using Godot;
using System;

public class Scroll : MeshInstance
{
	private PackedScene nextScene;
	private Node sceneNode;
	public bool topCircle = false;
	public bool midCircle = false;

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
