using Godot;
using System;

namespace MozarellaHills
{
    public class ObjectCheese : KinematicBody2D, ITaken
    {
        public TakenObjects ObjectType => TakenObjects.Cheese;

        public bool IsItem => true;

        private bool near = false;

        public void ToggleNearest(bool can)
        {

            near = can;
            //spriteOutline.Visible = can;
        }

        public void Take()
        {
            QueueFree();
        }

        public float Gravity => 120.0f;
        public float MaxFallSpeed = 620.0f;
        
        private float verticalVelocity = 0.0f;
        public float VerticalVelocity => verticalVelocity;

        private Vector2 prevPosition = Vector2.Zero;

        private Sprite spriteOutline;

        public override void _Ready()
        {
            spriteOutline = (Sprite) GetNode("SpriteOutline");

            spriteOutline.Hide();
        }

        public override void _PhysicsProcess(float delta)
        {
            MoveAndSlide(Vector2.Down * verticalVelocity);

            UpdateVerticalVelocity();
            FixSlope();

            spriteOutline.Visible = near;

            prevPosition = GlobalPosition;
        }

        private void UpdateVerticalVelocity()
        {
            verticalVelocity += Gravity;
            if (IsOnFloor())
            {
                verticalVelocity = 0.1f;
            }
            if (verticalVelocity > MaxFallSpeed)
            {
                verticalVelocity = MaxFallSpeed;
            }
        }

        private void FixSlope()
        {
            if (GlobalPosition.DistanceTo(prevPosition) < 0.1f)
            {
                GlobalPosition = prevPosition;
            }
        }
    }

}
