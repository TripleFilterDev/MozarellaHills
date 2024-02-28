using Godot;
using System;

namespace MozarellaHills
{
    public class ItemBase : Area2D
    {
        public virtual float DisappearTime => 4.0f;
        public virtual float HoverTime => 0.7f;
        public virtual string AnimationDefault => "default";
        public virtual string AnimationDisappear => "disappear";

        private bool hoverDir = false;
        private float hoverTarget;
        private float hoverStart = -1.0f;
        private float hoverEnd = 1.0f;

        private bool triggered = false;

        private AnimatedSprite animatedItem;
        private Timer timerDisappear;
        private Timer timerMove;
        private AudioStreamPlayer playerPickup;

        protected PlatformCharacter mainCharacter = null;

        protected virtual void Make()
        {

        }

        public override void _Ready()
        {
            animatedItem = (AnimatedSprite) GetNode("AnimatedItem");
            timerDisappear = (Timer) GetNode("TimerDisappear");
            timerMove = (Timer) GetNode("TimerMove");
            playerPickup = (AudioStreamPlayer) GetNode("PlayerPickup");

            this.Connect("body_entered", this, "OnBodyEntered");
            timerDisappear.Connect("timeout", this, "OnTimerTimeout");
            animatedItem.Position = Vector2.Down * hoverStart;
            
            timerDisappear.WaitTime = DisappearTime;
            animatedItem.Animation = AnimationDefault;

            timerMove.WaitTime = HoverTime;
            timerMove.Connect("timeout", this, "OnTimerMoveTimeout");

            hoverTarget = hoverStart;
            timerMove.Start();
        }

        public override void _Process(float delta)
        {
            if (triggered) return;

            /*if (hoverDir > 0)
            {
                hoverPos += hoverSpeed * delta;
                if (hoverPos > hoverEnd)
                {
                    hoverPos = hoverEnd;
                    hoverDir *= -1;
                }
            }
            else if (hoverDir < 0)
            {
                hoverPos -= hoverSpeed * delta;
                if (hoverPos < hoverStart)
                {
                    hoverPos = hoverStart;
                    hoverDir *= -1;
                }
            }*/



            //animatedItem.Position = Vector2.Down * hoverPos;
            animatedItem.Position = Vector2.Down * Mathf.Lerp(animatedItem.Position.y, hoverTarget, 0.1f);
        }

        private void OnBodyEntered(Node body)
        {
            if (body is PlatformCharacter && !triggered)
            {
                mainCharacter = (PlatformCharacter) body;
                triggered = true;
                animatedItem.Animation = AnimationDisappear;
                timerDisappear.Start();
                GD.Randomize();
                playerPickup.PitchScale = (float) GD.RandRange(0.8f, 1.2f);
                playerPickup.Play();
                Make();
            }
        }

        private void OnTimerTimeout()
        {
            QueueFree();
        }

        private void OnTimerMoveTimeout()
        {
            hoverDir = !hoverDir;
            hoverTarget = hoverDir ? hoverStart : hoverEnd;
            timerMove.Start();
        }
    }
}