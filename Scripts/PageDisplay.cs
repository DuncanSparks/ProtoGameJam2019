using Godot;
using System;

public class PageDisplay : CanvasLayer
{
	private string nextLevel;
	private bool pressed = false;

	public void SetText(string text)
	{
		GetNode<Label>("Label").Text = text;
	}


	public void SetNextLevel(string level)
	{
		nextLevel = level;
	}


	public override void _Process(float delta)
	{
		if (Input.IsActionJustPressed("action") && !pressed)
		{
			Controller.Fade(true);
			GetNode<Timer>("Timer").Start();
			pressed = true;
		}
	}


	private void _on_Timer_timeout()
	{
		GetTree().ChangeScene(nextLevel);
		Controller.Fade(false);
		QueueFree();
	}
}
