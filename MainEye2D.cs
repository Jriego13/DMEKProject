using Godot;
using System;

public class MainEye2D : Node2D
{
    String levelName = "";
    Graft confirmation;
    // TODO: put these in a global constants class:
    String scenePrefix = "res://";
    String sceneSuffix = ".tscn";
    public override void _Ready()
    {
        // Load the singleton:
        var levelSwitcher = GetNode<LevelSwitcher>("/root/LevelSwitcher");
        // Get the levelName from the levelSwitcher:
        levelName = levelSwitcher.getLevelName();
        
        
        loadConfirmation(levelName);
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
            String nextLevel = getNextConfirmation(levelName);
            loadConfirmation(nextLevel);
        }
    }
    // Based on the input current confirmation, returns a new confirmation:
    // Helper function that doesn't necessarily have to be in this class
    private String getNextConfirmation(String current)
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
        nextLevel = scenePrefix + nextLevel + sceneSuffix;
        return nextLevel;
    }
    // Load the specified level/fold, instance it as a Node2D, then place it in the tree:
    private void loadConfirmation(String next)
    {
        levelName = next;
        GD.Print("instancing "+ levelName);
        var confirmationScene = GD.Load<PackedScene>(levelName);
        confirmation = (Graft)confirmationScene.Instance();
        GetNode("/root/Main").AddChild(confirmation);
    }
}
