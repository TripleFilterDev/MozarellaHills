using Godot;
using System;

namespace MozarellaHills
{
    public class DialogHunter : DialogBase
    {
        public override Participant[] Participants => new[] 
            {
                new Participant("CH_MAIN", Color.Color8(84, 78, 104), new Vector2(-32, -16)),
                new Participant("CH_HUNTER", Color.Color8(32, 60, 86), new Vector2(32, -16))
            };
        

        protected override void Scenario()
        {
            Speech(1, "DL_4_0");
            Speech(1, "DL_4_1");
            Speech(0, "DL_4_2");
            Speech(1, "DL_4_3");
            Speech(1, "DL_4_4");
            Speech(1, "DL_4_5");
            Speech(0, "DL_4_6");
            Speech(1, "DL_4_7");
            Speech(1, "DL_4_8");
            Speech(1, "DL_4_9");
            Speech(1, "DL_4_10");
            Speech(1, "DL_4_11");
            Speech(1, "DL_4_12");
            Speech(0, "DL_4_13");
            Speech(1, "DL_4_14");
            Speech(1, "DL_4_15");
            Speech(1, "DL_4_16");
            Speech(1, "DL_4_17");
            Speech(0, "DL_4_18");
            Speech(1, "DL_4_19");
        }

        protected override void End()
        {
            GameState.Current.AdvancementGriffinHunter = true;

            GameState.SavedPosition = new Vector2(216, -664);
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