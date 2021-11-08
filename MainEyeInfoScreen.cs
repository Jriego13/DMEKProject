using Godot;
using System;

public class MainEyeInfoScreen : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        var goBackButton = GetNode("Background/GoBackButton");
		goBackButton.Connect("pressed", this, "onGoBackButtonPressed");   
    }

    private void onGoBackButtonPressed()
    {
        GD.Print("Go back pressed");
		GetTree().ChangeScene(Helper.toFileName("MainMenu"));
    }
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        
   }
}
