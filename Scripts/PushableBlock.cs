using Godot;
using System;

public class PushableBlock : KinematicBody
{
	[Export]
	private NodePath partnerBlock;

	private Vector3 velocity = new Vector3(0, 0, 0);

	private const float Gravity = 9.8f;

	private bool moving = false;

	private bool inArea = false;
	private bool areaSide = false;

	[Export]
	private bool inDarkWorld = false;

	private PushableBlock partner;

	private Player player;

	private Timer timerPush;

	// ================================================================
	
	public override void _Ready()
	{
		partner = GetNode<PushableBlock>(partnerBlock);

		player = GetTree().GetRoot().GetNode<Spatial>("Scene").GetNode<Player>("Player");
		timerPush = GetNode<Timer>("TimerPush");
	}

	public override void _Process(float delta)
	{
		if (!inDarkWorld)
		{
			if (Input.IsActionJustPressed("action") && inArea)
			{
				velocity.x = areaSide ? -0.5f : 0.5f;
				moving = true;
				timerPush.Start();
			}
		}
		else
			Translation = partner.Translation + new Vector3(0, 0, 11);
	}


	public override void _PhysicsProcess(float delta)
	{
		if (!inDarkWorld)
		{
			if (!IsOnFloor())
			velocity.y -= Gravity * delta;

			MoveAndSlide(velocity, new Vector3(0, 1, 0));
		}
	}

	// ================================================================

	private void _on_AreaPushLeft_body_entered(Node body)
	{
		if (body.IsInGroup("Player"))
		{
			inArea = true;
			areaSide = false;

			if (!inDarkWorld)
				player.ShowInteract(true);
		}
	}


	private void _on_AreaPushLeft_body_exited(Node body)
	{
		if (body.IsInGroup("Player"))
		{
			inArea = false;
			player.ShowInteract(false);
		}
	}


	private void _on_AreaPushRight_body_entered(Node body)
	{
		if (body.IsInGroup("Player"))
		{
			inArea = true;
			areaSide = true;
			if (!inDarkWorld)
				player.ShowInteract(true);
		}
	}


	private void _on_AreaPushRight_body_exited(Node body)
	{
		if (body.IsInGroup("Player"))
		{
			inArea = false;
			player.ShowInteract(false);
		}
	}


	private void _on_TimerPush_timeout()
	{
		velocity.x = 0;
		moving = false;
	}
}
