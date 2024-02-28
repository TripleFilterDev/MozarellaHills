using Godot;
using System;

namespace MozarellaHills
{
    public class MenuDevelopers : Node2D
    {
        public override void _Ready()
        {
            Timer timer = new Timer();
            AddChild(timer);
            timer.Connect("timeout", this, "OnTimerTimeout");
            timer.WaitTime = 2.5f;
            timer.Start();
        }

        public void OnTimerTimeout()
        {
            GlobalTransition.Instance.ChangeScene("res://scenes/menu/main.mn.tscn");
        }
    }
}