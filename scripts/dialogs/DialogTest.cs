using Godot;
using System;

namespace MozarellaHills
{
    public class DialogTest : DialogBase
    {
        public override Participant[] Participants => new[] 
            {
                new Participant("Green Man", Color.Color8(0, 255, 0), new Vector2(-31, -17)),
                new Participant("Dark Man", Color.Color8(255, 0, 0), new Vector2(31, -17))
            };
        

        protected override void Scenario()
        {
            Speech(0, "speech_1_1");
            Speech(1, "speech_1_2");
            Speech(0, "speech_1_3");
            Speech(0, "speech_1_4");
            Speech(1, "speech_1_5");
            Speech(0, "speech_1_6");
            Speech(1, "speech_1_7");
            Speech(0, "speech_1_8");
            Speech(1, "speech_1_9");
        }

        protected override void End()
        {
            
        }

        protected override void HandleActions(int action)
        {
            
        }
    }
}