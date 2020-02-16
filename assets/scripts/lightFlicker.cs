using Godot;
using System;

public class lightFlicker : Light2D
{
	// Declare member variables here. Examples:
	[Export]
	float period = 1;
	[Export]
	float maxSize = 3;
	[Export]
	float minSize = 2.5f;

	private float sizeDiff;
	private float time = 0;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		sizeDiff = maxSize - minSize;
		TextureScale = (sizeDiff / 2) * (float)Math.Sin(Math.PI / (2 * period) * time) + ((minSize + maxSize) / 2);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		// flicker the flame
		time += delta;
		TextureScale = (sizeDiff/2) * (float)Math.Sin(Math.PI/(2*period) * time) + ((minSize + maxSize)/2);

	}
}
