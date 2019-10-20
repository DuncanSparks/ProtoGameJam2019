using Godot;
using System;

public class Player : KinematicBody
{
	[Export]
	private NodePath cameraPath;

	[Export]
	private NodePath backgroundAnimPlayerPath;

	public enum PlayerState { Move, NoInput, NoPhysics };
	private PlayerState state = PlayerState.Move;

	private Vector3 velocity = new Vector3(0, 0, 0);

	private bool canJump = false;
	private bool onFloor = false;

	private const float Speed = 3f;
	private const float Gravity = 9.8f;
	private const float JumpForce = 3f;

	private Sprite3D sprite;
	private Camera camera;
	private AnimationPlayer backgroundAnimPlayer;

	// ================================================================
	
	public override void _Ready()
	{
		camera = GetNode<Camera>(cameraPath);
		backgroundAnimPlayer = GetNode<AnimationPlayer>(backgroundAnimPlayerPath);
	}


	public override void _Process(float delta)
	{
		// Move the camera smoothly toward the player's position
		Vector3 pos = camera.Translation;
		camera.Translation = new Vector3(Controller.LerpDelta(pos.x, Translation.x, 0.03f, delta), Controller.LerpDelta(pos.y, Translation.y + 0.25f, 0.02f, delta), Controller.LerpDelta(pos.z, Translation.z + 2.2f, 0.03f, delta));
	
		if (Input.IsActionJustPressed("debug_1"))
		{
			Vector3 t = Translation;
			Translation = new Vector3(t.x, t.y + 1.5f, 11.5f);
			backgroundAnimPlayer.Play("Dark");
		}

		if (Input.IsActionJustPressed("debug_2"))
		{
			Vector3 t = Translation;
			Translation = new Vector3(t.x, t.y + 1.5f, 0.5f);
			backgroundAnimPlayer.Play("Light");
		}
	}


	public override void _PhysicsProcess(float delta)
	{
		if (state != PlayerState.NoPhysics)
		{
			// Check for left/right input
			int x1 = Input.IsActionPressed("move_right") ? 1 : 0;
			int x2 = Input.IsActionPressed("move_left") ? 1 : 0;

			velocity.x = x1 - x2;

			// Check for jump input
			if (Input.IsActionJustPressed("move_jump") && canJump)
			{
				velocity.y = JumpForce;
				canJump = false;
			}

			// If we aren't on the floor, accelerate downward (gravity)
			if (!onFloor)
				velocity.y -= Gravity * delta;

			// Apply velocity to player
			MoveAndSlide(velocity * Speed, new Vector3(0, 1, 0));
		}
	}


	private void _on_Area_body_entered(Node body)
	{
		if (body.IsInGroup("Floor"))
		{
			velocity.y = 0;
			canJump = true;
			onFloor = true;
		}

		if (body.IsInGroup("TB"))
		{
			var t = (TransferBlock)body;
			t.Transfer();
		}
	}

	private void _on_Area_body_exited(Node body)
	{
		if (body.IsInGroup("Floor"))
		{
			onFloor = false;
			canJump = false;
		}
	}
}
