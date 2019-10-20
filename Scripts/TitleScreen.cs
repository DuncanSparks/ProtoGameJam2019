using Godot;
using System;

public class TitleScreen : Control
{
    private string GAME_PATH = "res://Scenes/Test.tscn";
    private string OPTIONS_PATH = "res://Scenes/TitleScreen.tscn"; // Temporary path
    private string CREDITS_PATH = "res://Scenes/TitleScreen.tscn"; // Temporary path

    private void _on_PlayButton_pressed()
    {
        GD.Print("Play");
        GetTree().ChangeScene(GAME_PATH);
    }

    private void _on_OptionsButton_pressed()
    {
        GD.Print("Options");
        GetTree().ChangeScene(OPTIONS_PATH);
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