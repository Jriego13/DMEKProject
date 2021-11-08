using Godot;
using System;

public class InfoScreenPrompt : RichTextLabel
{
    private int enterCount;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
        enterCount = 0;
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        if (Input.IsActionJustPressed("continue")) {
            enterCount++;
        }
      if (enterCount == 0) {
          SetText("Welcome to our DMEK Simulation Game \n\n" + 
                            "Descemet Membrane Edothelial Keratoplasty (DMEK) Surgery " +
                            "is the most advanced form of partial corneal transplant " +
                            "procedure for diseases affecting the innermost endothelial " +
                            "layer of the cornea. \n\nOnly a single thing layer of cells are replaced" +
                            " which allows for an exact replacement of the damaged cells\n\n" + 
                            "Press enter to read more!");
      }
      if (enterCount == 1) {
          SetText("The point of the game is to simulate when the graft to be transplanted " +
          "has already been inserted into the patient's eye. \n\nThe graft will tend to fold up into certain " +
          "common configurations as demonstrated in the figure on the right. \n\nThe game will train you on how to use the tap of the cannulas and " +
          "the insertion and extraction of liquid into/from the eye to make the graft lie flat.");
      }
    }   
}
