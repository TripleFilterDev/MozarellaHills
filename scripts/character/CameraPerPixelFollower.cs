using Godot;
using System;


namespace MozarellaHills
{
	public class CameraPerPixelFollower : Camera2D
	{
		Node2D followNode;

		Vector2 offsetVector = Vector2.Zero;

		public override void _Ready()
		{
			followNode = (Node2D) GetParent();
			offsetVector = followNode.GlobalPosition;
		}

		public override void _Process(float delta)
		{
			offsetVector = new Vector2(Mathf.Lerp(offsetVector.x, followNode.GlobalPosition.x, 0.15f), Mathf.Lerp(offsetVector.y, followNode.GlobalPosition.y, 0.15f));

			var follow = offsetVector;

			var x = Mathf.Lerp(follow.x, Mathf.RoundToInt(follow.x), 0.5f);
			var y = Mathf.Lerp(follow.y, Mathf.RoundToInt(follow.y), 0.5f);

			GlobalPosition = new Vector2(x, y);
		}
	}
}
