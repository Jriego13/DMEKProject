using Godot;
using System;

// Menu that appears when escape is pressed during the main game:
public class EndMenu : Popup
{
    public bool visible;

    public override void _Ready()
    {
        visible = true;
        String menuOptions = "MarginContainer/MarginContainer/VBoxContainer/";
        var quitButton = GetNode(menuOptions + "Quit");
        quitButton.Connect("pressed", this, "onQuitPressed");
    }

    public override void _Process(float delta)
    {
        if (visible)
        {
            Popup_();
            // Make mouse visible:
            Input.SetMouseMode((Godot.Input.MouseMode)0);
        }
        else
        {
            Hide();
        }
    }

    private void onQuitPressed()
    {
        GD.Print("Quit pressed, returning to main menu");
        GetTree().ChangeScene("res://MainMenu.tscn");
    }
}
