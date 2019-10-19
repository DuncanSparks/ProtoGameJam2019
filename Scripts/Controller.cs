using Godot;
using System;

public class Controller : Node
{

	// ================================================================
	
	public override void _Ready()
	{
		
	}


	public override void _Process(float delta)
	{
		if (Input.IsActionJustPressed("sys_fullscreen"))
			OS.SetWindowFullscreen(!OS.IsWindowFullscreen());
	}
}
