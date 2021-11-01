using Godot;
using System;

// This class should only contain code that is specific to the non-tutorial game
// Otherwise, it should go in MainGame.cs
public class MainEye2D : MainGame
{
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
