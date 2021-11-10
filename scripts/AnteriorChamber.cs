using Godot;
using System;
using System.Collections.Generic;

public class AnteriorChamber : Sprite {
  MainGame eye;
  List<Texture> depths = new List<Texture>();

  public override void _Ready() {
	Texture shallowDepth = GD.Load("res://sprites/AnteriorChamber50.png") as Texture;
	eye = GetNode("../..") as MainGame;
	depths.Add(this.GetTexture());
	depths.Add(shallowDepth);
  }

  public override void _Process(float delta) {
	if(eye.getWaterLevel() <= 50) {
	  this.SetTexture(depths[1]);
	}
	else {
	  this.SetTexture(depths[0]);
	}
  }
}
