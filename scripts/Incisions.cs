using Godot;
using System;

public class Incisions : Node2D
{
    protected CannulaMain2D cannulas;
    protected MainEye2D eye;
    protected RichTextLabel waterLevelCounter;
    protected TextureProgress bar;
    protected bool flag = false;
    protected float waterLevel;
    protected int incisionState = 0;

    public override void _Process(float delta) {

      if((cannulas.getLHeld() || cannulas.getRHeld()) || (cannulas.getLLocked() || cannulas.getRLocked())) {
        if(incisionState != 0) {
          GD.Print(incisionState);
          if((cannulas.getLCannula().CheckCannulaRotation(1.3f, 1.9f) && (cannulas.getLHeld() || cannulas.getLLocked()) && incisionState !=2)
            || (cannulas.getRCannula().CheckCannulaRotation(1.3f, 1.9f) && (cannulas.getRHeld() || cannulas.getRLocked()) && incisionState != 1)) {
            if(waterLevel > 0) {
              waterLevel -= 0.50f;
              eye.setWaterLevel(waterLevel);
              bar.Value = waterLevel;

              int waterLevelInt = (int) waterLevel;
              waterLevelCounter.Text = (waterLevelInt.ToString() + "uL");
            }
          }
        }
      }

      if(Input.IsActionPressed("cann_inject")) {
        if(incisionState != 0) {
          if((cannulas.getLCannula().CheckCannulaRotation(1.3f, 1.9f) && incisionState !=2)
            || (cannulas.getRCannula().CheckCannulaRotation(1.3f, 1.9f) && incisionState != 1)) {
            if(waterLevel < 250) {
              waterLevel+= 0.50f;
              eye.setWaterLevel(waterLevel);
              bar.Value = waterLevel;
              waterLevelCounter.Text = waterLevel.ToString();
            }
          }
        }
      }
    }

    public override void _Ready() {
        // bar = GetNode("../UI/TextureProgress") as TextureProgress;
        waterLevelCounter = GetNode("../UI/NinePatchRect/WaterLevel") as RichTextLabel;
        cannulas = GetNode("../Cannulas") as CannulaMain2D;
        eye = GetNode("../") as MainEye2D;
        waterLevel = eye.getWaterLevel();
    }

    // we need signals for each incision
    public void _on_Incision1_area_entered(Area2D area) {
        GD.Print(area.GetName());
        bar = GetNode("../UI/TextureProgress") as TextureProgress;
        waterLevel = (float)bar.Value;
        eye.setInIncision(true);
        int nextState = Helper.getNextHitboxState(area, true, incisionState);
        if (nextState != -1)
          incisionState = nextState;
    }

    public void _on_Incision1_area_exited(Area2D area) {
        bar = GetNode("../UI/TextureProgress") as TextureProgress;
        waterLevel = (float)bar.Value;
        eye.setInIncision(false);
        int nextState = Helper.getNextHitboxState(area, false, incisionState);
        if (nextState != -1)
          incisionState = nextState;
    }
}
