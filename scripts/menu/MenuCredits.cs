using Godot;
using System;

namespace MozarellaHills
{
    public class MenuCredits : Node2D
    {
        private RichTextLabel richCredits;

        private Button buttonReturn;

        public override void _Ready()
        {
            buttonReturn = (Button) GetNode("CanvasMenu/ControlMenu/ButtonReturn");

            buttonReturn.Connect("pressed", this, "OnButtonReturnPressed");

            buttonReturn.GrabFocus();
        }

        private void OnButtonReturnPressed()
        {
            GlobalTransition.Instance.ChangeScene("res://scenes/menu/main.mn.tscn");
        }

    }
}