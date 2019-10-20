using Godot;
using System;

public class CreditScreen : Node
{
    private string MAIN_MENU_PATH = "res://Scenes/TitleScreen.tscn";
    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event is InputEventKey eventKey)
        {
            if (eventKey.Pressed & eventKey.Scancode == (int)KeyList.Escape)
            {
                GetTree().ChangeScene(MAIN_MENU_PATH);
            }
        }
    }
}
