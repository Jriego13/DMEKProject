using Godot;
using System;
using System.Collections.Generic;

// base graft class that I would like to use for all derivative grafts
public class Graft : Node
{
  protected List<PackedScene> nextConfirmations;
  protected bool unfolded = false;

  // function to move to next graft that is set by child
  protected void NextConfirmation(PackedScene nextScene)
  {

  }
}
