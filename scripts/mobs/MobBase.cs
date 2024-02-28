using Godot;
using System;

namespace MozarellaHills
{
    public class MobBase : KinematicBody2D
    {
        public virtual float TickTime => 1.0f / 20.0f;

        Timer tickTimer;

        public override void _Ready()
        {
            tickTimer = new Timer();
            
            tickTimer.WaitTime = TickTime;
            tickTimer.Connect("timeout", this, "OnTickTimerTimeout");
            
            AddChild(tickTimer);

            tickTimer.Start();

            Start();
        }

        public override void _Process(float delta)
        {
            Process(delta);
        }

        public override void _PhysicsProcess(float delta)
        {
            PhysicsProcess(delta);
        }

        protected virtual void Start()
        {

        }

        protected virtual void Process(float delta)
        {

        }

        protected virtual void PhysicsProcess(float delta)
        {
            
        }

        protected virtual void Tick()
        {

        }

        public virtual void Turn(HorizontalDirection dir)
        {

        }

        private void OnTickTimerTimeout()
        {
            Tick();
            tickTimer.Start();
        }
    }
}