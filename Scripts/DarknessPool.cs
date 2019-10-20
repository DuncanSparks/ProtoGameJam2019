using Godot;
using System;

public class DarknessPool : MeshInstance
{
    private void _on_Area_body_entered(Node body)
    {
        if (body.IsInGroup("Player"))
        {
            GetTree().ReloadCurrentScene();
        }
    }
}
