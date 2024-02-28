using Godot;
using System;

namespace MozarellaHills
{
    public class DialogGirlfriend : DialogBase
    {
        public override Participant[] Participants => new[] 
            {
                new Participant("CH_MAIN", Color.Color8(84, 78, 104), new Vector2(-32, -16)),
                new Participant("CH_GIRLFRIEND", Color.Color8(32, 60, 86), new Vector2(32, -16))
            };
        

        protected override void Scenario()
        {
            Speech(0, "DL_6_0");
            Speech(1, "DL_6_1");
            Speech(0, "DL_6_2");
            Speech(1, "DL_6_3");
            Speech(1, "DL_6_4");
            Speech(0, "DL_6_5");
            Speech(1, "DL_6_6");
            Speech(0, "DL_6_7");
            Speech(1, "DL_6_8");
            Speech(1, "DL_6_9");
            Speech(0, "DL_6_10");
            Speech(1, "DL_6_11");
            Speech(1, "DL_6_12");
            Speech(1, "DL_6_13");
            Speech(1, "DL_6_14");
            Speech(0, "DL_6_15");
            Speech(0, "DL_6_16");
            Speech(1, "DL_6_17");
            Speech(1, "DL_6_18");
            Speech(1, "DL_6_19");
            Speech(0, "DL_6_20");
            Speech(1, "DL_6_21");
        }

        protected override void End()
        {
            GameState.SavedPosition = new Vector2(-624, -216);
            GameState.SavedScene = "res://scenes/redemption.lvl.tscn";

            GlobalTransition.Instance.ChangeScene(GameState.SavedScene);

            GameState.Save();
            GlobalAlert.Show(true, "AL_SAVE");
        }

        protected override void HandleActions(int action)
        {
            
        }
    }
}