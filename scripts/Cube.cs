using Godot;
using System;

// script that is attached to the sliding cube
// animator has several attributes that need to be enabled from a script such as:
// SetLoop, SetLength, and Play when we want it to
public class Cube : Spatial {
  Animation animation;
  AnimationPlayer animator;

  public override void _Ready() {
    animator = GetNode("./AnimationPlayer") as AnimationPlayer;
    animation = animator.GetAnimation("default") as Animation;
    GD.Print(animator);
    animation.SetLoop(true);
    animation.SetLength(4.25f);
    animator.Play("default");
  }
}
