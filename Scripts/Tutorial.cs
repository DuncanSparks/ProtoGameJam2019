using Godot;
using System;

public class Tutorial : Spatial
{
	[Export]
	private NodePath playerPath;

	private Player player;

	// ================================================================
	
	public override void _Ready()
	{
		player = GetNode<Player>(playerPath);

		player.State = Player.PlayerState.NoInput;
		player.CanGoBack = false;
	}

	// ================================================================

	private void _on_TimerTut1_timeout()
	{
		player.GoToDarkWorld(0.95f, true);
	}


	private void AllowGoBack()
	{
		player.CanGoBack = true;
	}
}
