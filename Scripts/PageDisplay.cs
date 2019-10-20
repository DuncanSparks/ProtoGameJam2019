using Godot;
using System;

public class PageDisplay : CanvasLayer
{
	public void SetText(string text)
	{
		GetNode<Label>("Label").Text = text;
	}
}
