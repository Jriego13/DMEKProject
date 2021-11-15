using Godot;
using System;

public class MainMenu : MarginContainer
{
	public override void _Ready()
	{
		String menuOptions = "MarginContainer/HBoxContainer/VBoxContainer/MenuOptions/";
		var optionsButton = GetNode(menuOptions + "Options");
		optionsButton.Connect("pressed", this, "onOptionsPressed");
		var levelSelectButton = GetNode(menuOptions + "Level Select");
		levelSelectButton.Connect("pressed", this, "onLevelSelectPressed");
		var tutorialButton = GetNode(menuOptions + "Tutorial");
		tutorialButton.Connect("pressed", this, "onTutorialPressed");
		var playButton = GetNode("MarginContainer/HBoxContainer/VBoxContainer/MenuOptions/Play");
		playButton.Connect("pressed", this, "onPlayPressed");
		var music = GetNode("/root/Music") as AudioStreamPlayer;
        music.Stream = ResourceLoader.Load("res://music/MainMenuMusic.wav") as AudioStream;
        music.Play();
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
	private void onTutorialPressed()
	{
		GD.Print("Tutorial pressed");
		GetTree().ChangeScene(Helper.toFileName("TutorialSelect"));
	}
}
