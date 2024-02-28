using Godot;
using System;

namespace MozarellaHills
{
    public class GlobalAlert : Node2D
    {
        public static GlobalAlert Instance = null;

        private Label labelAlert;

        private float disappearCur = 1.0f;
        private readonly float disappearStep = 0.9f;

        public static void Show(bool localized, string text)
        {
            if (Instance == null) return;

            Instance.labelAlert.Text = localized ? Instance.Tr(text) : text;
            Instance.disappearCur = 1.0f;
        }

        public override void _Ready()
        {
            Instance = this;

            labelAlert = (Label) GetNode("CanvasAlert/ControlAlert/LabelAlert");
        }

        public override void _Process(float delta)
        {
            disappearCur -= delta * disappearStep;

            if (disappearCur < 0.0f) disappearCur = 0.0f;

            labelAlert.Modulate = new Color(labelAlert.Modulate.r, labelAlert.Modulate.g, labelAlert.Modulate.b, disappearCur);
        } 
    }
}