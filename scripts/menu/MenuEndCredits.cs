using Godot;
using System;

namespace MozarellaHills
{
    public class MenuEndCredits : Node2D
    {
        private Timer timerEnd;
        private CanvasLayer canvasCredits;

        private float speedOffset = 12f;
        private float currOffset = 0;
        private float waitTime => 100.0f;

        public override void _Ready()
        {
            canvasCredits = (CanvasLayer) GetNode("CanvasCredits");
            timerEnd = (Timer) GetNode("TimerEnd");

            timerEnd.WaitTime = waitTime;
            timerEnd.Connect("timeout", this, "OnTimerTimeout");
            timerEnd.Start();
        }

        public override void _Process(float delta)
        {
            currOffset += speedOffset * delta;

            canvasCredits.Offset = Vector2.Up * currOffset;
        }

        private void OnTimerTimeout()
        {
            GlobalTransition.Instance.ChangeScene("res://scenes/menu/main.mn.tscn", Colors.Black);

            GameState.Default();
        }
    }
}