using Godot;
using System;

public class Cannula2D : Sprite {
  public bool tapped = false;
  public bool locked = false;

  public void LockCannula() {
    //Sprite currentMat = this.GetActiveMaterial(0) as Sprite;

    if(!locked) { // this means the user wants to hold the cannula in place
      //currentMat.SetAlbedo(new Color(1,0,0,1));
      locked = true;
    }
    else { // this unlocks the cannula and resets the flag
      //currentMat.SetAlbedo(new Color(0,1,0,1));
      locked = false;
    }
    GD.Print("you locked the " + this.GetName() + ".");
  }

  // check rotation function where range can be fed as argument? and we return a bool?
  public bool CheckCannulaRotation(float lowerBound, float upperBound) {
    if(Math.Abs(this.GetRotation()) >= lowerBound && Math.Abs(this.GetRotation()) <= upperBound)
      return true;

    return false;
  }
}
