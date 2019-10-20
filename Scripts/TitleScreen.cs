using Godot;
using System;

public class TitleScreen : Control
{
    private string GAME_PATH = "res://Scenes/Tutorial.tscn";
    private string LEVEL_PATH = "res://Scenes/LevelSelect.tscn";
    private string ABOUT_PATH = "res://Scenes/AboutScreen.tscn";
    private string CREDITS_PATH = "res://Scenes/CreditsScreen.tscn"; // Temporary path

    private void _on_PlayButton_pressed()
    {
        GD.Print("Play");
        GetTree().ChangeScene(GAME_PATH);
    }

    private void _on_LevelSelectButton_pressed()
    {
        GetTree().ChangeScene(LEVEL_PATH);
    }

    private void _on_AboutButton_pressed()
    {
        GetTree().ChangeScene(ABOUT_PATH);
    }

    private void _on_CreditsButton_pressed()
    {
        GD.Print("Credits");
        GetTree().ChangeScene(CREDITS_PATH);
    }

    private void _on_QuitButton_pressed()
    {
        GD.Print("Quit");
        GetTree().Quit();
    }
}
