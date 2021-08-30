using Godot;
using System;
using System.Collections.Generic;

// base graft class that maintains how many taps are needed to unfold a graft
// how many taps have already been done
// and two unused variables: nextConfirmations and unfolded
public class Graft : Node
{
  protected List<PackedScene> nextConfirmations = new List<PackedScene>();
  protected bool unfolded;

  protected int numTaps;
  protected int numTapsComplete;
  protected Random rng = new Random();
}
