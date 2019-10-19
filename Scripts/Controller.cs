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

	// ================================================================
	
	public override void _Ready()
	{
		
	}


	public override void _Process(float delta)
	{
		if (Input.IsActionJustPressed("sys_fullscreen"))
			OS.SetWindowFullscreen(!OS.IsWindowFullscreen());
	}

	// ================================================================

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float LerpDelta(float from, float to, float weight, float delta)
	{
		return Mathf.Lerp(from, to, 1f - Mathf.Pow(weight, delta));
	}
}
