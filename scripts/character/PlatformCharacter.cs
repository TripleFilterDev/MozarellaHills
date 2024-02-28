using Godot;
using System;
using System.Collections.Generic;

namespace MozarellaHills
{
    public class PlatformCharacter : KinematicBody2D
    {
        public static PlatformCharacter Instance = null;

        public enum Features
        {
            DoubleJump
        }

        public float MoveSpeed => 70.0f;
        public float JumpForce => 280.0f;
        public float Gravity => 20.0f;
        public float MaxFallSpeed => 380.0f;

        private float verticalVelocity = 0.0f;
        public float VerticalVelocity => verticalVelocity;

        private float acceleration = 0.0f;
        public float Acceleration => acceleration;
        public float AbsoluteAcceleration => Mathf.Abs(acceleration);

        public float AccelerationSpeed => 0.1f;
        public float DeccelerationSpeed => 0.3f;

        private bool grounded = false;
        public bool Grounded => grounded;

        public float GroundedVerticalVelocity => 4.0f;

        private bool canTake = false;
        public bool CanTake => canTake;

        private bool canControl = true;
        public bool CanControl
        {
            get => canControl;
            set {
                canControl = value;
            }
        }

        private bool doubleJumpCompleted = false;
        public bool CanDoubleJump => !doubleJumpCompleted && !Grounded && takenObject == TakenObjects.None;

        private List<ITaken> canTakeObjects = new List<ITaken>(); 
        private ITaken nearestObject = null;
        private TakenObjects takenObject = TakenObjects.None;
        private Vector2 dropOffset = new Vector2(0, -8);
        private Vector2 takeOffset = new Vector2(0, 8);

        public bool WithStone => takenObject == TakenObjects.Stone;

        private HorizontalDirection inputMoveDirection = HorizontalDirection.None;
        public HorizontalDirection InputMoveDirection => inputMoveDirection;

        private float deltaPhysicFrame = 0.0f;
        public float DeltaPhysicFrame => deltaPhysicFrame;

        private float deltaFrame = 0.0f;
        public float DeltaFrame => deltaFrame;

        private int lastFrame = 0;
        private int currentFrame = 0;

        [Export]
        private bool cantDrop = false;

        private bool wearJacket = false;
        public bool WearJacket 
        {
            get => wearJacket;
            set {
                wearJacket = value;
                animatedWearJacket.Visible = value;
            }
        }

        private bool wearCrown = false;
        public bool WearCrown 
        {
            get => wearCrown;
            set {
                wearCrown = value;
                animatedWearCrown.Visible = value;
            }
        }

        private Vector2 impulseVector = Vector2.Zero;
        private Vector2 ImpulseVector => impulseVector;

        public float CoefMoveWithObject
        {
            get {
                switch (takenObject)
                {
                    case TakenObjects.Stone: return 0.3f;
                    case TakenObjects.Cobblestone: return 0.6f;
                    default: return 1.0f;
                }
            }
        }

        public float CoefJumpWithObject
        {
            get {
                switch (takenObject)
                {
                    case TakenObjects.Stone: return 0.75f;
                    case TakenObjects.Cobblestone: return 0.98f;
                    default: return 1.0f;
                }
            }
        }

        //private int cheeseCount = 0;
        //public int CheeseCount => cheeseCount;

        private List<Features> featuresList = new List<Features>();
        public Features[] FeaturesList => featuresList.ToArray();

        Area2D areaTakeStone;
        AnimatedSprite animatedCharacter;
        AnimatedSprite animatedDead;
        CollisionShape2D collision;
        AudioStreamPlayer playerSteps;
        AudioStreamPlayer playerJump;
        Camera2D camera;

        AnimatedSprite animatedWearJacket;
        AnimatedSprite animatedWearCrown;

        Sprite spriteStone;
        Sprite spriteCobblestone;

        public override void _Ready()
        {
            Instance = this;

            areaTakeStone = (Area2D) GetNode("AreaTake");
            animatedCharacter = (AnimatedSprite) GetNode("AnimatedCharacter");
            animatedDead = (AnimatedSprite) GetNode("AnimatedDead");
            collision = (CollisionShape2D) GetNode("Collision");
            playerSteps = (AudioStreamPlayer) GetNode("PlayerSteps");
            playerJump = (AudioStreamPlayer) GetNode("PlayerJump");
            camera = (Camera2D) GetNode("Camera");

            animatedWearJacket = (AnimatedSprite) GetNode("AnimatedCharacter/AnimatedWearJacket");
            animatedWearCrown = (AnimatedSprite) GetNode("AnimatedCharacter/AnimatedWearCrown");

            spriteStone = (Sprite) GetNode("NodeTaken/SpriteStone");
            spriteCobblestone = (Sprite) GetNode("NodeTaken/SpriteCobblestone");

            areaTakeStone.Connect("body_entered", this, "OnAreaTakeEntered");
            areaTakeStone.Connect("body_exited", this, "OnAreaTakeExited");

            animatedWearJacket.Hide();
            animatedWearCrown.Hide();

            spriteStone.Hide();
            spriteCobblestone.Hide();

            animatedDead.Hide();

            if (FuncCameraBorders.Instance != null)
            {
                camera.LimitLeft = FuncCameraBorders.Instance.LimitLeft;
                camera.LimitRight = FuncCameraBorders.Instance.LimitRight;
                camera.LimitBottom = FuncCameraBorders.Instance.LimitBottom;
                camera.LimitTop = FuncCameraBorders.Instance.LimitTop;
            }

            if (FuncPositioning.Instance != null && !GameState.StartGame)
            {
                this.GlobalPosition = FuncPositioning.Instance.NewCharacterPos;
            }

            GameState.StartGame = false;

            takenObject = TakenObjects.Stone;
            ViewObject(takenObject);

            camera.MakeCurrent();
            //cheeseCount = GameState.Cheeses;

            if (GlobalAlert.Instance != null)
            {
                //GlobalAlert.Show(false, GetTree().GetCurrentScene().Filename);
            }

            if (GameState.Saved.WearJacket) WearJacket = true;
            if (GameState.Saved.WearCrown)  WearCrown  = true;
            //if (GameState.Saved.WearCrown) W= true;

            //WearJacket = false;
            //WearCrown = true;
        }

        public override void _PhysicsProcess(float delta)
        {
            deltaPhysicFrame = delta;

            UpdateInputMoveDirection();
            UpdateDoubleJump();
            UpdateVerticalVelocity();
            UpdateGrounded();
            UpdateDownSliding();
            UpdateAcceleration();
            UpdateNearest();
            UpdateTake();
            UpdateAnimation();
            UpdateParallaxBackground();
            UpdateLastFrame();
            UpdateStepSound();
            UpdateDropCheese();
            UpdateImpulseVector();
            CheckStuck();
            
            MoveAndSlide(new Vector2(AbsoluteAcceleration * ((int) InputMoveDirection) * MoveSpeed * CoefMoveWithObject, VerticalVelocity) + impulseVector, Vector2.Up);

            //GD.Print(GlobalPosition);

            //GD.Print(canTakeObjects.ToArray());
        }

        public override void _Process(float delta)
        {
            deltaFrame = delta;
        }

        public void AddFeature(Features feature)
        {
            if (!featuresList.Contains(feature))
            {
                featuresList.Add(feature);
            }
        }

        public void RemoveFeature(Features feature)
        {
            if (featuresList.Contains(feature))
            {
                featuresList.Remove(feature);
            }
        }

        private void UpdateInputMoveDirection()
        {
    
            var dir = 0;

            if (Input.IsActionPressed("ch_mv_left") && CanControl) dir -= 1;
            if (Input.IsActionPressed("ch_mv_right") && CanControl) dir += 1;

            inputMoveDirection = (HorizontalDirection) dir;
        }

        private void UpdateGrounded()
        {
            grounded = IsOnFloor();
        }

        private void UpdateDoubleJump()
        {
            if (Grounded) doubleJumpCompleted = false;
        }

        private void UpdateVerticalVelocity()
        {
            verticalVelocity += Gravity;
            if (Grounded && Input.IsActionJustPressed("ch_jump") && CanControl)
            {
                verticalVelocity = -JumpForce * CoefJumpWithObject;

                playerJump.PitchScale = 1.0f;
                playerJump.Play();
            }
            else if (CanDoubleJump && Input.IsActionJustPressed("ch_jump") && CanControl)
            {
                verticalVelocity = -JumpForce * CoefJumpWithObject;
                doubleJumpCompleted = true;

                playerJump.PitchScale = 1.2f;
                playerJump.Play();
            }

            if (Grounded && verticalVelocity >= 0)
            {
                verticalVelocity = GroundedVerticalVelocity;
            }
            if (verticalVelocity > MaxFallSpeed)
            {
                verticalVelocity = MaxFallSpeed;
            }
        }

        private void UpdateImpulseVector()
        {
            impulseVector = new Vector2(Mathf.Lerp(impulseVector.x, 0, 0.15f), Mathf.Lerp(impulseVector.y, 0, 0.15f));
        }

        private void UpdateAcceleration()
        {
            if (InputMoveDirection == HorizontalDirection.Left)
            {
                acceleration = Mathf.Lerp(acceleration, -1.0f, AccelerationSpeed);
            }
            else if (InputMoveDirection == HorizontalDirection.Right)
            {
                acceleration = Mathf.Lerp(acceleration, 1.0f, AccelerationSpeed);
            }
            else if (InputMoveDirection == HorizontalDirection.None)
            {
                acceleration = Mathf.Lerp(acceleration, 0.0f, DeccelerationSpeed);
            }
        }

        private void UpdateTake()
        {
            if (!CanControl) return;
            if (cantDrop) return;

            if (Input.IsActionJustPressed("ch_take"))
            {   
                if (nearestObject != null ? nearestObject.ObjectType == TakenObjects.Cheese : false && CanTake)
                {
                    canTakeObjects.Remove(nearestObject);
                    nearestObject.Take();
                    nearestObject = null;
                    
                    GameState.Current.CheesesCount += 1;
                }
                else if (takenObject != TakenObjects.None)
                {
                    DropObject();
                }
                else if (CanTake)
                {
                    takenObject = nearestObject.ObjectType;
                    canTakeObjects.Remove(nearestObject);
                    nearestObject.Take();
                    nearestObject = null;

                    ViewObject(takenObject);
                }
                else
                {
                    DropObject();
                }
            }
        }

        private void UpdateDropCheese()
        {
            if (!CanControl) return;

            if (!Input.IsActionJustPressed("ch_drop_cheese")) return;

            if (GameState.Current.CheesesCount > 0)
            {
                GameState.Current.CheesesCount -= 1;

                if (FuncObjectSpawner.Instance != null)
                {
                    FuncObjectSpawner.Instance.SpawnObject(TakenObjects.Cheese, GlobalPosition + dropOffset);
                }
            }
        }

        private void UpdateNearest()
        {
            if (!canTake) return;

            ITaken near = null;
            float dist = 280.0f;

            foreach (var taken in canTakeObjects)
            {
                if ((taken.GlobalPosition).DistanceTo(GlobalPosition + takeOffset) < dist)
                {
                    dist = taken.GlobalPosition.DistanceTo(GlobalPosition);
                    near = taken;
                }
            }

            if (nearestObject != null) nearestObject.ToggleNearest(false);
            //if (near != null) nearestObject = near; else nearestObject = null;
            nearestObject = near;
            if (nearestObject != null) nearestObject.ToggleNearest(true);
        }

        private void UpdateAnimation()
        {
            if (inputMoveDirection == HorizontalDirection.Right)
            {
                animatedCharacter.FlipH = false;
            }
            if (inputMoveDirection == HorizontalDirection.Left)
            {
                animatedCharacter.FlipH = true;
            }

            if (takenObject == TakenObjects.None)
            {
                if (inputMoveDirection != HorizontalDirection.None)
                {
                    animatedCharacter.Animation = "run";
                }
                else
                {
                    animatedCharacter.Animation = "default";
                }
            }
            else
            {
                if (inputMoveDirection != HorizontalDirection.None)
                {
                    animatedCharacter.Animation = "carry_run";
                }
                else
                {
                    animatedCharacter.Animation = "carry_stay";
                }
            }
            
        }

        private void UpdateStepSound()
        {   
            if (animatedCharacter.Animation == "run" && lastFrame != currentFrame && Grounded)
            {
                playerSteps.PitchScale = 1;
                playerSteps.Play();
            }
            if (animatedCharacter.Animation == "carry_run" && lastFrame != currentFrame && Grounded)
            {
                playerSteps.PitchScale = 1;
                playerSteps.Play();
            }
        }

        private void UpdateLastFrame()
        {
            lastFrame = currentFrame;
            currentFrame = animatedCharacter.Frame;
        }

        private void UpdateParallaxBackground()
        {
            if (FuncParallax.Instance == null) return;

            FuncParallax.Instance.ChangeParallax(camera.GetCameraScreenCenter().x, camera.GetCameraScreenCenter().y);
        }

        private void UpdateDownSliding()
        {
            if (Input.IsActionJustPressed("ch_slide") && Grounded && TestMove(Transform, Vector2.Down))
            {
                GlobalPosition += Vector2.Down;
            }
        }
        

        private void CheckStuck()
        {
            if (IsStuck())
            {  
                //GetTree().ReloadCurrentScene();
            }
        }

        private void OnAreaTakeEntered(Node body)
        {
            if (body is ITaken)
            {
                canTake = true;
                canTakeObjects.Add((ITaken) body);
                canTake = true;
            }
        }

        private void OnAreaTakeExited(Node body)
        {
            if (body is ITaken)
            {
                canTakeObjects.Remove((ITaken) body);
                if (nearestObject == (ITaken) body) nearestObject = null;
                ((ITaken) body).ToggleNearest(false);
                if (canTakeObjects.Count == 0) canTake = false;
                //UpdateNearest();
            }
        }

        private void ViewObject(TakenObjects obj)
        {
            if (obj == TakenObjects.None)
            {
                spriteStone.Hide();
                spriteCobblestone.Hide();
            }
            if (obj == TakenObjects.Stone)
            {
                spriteStone.Show();
            }
            if (obj == TakenObjects.Cobblestone)
            {
                spriteCobblestone.Show();
            }
        }

        public void DropObject()
        {
            if (takenObject == TakenObjects.None) return;

            if (FuncObjectSpawner.Instance != null)
            {
                FuncObjectSpawner.Instance.SpawnObject(takenObject, GlobalPosition + dropOffset);
            }

            takenObject = TakenObjects.None;
            ViewObject(takenObject);
        }

        public bool IsStuck()
        {
            int stucks = 0;

            if (TestMove(Transform, Vector2.Left)) stucks += 1;
            if (TestMove(Transform, Vector2.Right)) stucks += 1;
            if (TestMove(Transform, Vector2.Up)) stucks += 1;
            if (TestMove(Transform, Vector2.Down)) stucks += 1;

            return stucks == 4;
        }

        public void AddCheese()
        {
            //cheeseCount += 1;
            GameState.Current.CheesesCount += 1;
        }

        public void ApplyImpulse(Vector2 impulse)
        {
            impulseVector += impulse;
        }

        public void Catch()
        {
            canControl = false;

            //GameState.IsRevive = true;

            GlobalTransition.Instance.ChangeScene(GameState.SavedScene, Color.Color8(255, 236, 214));
        }

        public void Kill()
        {
            canControl = false;

            DropObject();

            animatedDead.FlipH = animatedCharacter.FlipH;
            animatedCharacter.Hide();
            animatedDead.Show();

            GlobalTransition.Instance.ChangeScene(GameState.SavedScene, Color.Color8(255, 236, 214));
        }
    }
}