using Godot;
using System;

public class Options : MarginContainer
{
    private bool visible = false;
    private HSlider musicSlider;
    private HSlider effectsSlider;
    private AudioStreamPlayer music;
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
        musicSlider = GetNode("MarginContainer/VBoxContainer/SliderVBox/MusicSliderHBox/MusicSlider") as HSlider;
        musicSlider.Connect("value_changed", this, "onMusicVolumeChanged");
        effectsSlider = GetNode("MarginContainer/VBoxContainer/SliderVBox/EffectsSliderHBox/EffectsSlider") as HSlider;
        effectsSlider.Connect("value_changed", this, "onEffectsVolumeChanged");
        music = GetNode("/root/Music") as AudioStreamPlayer;
        musicSlider.Value = Helper.musicVolumeDb;
        effectsSlider.Value = Helper.soundEffectsVolumeDb;
    }

    // Navigates back to the main menu:
    private void onGoBackButtonPressed()
    {
        GD.Print("Go back pressed");
		this.QueueFree();
    }
    private void onMusicVolumeChanged(float value)
    {
        var music = GetNode("/root/Music") as AudioStreamPlayer;
        GD.Print(value);
        music.VolumeDb = value; 
        Helper.musicVolumeDb = value;
    }
    private void onEffectsVolumeChanged(float value)
    {
        Helper.soundEffectsVolumeDb = value;
    }
}
