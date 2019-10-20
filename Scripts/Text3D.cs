using Godot;
using System;

public class Text3D : MeshInstance
{
	private Label text;

	// ================================================================
	
	public override void _Ready()
	{
		text = GetNode<Viewport>("Viewport").GetNode<Label>("Label");
		GetNode<Timer>("Timer").Start();
	}
	
	//public void Update(float timeLeft, float totalTime)
	//{
		//text.Text = 
//	//}

	private void _on_Timer_timeout()
	{
		var img = GetNode<Viewport>("Viewport").GetTexture();

		var mat = new SpatialMaterial();
		mat.FlagsTransparent = true;
		mat.AlbedoTexture = img;
		SetSurfaceMaterial(0, mat);
	}
}
