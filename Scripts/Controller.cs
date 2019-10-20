using Godot;
using System;
using System.Runtime.CompilerServices;

public class Controller : Node
{
	public static Controller Singleton;

	Controller()
	{
		Singleton = this;
	}

	private bool paused = false;

	private AnimationPlayer animPlayer;

	private PackedScene PauseMenuRef = GD.Load<PackedScene>("res://Scenes/PauseMenu.tscn");

	// ================================================================
	
	public static bool Paused { get => Controller.Singleton.paused; set => Controller.Singleton.paused = value; }

	// ================================================================

	public override void _Ready()
	{
		animPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
	}


	public override void _Process(float delta)
	{
		if (Input.IsActionJustPressed("sys_fullscreen"))
			OS.SetWindowFullscreen(!OS.IsWindowFullscreen());

		if (Input.IsActionJustPressed("sys_pause") && !paused)
		{
			var pm = (PauseMenu)PauseMenuRef.Instance();
			GetTree().GetRoot().AddChild(pm);

			paused = true;
		}
	}

	// ================================================================

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float LerpDelta(float from, float to, float weight, float delta)
	{
		return Mathf.Lerp(from, to, 1f - Mathf.Pow(weight, delta));
	}


	public static void Crossfade(bool dark)
	{
		Controller.Singleton.animPlayer.Play(dark ? "ToDark" : "ToLight");
	}
}
