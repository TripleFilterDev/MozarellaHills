using Godot;
using System;

namespace MozarellaHills
{
    public class DialogGod2 : DialogBase
    {
        public override Participant[] Participants => new[] 
            {
                new Participant("CH_MAIN", Color.Color8(84, 78, 104), new Vector2(-32, -16)),
                new Participant("CH_GOD", Color.Color8(32, 60, 86), new Vector2(32, -16))
            };
        

        protected override void Scenario()
        {
            Speech(1, "DL_1_0");
            Speech(1, "DL_1_1");
            Speech(0, "DL_1_2");
            Speech(1, "DL_1_3");
            Speech(1, "DL_1_4");
            Speech(1, "DL_1_5");
            Speech(0, "DL_1_6");
            Speech(1, "DL_1_7");
            Speech(0, "DL_1_8");
            Speech(1, "DL_1_9");
            Speech(1, "DL_1_10");
            Speech(0, "DL_1_11");
            Speech(1, "DL_1_12");
        }

        protected override void End()
        {
            GameState.SavedPosition = new Vector2(-648, 568);
            GameState.SavedScene = "res://scenes/final.lvl.tscn";

            GlobalTransition.Instance.ChangeScene(GameState.SavedScene);

            GameState.Save();
            GlobalAlert.Show(true, "AL_SAVE");
        }

        protected override void HandleActions(int action)
        {
            
        }
    }
}