using Godot;
using System;

public class PageDisplay : CanvasLayer
{
	private string nextLevel;

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
		if (Input.IsActionJustPressed("action"))
		{
			GetTree().ChangeScene(nextLevel);
			QueueFree();
		}
	}
}
