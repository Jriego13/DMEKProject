using Godot;
using System;

public class MainMenu2 : MarginContainer
{
	public override void _Ready()
	{
		var optionsButton = GetNode("MarginContainer/HBoxContainer/VBoxContainer/MenuOptions/Options");
		optionsButton.Connect("pressed", this, "onOptionsPressed");
		var levelSelectButton = GetNode("MarginContainer/HBoxContainer/VBoxContainer/MenuOptions/Level Select");
		levelSelectButton.Connect("pressed", this, "onLevelSelectPressed");
		var playButton = GetNode("MarginContainer/HBoxContainer/VBoxContainer/MenuOptions/Play");
		playButton.Connect("pressed", this, "onPlayPressed");
	}

	// Starts the game at a random confirmation:
	private void onPlayPressed()
	{
		GD.Print("Play pressed");
		GetTree().ChangeScene(Helper.toFileName("MainEye2D"));
	}
	// Navigates to the level select screen:
	private void onLevelSelectPressed()
	{
		GD.Print("Level select pressed");
		GetTree().ChangeScene(Helper.toFileName("LevelSelect"));
	}
	// Navigates to the options screen:
	private void onOptionsPressed()
	{
		GD.Print("Options pressed");
		var optionsScene = GD.Load<PackedScene>("res://Options.tscn");
		var optionsMenu = (Node)optionsScene.Instance();
		GetNode("/root/MarginContainer").AddChild(optionsMenu);

	}
}
