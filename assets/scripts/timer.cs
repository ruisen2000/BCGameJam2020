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
	TextureButton begin_game;

	[Export]
	NodePath p1;
	
	[Export]
	NodePath p2;
	
	[Export]
	NodePath hinting;

	AnimatedSprite p1_;
	AnimatedSprite p2_;
	Label hint_field;



	public override void _Ready()
	{
		hint_field = (Label)GetNode(hinting);

		set_hint();

		p1_ = (AnimatedSprite)GetNode(p1);
		p2_ = (AnimatedSprite)GetNode(p2);
		timer_ = (Timer)GetNode("myTimer");
		label = (Label)GetNode("display");
		begin_game = (TextureButton)GetNode("climb_button");
		begin_game.Visible = false;
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(float delta)
 {
		label.Text = ((int)timer_.TimeLeft).ToString();

		if(label.Text == "0")
		{
			p1_.FlipH = true;
			p2_.FlipH = false;
			begin_game.Visible = true;
			if (begin_game.Pressed)
			{
				GetTree().ChangeScene("res://assets/scenes/FirstLevel.tscn");
			}
		}
 }

	public void set_hint()
	{
		Random x = new Random();
		int k = x.Next(0, 3);
		switch (k) {
			case 0:
				hint_field.Text = "Jump together for a higher jump";
				break;
			case 1:
				hint_field.Text = "Time your jump with partner by Counting";
				break;
			case 2:
				hint_field.Text = "For the best experience, play with a friend. Or you're a psycho.";
				break;

		}

		
	}
}
