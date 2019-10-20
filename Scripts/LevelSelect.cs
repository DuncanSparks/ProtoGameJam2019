using Godot;
using System;

public class LevelSelect : Control
{

    private string MAIN_PATH = "res://Scenes/TitleScreen.tscn";
    private string PATH1 = "res://Scenes/Tutorial.tscn";
    private string PATH2 = "res://Scenes/Level1.tscn";
    private string PATH3 = "res://Scenes/Level2.tscn";
    private string PATH4 = "res://Scenes/Level3.tscn";
    private void _on_Level1_pressed()
    {
        GetTree().ChangeScene(PATH1);
    }
    private void _on_Level2_pressed()
    {
        GetTree().ChangeScene(PATH2);
    }
    private void _on_Level3_pressed()
    {
        GetTree().ChangeScene(PATH3);
    }
    private void _on_Level4_pressed()
    {
        GetTree().ChangeScene(PATH4);
    }

    private void _on_BackButton_pressed()
    {
        GetTree().ChangeScene(MAIN_PATH);
    }
}
