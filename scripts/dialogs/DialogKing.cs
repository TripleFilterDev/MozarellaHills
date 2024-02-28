using Godot;
using System;

namespace MozarellaHills
{
    public class DialogKing : DialogBase
    {
        public override Participant[] Participants => new[] 
            {
                new Participant("CH_MAIN", Color.Color8(84, 78, 104), new Vector2(32, -16)),
                new Participant("CH_KING", Color.Color8(32, 60, 86), new Vector2(-32, -16))
            };
        

        protected override void Scenario()
        {
            GameState.Current.AdvancementKingTalking = true;
            GameState.Current.WearCrown = true;

            Speech(0, "DL_11_0");
            Speech(1, "DL_11_1");
            Speech(1, "DL_11_2");
            Speech(0, "DL_11_3");
            Speech(1, "DL_11_4");
            Speech(0, "DL_11_5");
            Speech(1, "DL_11_6");
            Speech(0, "DL_11_7");
            Speech(1, "DL_11_8");
            Speech(0, "DL_11_9");
            Speech(0, "DL_11_10");
            Speech(1, "DL_11_11");
            Speech(1, "DL_11_12");
            Speech(1, "DL_11_13");
            Speech(0, "DL_11_14");
            Speech(1, "DL_11_15");
            Speech(1, "DL_11_16");
            Speech(1, "DL_11_17");
            Speech(1, "DL_11_18");
            Speech(1, "DL_11_19");
            Speech(0, "DL_11_20");
            Speech(0, "DL_11_21");
            Speech(0, "DL_11_22");
            Speech(1, "DL_11_23");
        }

        protected override void End()
        {
            GameState.SavedPosition = new Vector2(-352, -528);
            GameState.SavedScene = "res://scenes/desert.lvl.tscn";

            GlobalTransition.Instance.ChangeScene(GameState.SavedScene);

            GameState.Save();
            GlobalAlert.Show(true, "AL_SAVE");
        }

        protected override void HandleActions(int action)
        {
            
        }
    }
}