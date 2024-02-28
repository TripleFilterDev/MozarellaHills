using Godot;
using System;

namespace MozarellaHills
{
    public class DialogBoy : DialogBase
    {
        public override Participant[] Participants => new[] 
            {
                new Participant("CH_MAIN", Color.Color8(84, 78, 104), new Vector2(-32, -16)),
                new Participant("CH_SON", Color.Color8(32, 60, 86), new Vector2(32, -32))
            };
        

        protected override void Scenario()
        {
            Speech(0, "DL_5_0");
            Speech(1, "DL_5_1");
            Speech(0, "DL_5_2");
            Speech(1, "DL_5_3");
            Speech(0, "DL_5_4");
            Speech(1, "DL_5_5");
            Speech(1, "DL_5_6");
            Speech(1, "DL_5_7");
            Speech(1, "DL_5_8");
            Speech(0, "DL_5_9");
            Speech(1, "DL_5_10");
            Speech(0, "DL_5_11");
            Speech(1, "DL_5_12");
            Speech(1, "DL_5_13");
        }

        protected override void End()
        {
            GameState.SavedPosition = new Vector2(-584, -360);
            GameState.SavedScene = "res://scenes/dungeon.lvl.tscn";

            GlobalTransition.Instance.ChangeScene(GameState.SavedScene);

            GameState.Save();
            GlobalAlert.Show(true, "AL_SAVE");
        }

        protected override void HandleActions(int action)
        {
            
        }
    }
}