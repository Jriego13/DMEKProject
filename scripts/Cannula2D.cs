using Godot;
using System;

public class Cannula2D : Sprite {
  public bool inArea = false;
  public bool tapped = false;
  public bool locked = false;
<<<<<<< HEAD
  private Texture circleTexture;
=======
  public bool injecting = false;
>>>>>>> 952c55bf2d2207361841cf844bfcd930390e3060

  public override void _Ready()
  {
    circleTexture = GD.Load("res://images/circle.png") as Texture;
  }
  public void LockCannula() {
    //Sprite currentMat = this.GetActiveMaterial(0) as Sprite;

    if(!locked) { // this means the user wants to hold the cannula in place
      this.Modulate = new Color(1,0,0,1);
      this.SetScale(new Vector2(0.28f, 0.28f));
      locked = true;
    }
    else { // this unlocks the cannula and resets the flag
      this.Modulate = new Color(1,1,1,1);
      this.SetScale(new Vector2(0.3f, 0.3f));
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
  public async void ShowMisclickCircle()
  {
    Sprite misclickCircle = new Sprite();
    misclickCircle.Texture = circleTexture;
    misclickCircle.Scale = new Vector2(0.1f , 0.1f);
    misclickCircle.Modulate = new Color(1, 0 , 0);
    misclickCircle.Position = GlobalPosition;
    GetTree().Root.AddChild(misclickCircle);
    await ToSignal(GetTree().CreateTimer(0.25f), "timeout");
    misclickCircle.QueueFree();
  }
}
