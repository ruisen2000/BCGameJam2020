using Godot;
using System;

public class timer : Node2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	Label label;
	Timer timer_;

	public override void _Ready()
	{
		timer_ = (Timer)GetNode("myTimer");
		label = (Label)GetNode("display");
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(float delta)
 {
		label.Text = ((int)timer_.TimeLeft).ToString();

		if(label.Text == "0")
		{
			GetTree().ChangeScene("res://assets/scenes/Roping.tscn");
		}
 }
}
