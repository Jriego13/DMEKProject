using Godot;
using System;

public class LevelSwitcher : Node
{
    String foldName = "";

    public void ChangeScene(String next_scene, String levelName)
    {
        foldName = levelName;
        GetTree().ChangeScene(next_scene);
    }
    public String getFoldName()
    {
        return foldName;
    }
}
