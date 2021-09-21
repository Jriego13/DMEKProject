using Godot;
using System;
/*
 *  DoubleScroll has only one way to be unfolded but may become:
 *  a. Simple Fold (most likely situation as one will full unravel)
 *  b. Bouquet (if tapping is eccentric, meaning its off to one end, top or bottom)
 */
public class DoubleScroll : Graft {
  Cannula lCannula;
  Cannula rCannula;
  PackedScene scene1;
  PackedScene scene2;
  bool areaEntered = false;

  public override void _Ready() {
    // graft specific initialization.
    numTapsComplete = rng.Next(3,6);
    scene1 = GD.Load<PackedScene>("res://SimpleFold.tscn");
    scene2 = GD.Load<PackedScene>("res://Bouquet.tscn");
    lCannula = GetNode("../../Cannulas/CannulaLMesh") as Cannula;
    rCannula = GetNode("../../Cannulas/CannulaRMesh") as Cannula;
    GD.Print("you have to tap " + numTapsComplete + " times!");
  }

  public override void _Process(float delta) {
    if(numTaps >= numTapsComplete) {
      numTaps = 0;
      Node sceneNode = scene1.Instance();
      GetNode("/root/Spatial/MainEye").AddChild(sceneNode);
      GetParent().RemoveChild(this);
    }

    // tap function can be made?
    if(areaEntered) {
      if(lCannula.tapped || rCannula.tapped) {
        numTaps += 1;
        lCannula.tapped = false;
        rCannula.tapped = false;
        GD.Print("tap registered");
      }
    }
  }

  private void _on_Area_area_entered(object area) {
    areaEntered = true;
  }

  private void _on_Area_area_exited(object area) {
    areaEntered = false;
  }
}
