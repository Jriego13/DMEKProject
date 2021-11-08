using Godot;
using System;

public class MainEye2D : MainGame
{
    // levelName is random by default so it can be loaded without levelSelect
    private String levelName = Helper.getRandomConfirmation();
    private Graft confirmation;

    private TextureProgress bar;
    private RichTextLabel waterLevelCounter;
    private RichTextLabel levelCompletePrompt;
    private RichTextLabel successfulTapPrompt;
    private int waterLevel;


    public override void _Ready()
    {
        // PrintTreePretty();

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
        successfulTapPrompt = GetNode("Overlay/SuccessfulTapPrompt") as RichTextLabel;
        successfulTapPrompt.SetVisible(false);
        levelCompletePrompt = GetNode("Overlay/LevelCompletePrompt") as RichTextLabel;
        levelCompletePrompt.SetVisible(false);
    }
    protected override void OnConfirmationFinished()
    {
        // Add new node to the tree:
        String nextLevel = Helper.getNextConfirmation(levelName);
        loadConfirmation(nextLevel);
    }
    protected override void SetUp()
    {
        
    }
}
