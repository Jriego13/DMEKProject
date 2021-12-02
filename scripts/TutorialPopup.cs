using Godot;
using System;

public class TutorialPopup : Popup
{
    private int enterCount;
    private Sprite graftSprite;
    private Sprite basicSprite;
    private String levelName;
    private Texture spriteTexture;
    public override void _Ready()
    {
        
        enterCount = 0;
        this.Visible = true;
        var levelSwitcher = GetNode<LevelSwitcher>("/root/LevelSwitcher");
        levelName = levelSwitcher.getLevelName(); 
        levelName = levelName.Substring(6);
        levelName = levelName.Substring(0, levelName.Length-5);
        GD.Print(levelName);
        String graftSpritePath = "Panel/" + levelName + "Sprite";
        graftSprite = GetNode(graftSpritePath) as Sprite;
        basicSprite = GetNode("Panel/MessageSprite") as Sprite;
        graftSprite.Visible = true;
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        this.Popup_();
        if (Input.IsActionJustPressed("continue")) {
          enterCount++;
        }
        if (Input.IsActionJustPressed("K")) {
            if (enterCount > 1) {
                enterCount = 0;
            }
            else {
                enterCount = 2;
            }
        }
        if (enterCount == 0) {
            graftSprite.Visible = true;
            basicSprite.Visible = false;
        }
        else if (enterCount == 1){
            graftSprite.Visible = false;
            basicSprite.Visible = true;
        }
        else {
            this.Hide();
        }
    }

    public void HidePopup() {
        enterCount = 2;
    }
}
