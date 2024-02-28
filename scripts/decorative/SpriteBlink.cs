using Godot;
using System;

namespace MozarellaHills
{
    public class SpriteBlink : Sprite
    {
        private Timer timer;

        public override void _Ready()
        {
            timer = new Timer();
            AddChild(timer);
            GD.Randomize();
            timer.WaitTime = (float) GD.RandRange(0.4f, 1.2f);
            timer.Connect("timeout", this, "OnTimerTimeout");
            timer.Start();

            GD.Randomize();
            Modulate = new Color((float) GD.RandRange(0f, 1f), (float) GD.RandRange(0f, 1f), (float) GD.RandRange(0f, 1f), 0.4f);
        }

        private void OnTimerTimeout()
        {
            GD.Randomize();
            timer.WaitTime = (float) GD.RandRange(0.4f, 1.2f);
            timer.Start();
            Modulate = new Color((float) GD.RandRange(0f, 1f), (float) GD.RandRange(0f, 1f), (float) GD.RandRange(0f, 1f), 0.4f);
        }
    }
}