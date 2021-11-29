using Godot;
using System;

public class Cannula2D : Sprite {
  // the number of hitboxes the cannula is in. If it's 0, and they tap, it's a misclick.
  public int numAreasIn = 0;
  public bool tapped = false;
  public bool locked = false;
  public bool injecting = false;
  private Texture circleTexture;

  public override void _Ready()
  {
    circleTexture = GD.Load("res://images/circle.png") as Texture;
  }

  public void LockCannula() {
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

  public bool CheckCannulaRotation(float lowerBound, float upperBound) {
    if(GetRotation() >= lowerBound && GetRotation() <= upperBound)
      return true;

    return false;
  }

  public void Inject() {
    if(injecting) {
      this.Modulate = new Color(0,1,0,1);
      this.SetScale(new Vector2(0.28f, 0.28f));
    }
    else {
      this.injecting = false;
      this.Modulate = new Color(1,1,1,1);
      this.SetScale(new Vector2(0.3f, 0.3f));
    }
  }

  // check rotation function where we calculate the cannulas rotation relative to the graft
  // and check whether its between the bound arguments.
  public bool CheckCannulaRotationRelative(float graftRotation, float lowerBound, float upperBound) {
    float calculated_rotation = Math.Abs((Math.Abs(graftRotation) - Math.Abs(this.GetRotation())));
    // GD.Print(calculated_rotation);
    if(calculated_rotation >= lowerBound && calculated_rotation <= upperBound)
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
