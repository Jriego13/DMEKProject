using Godot;
using System;

public class Incisions : Node2D
{
    protected TextureProgress bar;
    protected RichTextLabel waterLevelCounter;
    protected float waterLevel;
    protected CannulaMain2D cannulas;
    protected bool flag = false;
    protected MainEye2D eye;

    public override void _Process(float delta){
        if(flag){
            if(  (cannulas.getLHeld() || cannulas.getRHeld()) || (cannulas.getLLocked() || cannulas.getRLocked())  ){
                // holding left cannula will remove liquid

                if(waterLevel > 0 ){
                // water level can't go below zero
                waterLevel-= 0.25f;
                eye.setWaterLevel(waterLevel);
                bar.Value = waterLevel;
                waterLevelCounter.Text = waterLevel.ToString();
                }
            }

            if(Input.IsActionPressed("cann_inject")){
                // holding right cannula will add liquid

                if(waterLevel < 100 ){
                // water level can't go below zero
                waterLevel+= 0.25f;
                eye.setWaterLevel(waterLevel);
                bar.Value = waterLevel;
                waterLevelCounter.Text = waterLevel.ToString();
                }
            }
        }
    }

    public override void _Ready()
    {
        // bar = GetNode("../UI/TextureProgress") as TextureProgress;
        waterLevelCounter = GetNode("../UI/NinePatchRect/WaterLevel") as RichTextLabel;
        cannulas = GetNode("../Cannulas") as CannulaMain2D;
        eye = GetNode("../") as MainEye2D;
        waterLevel = eye.getWaterLevel();
    }

    public void _on_Incision1_area_entered(object area){
        bar = GetNode("../UI/TextureProgress") as TextureProgress;
        waterLevel = (float)bar.Value;
        flag = true;
        // set a flag in the main eye that doesnt allow for misclicks
        eye.setInIncision(true);
    }

    public void _on_Incision1_area_exited(object area){
        bar = GetNode("../UI/TextureProgress") as TextureProgress;
        waterLevel = (float)bar.Value;
        flag = false;
        eye.setInIncision(false);
    }


}
