using Godot;
using System;

// This class should only contain code that is specific to the non-tutorial game.
// Otherwise, it should go in MainGame.cs
public class MainEye2D : MainGame
{
    // levelName is random by default so it can be loaded without levelSelect

    protected override void OnConfirmationFinished()
    {
            // Delete the current node:
            confirmation.QueueFree();
            // Add new node to the tree:
            String nextLevel = Helper.getNextConfirmation(levelName);
            loadConfirmation(nextLevel);
    }
}
