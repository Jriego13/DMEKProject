using System;

// Static class for things like constants and helper functions:
public static class Helper
{
    public const String scenePrefix = "res://";
    public const String sceneSuffix = ".tscn";
    public const String mainSceneName = "MainEye2D";

    // Adds the scene prefix and suffix to a name:
    public static String toFileName(String name)
    {
        return scenePrefix + name + sceneSuffix;
    }
    // Based on the input current confirmation, returns a new confirmation:
    public static String getNextConfirmation(String current)
    {
        var rand = new Random();
        var randomNumber = rand.Next() % 100;
        String nextLevel = "";
        // Chosen next confirmations are based on the powerpoint
        // Actual percentage chances are made up
        if (current.Contains("Scroll"))
        {
            if (randomNumber < 50)
                nextLevel = "SimpleFold";
            else if (randomNumber < 75)
                nextLevel = "Bouquet";
            else
                nextLevel = "DoubleScroll";
        }
        else if (current.Contains("DoubleScroll"))
        {
            if (randomNumber < 75)
                nextLevel = "SimpleFold";
            else
                nextLevel = "Bouquet";
        }
        else if (current.Contains("SimpleFold"))
        {
            nextLevel = "EdgeFold";
        }
        else if (current.Contains("Inverted"))
        {
            if (randomNumber < 50)
                nextLevel = "Scroll";
            else
                nextLevel = "SimpleFold";
        }
        else if (current.Contains("Taco"))
        {
            if (randomNumber < 33)
            {
                nextLevel = "SimpleFold";
            }
            else if (randomNumber < 66)
            {
                nextLevel = "Scroll";
            }
            else if (randomNumber < 100)
            {
                nextLevel = "Origami";
            }
        }
        else if (current.Contains("EdgeFold"))
        {
            nextLevel = "Done"; // Need special behavior for "Done"
        }
        else if (current.Contains("Bouquet"))
        {
            if (randomNumber < 50)
                nextLevel = "SimpleFold";
            else
                nextLevel = "EdgeFold";
        }
        else if (current.Contains("Origami"))
        {
            nextLevel = "Scroll";
        }
        nextLevel = toFileName(nextLevel);
        return nextLevel;
    }
}