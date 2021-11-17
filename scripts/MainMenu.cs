using Godot;
using System;

public class MainMenu : MarginContainer
{
	AudioStreamPlayer hoverSound;
	AudioStreamPlayer clickSound;
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
		SetUpSound();
		var buttons = new[] {optionsButton, levelSelectButton, tutorialButton, playButton};
		foreach (var button in buttons)
        {
            button.Connect("mouse_entered", this, "onMouseEnteredButton");
        }
	}
	// Play a sound when the player hovers over a button:
	private void onMouseEnteredButton()
	{
		hoverSound.VolumeDb = Helper.soundEffectsVolumeDb;
		hoverSound.Play();
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
		clickSound.VolumeDb = Helper.soundEffectsVolumeDb;
		clickSound.Play();
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
	private void SetUpSound()
	{
		// Music:
		var music = GetNode("/root/Music") as AudioStreamPlayer;
		music.VolumeDb = Helper.musicVolumeDb;
		// Only restart menu music if it isn't already playing:
		if (music.Stream.ResourcePath != "res://music/MainMenuMusic.wav")
		{
        	music.Stream = ResourceLoader.Load("res://music/MainMenuMusic.wav") as AudioStream;
        	music.Play();
		}
		hoverSound = GetNode("HoverSound") as AudioStreamPlayer;
		clickSound = GetNode("ClickSound") as AudioStreamPlayer;
		hoverSound.VolumeDb = Helper.soundEffectsVolumeDb;
		clickSound.VolumeDb = Helper.soundEffectsVolumeDb;
		clickSound.Play();
	}
}
