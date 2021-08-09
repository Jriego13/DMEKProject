using Godot;
using System;
using System.Collections.Generic;

// base graft class that I would like to use for all derivative grafts
public class Graft : Node
{
  protected List<PackedScene> nextConfirmations = new List<PackedScene>();
  protected bool unfolded;

  protected int numTaps;
  protected int numTapsComplete;
  protected Random rng = new Random();

  // function to move to next graft that is set by child
  protected void NextConfirmation(PackedScene nextScene) {

  }
}
