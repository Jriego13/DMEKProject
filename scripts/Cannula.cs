using Godot;
using System;

// individual cannula class that maintains whether a cannula is being tapped or locked
public class Cannula : MeshInstance {
  public bool tapped = false;
  public bool locked = false;
}
