using System;
using Godot;

// Static class for things like constants and helper functions that don't
// need to be in any particular class:
public static class Helper
{
    public const String scenePrefix = "res://";
    public const String sceneSuffix = ".tscn";
    public const String mainSceneName = "MainEye2D";
    public const String tutorialSceneName = "MainEye2DTutorial";
    public static float musicVolumeDb = -10;
    public static float soundEffectsVolumeDb = -10;


    public static String[] confirmations = {"Scroll", "DoubleScroll", "SimpleFold",
    "Inverted", "Taco", "EdgeFold", "Bouquet", "Origami"};
    public static Random rand = new Random();
    // The level the game started on.
    public static String startLevel = "";
    // Adds the scene prefix and suffix to a name:
    public static String toFileName(String name)
    {
        return scenePrefix + name + sceneSuffix;
    }
    // Useful for starting the game without level select:
    public static String getRandomConfirmation()
    {
        return toFileName(confirmations[rand.Next() % confirmations.Length]);
    }
    // Based on the input current confirmation, returns a new confirmation:
    public static String getNextConfirmation(String current)
    {
        GD.Print("HHHHH " + current);
        var randomNumber = rand.Next() % 100;
        String nextLevel = "";
        // Chosen next confirmations are based on the powerpoint
        // Actual percentage chances are made up
        if (current.Equals("res://Scroll.tscn"))
        {
            if (randomNumber < 50)
                nextLevel = "SimpleFold";
            else if (randomNumber < 75)
                nextLevel = "Bouquet";
            else
                nextLevel = "DoubleScroll";
        }
        else if (current.Equals("res://DoubleScroll.tscn"))
        {
            GD.Print("Double scroll");
            if (randomNumber < 95)
                nextLevel = "SimpleFold";
            else
                nextLevel = "Bouquet";
        }
        else if (current.Equals("res://SimpleFold.tscn"))
        {
            nextLevel = "EdgeFold";
        }
        else if (current.Equals("res://Inverted.tscn"))
        {
            if (randomNumber < 50)
                nextLevel = "Scroll";
            else
                nextLevel = "SimpleFold";
        }
        else if (current.Equals("res://Taco.tscn"))
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
        else if (current.Equals("res://EdgeFold.tscn"))
        {
            nextLevel = "Done"; // Need special behavior for "Done"
        }
        else if (current.Equals("res://Bouquet.tscn"))
        {
            if (randomNumber < 50)
                nextLevel = "SimpleFold";
            else
                nextLevel = "EdgeFold";
        }
        else if (current.Equals("res://Origami.tscn"))
        {
            nextLevel = "Scroll";
        }

        nextLevel = toFileName(nextLevel);
        return nextLevel;
    }

    public static String setNextConfirmation(String nextLevel) {
      nextLevel = toFileName(nextLevel);
      return nextLevel;
    }

     public static String getNextTutorialConfirmation(String current) {
        String nextLevel = "";
        if (current.Equals("res://Scroll.tscn"))
        {
                nextLevel = "DoubleScroll";
        }
        else if (current.Equals("res://DoubleScroll.tscn"))
        {
                nextLevel = "SimpleFold";
        }
        else if (current.Equals("res://SimpleFold.tscn"))
        {
            nextLevel = "EdgeFold";
        }
        else if (current.Equals("res://EdgeFold.tscn"))
        {
            nextLevel = "Inverted";
        }
        nextLevel = toFileName(nextLevel);
        return nextLevel;
    }
    // Gets the next hitbox state based on what cannula just entered/exited and the current state
	public static int getNextHitboxState(Area2D area, bool entered, int currentState)
	{
		Cannula2D currentCannula = area.GetParent() as Cannula2D;
		if (currentCannula == null)
			return -1;
		if (entered)
		{
            currentCannula.numAreasIn += 1;
			if (area.Name == "CannulaL")
			{
				// no cannulas in area-> only left in area
				if (currentState == 0)
					return 1;
				// only right in area -> both
				else if (currentState == 2)
					return 3;
			}
			else if (area.Name == "CannulaR")
			{
				// no cannulas in area-> only right in area
				if (currentState == 0)
					return 2;
				// only left in area -> both
				else if (currentState == 1)
					return 3;
			}
		}
		// when cannula leaves area
		else if (!entered)
		{
            currentCannula.numAreasIn -=1;
			if (area.Name == "CannulaL")
			{
				// only left in area -> none
				if (currentState == 1)
					return 0;
				// both -> only right
				else if (currentState == 3)
					return 2;
			}
			else if (area.Name == "CannulaR")
			{
				// only right -> none
				if (currentState == 2)
					return 0;
				// both -> only left
				else if (currentState == 3)
					return 1;
			}
		}
        return -1;
	}
}
