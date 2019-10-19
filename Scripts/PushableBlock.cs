using Godot;
using System;

public class PushableBlock : KinematicBody
{

	private Vector3 velocity = new Vector3(0, 0, 0);

	private const float Gravity = 9.8f;

	//private bool inArea = false;
	//private bool areaSide = false;

	// ================================================================
	
	public override void _Ready()
	{
		
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
			//inArea = true;
			//areaSide = false;
			velocity.x = 3f;
		}
	}


	private void _on_AreaPushLeft_body_exited(Node body)
	{
		//if (body.IsInGroup("Player"))
	//	{
		//	inArea = false;
		//}
	}


	private void _on_AreaPushRight_body_entered(Node body)
	{
		if (body.IsInGroup("Player"))
		{
		//	inArea = true;
		//	areaSide = true;
		}//
	}


	private void _on_AreaPushRight_body_exited(Node body)
	{
		//if (body.IsInGroup("Player"))
		//{
		//	inArea = false;
		//}
	}
}
