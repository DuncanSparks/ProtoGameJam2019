using Godot;
using System;

public class PauseMenu : Control
{
    private bool isPaused = false;
    private string MAIN_MENU_PATH = "res://Scenes/TitleScreen.tscn";
    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event is InputEventKey eventKey)
        {
            if (eventKey.Pressed & eventKey.Scancode == (int)KeyList.Escape)
            {
                isPaused = !isPaused;
                switch (isPaused)
                {
                    case true:
                        GetNode<ColorRect>("ColorRect").Hide();
                        break;
                    default:
                        GetNode<ColorRect>("ColorRect").Show();
                        break;
                }
                GetTree().Paused = isPaused;
            }
        }
    }

    private void _on_MainMenuButton_pressed()
    {
        GetTree().ChangeScene(MAIN_MENU_PATH);
    }

    private void _on_QuitButton_pressed()
    {
        GD.Print("Quit");
        GetTree().Quit();
    }
}
