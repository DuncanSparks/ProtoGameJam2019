using Godot;
using System;

public class Switch : StaticBody
{
	[Signal]
	public delegate void pulled();

	[Export]
	private NodePath partnerSwitch;

	[Export]
	private bool oneShot;

	private Switch partner;

	private bool inArea = false;
	private bool canTrigger = true;

	private bool on = false;

	private AnimationPlayer animationPlayer;

	// ================================================================
	
	public override void _Ready()
	{
		partner = GetNode<Switch>(partnerSwitch);
		animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
	}


	public override void _Process(float delta)
	{
		if (Input.IsActionJustPressed("action") && inArea && canTrigger)
		{
			Trigger();
			partner.Trigger();
		}
	}

	// ================================================================

	private void Trigger()
	{
		animationPlayer.Play(on ? "Pull" : "Push");
		on ^= true;
	}


	private void _on_AreaInteract_body_entered(Node body)
	{
		if (body.IsInGroup("Player"))
			inArea = true;
	}


	private void _on_AreaInteract_body_exited(Node body)
	{
		if (body.IsInGroup("Player"))
			inArea = false;
	}
}
