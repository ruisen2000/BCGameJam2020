using Godot;
using System;

public class global_ : Node
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.

	AudioStreamPlayer music_ = new AudioStreamPlayer();
	AudioStream x;
	public override void _Ready()
	{


		AddChild(music_);
        x = new AudioStream();
        x.ResourcePath = "res://assets/sprites/soundtrack.ogg";
		music_.Stream = x;

		music_.Play();
	}

	//  // Called every frame. 'delta' is the elapsed time since the previous frame.
	 public override void _Process(float delta)
	 {
		 
	  }
}
