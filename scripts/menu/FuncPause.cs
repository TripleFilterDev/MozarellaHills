using Godot;
using System;
using System.Security.Cryptography.X509Certificates;

namespace MozarellaHills
{
    public class FuncPause : Node2D
    {
        public static FuncPause Instance = null;

        private ColorRect rectShader;
        private Button buttonContinue;
        private Button buttonExit;
        private Button buttonRestart;
        private CanvasLayer canvasPause;
        private Timer timerPause;
        private Control controlPause;

        private float targetBlur = 0.0f;
        private float curBlur = 0.0f;

        private bool inPause = false;
        private bool startTimerPause = false;

        public override void _Ready()
        {
            Instance = this;

            controlPause = (Control) GetNode("CanvasPause/ControlPause");
            canvasPause = (CanvasLayer) GetNode("CanvasPause");
            rectShader = (ColorRect) GetNode("CanvasPause/ControlPause/RectShader");
            buttonContinue = (Button) GetNode("CanvasPause/ControlPause/ControlMenu/ButtonContinue");
            buttonRestart = (Button) GetNode("CanvasPause/ControlPause/ControlMenu/ButtonRestart");
            buttonExit = (Button) GetNode("CanvasPause/ControlPause/ControlMenu/ButtonExit");
            timerPause = (Timer) GetNode("TimerPause");

            canvasPause.Visible = false;

            buttonContinue.Connect("pressed", this, "OnButtonContinuePressed");
            buttonRestart.Connect("pressed", this, "OnButtonRestartPressed");
            buttonExit.Connect("pressed", this, "OnButtonExitPressed");

            timerPause.Connect("timeout", this, "OnTimerPauseTimeout");

            buttonContinue.GrabFocus();
            //buttonContinue.GrabFocus();
            
        }

        public override void _PhysicsProcess(float delta)
        {
            if (Input.IsActionJustPressed("mn_pause"))
            {
                if (!inPause && !startTimerPause)
                {
                    
                    Pause();
                    targetBlur = 4.0f;
                    inPause = true;
                    //buttonContinue.CallDeferred("grab_focus");
                    //buttonContinue.GrabFocus();
                }
                else if (inPause && !startTimerPause)
                {
                    timerPause.Start();
                    startTimerPause = true;

                    targetBlur = 0.0f;
                }
            }
        }

        public override void _Process(float delta)
        {
            curBlur = Mathf.Lerp(curBlur, targetBlur, 0.1f);

            controlPause.Modulate = new Color(1.0f, 1.0f, 1.0f, curBlur * 0.25f);
            ((ShaderMaterial) rectShader.Material).SetShaderParam("blur_amount", curBlur);
        }

        private void OnButtonContinuePressed()
        {
            if (inPause && !startTimerPause)
            {
                timerPause.Start();
                startTimerPause = true;

                targetBlur = 0.0f;
            }
        }

        private void OnButtonExitPressed()
        {
            if (inPause && !startTimerPause)
            {
                //timerPause.Start();
                //startTimerPause = true;

                targetBlur = 0.0f;

                GlobalTransition.Instance.ChangeSceneAndDisablePause("res://scenes/menu/main.mn.tscn", Color.Color8(255, 236, 214));
            }
        }

        private void OnButtonRestartPressed()
        {
            if (inPause && !startTimerPause)
            {
                timerPause.Start();
                startTimerPause = true;

                targetBlur = 0.0f;

                //if (PlatformCharacter.Instance != null) PlatformCharacter.Instance.CanControl = false;

                //GameState.IsRevive = true;

                GlobalTransition.Instance.ChangeScene(GameState.SavedScene, Color.Color8(255, 236, 214));
            }
        }

        private void OnTimerPauseTimeout()
        {
            if (inPause && startTimerPause)
            {
                Unpause();

                inPause = false;

                startTimerPause = false;
            }
        }

        public void Pause()
        {
            GetTree().Paused = true;
            
            canvasPause.Visible = true;

            //GrabButtonContinueFocus();
            buttonContinue.Visible = true;
            GrabButtonContinueFocus();
            

            //buttonContinue.GrabFocus();
            //await ToSignal(GetTree().CreateTimer(0.1f), "timeout");
            //buttonContinue.GrabFocus();
        }

        public void Unpause()
        {
            GetTree().Paused = false;
            //targetBlur = 0.0f;
            canvasPause.Visible = false;
        }

        public async void GrabButtonContinueFocus()
        {
            //buttonContinue.Disabled = true;

            //buttonContinue.ReleaseFocus();
            //buttonContinue.GrabFocus();

            buttonContinue.ReleaseFocus();
            await ToSignal(GetTree(), "physics_frame");
            buttonContinue.GrabFocus();
            await ToSignal(GetTree(), "physics_frame");
            buttonContinue.ReleaseFocus();
            await ToSignal(GetTree(), "physics_frame");
            buttonContinue.GrabFocus();
            //buttonContinue.

            //buttonContinue.Disabled = false;
            
            
        }
    }
}