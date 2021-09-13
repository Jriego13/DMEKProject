using Godot;
using System;

public class Cannula : MeshInstance {
  public bool tapped = false;
  public bool locked = false;

  public void LockCannula() {
    //MeshInstance lCannulaMesh = GetChild(0) as MeshInstance;
    //SpatialMaterial currentMat = lCannulaMesh.GetActiveMaterial(0) as SpatialMaterial;
    SpatialMaterial currentMat = this.GetActiveMaterial(0) as SpatialMaterial;

    if(!locked) { // this means the user wants to hold the cannula in place
      currentMat.SetAlbedo(new Color(1,0,0,1));
      locked = true;
    }
    else { // this unlocks the cannula and resets the flag
      currentMat.SetAlbedo(new Color(0,1,0,1));
      locked = false;
    }
  }

  // check rotation function where range can be fed as argument? and we return a bool?
  public bool CheckCannulaRotation(float lowerBound, float upperBound) {
    if(Math.Abs(this.GetRotation().y) >= lowerBound && Math.Abs(this.GetRotation().y) <= upperBound)
      return true;

    return false;
  }
}
