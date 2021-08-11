using Godot;
using System;

public class Cube : Spatial {
  Animation animation;
  AnimationPlayer animator;

  public override void _Ready() {
    animator = GetNode("./AnimationPlayer") as AnimationPlayer; // get animation player
    animation = animator.GetAnimation("default") as Animation; // get the animation you want to modify
    animation.SetLoop(true); // set loop
    animation.SetLength(4.25f); // change length cause it came out super long
    animator.Play("default"); // play
  }
}
