using Godot;
using System;

public class Player : KinematicBody
{
	[Export]
	private NodePath cameraPath;

	private Vector3 velocity = new Vector3(0, 0, 0);

	private const float Speed = 2f;
	private const float Gravity = 9.8f;
	private const float JumpForce = 3f;

	private Sprite3D sprite;
	private Camera camera;

	// ================================================================
	
	public override void _Ready()
	{
		camera = GetNode<Camera>(cameraPath);
	}


	public override void _Process(float delta)
	{
		Vector3 pos = camera.Translation;
		camera.Translation = new Vector3(Mathf.Lerp(pos.x, Translation.x, 0.03f), pos.y, Mathf.Lerp(pos.z, Translation.z + 2.5f, 0.03f));
	}


	public override void _PhysicsProcess(float delta)
	{
		int x1 = Input.IsActionPressed("move_right") ? 1 : 0;
		int x2 = Input.IsActionPressed("move_left") ? 1 : 0;

		velocity.x = x1 - x2;

		if (Input.IsActionJustPressed("move_jump") && IsOnFloor())
			velocity.y = JumpForce;

		if (!IsOnFloor())
			velocity.y -= Gravity * delta;

		MoveAndSlide(velocity * Speed, new Vector3(0, 1, 0));
	}
}
