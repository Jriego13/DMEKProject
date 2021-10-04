using Godot;
using System;

public class MainMenu2 : MarginContainer
{
	public override void _Ready()
	{
		String menuOptions = "MarginContainer/HBoxContainer/VBoxContainer/MenuOptions/";
		var optionsButton = GetNode(menuOptions + "Options");
		optionsButton.Connect("pressed", this, "onOptionsPressed");
		var levelSelectButton = GetNode(menuOptions + "Level Select");
		levelSelectButton.Connect("pressed", this, "onLevelSelectPressed");
		var continueButton = GetNode(menuOptions + "Continue");
		continueButton.Connect("pressed", this, "onContinuePressed");
		var tutorialButton = GetNode(menuOptions + "Tutorial");
		tutorialButton.Connect("pressed", this, "onTutorialPressed");
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
	private void onTutorialPressed()
	{
		GD.Print("Tutorial pressed");
		GetTree().ChangeScene(Helper.toFileName("TutorialSelect"));
	}
}



