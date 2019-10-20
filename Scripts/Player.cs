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

	private bool inDarkWorld = false;

	private bool canJump = false;
	private bool onFloor = false;

	//private bool walking = false;

	private const float Speed = 3f;
	private const float Gravity = 9.8f;
	private const float JumpForce = 3f;

	private Sprite3D sprite;
	private Camera camera;
	private AnimationPlayer animPlayer;
	private AnimationPlayer backgroundAnimPlayer;
	private Timer timerSpawnPlayer2;
	private Timer timerKillPlayer2;

	private KinematicBody otherPlayer = null;

	private PackedScene Player2Ref = GD.Load<PackedScene>("res://Prefabs/PlayerDummy.tscn");
	private PackedScene PartsAppearRef = GD.Load<PackedScene>("res://Prefabs/Particles/PartsAppear.tscn");

	// ================================================================

	public bool InDarkWorld { get => inDarkWorld; }

	// ================================================================
	
	public override void _Ready()
	{
		sprite = GetNode<Sprite3D>("Sprite");
		camera = GetNode<Camera>(cameraPath);
		animPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		backgroundAnimPlayer = GetNode<AnimationPlayer>(backgroundAnimPlayerPath);
		timerSpawnPlayer2 = GetNode<Timer>("TimerSpawnPlayer2");
		timerKillPlayer2 = GetNode<Timer>("TimerKillPlayer2");
	}


	public override void _Process(float delta)
	{
		// Move the camera smoothly toward the player's position
		Vector3 pos = camera.Translation;
		camera.Translation = new Vector3(Controller.LerpDelta(pos.x, Translation.x, 0.03f, delta), Controller.LerpDelta(pos.y, Translation.y + 0.25f, 0.02f, delta), Controller.LerpDelta(pos.z, Translation.z + 2.2f, 0.03f, delta));
	
		//walking = velocity.x != 0;

		if (Input.IsActionJustPressed("move_left"))
			sprite.FlipH = true;
		
		if (Input.IsActionJustPressed("move_right"))
			sprite.FlipH = false;

		//if (Input.IsActionJustReleased("move_left") || Input.IsActionJustReleased("move_right"))
		//	animPlayer.Play("Idle");
		animPlayer.Play(inDarkWorld ? velocity.x != 0 ? "WalkDark" : "IdleDark" : velocity.x != 0 ? "Walk" : "Idle");

		if (Input.IsActionJustPressed("debug_1"))
		{
			GoToDarkWorld();
		}

		if (Input.IsActionJustPressed("debug_2"))
		{
			GoToLightWorld();
		}
		if (Input.IsActionJustPressed("restart_level"))
		{
			GetTree().ReloadCurrentScene();
		}
	}


	public override void _PhysicsProcess(float delta)
	{
		if (state != PlayerState.NoInput)
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


	private void GoToDarkWorld()
	{
		state = PlayerState.NoInput;
		Vector3 t = Translation;

		var p2 = (KinematicBody)Player2Ref.Instance();
		otherPlayer = p2;
		otherPlayer.Translation = Translation;
		GetTree().GetRoot().AddChild(p2);

		Hide();
		Translation = new Vector3(t.x, t.y, 11.5f);
		backgroundAnimPlayer.Play("Dark");

		var ball = camera.GetNode<MeshInstance>("DarkBall");
		ball.Show();
		ball.GetNode<AnimationPlayer>("AnimationPlayer").Play("Anim");

		inDarkWorld = true;
		timerSpawnPlayer2.Start();
	}


	private void GoToLightWorld()
	{
		state = PlayerState.NoInput;
		Vector3 t = Translation;

		Hide();
		Translation = otherPlayer.Translation;
		backgroundAnimPlayer.Play("Light");
		
		var ball = camera.GetNode<MeshInstance>("DarkBall");
		ball.Show();
		ball.GetNode<AnimationPlayer>("AnimationPlayer").Play("Anim");

		inDarkWorld = false;
		timerKillPlayer2.Start();
	}


	private void _on_Area_body_entered(Node body)
	{
		if (body.IsInGroup("Floor"))
		{
			velocity.y = 0;
			canJump = true;
			onFloor = true;
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


	private void _on_Area2_body_entered(Node body)
	{
		if (body.IsInGroup("TB"))
		{
			var t = (TransferBlock)body;
			t.Transfer();
		}
	}


	private void _on_TimerSpawnPlayer2_timeout()
	{
		var parts = (Particles)PartsAppearRef.Instance();
		parts.Translation = Translation;
		parts.Emitting = true;
		GetTree().GetRoot().AddChild(parts);

		Show();
		camera.GetNode<MeshInstance>("DarkBall").Hide();
		state = PlayerState.Move;
	}


	private void _on_TimerKillPlayer2_timeout()
	{
		var parts = (Particles)PartsAppearRef.Instance();
		parts.Translation = Translation;
		parts.Emitting = true;
		GetTree().GetRoot().AddChild(parts);

		otherPlayer.QueueFree();
		Show();
		camera.GetNode<MeshInstance>("DarkBall").Hide();
		state = PlayerState.Move;
	}
}
