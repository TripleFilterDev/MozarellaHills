using Godot;
using System;

namespace MozarellaHills
{
    public class DialogGoodEnd : DialogBase
    {
        public override Participant[] Participants => new[] 
            {
                new Participant("CH_MAIN", Color.Color8(84, 78, 104), new Vector2(-32, -16)),
                new Participant("CH_GIRLFRIEND", Color.Color8(32, 60, 86), new Vector2(32, -16)),
                new Participant("CH_SON", Color.Color8(32, 60, 86), new Vector2(24, -16))
            };
        

        protected override void Scenario()
        {
            Speech(2, "DL_10_0");
            Speech(2, "DL_10_1");
            Speech(0, "DL_10_2");
            Speech(0, "DL_10_3");
            Speech(1, "DL_10_4");
            Speech(1, "DL_10_5");
            Speech(0, "DL_10_6");
            Speech(0, "DL_10_7");
            Speech(2, "DL_10_8");
            Speech(1, "DL_10_9");
            Speech(2, "DL_10_10");
            Speech(1, "DL_10_11");
            Speech(0, "DL_10_12");
            Speech(1, "DL_10_13");
            Speech(1, "DL_10_14");
            Speech(1, "DL_10_15");
            Speech(1, "DL_10_16");
            Speech(1, "DL_10_17");
            Speech(0, "DL_10_18");
            Speech(2, "DL_10_19");
            Speech(2, "DL_10_20");
            Speech(1, "DL_10_21");
            Speech(1, "DL_10_22");
            Speech(1, "DL_10_23");
            Speech(1, "DL_10_24");
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