using Godot;
using System;

// This is a parent class for LevelSelect and TutorialSelect as they are very similar.
// Any shared functionality between the two menus goes here.
public class LevelMenu : MarginContainer
{
    AudioStreamPlayer hoverSound;
    AudioStreamPlayer clickSound;
    public override void _Ready()
    {
        var goBackButton = GetNode("MarginContainer/VBoxContainer2/HBoxContainer2/GoBackButton");
        goBackButton.Connect("pressed", this, "onGoBackButtonPressed");
        // Create level select button variables:
        String levelHBoxPath = "MarginContainer/VBoxContainer2/levelHBox";
        var simpleFoldButton = GetNode(levelHBoxPath + "/Levels1and4/Level1/SimpleFold");
        var invertedButton = GetNode(levelHBoxPath + "/Levels1and4/Level4/Inverted");
        var scrollButton = GetNode(levelHBoxPath + "/Levels2and5/Level2/Scroll");
        var tacoButton = GetNode(levelHBoxPath + "/Levels2and5/Level5/Taco");
        var doubleScrollButton = GetNode(levelHBoxPath + "/Levels3and6/Level3/DoubleScroll");
        var edgeFoldButton = GetNode(levelHBoxPath + "/Levels3and6/Level6/EdgeFold");
        var bouquetButton = GetNode(levelHBoxPath + "/Level7/Level7/Bouquet");

        SetUpSound();
        // Create array of the buttons so we can loop over all of them:
        var buttons = new[] {simpleFoldButton, invertedButton, scrollButton, tacoButton, doubleScrollButton, edgeFoldButton, bouquetButton};

        // Connect each level select button to its corresponding scene:
        foreach (var button in buttons)
        {
            button.Connect("pressed", this, "loadLevel", new Godot.Collections.Array {button.Name});
            button.Connect("mouse_entered", this, "onMouseEnteredButton");
        }
        
    }
    public override void _Input(InputEvent @event)
    {
        base._Input(@event);
        if (@event.IsActionPressed("ui_cancel")) {
            GetTree().ChangeScene(Helper.toFileName("MainMenu"));
        }
    }

    // Navigates back to the main menu:
    private void onGoBackButtonPressed()
    {
        GD.Print("Go back pressed");
		GetTree().ChangeScene(Helper.toFileName("MainMenu"));
    }

    // Loads the selected level:
    public virtual void loadLevel(String sceneName)
    {
        GD.Print("Loading scene " + sceneName);
        // Load the singleton levelSwitcher:
        var levelSwitcher = GetNode<LevelSwitcher>("/root/LevelSwitcher");
        // Store the next level and change to the MainEye scene:
        levelSwitcher.ChangeLevel(Helper.toFileName(Helper.mainSceneName), Helper.toFileName(sceneName));
    }
    // Play a sound when the player hovers over a button:
	private void onMouseEnteredButton()
	{
		hoverSound.Play();
	}
    private void SetUpSound()
    {
        hoverSound = GetNode("HoverSound") as AudioStreamPlayer;
        clickSound = GetNode("ClickSound") as AudioStreamPlayer;
        hoverSound.VolumeDb = Helper.soundEffectsVolumeDb;
		clickSound.VolumeDb = Helper.soundEffectsVolumeDb;
        clickSound.Play(); // player just clicked into this scene, so play sound
    }
}
