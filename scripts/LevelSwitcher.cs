using Godot;
using System;

// This is a singleton class whose sole purpose is to pass the parameter
// of the next level name to a new scene.
// Used when first starting the game from level select.
// Singletons: https://docs.godotengine.org/en/stable/getting_started/step_by_step/singletons_autoload.html
public class LevelSwitcher : Node
{
    String levelName = "";

    // Loads the lext level by storing the level name and switching to the
    // Main eye scene:
    public void ChangeLevel(String next_scene, String levelName)
    {
        this.levelName = levelName;
        GetTree().ChangeScene(next_scene);
    }
    // Returns the fold or level name
    public String getLevelName()
    {
        return levelName;
    }
}