using Godot;
using System;

// Menu that appears when escape is pressed during the main game:
public class EscapeMenu : Popup
{
    private bool visible;
    private bool optionsVisible;
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
        visible = false;
        optionsVisible = false;
        String menuOptions = "MarginContainer/MarginContainer/VBoxContainer/";
        var optionsButton = GetNode(menuOptions + "Options");
		optionsButton.Connect("pressed", this, "onOptionsPressed");
		var resumeGameButton = GetNode(menuOptions + "ResumeGame");
		resumeGameButton.Connect("pressed", this, "onResumeGamePressed");
		var restartLevelButton = GetNode(menuOptions + "RestartLevel");
		restartLevelButton.Connect("pressed", this, "onRestartLevelPressed");
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
        if (optionsVisible)
        {
            var optionsMenu = GetNode("OptionsPopup") as Popup;
            visible = false;
            optionsMenu.Popup_();
            optionsVisible = false;
        }
    }
    private void onOptionsPressed()
    {
        optionsVisible = true;
    }
    private void onResumeGamePressed()
    {
        GD.Print("Resume game pressed");
        visible = false;
    }
    private void onRestartLevelPressed()
    {
        GD.Print("Loading scene " + Helper.startLevel);
        // Load the singleton levelSwitcher:
        var levelSwitcher = GetNode<LevelSwitcher>("/root/LevelSwitcher");
        // Store the next level and change to the MainEye scene:
        levelSwitcher.ChangeLevel(Helper.toFileName(Helper.mainSceneName), Helper.startLevel);
    }
    private void onQuitPressed()
    {
        GD.Print("Quit pressed, returning to main menu");
        GetTree().ChangeScene("res://MainMenu2.tscn");
    }
}
