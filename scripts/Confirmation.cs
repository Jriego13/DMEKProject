using Godot;
using System;

// text at the front which tells you which confirmation you are on
public class Confirmation : RichTextLabel {
  String currentConfirmation;

  public override void _Ready() {
	currentConfirmation = GetNode("/root/Spatial/MainEye").GetChild(0).GetName();
  }

  public override void _Process(float delta) {
	SetText("Current Conformation: " + currentConfirmation);
	currentConfirmation = GetNode("/root/Spatial/MainEye").GetChild(0).GetName();
	if(currentConfirmation == "Finished") {
	  currentConfirmation = "YUP YOU DID IT. GOOD JOB CLICKING :D";
	}
  }
}
