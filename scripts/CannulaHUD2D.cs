using Godot;
using System;

public class CannulaHUD2D : RichTextLabel
{
    Cannula2D lCannula; // left cannula
	Cannula2D rCannula; // right cannula
    private float lCannulaRotation;
    private float rCannulaRotation;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        lCannula = GetNode("../Cannulas/CannulaLSprite") as Cannula2D;
		rCannula = GetNode("../Cannulas/CannulaRSprite") as Cannula2D;
        lCannulaRotation = 0;
        rCannulaRotation = 0;
        SetText("Right Cannula Angle: " + rCannulaRotation + "\n" 
                + "Left Cannula Rotation Angle: " + lCannulaRotation);
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        if(Input.IsActionPressed("cann_counterclock")) {
			rCannulaRotation = Math.Abs(rCannula.RotationDegrees);
            lCannulaRotation = Math.Abs(lCannula.RotationDegrees);
		}
		else if(Input.IsActionPressed("cann_clock")) {
			rCannulaRotation = Math.Abs(rCannula.RotationDegrees);
            lCannulaRotation = Math.Abs(lCannula.RotationDegrees);
		}
        else if(Input.IsActionPressed("cann_reset")) {
            rCannulaRotation = Math.Abs(rCannula.RotationDegrees);
            lCannulaRotation = Math.Abs(lCannula.RotationDegrees);
        }

        SetText("Right Cannula Angle: " + rCannulaRotation.ToString("0") + "\n" 
                + "Left Cannula Angle: " + lCannulaRotation.ToString("0"));
    }
}
