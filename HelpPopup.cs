using Godot;
using System;

public class HelpPopup : Popup
{
    public bool visible;
    private RichTextLabel label;
    private ColorRect colorRect;
    private CannulaMain2D cannula;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        var goBackButton = GetNode("GoBackButton");
        goBackButton.Connect("pressed", this, "onGoBackButtonPressed");
        cannula = GetNode("../Cannulas") as CannulaMain2D;
        colorRect = GetNode("ColorRect") as ColorRect;
        colorRect.Color = new Color( 0.0f, 0.13f, 0.65f, 0.5f );
        /* label = GetNode("ColorRect/RichTextLabel") as RichTextLabel;

        label.SetText("[right] Press esc to return to menu \n" 
              +"Rotate cannula clockwise: s ccw: w" + "\n" + "\n"
              + "swap cannula control: r" + "\n"
              + "reset cannula: e" + "\n" 
              + "inject liquid in: p out: o \n \n"
              + "tap l/r cannula: l/r mouse" + "\n"+ "\n"
              + "hold mouse to keep cannula down[/right]"); */
        visible = false;
    }

    private void onGoBackButtonPressed() {
        GD.Print("button regd");
        visible = false;
        Hide();
        
    }

    public void changeVisiblity(bool input) {
        visible = input;
    }
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        if (visible)
        {
            cannula.setMenuOpen(true);
            Popup_();
            // Make mouse visible:
        }
        else
        {
            
            cannula.setMenuOpen(false);
            Hide();
        }
    }
}
