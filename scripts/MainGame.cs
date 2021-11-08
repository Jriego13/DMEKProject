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
    protected RichTextLabel waterLevelCounter;
    protected int waterLevel;
    protected EscapeMenu escapeMenu;
    protected RichTextLabel successfulTapPrompt;
    protected RichTextLabel levelCompletePrompt;
   
    public override void _Input(InputEvent @event)
    {
        base._Input(@event);
        if (@event.IsActionPressed("ui_cancel"))
        {
            GD.Print("escape pressed");
            escapeMenu.visible = !escapeMenu.visible;
        }
        if(@event.IsActionPressed("addLiquid")){
            GD.Print("trying to add liquid");
            if(waterLevel <= 99){
               waterLevel += 1;
               bar.Value = waterLevel;
               waterLevelCounter.Text = waterLevel.ToString();
            }
        }
        if(@event.IsActionPressed("removeLiquid")){
            GD.Print("trying to remove liquid");
            if(waterLevel >= 1){
                waterLevel -= 1;
                bar.Value = waterLevel;
                waterLevelCounter.Text = waterLevel.ToString();
            }
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
        waterLevel = 100;
        bar.Value = waterLevel;

        escapeMenu = GetNode("MenuPopup") as EscapeMenu;
        successfulTapPrompt = GetNode("Overlay/SuccessfulTapPrompt") as RichTextLabel;
        successfulTapPrompt.Visible = false;
        levelCompletePrompt = GetNode("Overlay/LevelCompletePrompt") as RichTextLabel;
        levelCompletePrompt.Visible = false;
        SetUp();
    }
    // What happens when the confirmation is finished in the respective game mode:
    protected virtual void OnConfirmationFinished(){}
    // Set up everything needed specifically for tutorial or non-tutorial:
    protected virtual void SetUp(){}
    protected virtual void CheckObjectives(){}
    public override void _Process(float delta)
    {
        CheckObjectives();
        // Check if objective complete:
        if (confirmation.getIsFinished())
        {
            // Add new node to the tree:
            OnConfirmationFinished();
        }
        else
            confirmation.gamePaused = (escapeMenu.visible || escapeMenu.optionsVisible);
    }

    // Load the specified level/fold, instance it as a Node2D, then place it in the tree:
    protected void loadConfirmation(String next)
    {
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
}
