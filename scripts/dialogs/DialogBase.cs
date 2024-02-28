using Godot;
using System;
using System.Collections.Generic;

namespace MozarellaHills
{
    public class DialogBase : Node2D
    {
        public virtual Participant[] Participants { get; }
        
        private List<(Participant, string)> speeches = new List<(Participant, string)>();
        public List<(Participant, string)> Speeches => speeches;

        Label labelParticipant;
        RichTextLabel richSpeech;
        Camera2D cameraDialog;

        int currentSpeech = -1;

        protected virtual void Scenario()
        {

        }

        protected virtual void End()
        {

        }

        protected virtual void HandleActions(int action)
        {

        }

        protected void Speech(int person, string say)
        {
            speeches.Add((Participants[person], say));
        }

        protected void Action(int action)
        {
            speeches.Add((new Participant(action), ""));
        }

        public override void _Ready()
        {
            labelParticipant = (Label) GetNode("CanvasDialog/ControlDialog/RectSpeech/LabelParticipant");
            richSpeech = (RichTextLabel) GetNode("CanvasDialog/ControlDialog/RectSpeech/RichSpeech");
            cameraDialog = (Camera2D) GetNode("CameraDialog");

            Scenario();
            NextSpeech();
        }

        public override void _Process(float delta)
        {
            
        }

        public override void _PhysicsProcess(float delta)
        {
            if (Input.IsActionJustPressed("dlg_skip"))
            {
                NextSpeech();
            }
        }

        private void NextSpeech()
        {
            GetNode<AudioStreamPlayer>("CanvasDialog/PlayerSkip").Play();
            currentSpeech += 1;

            if (currentSpeech >= Speeches.Count)
            {
                End();
                return;
            }

            var participant = Speeches[currentSpeech].Item1;
            var say = Speeches[currentSpeech].Item2;

            if (participant.IsAction)
            {
                HandleActions(participant.ActionId);
            }
            else
            {
                labelParticipant.Text = Tr(participant.Name);
                labelParticipant.AddColorOverride("font_color", participant.NameColor);
                richSpeech.BbcodeText = Tr(say);

                cameraDialog.GlobalPosition = participant.Position;
            }
        }
    }

    public class Participant
    {
        public readonly string Name;
        public readonly Color NameColor;
        public readonly Vector2 Position;
        public readonly bool IsAction;
        public readonly int ActionId;

        public Participant(string name, Color nameColor, Vector2 position)
        {
            Name = name;
            NameColor = nameColor;
            Position = position;
            IsAction = false;
        }

        public Participant(int actionId)
        {
            IsAction = true;
            ActionId = actionId;
        }
    }
}