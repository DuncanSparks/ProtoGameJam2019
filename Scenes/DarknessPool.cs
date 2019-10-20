using Godot;
using System;

public class DarknessPool : MeshInstance
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    private void _on_Area_body_entered(Node body)
    {
        if (body.IsInGroup("Player"))
        {
            GetTree().ReloadCurrentScene();
        }
    }
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
