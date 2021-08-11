using Godot;
using System;

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
