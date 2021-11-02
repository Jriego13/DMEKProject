using Godot;
using System;

public class MainEye2D : Node2D
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
    public override void _Process(float delta)
    {
        base._Process(delta);
        // If confirmation is complete, load the next confirmation:
        if (confirmation.getIsFinished())
        {
            // Delete the current node:
            confirmation.QueueFree();
            // Add new node to the tree:
            String nextLevel;
            if(confirmation.getIsNextLevelSet()) {
              nextLevel = Helper.setNextConfirmation(confirmation.getNextConfirmation());
            }
            else {
              nextLevel = Helper.getNextConfirmation(levelName);
            }
            loadConfirmation(nextLevel);
        }

        if(Input.IsActionPressed("addLiquid")){
            GD.Print("trying to add liquid");
            if(waterLevel <= 99){
               waterLevel += 1;
               bar.Value = waterLevel;
               waterLevelCounter.Text = waterLevel.ToString();
            }

        }
        if(Input.IsActionPressed("removeLiquid")){
            GD.Print("trying to remove liquid");
            if(waterLevel >= 1){
                waterLevel -= 1;
                bar.Value = waterLevel;
                waterLevelCounter.Text = waterLevel.ToString();

            }

        }
    }

    // Load the specified level/fold, instance it as a Node2D, then place it in the tree:
    private void loadConfirmation(String next)
    {
        GD.Print(next);
        if(next == "res://Done.tscn") {
          GetTree().ChangeScene("res://Done.tscn");
          return;
        }

        levelName = next;
        GD.Print("instancing "+ levelName);
        var confirmationScene = GD.Load<PackedScene>(levelName);
        confirmation = (Graft)confirmationScene.Instance();
        GetNode("/root/Main").AddChild(confirmation);
    }
}
