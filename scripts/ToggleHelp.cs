using Godot;
using System;
using System.Threading;

// text at the front which tells you which confirmation you are on
public class ToggleHelp : RichTextLabel {
  bool showHelp;

  public override void _Ready() {
	   showHelp = false;
  }

  public override void _Process(float delta) {
    if(Input.IsActionJustPressed("toggle_help")) {
      showHelp = !showHelp;
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
