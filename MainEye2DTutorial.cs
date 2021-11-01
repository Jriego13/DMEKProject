using Godot;
using System;

// This class should only contain code that is specific to the tutorial game
// Otherwise, it should go in MainGame.cs
public class MainEye2DTutorial : MainGame
{
    RichTextLabel tutorialPrompt;
    protected override void SetUp()
    {
        GD.Print("here inside maineye2dtut");
        tutorialPrompt = GetNode("Overlay/TutorialPrompt") as RichTextLabel;
        confirmation.misclicksOn = false;
    }    
    protected override void OnConfirmationFinished()
    {
        GetTree().ChangeScene(Helper.toFileName("TutorialSuccessScreen"));
    }
}