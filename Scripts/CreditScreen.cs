using Godot;
using System;

public class CreditScreen : Node
{
    private string MAIN_MENU_PATH = "res://Scenes/TitleScreen.tscn";
    private void _on_BackButton_pressed()
    {
        GetTree().ChangeScene(MAIN_MENU_PATH);
    }
}
