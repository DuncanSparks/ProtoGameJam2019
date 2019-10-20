using Godot;
using System;

public class TransferBlock : StaticBody
{
	[Export]
	private bool darkRealm = false;

	private Vector3 target;

	// ================================================================
	
	public override void _Ready()
	{
		if (darkRealm)
			Translation += new Vector3(0, 0, 11);

		target = Translation;
	}


	public override void _PhysicsProcess(float delta)
	{
		Vector3 pos = Translation;
		Translation = new Vector3(pos.x, pos.y, Controller.LerpDelta(pos.z, target.z, 0.3f, delta));
	}


	public void Transfer()
	{
		target.z += darkRealm ? -11 : 11;
		darkRealm ^= true;
	}
}
