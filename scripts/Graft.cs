using Godot;
using System;
using System.Collections.Generic;

// base graft class that I would like to use for all derivative grafts
public class Graft : Node2D {
  protected List<PackedScene> nextConfirmations = new List<PackedScene>();
  protected Random rng = new Random();
  protected int numTaps;
  protected int numTapsComplete;
  protected bool isFinished;
  protected int numTapsWrong;
  protected Texture circleTexture;
  protected String previousConfirmation;


  public bool getIsFinished() {
		return isFinished;
	}
}
