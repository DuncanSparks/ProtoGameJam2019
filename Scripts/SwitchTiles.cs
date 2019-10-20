using Godot;
using System;

public class SwitchTiles : GridMap
{
	[Export]
	private Vector3 positionAlpha;

	[Export]
	private Vector3 positionBeta;

	private Vector3 target;

	// ================================================================
	
	public override void _Ready()
	{
		target = Translation;
	}


	public override void _PhysicsProcess(float delta)
	{
		Vector3 pos = Translation;
		Translation = new Vector3(Controller.LerpDelta(pos.x, target.x, 0.1f, delta), Controller.LerpDelta(pos.y, target.y, 0.1f, delta), pos.z);
	}


	public void Move(bool beta)
	{
		target = beta ? positionBeta : positionAlpha;
	}
}
