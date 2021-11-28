using Godot;
using System;

// This is a singleton class whose sole purpose is to pass the parameter
// of the next level name to a new scene.
// Used when first starting the game from level select.
// Singletons: https://docs.godotengine.org/en/stable/getting_started/step_by_step/singletons_autoload.html
public class LevelSwitcher : Node
{
    Boolean isTutorialMode = false;
    String levelName = Helper.getRandomConfirmation();
    Boolean shownWelcomeMessage = false;

    // Loads the lext level by storing the level name and switching to the
    // Main eye scene:
    public void ChangeLevel(String next_scene, String levelName)
    {
        if (next_scene == "res://MainEye2DTutorial.tscn"
            || next_scene == "res://TutorialWelcomeScreen.tscn") {
            isTutorialMode = true;
        }
        else { 
            isTutorialMode = false;
        }
        this.levelName = levelName;
        GetTree().ChangeScene(next_scene);
    }
    // Returns the fold or level name
    public String getLevelName()
    {
        return levelName;
    }

    public Boolean tutorialMode() {
        return isTutorialMode;
    }

    public Boolean welcomeMessage() {
        return shownWelcomeMessage;
    }

    public void setWelcomeMessage(Boolean shownWelcomeMessage) {
        this.shownWelcomeMessage = shownWelcomeMessage;
    }
}
