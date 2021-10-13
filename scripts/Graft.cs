using Godot;
using System;
using System.Collections.Generic;

// base graft class that I would like to use for all derivative grafts
public class Graft : Sprite {
  protected List<PackedScene> nextConfirmations = new List<PackedScene>();
  protected Random rng = new Random();
  protected int numTaps;
  protected int numTapsComplete;
  protected bool isFinished;
  protected int numTapsWrong;
  protected Texture circleTexture;
  protected String previousConfirmation;
  protected Cannula2D lCannula;
	protected Cannula2D rCannula;
	protected bool isTutorialMode;

  public override void _Process(float delta)
  {
    CheckObjectives();
  }
  public override void _Ready()
  {
    SetObjectives();
    lCannula = GetNode("../Cannulas/CannulaLSprite") as Cannula2D;
    rCannula = GetNode("../Cannulas/CannulaRSprite") as Cannula2D;
	  circleTexture = GD.Load("res://images/circle.png") as Texture;
  	var levelSwitcher = GetNode<LevelSwitcher>("/root/LevelSwitcher");
  	isTutorialMode = levelSwitcher.tutorialMode();
  }
  // This is where each graft will check for their specific objectives.
  // This separation allows for a universal _Process function.
  protected virtual void CheckObjectives()
  {

  }
  // This is where each graft sets up their specific objectives.
  // This separation allows for a universal _Ready function.
  protected virtual void SetObjectives()
  {
      
  	var levelSwitcher = GetNode<LevelSwitcher>("/root/LevelSwitcher");
  	isTutorialMode = levelSwitcher.tutorialMode();
    GD.Print("in graft tut mode = " + isTutorialMode);
  }
  // What will happen when the player clicks outside of the correct areas:
  protected async void registerMisclick()
  {
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
					lCannula.locked = true;
					rCannula.locked = true;
					previousConfirmation = "Scroll";
          Input.SetMouseMode((Godot.Input.MouseMode)0);
					GetTree().ChangeScene("res://FailScreen.tscn");
				}
  }
  public bool getIsFinished() {
		return isFinished;
	}
}
