using Godot;
using System;
/*
 *  DoubleScroll has only one way to be unfolded but may become:
 *  a. Simple Fold (most likely situation as one will full unravel)
 *  b. Bouquet (if tapping is eccentric, meaning its off to one end, top or bottom)
 */
public class DoubleScroll : Graft {
  bool areaEntered = false;
  Cannula cannulaL;
  Cannula cannulaR;
  PackedScene scene1;
  PackedScene scene2;

  public override void _Ready() {
    // graft specific initialization.
    numTapsComplete = rng.Next(3,6);
    GD.Print("you have to tap " + numTapsComplete + " times!");
    scene1 = GD.Load<PackedScene>("res://SimpleFold.tscn");
    scene2 = GD.Load<PackedScene>("res://Bouquet.tscn");
    cannulaL = GetNode("../../Cannulas/CannulaLMesh") as Cannula;
    cannulaR = GetNode("../../Cannulas/CannulaRMesh") as Cannula;
  }

  private void _on_Area_area_entered(object area) {
    areaEntered = true;
  }

  private void _on_Area_area_exited(object area) {
    areaEntered = false;
  }

  public override void _Process(float delta) {
    if(numTaps >= numTapsComplete) {
      numTaps = 0;
      Node sceneNode = scene1.Instance();
      GetNode("/root/Spatial/MainEye").AddChild(sceneNode);
      GetParent().RemoveChild(this);
    }

    if(areaEntered) {
      if(cannulaL.tapped || cannulaR.tapped) {
        // when tapped we need to act -> tapFunction() -> then we reset the tap.
        GD.Print("tap registered");
        numTaps += 1;
        cannulaL.tapped = false;
        cannulaR.tapped = false;
      }
    }
  }
}
