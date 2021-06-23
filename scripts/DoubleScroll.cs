using Godot;
using System;

/*
 *  DoubleScroll has only one way to be unfolded but may become:
 *  a. Simple Fold (most likely situation as one will full unravel)
 *  b. Bouquet (if tapping is eccentric, meaning its off to one end, top or bottom)
 */
public class DoubleScroll : Graft
{
  public override void _Ready()
  {

  }

  private void _on_Area_area_entered()
  {
    
  }
}
