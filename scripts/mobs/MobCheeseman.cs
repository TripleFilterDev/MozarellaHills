using Godot;
using System;

namespace MozarellaHills
{
    public class MobCheeseman : MobBase
    {
        public override float TickTime => 1.0f / 2.0f;

        public float MoveSpeed => 10.0f;

        private HorizontalDirection direction = 0;
        private int tickDo = 0;
        private bool isLive = true;

        private AnimatedSprite animatedCheeseman;

        private Area2D areaKill;
        
        protected override void Start()
        {
            animatedCheeseman = (AnimatedSprite) GetNode("AnimatedCheeseman");

            areaKill = (Area2D) GetNode("AreaKill");

            areaKill.Connect("body_entered", this, "OnBodyKillEntered");
            areaKill.Connect("body_exited", this, "OnBodyKillExited");

            SetRandomTick();
            SetRandomDirection();
        }

        protected override void Process(float delta)
        {

        }

        protected override void PhysicsProcess(float delta)
        {
            MoveAndSlide(new Vector2((int) direction * MoveSpeed, 1.0f), Vector2.Up);
        }

        protected override void Tick()
        {
            tickDo -= 1;

            if (tickDo <= 0)
            {
                if (isLive)
                {
                    SetRandomTick();
                    SetRandomDirection();
                }
            }
        }

        public override void Turn(HorizontalDirection dir)
        {
            tickDo = (((int)GD.Randi() % 4) + 4) * 5;
            direction = dir;

            UpdateView();
        }

        private void SetRandomDirection()
        {
            GD.Randomize();

            if (direction == HorizontalDirection.None)
            {
                switch (GD.Randi() % 2)
                {
                    case 0: direction = HorizontalDirection.Left; break;
                    case 1: direction = HorizontalDirection.Right; break;
                }
            }
            else
            {
                direction = HorizontalDirection.None;
            }
            
            UpdateView();

            //direction = (HorizontalDirection) (((int)(GD.Randi() % 3)) - 1);
        }

        private void SetRandomTick()
        {
            GD.Randomize();

            tickDo = (((int)GD.Randi() % 10) + 2) * 10;
        }

        private void UpdateView()
        {
            if (direction == HorizontalDirection.None)
            {
                animatedCheeseman.Animation = "default";
            }
            else if (direction == HorizontalDirection.Left)
            {
                animatedCheeseman.FlipH = true;
                animatedCheeseman.Animation = "run";
            }
            else if (direction == HorizontalDirection.Right)
            {
                animatedCheeseman.FlipH = false;
                animatedCheeseman.Animation = "run";
            }
        }

        private void OnBodyKillEntered(Node body)
        {
            if (body is ObjectCobblestone)
            {
                var obj = (ObjectCobblestone) body;

                if (obj.VerticalVelocity > 10.0f && obj.IsFly)
                {
                    Kill();
                }
            }

            if (body is ObjectStone)
            {
                var obj = (ObjectStone) body;

                if (obj.VerticalVelocity > 10.0f && obj.IsFly)
                {
                    Kill();
                }
            }
        }

        private void OnBodyKillExited(Node body)
        {
            
        }

        public void Kill()
        {
            animatedCheeseman.Animation = "dead";
            direction = HorizontalDirection.None;
            isLive = false;

            GameState.Current.AdvancementSinless = false;
        }
    }
}