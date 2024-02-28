using Godot;
using System;

namespace MozarellaHills
{
    public class MobCheeseguard : MobBase
    {
        public override float TickTime => 1.0f / 2.0f;

        public float MoveSpeed => 10.0f;
        public float NoticedSpeed => 100.0f;

        private bool isNoticed = false;
        public bool IsNoticed => isNoticed;

        private bool characterCaught = false;

        private HorizontalDirection direction = 0;

        private AnimatedSprite animatedCheeseguard;
        private Area2D areaRangeLeft;
        private Area2D areaRangeRight;
        private Sprite spriteRange;

        private PlatformCharacter character;

        private float targetRangeScale = 0;
        private float rangeScale = 0;

        private float verticalVelocity = 0.0f;

        private bool inAreaLeft = false;
        private bool inAreaRight = false;

        private bool catched = false;
        
        protected override void Start()
        {
            animatedCheeseguard = (AnimatedSprite) GetNode("AnimatedCheeseguard");
            areaRangeLeft = (Area2D) GetNode("AreaRangeLeft");
            areaRangeRight = (Area2D) GetNode("AreaRangeRight");
            spriteRange = (Sprite) GetNode("SpriteRange");

            areaRangeLeft.Connect("body_entered", this, "OnBodyRangeEntered", new Godot.Collections.Array(HorizontalDirection.Left));
            areaRangeRight.Connect("body_entered", this, "OnBodyRangeEntered", new Godot.Collections.Array(HorizontalDirection.Right));
            areaRangeLeft.Connect("body_exited", this, "OnBodyRangeExited", new Godot.Collections.Array(HorizontalDirection.Left));
            areaRangeRight.Connect("body_exited", this, "OnBodyRangeExited", new Godot.Collections.Array(HorizontalDirection.Right));

            SetRandomDirection();
            spriteRange.Show();
        }

        protected override void Process(float delta)
        {
            rangeScale = Mathf.Lerp(rangeScale, targetRangeScale, 0.6f);

            spriteRange.Scale = new Vector2(rangeScale, 1);
        }

        protected override void PhysicsProcess(float delta)
        {
            UpdateVerticalVelocity();
            
            MoveAndSlide(new Vector2((int) direction * (IsNoticed ? NoticedSpeed : MoveSpeed), verticalVelocity), Vector2.Up);
        }

        protected override void Tick()
        {
            if (IsNoticed)
            {
                direction = character.GlobalPosition.x < this.GlobalPosition.x ? HorizontalDirection.Left : HorizontalDirection.Right;

                if (Mathf.Abs(Mathf.Max(character.GlobalPosition.x, this.GlobalPosition.x) - Mathf.Min(character.GlobalPosition.x, this.GlobalPosition.x)) < 20.0f)
                {
                    direction = HorizontalDirection.None;
                    characterCaught = true;
                }

                UpdateView();
            }

            if (inAreaLeft && direction == HorizontalDirection.Left)
            {
                isNoticed = true;

                if (!catched)
                {
                    character.Catch();
                    catched = true;
                }

                
            }

            if (inAreaRight && direction == HorizontalDirection.Right)
            {
                isNoticed = true;

                if (!catched)
                {
                    character.Catch();
                    catched = true;
                }
            }
        }

        public override void Turn(HorizontalDirection dir)
        {
            if (!characterCaught && !catched)
            {
                direction = dir;

                UpdateView();
            }
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
        }

        private void UpdateVerticalVelocity()
        {
            verticalVelocity += 20.0f;

            if (IsOnFloor())
            {
                verticalVelocity = 1.0f;
            }
            if (verticalVelocity > 200.0f)
            {
                verticalVelocity = 200.0f;
            }
        }

        private void UpdateView()
        {
            if (direction == HorizontalDirection.None)
            {
                animatedCheeseguard.Animation = "default";
                targetRangeScale = 0;
            }
            else if (direction == HorizontalDirection.Left && IsNoticed)
            {
                animatedCheeseguard.FlipH = true;
                animatedCheeseguard.Animation = "noticed";
                targetRangeScale = -1;
            }
            else if (direction == HorizontalDirection.Right && IsNoticed)
            {
                animatedCheeseguard.FlipH = false;
                animatedCheeseguard.Animation = "noticed";
                targetRangeScale = 1;
            }
            else if (direction == HorizontalDirection.Left)
            {
                animatedCheeseguard.FlipH = true;
                animatedCheeseguard.Animation = "run";
                targetRangeScale = -1;
            }
            else if (direction == HorizontalDirection.Right)
            {
                animatedCheeseguard.FlipH = false;
                animatedCheeseguard.Animation = "run";
                targetRangeScale = 1;
            }
        }

        private void OnBodyRangeEntered(Node body, HorizontalDirection dir)
        {
            if (body is PlatformCharacter)
            {
                if (dir == HorizontalDirection.Left) inAreaLeft = true;
                if (dir == HorizontalDirection.Right) inAreaRight = true;
                character = (PlatformCharacter) body;
            }
        }

        private void OnBodyRangeExited(Node body, HorizontalDirection dir)
        {
            if (body is PlatformCharacter)
            {
                if (dir == HorizontalDirection.Left) inAreaLeft = false;
                if (dir == HorizontalDirection.Right) inAreaRight = false;
                //character = (PlatformCharacter) body;
            }
        }
    }
}