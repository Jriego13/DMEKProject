using Godot;
using System;

public class Incisions : Node2D
{
    protected TextureProgress bar;
    protected RichTextLabel waterLevelCounter;
    protected int waterLevel;
    protected CannulaMain2D cannulas;
    protected bool flag = false;

    public override void _Process(float delta){
        if(flag){
            if(cannulas.getLHeld() || cannulas.getRHeld()){
                if(waterLevel >= 0) {
                  waterLevel--;
                }

                bar.Value = waterLevel;
                waterLevelCounter.Text = waterLevel.ToString();
            }
        }
    }

    public override void _Ready()
    {
        // bar = GetNode("../UI/TextureProgress") as TextureProgress;

        waterLevelCounter = GetNode("../UI/NinePatchRect/WaterLevel") as RichTextLabel;
        cannulas = GetNode("../Cannulas") as CannulaMain2D;
    }

    public void _on_Incision1_area_entered(object area){
        bar = GetNode("../UI/TextureProgress") as TextureProgress;
        waterLevel = (int)bar.Value;
        flag = true;
    }

    public void _on_Incision1_area_exited(object area){
        bar = GetNode("../UI/TextureProgress") as TextureProgress;
        waterLevel = (int)bar.Value;
        flag = false;
    }


}
