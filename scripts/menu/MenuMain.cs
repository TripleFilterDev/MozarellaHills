using Godot;
using System;
namespace MozarellaHills
{
    public class MenuMain : Node2D
    {   
        private Button buttonPlay;
        private Button buttonSettings;
        private Button buttonCredits;
        private Button buttonQuit;

        public override void _Ready()
        {
            buttonPlay = (Button) GetNode("CanvasMenu/ControlMenu/ButtonPlay");
            buttonSettings = (Button) GetNode("CanvasMenu/ControlMenu/ButtonSettings");
            buttonCredits = (Button) GetNode("CanvasMenu/ControlMenu/ButtonCredits");
            buttonQuit = (Button) GetNode("CanvasMenu/ControlMenu/ButtonQuit");

            buttonPlay.Connect("pressed", this, "OnButtonPlayPressed");
            buttonSettings.Connect("pressed", this, "OnButtonSettingsPressed");
            buttonCredits.Connect("pressed", this, "OnButtonCreditsPressed");
            buttonQuit.Connect("pressed", this, "OnButtonQuitPressed");

            buttonPlay.GrabFocus();
        }

        private void OnButtonPlayPressed()
        {
            GlobalTransition.Instance.ChangeScene("res://scenes/tutorial.lvl.tscn");

            GameState.SavedScene = "res://scenes/tutorial.lvl.tscn";
            GameState.SavedPosition = new Vector2(-144, -48);
        }

        private void OnButtonCreditsPressed()
        {
            GlobalTransition.Instance.ChangeScene("res://scenes/menu/credits.mn.tscn");
        }

        private void OnButtonSettingsPressed()
        {
            GlobalTransition.Instance.ChangeScene("res://scenes/menu/settings.mn.tscn");
        }

        private void OnButtonQuitPressed()
        {
            if (GameUtil.IsHtml5)
            {
                JavaScript.Eval("window.close()");
            }
            else
            {
                GetTree().Quit();
            }
        }
    }
}