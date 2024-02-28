using Godot;
using System;

namespace MozarellaHills
{

    public class ObstacleBridge : Node2D
    {
        public override void _Ready()
        {
            if (!GameState.Current.AdvancementSinless)
            {
                GetNode<Node2D>("BridgeMiddle").Visible = false;
                GetNode<CollisionShape2D>("BodyMiddle/Collision").Disabled = true;
            }
        }

    }
}