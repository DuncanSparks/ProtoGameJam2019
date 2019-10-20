using Godot;
using System;

public class Tether : Area
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
			player.CurrentTether = this;
			player.GoToDarkWorld();
		}
	}

	// ================================================================


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
}
