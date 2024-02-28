using Godot;
using System;

namespace MozarellaHills
{
    public class DialogCasual : DialogBase
    {
        public override Participant[] Participants => new[] 
            {
                new Participant("CH_MAIN", Color.Color8(84, 78, 104), new Vector2(-32, -16)),
                new Participant("CH_CASUAL", Color.Color8(32, 60, 86), new Vector2(32, -16))
            };
        

        protected override void Scenario()
        {
            if (GameState.Saved.WearJacket)
            {
                Speech(0, "DL_3_0");
                Speech(1, "DL_3_1");
                Speech(0, "DL_3_2");
                Speech(1, "DL_3_3");
                Speech(0, "DL_3_4");
                Speech(1, "DL_3_5");
                Speech(1, "DL_3_6");
                Speech(0, "DL_3_7");
                Speech(1, "DL_3_8");
                Speech(1, "DL_3_9");
                Speech(0, "DL_3_10");
                Speech(1, "DL_3_11");
                Speech(1, "DL_3_12");
            }
            else
            {
                Speech(0, "DL_2_0");
                Speech(1, "DL_2_1");
                Speech(0, "DL_2_2");
                Speech(1, "DL_2_3");
                Speech(0, "DL_2_4");
                Speech(1, "DL_2_5");
                Speech(0, "DL_2_6");
                Speech(1, "DL_2_7");
                Speech(1, "DL_2_8");
                Speech(0, "DL_2_9");
                Speech(1, "DL_2_10");
                Speech(1, "DL_2_11");
                Speech(0, "DL_2_12");
                Speech(1, "DL_2_13");
            }
        }

        protected override void End()
        {
            if (GameState.Saved.WearJacket)
            {
                GameState.Current.AdvancementDisco = true;

                GameState.SavedPosition = new Vector2(-568, -352);
                GameState.SavedScene = "res://scenes/disco.lvl.tscn";

                GlobalTransition.Instance.ChangeScene(GameState.SavedScene);

                GameState.Save();
                GlobalAlert.Show(true, "AL_SAVE");
            }
            else
            {
                GameState.SavedPosition = new Vector2(216, -8);
                GameState.SavedScene = "res://scenes/desert.lvl.tscn";

                GlobalTransition.Instance.ChangeScene(GameState.SavedScene);

                GameState.Save();
                GlobalAlert.Show(true, "AL_SAVE");
            }
            
        }

        protected override void HandleActions(int action)
        {
            
        }
    }
}