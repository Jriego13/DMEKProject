using Godot;
using System;

public class Options : MarginContainer
{
    private bool visible = false;
    public override void _Input(InputEvent @event)
    {
        base._Input(@event);
        if (@event.IsActionPressed("ui_cancel"))
        {
            GD.Print("escape pressed");
            visible = !visible;
        }
    }
    public override void _Ready()
    {
        var goBackButton = GetNode("MarginContainer/VBoxContainer/HBoxContainer/CenterContainer/GoBackButton");
        goBackButton.Connect("pressed", this, "onGoBackButtonPressed");
    }
    // public override void _Process(float delta)
    // {
    //     if (!visible)
    //     {
    //         Popup_();
    //     }
    //     else
    //     {
    //         Hide();
    //     }
    // }

    // Navigates back to the main menu:
    private void onGoBackButtonPressed()
    {
        GD.Print("Go back pressed");
		this.QueueFree();
    }
}
