using Godot;
using System;
using System.Threading;

// text at the front which tells you which confirmation you are on
public class ToggleHelp : RichTextLabel {
  Boolean showHelp;

  public override void _Ready() {
	   showHelp = false;
  }

  public override async void _Process(float delta) {
    if(Input.IsActionPressed("toggle_help")) {
      showHelp = !showHelp;
      await ToSignal(GetTree().CreateTimer(.2f), "timeout");
    }
    if (!showHelp) {
        SetText("Press h to show controls");
    }
    else {
      SetText("Rotate cannula clockwise: s" + "\n"
              + "counterclockwise: w" + "\n" + "\n"
              + "swap cannula: s" + "\n"
              + "reset cannula: e" + "\n" + "\n"
              + "tap left cannula: left-mouse" + "\n"
              + "tap right cannula: right-mouse" + "\n"
              + "hold mouse to keep cannula down");
    }
  }
}
