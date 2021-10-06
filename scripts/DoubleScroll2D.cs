using Godot;
using System;

public class DoubleScroll2D : Graft {
  Cannula2D lCannula;
  Cannula2D rCannula;
  PackedScene scene1;
  PackedScene scene2;
  bool areaEntered = false;

  public override void _Ready() {
    numTapsComplete = rng.Next(3,6);
    lCannula = GetNode("../Cannulas/CannulaLSprite") as Cannula2D;
    rCannula = GetNode("../Cannulas/CannulaRSprite") as Cannula2D;
    circleTexture = GD.Load("res://images/circle.png") as Texture;
    GD.Print("you have to tap the bottom " + numTapsComplete + " times!");

  }

  public async override void _Process(float delta) {
    if(numTaps >= numTapsComplete) {
      numTaps = 0;
      GD.Print("tapping complete.");
      isFinished = true;
    }

    if(areaEntered) {
      if(lCannula.tapped || rCannula.tapped) {
        numTaps += 1;
        lCannula.tapped = false;
        rCannula.tapped = false;
        GD.Print("tap registered");
      }
    }

    if(!areaEntered){
			if(rCannula.tapped || lCannula.tapped){
				if(numTapsWrong < 3){
					GD.Print("You clicked outside of the correct areas");
					Vector2 mousePos = GetViewport().GetMousePosition();
					GD.Print(mousePos);
					Sprite misclickCircle = new Sprite();
					misclickCircle.Texture = circleTexture;
					misclickCircle.Scale = new Vector2(0.1f , 0.1f);
					misclickCircle.Position = mousePos;
					misclickCircle.Modulate = new Color(1, 0 , 0);

					GetTree().GetRoot().AddChild(misclickCircle);
					await ToSignal(GetTree().CreateTimer(0.25f), "timeout");
					misclickCircle.QueueFree();
					numTapsWrong++;
				}
				else{
					GD.Print("Misclicked too many times. You fail!");
					GetTree().ChangeScene("res://FailScreen.tscn");

				}
			}
		}
  }

  private void _OnAreaEntered(object area) {
    areaEntered = true;
  }

  private void _OnAreaExited(object area) {
    areaEntered = false;
  }
}
