using Godot;
using System;

public class TutorialSelect : LevelMenu
{
    public override void loadLevel(String sceneName)
    {
        GD.Print("Loading scene " + sceneName);
        // Load the singleton levelSwitcher:
        var levelSwitcher = GetNode<LevelSwitcher>("/root/LevelSwitcher");
        // Store the next level and change to the MainEye scene:
        levelSwitcher.ChangeLevel(Helper.toFileName(Helper.tutorialSceneName), Helper.toFileName(sceneName));
    }
}
