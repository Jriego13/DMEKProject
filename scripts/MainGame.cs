using Godot;
using System;
// This class should contain code that is applicable to both
// the tutorial and non-tutorial game modes.
public class MainGame : Node2D
{
    // levelName is random by default so it can be loaded without levelSelect
    protected String levelName = Helper.getRandomConfirmation();
    protected Graft confirmation;
    protected TextureProgress bar;
    protected EscapeMenu escapeMenu;
    protected RichTextLabel successfulTapPrompt;
    protected RichTextLabel levelCompletePrompt;
    protected RichTextLabel waterLevelCounter;
    protected float waterLevel = 250.0f;
    protected bool inIncision = false;

    public override void _Input(InputEvent @event)
    {
        base._Input(@event);
        if (@event.IsActionPressed("ui_cancel"))
        {
            GD.Print("escape pressed");
            escapeMenu.visible = !escapeMenu.visible;
        }
    }

    public override void _Ready()
    {
        // Load the singleton:
        var levelSwitcher = GetNode<LevelSwitcher>("/root/LevelSwitcher");
        // Get the levelName from the levelSwitcher:
        levelName = levelSwitcher.getLevelName();
        Helper.startLevel = levelName;
        loadConfirmation(levelName);

        // _tween = GetNode("UI/Tween") as Tween;
        bar = GetNode("UI/TextureProgress") as TextureProgress;

        waterLevelCounter = GetNode("UI/NinePatchRect/WaterLevel") as RichTextLabel;
        // waterLevel = 100.0f;
        bar.Value = this.waterLevel;
        bar.RectRotation = -90;

        escapeMenu = GetNode("MenuPopup") as EscapeMenu;
        successfulTapPrompt = GetNode("Overlay/SuccessfulTapPrompt") as RichTextLabel;
        successfulTapPrompt.Visible = false;
        levelCompletePrompt = GetNode("Overlay/LevelCompletePrompt") as RichTextLabel;
        levelCompletePrompt.Visible = false;
        SetUp();
        var music = GetNode("/root/Music") as AudioStreamPlayer;
        music.Stream = ResourceLoader.Load("res://music/MainGameMusic.wav") as AudioStream;
        music.VolumeDb = Helper.musicVolumeDb;
        music.Play();
    }

    // What happens when the confirmation is finished in the respective game mode:
    protected virtual void OnConfirmationFinished(){}

    // Set up everything needed specifically for tutorial or non-tutorial:
    protected virtual void SetUp(){}
    protected virtual void CheckObjectives(float delta){}

    public override void _Process(float delta) {
        CheckObjectives(delta);
        // Check if objective complete:
        if (confirmation.getIsFinished()) {
            // Add new node to the tree:
            OnConfirmationFinished();
        }
        else {
            confirmation.gamePaused = (escapeMenu.visible || escapeMenu.optionsVisible);
            confirmation.lCannula.Visible = !(escapeMenu.visible || escapeMenu.optionsVisible);
            confirmation.rCannula.Visible = !(escapeMenu.visible || escapeMenu.optionsVisible);
        }
    }

    // Load the specified level/fold, instance it as a Node2D, then place it in the tree:
    protected void loadConfirmation(String next) {
        levelName = next;
        GD.Print("instancing "+ levelName);
        var confirmationScene = GD.Load<PackedScene>(levelName);
        if (levelName == "res://Done.tscn")
        {
            GetTree().ChangeScene("res://Done.tscn");
            return;
        }
        confirmation = (Graft)confirmationScene.Instance();
        GetNode("/root/Main").AddChild(confirmation);
    }

    public float getWaterLevel() {
      return this.waterLevel;
    }

    public void setWaterLevel(float level){
        this.waterLevel = level;
    }

    public bool getInIncision(){
        return this.inIncision;
    }
    public void setInIncision(bool value){
        this.inIncision = value;
    }
}
