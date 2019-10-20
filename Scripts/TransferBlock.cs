using Godot;
using System;

public class TransferBlock : StaticBody
{
	[Export]
	private bool darkRealm = false;

	private Vector3 target;

	private AudioStreamPlayer soundTransfer;

	// ================================================================
	
	public override void _Ready()
	{
		soundTransfer = GetNode<AudioStreamPlayer>("SoundTransfer");

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
		soundTransfer.Play();
		target.z += darkRealm ? -11 : 11;
		darkRealm ^= true;
	}
}
