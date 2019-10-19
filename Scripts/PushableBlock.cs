using Godot;
using System;

public class PushableBlock : KinematicBody
{

	private Vector3 velocity = new Vector3(0, 0, 0);

	private const float Gravity = 9.8f;

	private bool moving = false;

	private bool inArea = false;
	private bool areaSide = false;

	private Timer timerPush;

	// ================================================================
	
	public override void _Ready()
	{
		timerPush = GetNode<Timer>("TimerPush");
	}

	public override void _Process(float delta)
	{
		if (Input.IsActionJustPressed("action") && inArea)
		{
			velocity.x = areaSide ? -0.5f : 0.5f;
			moving = true;
			timerPush.Start();
		}
	}


	public override void _PhysicsProcess(float delta)
	{
		if (!IsOnFloor())
			velocity.y -= Gravity * delta;

		MoveAndSlide(velocity, new Vector3(0, 1, 0));
	}

	// ================================================================

	private void _on_AreaPushLeft_body_entered(Node body)
	{
		if (body.IsInGroup("Player"))
		{
			inArea = true;
			areaSide = false;
			//velocity.x = 1f;
			//moving = true;
			//timerPush.Start();
		}
	}


	private void _on_AreaPushLeft_body_exited(Node body)
	{
		if (body.IsInGroup("Player"))
			inArea = false;
	}


	private void _on_AreaPushRight_body_entered(Node body)
	{
		if (body.IsInGroup("Player"))
		{
			inArea = true;
			areaSide = true;
		}
	}


	private void _on_AreaPushRight_body_exited(Node body)
	{
		if (body.IsInGroup("Player"))
			inArea = false;
	}


	private void _on_TimerPush_timeout()
	{
		velocity.x = 0;
		moving = false;
	}
}
