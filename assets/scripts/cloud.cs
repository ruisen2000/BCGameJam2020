using Godot;
using System;

public class cloud : AnimatedSprite
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	[Export]
	public int cloud_velocity = 3;
	public override void _Ready()
	{
		
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(float delta)
  {
		Vector2 mm = GetPosition();
		mm.x -= cloud_velocity/2;
		
		SetPosition(mm);
  }
}
