using Godot;
using System;

// Menu that appears when escape is pressed during the main game:
public class EscapeMenu : Popup
{
    public bool visible;
    public bool optionsVisible;
    public OptionsPopUp optionsMenu;
    // public override void _Input(InputEvent @event)
    // {
    //     base._Input(@event);
    //     if (@event.IsActionPressed("ui_cancel"))
    //     {
    //         GD.Print("escape pressed");
    //         visible = !visible;
    //     }
    // }
    public override void _Ready()
    {
        visible = false;
        optionsVisible = false;
        String menuOptions = "MarginContainer/MarginContainer/VBoxContainer/";
        var optionsButton = GetNode(menuOptions + "Options");
		optionsButton.Connect("pressed", this, "onOptionsPressed");
		var resumeGameButton = GetNode(menuOptions + "ResumeGame");
		resumeGameButton.Connect("button_up", this, "onResumeGameButtonUp");
		var restartLevelButton = GetNode(menuOptions + "RestartLevel");
		restartLevelButton.Connect("pressed", this, "onRestartLevelPressed");
        var quitButton = GetNode(menuOptions + "Quit");
        quitButton.Connect("pressed", this, "onQuitPressed");
        optionsMenu = GetNode("OptionsPopup") as OptionsPopUp;
    }
    public override void _Process(float delta)
    {
        if (visible)
        {
            Popup_();
            // Make mouse visible:
            
        }
        if (visible || optionsMenu.visible)
        {
            Input.SetMouseMode((Godot.Input.MouseMode)0);
        }
        else
        {
            Hide();
        }
        // if (optionsVisible)
        // {
        //     var optionsMenu = GetNode("OptionsPopup") as Popup;
        //     visible = false;
        //     optionsMenu.Popup_();
        //     optionsVisible = false;
        // }
    }
    private void onOptionsPressed()
    {
        visible = false;
        optionsMenu.visible = true;
        optionsMenu.Popup_();
    }
    private async void onResumeGameButtonUp()
    {
        GD.Print("Resume game pressed");
        await ToSignal(GetTree().CreateTimer(0.2f), "timeout");
        visible = false;
    }
    private async void onRestartLevelPressed()
    {
        GD.Print("Loading scene " + Helper.startLevel);
        // Load the singleton levelSwitcher:
        var levelSwitcher = GetNode<LevelSwitcher>("/root/LevelSwitcher");
        await ToSignal(GetTree().CreateTimer(0.2f), "timeout");
        // Store the next level and change to the MainEye scene:
        levelSwitcher.ChangeLevel(Helper.toFileName(Helper.mainSceneName), Helper.startLevel);
    }
    private void onQuitPressed()
    {
        GD.Print("Quit pressed, returning to main menu");
        GetTree().ChangeScene("res://MainMenu.tscn");
    }
}
