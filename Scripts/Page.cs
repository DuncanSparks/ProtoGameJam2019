using Godot;
using System;

public class Page : Sprite3D
{
    private Player player;

	private bool inArea = false;

	// ================================================================

	public bool InArea { set => inArea = value; }

	// ================================================================

	public override void _Ready()
	{
		player = GetTree().GetRoot().GetNode<Spatial>("Scene").GetNode<Player>("Player");
	}
	
	public override void _Process(float delta)
	{
		if (Input.IsActionJustPressed("action") && inArea && player.State == Player.PlayerState.Move && player.OnFloor)
		{
			GetTree().ChangeScene("res://Scenes/Level1.tscn");
		}
	}

   private void _on_Area_body_entered(Node body)
    {
        if (body.IsInGroup("Player"))
        {
            player.ShowInteract(true);
            inArea = true;
        }
    }

    private void _on_Area_body_exited(Node body)
    {
        if (body.IsInGroup("Player"))
		{
			player.ShowInteract(false);
			inArea = false;
		}
    }
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
