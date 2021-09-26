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
		var continueButton = GetNode("MarginContainer/HBoxContainer/VBoxContainer/MenuOptions/Continue");
		continueButton.Connect("pressed", this, "onContinuePressed");
	}

	// Loads wherever the player was last:
	private void onContinuePressed()
	{
		GD.Print("Continue pressed");
		GetTree().ChangeScene(Helper.toFileName("SimpleFold"));
		
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
		GetTree().ChangeScene(Helper.toFileName("Options"));
		
	}
}



