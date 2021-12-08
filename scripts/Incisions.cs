using Godot;
using System;

public class Incisions : Node2D
{
    protected CannulaMain2D cannulas;
    protected MainEye2D eye;
	protected MainEye2DTutorial eyeTutorial; // this one is only used if in tutorial levels
    protected RichTextLabel waterLevelCounter;
    protected TextureProgress bar;
    protected bool flag = false;
    protected int waterLevelInt;
    protected int incisionState = 0;
    protected int incisionNumber = -1;
    protected float waterLevel ;
    protected float lowerBound;
    protected float upperBound;
	protected bool inTutorial = false; 

    public override void _Process(float delta) {

      if((cannulas.getLHeld() || cannulas.getRHeld()) || (cannulas.getLLocked() || cannulas.getRLocked())) {
    		if(incisionState != 0) {
    			if(incisionNumber == 1 ){
    				lowerBound = 2.9f;
    				upperBound = 3.3f;
    			}
    			else if(incisionNumber == 3) {
    				lowerBound = 1.3f;
    				upperBound = 1.8f;
    			}
    			else if(incisionNumber == 5) {
    				lowerBound = 0.1f;
    				upperBound = 0.5f;
    			}
    			else if(incisionNumber == 7) {
    				lowerBound = 1.4f;
    				upperBound = 1.8f;
    			}

    		  if((cannulas.getLCannula().CheckCannulaRotation(lowerBound, upperBound) && (cannulas.getLHeld() || cannulas.getLLocked()) && incisionState !=2)
    			|| (cannulas.getRCannula().CheckCannulaRotation(lowerBound, upperBound) && (cannulas.getRHeld() || cannulas.getRLocked()) && incisionState != 1)) {
      			if(waterLevel > 0) {
      			  waterLevel -= 0.50f;
					if(!inTutorial){
						eye.setWaterLevel(waterLevel);
					}
					else{
						eyeTutorial.setWaterLevel(waterLevel);
					}
      			  bar.Value = waterLevel;
      			  waterLevelInt = (int) waterLevel;
      			  waterLevelCounter.Text = (waterLevelInt.ToString() + "uL");
      			}
    		  }
    		}
  	  }


      if(Input.IsActionPressed("cann_inject")) {
        if(incisionState != 0) {
          if((cannulas.getLCannula().CheckCannulaRotation(lowerBound, upperBound) && incisionState !=2 && cannulas.getLCannula().injecting)
            || (cannulas.getRCannula().CheckCannulaRotation(lowerBound, upperBound) && incisionState != 1 && cannulas.getRCannula().injecting)) {
              if(incisionState == 1) {
                cannulas.getLCannula().Inject();
              }
              else if(incisionState == 2) {
                cannulas.getRCannula().Inject();
              }

              if(waterLevel < 250) {
                waterLevel+= 0.50f;
					if(!inTutorial){
						eye.setWaterLevel(waterLevel);
					}
					else{
						eyeTutorial.setWaterLevel(waterLevel);
					}
                bar.Value = waterLevel;

                int waterLevelInt = (int) waterLevel;
                waterLevelCounter.Text = (waterLevelInt.ToString() + "uL");
              }
          }
        }
      }
    }

    public override void _Ready() {
        waterLevelCounter = GetNode("../UI/NinePatchRect/WaterLevel") as RichTextLabel;
        cannulas = GetNode("../Cannulas") as CannulaMain2D;
		GD.Print(GetTree().GetCurrentScene().GetName());
        eye = GetNode("../") as MainEye2D;

		if(eye == null){
			inTutorial = true;
			eyeTutorial = GetNode("../") as MainEye2DTutorial;
			waterLevel = eyeTutorial.getWaterLevel();
		}
		else{
			waterLevel = eye.getWaterLevel();
		}

    }

  	public void _on_Incision1_area_entered(Area2D area) {
  		incisionNumber = 1;
  		GD.Print("in incision");
  		GD.Print(area.GetName());
  		bar = GetNode("../UI/TextureProgress") as TextureProgress;
  		waterLevel = (float)bar.Value;
		if(!inTutorial){
			eye.setInIncision(true);
		}
		else{
			eyeTutorial.setInIncision(true);
		}

  		int nextState = Helper.getNextHitboxState(area, true, incisionState);
  		if (nextState != -1)
  		  incisionState = nextState;
  	}

  	public void _on_Incision1_area_exited(Area2D area) {
  		incisionNumber = -1;
  		bar = GetNode("../UI/TextureProgress") as TextureProgress;
  		waterLevel = (float)bar.Value;
		if(!inTutorial){
			eye.setInIncision(false);
		}
		else{
			eyeTutorial.setInIncision(false);
		}
  		int nextState = Helper.getNextHitboxState(area, false, incisionState);
  		if (nextState != -1)
  		  incisionState = nextState;
  	}

  	public void _on_Incision3_area_entered(Area2D area){
  		incisionNumber = 3;
  		GD.Print("in incision 3");
  		GD.Print(area.GetName());
  		bar = GetNode("../UI/TextureProgress") as TextureProgress;
  		waterLevel = (float)bar.Value;
  		if(!inTutorial){
			eye.setInIncision(true);
		}
		else{
			eyeTutorial.setInIncision(true);
		}
  		int nextState = Helper.getNextHitboxState(area, true, incisionState);
  		if (nextState != -1)
  		  incisionState = nextState;
  	}

  	public void _on_Incision3_area_exited(Area2D area) {
  		incisionNumber = -1;
  		bar = GetNode("../UI/TextureProgress") as TextureProgress;
  		GD.Print("exiting incision 3");
  		waterLevel = (float)bar.Value;
		if(!inTutorial){
			eye.setInIncision(false);
		}
		else{
			eyeTutorial.setInIncision(false);
		}
  		int nextState = Helper.getNextHitboxState(area, false, incisionState);
  		if (nextState != -1)
  		  incisionState = nextState;
  	}

  	public void _on_Incision5_area_entered(Area2D area){
  		incisionNumber = 5;
  		// Replace with function body.
  		GD.Print("in incision 5");
  		GD.Print(area.GetName());
  		bar = GetNode("../UI/TextureProgress") as TextureProgress;
  		waterLevel = (float)bar.Value;
		if(!inTutorial){
			eye.setInIncision(true);
		}
		else{
			eyeTutorial.setInIncision(true);
		}
  		int nextState = Helper.getNextHitboxState(area, true, incisionState);
  		if (nextState != -1)
  		  incisionState = nextState;
  	}

  	public void _on_Incision5_area_exited(Area2D area) {
  		incisionNumber = -1;
  		bar = GetNode("../UI/TextureProgress") as TextureProgress;
  		GD.Print("exiting incision 5");
  		waterLevel = (float)bar.Value;
		if(!inTutorial){
			eye.setInIncision(false);
		}
		else{
			eyeTutorial.setInIncision(false);
		}
  		int nextState = Helper.getNextHitboxState(area, false, incisionState);
  		if (nextState != -1)
  		  incisionState = nextState;
  	}

  	public void _on_Incision7_area_entered(Area2D area){
  		incisionNumber = 7;
  		GD.Print("in incision 7");
  		GD.Print(area.GetName());
  		bar = GetNode("../UI/TextureProgress") as TextureProgress;
  		waterLevel = (float)bar.Value;
		if(!inTutorial){
			eye.setInIncision(true);
		}
		else{
			eyeTutorial.setInIncision(true);
		}
  		int nextState = Helper.getNextHitboxState(area, true, incisionState);
  		if (nextState != -1)
  		  incisionState = nextState;
  	}

  	public void _on_Incision7_area_exited(Area2D area) {
  		incisionNumber = -1;
  		bar = GetNode("../UI/TextureProgress") as TextureProgress;
  		GD.Print("exiting incision 7");
  		waterLevel = (float)bar.Value;
		if(!inTutorial){
			eye.setInIncision(false);
		}
		else{
			eyeTutorial.setInIncision(false);
		}
  		int nextState = Helper.getNextHitboxState(area, false, incisionState);
  		if (nextState != -1)
  		  incisionState = nextState;
  	}
}
