using Godot;
using System;

// Alternative options menu that acts as a popup.
public class OptionsPopUp : Popup
{
    public override void _Ready()
    {
        var goBackButton = GetNode("MarginContainer2/MarginContainer/VBoxContainer/HBoxContainer/CenterContainer/GoBackButton");
        goBackButton.Connect("pressed", this, "onGoBackButtonPressed");
    }

    private void onGoBackButtonPressed()
    {
        GD.Print("Go back pressed");
        Input.SetMouseMode((Godot.Input.MouseMode)1);
		Hide();
    }
    
}
