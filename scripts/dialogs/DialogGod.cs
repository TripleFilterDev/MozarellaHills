using Godot;
using System;

namespace MozarellaHills
{
    public class DialogGod : DialogBase
    {
        public override Participant[] Participants => new[] 
            {
                new Participant("CH_MAIN", Color.Color8(84, 78, 104), new Vector2(-32, -16)),
                new Participant("CH_GOD", Color.Color8(32, 60, 86), new Vector2(32, -16))
            };
        

        protected override void Scenario()
        {
            Speech(1, "DL_0_0");
            Speech(0, "DL_0_1");
            Speech(1, "DL_0_2");
            Speech(0, "DL_0_3");
            Speech(1, "DL_0_4");
            Speech(0, "DL_0_5");
            Speech(1, "DL_0_6");
            Speech(0, "DL_0_7");
            Speech(1, "DL_0_8");
            Speech(0, "DL_0_9");
            Speech(1, "DL_0_10");
            Speech(1, "DL_0_11");
            Speech(1, "DL_0_12");
            Speech(1, "DL_0_13");
            Speech(0, "DL_0_14");
            Speech(1, "DL_0_15");
            Speech(1, "DL_0_16");
            Speech(1, "DL_0_17");
            Speech(0, "DL_0_18");
            Speech(1, "DL_0_19");
            Speech(0, "DL_0_20");
            Speech(1, "DL_0_21");
            Speech(1, "DL_0_22");
            Speech(1, "DL_0_23");
            Speech(0, "DL_0_24");
            Speech(1, "DL_0_25");
        }

        protected override void End()
        {
            GameState.SavedPosition = new Vector2(-592, -352);
            GameState.SavedScene = "res://scenes/cave.lvl.tscn";

            GlobalTransition.Instance.ChangeScene(GameState.SavedScene);

            GameState.Save();
            GlobalAlert.Show(true, "AL_SAVE");
        }

        protected override void HandleActions(int action)
        {
            
        }
    }
}