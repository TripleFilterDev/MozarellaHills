using Godot;
using System;

namespace MozarellaHills
{
	public class MenuStart : Node2D
	{
		public override void _Ready()
		{
			Timer timer = new Timer();
			AddChild(timer);
			timer.Connect("timeout", this, "OnTimerTimeout");
			timer.WaitTime = 0.5f;
			timer.Start();
		}

		public void OnTimerTimeout()
		{
			GameState.Default();
			GlobalTransition.Instance.ChangeScene("res://scenes/menu/developers.mn.tscn");
		}
	}
}
