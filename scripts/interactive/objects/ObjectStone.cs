using Godot;
using System;

namespace MozarellaHills
{
    public class ObjectStone : KinematicBody2D, ITaken
    {
        public TakenObjects ObjectType => TakenObjects.Stone;

        public bool IsItem => false;

        public void ToggleNearest(bool can)
        {
            //Modulate = can ? Color.Color8(255, 170, 94) : Color.Color8(255, 255, 255);
            spriteOutline.Visible = can;
        }

        public void Take()
        {
            QueueFree();
        }

        public float Gravity => 20.0f;
        public float MaxFallSpeed = 420.0f;
        
        private float verticalVelocity = 0.0f;
        public float VerticalVelocity => verticalVelocity;

        private Vector2 prevPosition = Vector2.Zero;
        public Vector2 PrevPosition => prevPosition;
        public float PosDiff => prevPosition.DistanceTo(GlobalPosition);

        private bool isFly = true;
        public bool IsFly => isFly;

        private Sprite spriteOutline;
        private Area2D areaFly;

        public override void _Ready()
        {
            spriteOutline = (Sprite) GetNode("SpriteOutline");
            areaFly = (Area2D) GetNode("AreaFly");

            spriteOutline.Hide();

            areaFly.Connect("body_entered", this, "OnBodyFlyEntered");
            areaFly.Connect("body_exited", this, "OnBodyFlyExited");
        }

        public override void _PhysicsProcess(float delta)
        {
            MoveAndSlide(new Vector2(0, verticalVelocity));

            UpdateVerticalVelocity();
            FixSlope();

            prevPosition = GlobalPosition;
        }

        private void UpdateVerticalVelocity()
        {
            verticalVelocity += Gravity;
            if (IsOnFloor() || !IsFly)
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

        private void OnBodyFlyEntered(Node body)
        {
            if (body is TileMap || body is ITaken)
            {
                isFly = false;
            }
        }

        private void OnBodyFlyExited(Node body)
        {
            if (body is TileMap || body is ITaken)
            {
                isFly = true;
            }
        }
    }

}
