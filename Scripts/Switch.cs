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
	private AudioStreamPlayer soundPull;
	private Timer timer;

	// ================================================================
	
	public override void _Ready()
	{
		partner = GetNode<Switch>(partnerSwitch);
		animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		soundPull = GetNode<AudioStreamPlayer>("SoundPull");
		timer = GetNode<Timer>("TimerPull");

		player = GetTree().GetRoot().GetNode<Spatial>("Scene").GetNode<Player>("Player");
	}


	public override void _Process(float delta)
	{
		if (Input.IsActionJustPressed("action") && inArea && canTrigger)
		{
			soundPull.Play();
			Trigger();
			partner.Trigger();
			EmitSignal(on ? nameof(pulled_on) : nameof(pulled_off));
		
			canTrigger = false;
			timer.Start();
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


	private void _on_TimerPull_timeout()
	{
		canTrigger = true;
	}
}
