using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MozarellaHills
{
    public class MobJackal : MobBase
    {
        public override float TickTime => 1.0f / 2.0f;

        public float MoveSpeed => 20.0f;
        public float RunSpeed => 50.0f;

        private bool isAgressive = true;
        private bool viewCharacter = false;

        private PlatformCharacter character = null;

        private HorizontalDirection direction = 0;
        private int tickDo = 0;

        private Vector2 targetPos = Vector2.Zero;
        private ObjectCheese cheeseBody = null;
        private List<ObjectCheese> cheeseList = new List<ObjectCheese>();

        private float verticalVelocity = 0;
        private bool isLive = true;
        private bool catched = false;

        private AnimatedSprite animatedJackal;
        private Area2D areaCheck;
        private RayCast2D rayCharacter;
        private Area2D areaKill;
        
        protected override void Start()
        {
            animatedJackal = (AnimatedSprite) GetNode("AnimatedJackal");
            areaCheck = (Area2D) GetNode("AreaCheck");
            rayCharacter = (RayCast2D) GetNode("RayCharacter");
            areaKill = (Area2D) GetNode("AreaKill");

            areaCheck.Connect("body_entered", this, "OnBodyEntered");
            areaCheck.Connect("body_exited", this, "OnBodyExited");

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
            UpdateVerticalVelocity();

            MoveAndSlide(new Vector2((int) direction * (viewCharacter ? RunSpeed : MoveSpeed), verticalVelocity), Vector2.Up);
        }

        protected override void Tick()
        {
            if (!isLive) return;

            tickDo -= 1;

            if (!isAgressive && cheeseBody != null)
            {
                //if (Mathf.Abs(cheeseBody.GlobalPosition.x - this.GlobalPosition.x) < 10.0f)
                if (cheeseBody.GlobalPosition.DistanceTo(this.GlobalPosition) < 20.0f)
                {
                    direction = HorizontalDirection.None;
                }
                else if (cheeseBody.GlobalPosition.x < this.GlobalPosition.x)
                {
                    direction = HorizontalDirection.Left;
                }
                else
                {
                    direction = HorizontalDirection.Right;
                }

                tickDo = 0;

                UpdateView();
            }
            else if (isAgressive && viewCharacter && character != null)
            {
                rayCharacter.CastTo = (character.GlobalPosition - this.GlobalPosition) * 1.5f;

                if (character.GlobalPosition.DistanceTo(this.GlobalPosition) < 20.0f)
                {
                    direction = HorizontalDirection.None;
                }
                else if (character.GlobalPosition.x < this.GlobalPosition.x && rayCharacter.GetCollider() is PlatformCharacter)
                {
                    direction = HorizontalDirection.Left;
                }
                else if (character.GlobalPosition.x >= this.GlobalPosition.x && rayCharacter.GetCollider() is PlatformCharacter)
                {
                    direction = HorizontalDirection.Right;
                }

                tickDo = 0;

                if (!catched && rayCharacter.GetCollider() is PlatformCharacter && character.GlobalPosition.DistanceTo(this.GlobalPosition) < 41.0f)
                {
                    catched = true;
                    character.Catch();
                }

                UpdateView();
            }
            else if (tickDo <= 0)
            {
                SetRandomTick();
                SetRandomDirection();
            }
        }

        private void UpdateVerticalVelocity()
        {
            verticalVelocity += 20.0f;
            if (verticalVelocity > 200.0f)
            {
                verticalVelocity = 200.0f;
            }
            if (IsOnFloor())
            {
                verticalVelocity = 1.0f;
            }
        }

        public override void Turn(HorizontalDirection dir)
        {
            if (!isLive) return;

            if (isAgressive)
            {
                tickDo = (((int)GD.Randi() % 4) + 4) * 5;
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
                animatedJackal.Animation = "default";
            }
            else if (direction == HorizontalDirection.Left)
            {
                animatedJackal.FlipH = true;
                animatedJackal.Animation = "run";
            }
            else if (direction == HorizontalDirection.Right)
            {
                animatedJackal.FlipH = false;
                animatedJackal.Animation = "run";
            }
        }

        private void OnBodyEntered(Node body)
        {
            if (body is ObjectCheese)
            {
                isAgressive = false;
                cheeseBody = (ObjectCheese) body;
                cheeseList.Add(cheeseBody);
            }

            if (body is PlatformCharacter)
            {
                rayCharacter.Enabled = true;
                viewCharacter = true;
                character = (PlatformCharacter) body;
            }
        }

        private void OnBodyExited(Node body)
        {
            if (body is ObjectCheese)
            {
                //isAgressive = true;
                cheeseList.Remove((ObjectCheese) body);
                if (cheeseList.Count > 0)
                {
                    cheeseBody = cheeseList[cheeseList.Count-1];
                }
                else
                {
                    isAgressive = true;
                    cheeseBody = null;
                }
            }

            if (body is PlatformCharacter)
            {
                rayCharacter.Enabled = false;
                viewCharacter = false;
                character = null;
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
            animatedJackal.Animation = "dead";
            direction = HorizontalDirection.None;
            isLive = false;

            GameState.Current.AdvancementSinless = false;
        }
    }
}