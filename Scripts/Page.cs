using Godot;
using System;

public class Page : Sprite3D
{
	[Export(PropertyHint.File, "*.tscn")]
	private string nextLevel = string.Empty;

	[Export(PropertyHint.MultilineText)]
	private string pageText = string.Empty;

    private Player player;

	private bool inArea = false;

	private PackedScene PageDisplayRef = GD.Load<PackedScene>("res://Prefabs/PageDisplay.tscn");

	// ================================================================

	public bool InArea { set => inArea = value; }

	// ================================================================

	public override void _Ready()
	{
		player = GetTree().GetRoot().GetNode<Spatial>("Scene").GetNode<Player>("Player");
	}
	
	public override void _Process(float delta)
	{
		if (Input.IsActionJustPressed("action") && inArea && player.State == Player.PlayerState.Move && player.OnFloor)
		{
			player.State = Player.PlayerState.NoInput;
			var page = (PageDisplay)PageDisplayRef.Instance();
			page.SetText(pageText);
			page.SetNextLevel(nextLevel);
			GetTree().GetRoot().AddChild(page);
		}
	}

   private void _on_Area_body_entered(Node body)
    {
        if (body.IsInGroup("Player"))
        {
            player.ShowInteract(true);
            inArea = true;
        }
    }

    private void _on_Area_body_exited(Node body)
    {
        if (body.IsInGroup("Player"))
		{
			player.ShowInteract(false);
			inArea = false;
		}
    }
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
