using Godot;
using System;

// Alternative options menu that acts as a popup. Holds an Options.cs in it.
public class OptionsPopUp : Popup
{
    public bool visible = false;
    public override void _Ready()
    {
        var goBackButton = GetNode("MarginContainer2/MarginContainer/VBoxContainer/HBoxContainer/CenterContainer/GoBackButton");
        goBackButton.Connect("pressed", this, "onGoBackButtonPressed");
    }

    private void onGoBackButtonPressed()
    {
        GD.Print("Go back presseddd");
        Input.SetMouseMode((Godot.Input.MouseMode)1);
        visible = false;
		Hide();
    }
}
