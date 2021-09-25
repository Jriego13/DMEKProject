using Godot;
using System;

public class Options : MarginContainer
{
    public override void _Ready()
    {
        var goBackButton = GetNode("MarginContainer/VBoxContainer/HBoxContainer/CenterContainer/GoBackButton");
        goBackButton.Connect("pressed", this, "onGoBackButtonPressed");
    }

    // Navigates back to the main menu:
    private void onGoBackButtonPressed()
    {
        GD.Print("Go back pressed");
		GetTree().ChangeScene("res://MainMenu2.tscn");
    }
}
