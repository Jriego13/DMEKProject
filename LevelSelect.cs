using Godot;
using System;

// This class gains all of its functionality from LevelMenu.cs, but is remaining in case 
// non-tutorial specific logic is needed in the future.
public class LevelSelect : LevelMenu
{
    public override void _Ready()
    {
        var goBackButton = GetNode("MarginContainer/VBoxContainer2/HBoxContainer2/GoBackButton");
        goBackButton.Connect("pressed", this, "onGoBackButtonPressed");
        
        // Create level select button variables:
        String levelHBoxPath = "MarginContainer/VBoxContainer2/levelHBox";
        var simpleFoldButton = GetNode(levelHBoxPath + "/Levels1and4/Level1/SimpleFold");
        var invertedButton = GetNode(levelHBoxPath + "/Levels1and4/Level4/Inverted");
        var scrollButton = GetNode(levelHBoxPath + "/Levels2and5/Level2/Scroll");
        var tacoButton = GetNode(levelHBoxPath + "/Levels2and5/Level5/Taco");
        var doubleScrollButton = GetNode(levelHBoxPath + "/Levels3and6/Level3/DoubleScroll");
        var edgeFoldButton = GetNode(levelHBoxPath + "/Levels3and6/Level6/EdgeFold");

}
}