using Godot;
using System;

public class TutorialSuccessScreen : Control
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Input.SetMouseMode((Godot.Input.MouseMode)0); // displays the mouse
        var tutorialMenuButton = GetNode("ColorRect/CenterContainer/VBoxContainer/Buttons/MainMenu");
        tutorialMenuButton.Connect("pressed" , this , "onTutorialMenuPressed");
        var replayButton = GetNode("ColorRect/CenterContainer/VBoxContainer/Buttons/TryAgain");
        replayButton.Connect("pressed" , this , "onReplayPressed");
    }


    private void onTutorialMenuPressed() {
        Input.SetMouseMode((Godot.Input.MouseMode)0);
        GetTree().ChangeScene(Helper.toFileName("TutorialSelect"));
    }

    private void onReplayPressed() {
        var levelSwitcher = GetNode<LevelSwitcher>("/root/LevelSwitcher");
        levelSwitcher.ChangeLevel(Helper.toFileName(Helper.tutorialSceneName), levelSwitcher.getLevelName());
    }
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
