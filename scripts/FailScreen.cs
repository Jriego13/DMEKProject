using Godot;
using System;

public class FailScreen : Control
{
  // Declare member variables here. Examples:
  // private int a = 2;
  // private string b = "text";

  // Called when the node enters the scene tree for the first time.
  public override void _Ready()
  {
    GD.Print("Hi , you failed");
    Input.SetMouseMode((Godot.Input.MouseMode)0); // displays the mouse
    var mainMenuButton = GetNode("ColorRect/CenterContainer/VBoxContainer/Buttons/MainMenu");
    mainMenuButton.Connect("pressed" , this , "onMainMenuPressed");
    var restartLevelButton = GetNode("ColorRect/CenterContainer/VBoxContainer/Buttons/TryAgain");
    restartLevelButton.Connect("pressed", this, "onRestartLevelPressed");
  }

  //  // Called every frame. 'delta' is the elapsed time since the previous frame.
  //  public override void _Process(float delta)
  //  {
  //
  //  }

  private void onMainMenuPressed()
  {
    GD.Print("Quit pressed, returning to main menu");
    Input.SetMouseMode((Godot.Input.MouseMode)0);
    GetTree().ChangeScene("res://MainMenu2.tscn");
  }

  private void onRestartLevelPressed()
  {
    //         GD.Print("Loading scene " + Helper.startLevel);
    //         // Load the singleton levelSwitcher:
    var levelSwitcher = GetNode<LevelSwitcher>("/root/LevelSwitcher");
    //         // Store the next level and change to the MainEye scene:
    if (levelSwitcher.tutorialMode()) {
      levelSwitcher.ChangeLevel(Helper.toFileName(Helper.tutorialSceneName), levelSwitcher.getLevelName());
    }
    else {
      levelSwitcher.ChangeLevel(Helper.toFileName(Helper.mainSceneName), levelSwitcher.getLevelName());
    }
  }
}
