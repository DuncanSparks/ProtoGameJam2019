using Godot;
using System;

public class Switch : StaticBody
{
	[Signal]
	public delegate void pulled_on();
	
	[Signal]
	public delegate void pulled_off();

	[Export]
	private NodePath partnerSwitch;

	[Export]
	private bool oneShot;

	private Switch partner;

	private bool inArea = false;
	private bool canTrigger = true;

	private bool on = false;

	private Player player;
	private AnimationPlayer animationPlayer;

	// ================================================================
	
	public override void _Ready()
	{
		partner = GetNode<Switch>(partnerSwitch);
		animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		player = GetTree().GetRoot().GetNode<Spatial>("Scene").GetNode<Player>("Player");
	}


	public override void _Process(float delta)
	{
		if (Input.IsActionJustPressed("action") && inArea && canTrigger)
		{
			Trigger();
			partner.Trigger();
			EmitSignal(on ? nameof(pulled_on) : nameof(pulled_off));
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
		{
			player.ShowInteract(true);
			inArea = true;
		}
	}


	private void _on_AreaInteract_body_exited(Node body)
	{
		if (body.IsInGroup("Player"))
		{
			player.ShowInteract(false);
			inArea = false;
		}
	}
}
