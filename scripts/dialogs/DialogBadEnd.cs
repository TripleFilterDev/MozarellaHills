using Godot;
using System;

namespace MozarellaHills
{
    public class DialogBadEnd : DialogBase
    {
        public override Participant[] Participants => new[] 
            {
                new Participant("CH_MAIN", Color.Color8(84, 78, 104), new Vector2(-40, 16)),
                new Participant("CH_GOD", Color.Color8(32, 60, 86), new Vector2(32, -24))
            };
        

        protected override void Scenario()
        {
            Speech(1, "DL_9_0");
            Speech(0, "DL_9_1");
            Speech(1, "DL_9_2");
            Speech(1, "DL_9_3");
            Speech(0, "DL_9_4");
            Speech(1, "DL_9_5");
            Speech(1, "DL_9_6");
            Speech(1, "DL_9_7");
            Speech(1, "DL_9_8");
            Speech(1, "DL_9_9");
            Speech(0, "DL_9_10");
            Speech(1, "DL_9_11");
            Speech(0, "DL_9_12");
            Speech(0, "DL_9_13");
            Speech(0, "DL_9_14");
            Speech(0, "DL_9_15");
            Speech(1, "DL_9_16");
            Speech(1, "DL_9_17");
            Speech(1, "DL_9_18");
            Speech(1, "DL_9_19");
        }

        protected override void End()
        {
            GameState.SavedPosition = new Vector2(0, 0);
            GameState.SavedScene = "res://scenes/menu/end_credits.mn.tscn";

            GlobalTransition.Instance.ChangeScene(GameState.SavedScene);

            GameState.Save();
            GlobalAlert.Show(true, "AL_SAVE");
        }

        protected override void HandleActions(int action)
        {
            
        }
    }
}