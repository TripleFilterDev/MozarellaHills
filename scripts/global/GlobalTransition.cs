using Godot;
using System;
using System.Collections.Generic;

namespace MozarellaHills
{
    public class GlobalTransition : Node2D
    {
        public static GlobalTransition Instance = null;

        private Color transitionColor = Color.Color8(255, 236, 214);

        private float transition = 0.0f;
        private float step = 0.95f;

        private string scenePath = "";

        private bool doChange = false;
        private bool move = false;

        private bool disablePause = false;

        private ColorRect rectTransition;

        public void ChangeScene(string scene)
        {
            if (!FileUtil.IsFilePathValid(scene)) return;

            transitionColor = Color.Color8(255, 236, 214);
            NewScene(scene);
        }

        public void ChangeScene(string scene, Color color)
        {
            if (!FileUtil.IsFilePathValid(scene)) return;

            transitionColor = color;
            NewScene(scene);
        }

        public void ChangeSceneAndDisablePause(string scene, Color color)
        {
            if (!FileUtil.IsFilePathValid(scene)) return;

            transitionColor = color;
            disablePause = true;
            NewScene(scene);
        }

        private void NewScene(string scene)
        {
            scenePath = scene;
            doChange = true;
            move = true;
        }
        
        public override void _Ready()
        {
            Instance = this;

            rectTransition = (ColorRect) GetNode("CanvasTransition/RectTransition");
        }

        public override void _PhysicsProcess(float delta)
        {
            if (doChange)
            {
                if (move)
                {
                    transition += step * delta;
                    if (transition > 1.0f)
                    {
                        transition = 1.0f;
                        move = false;
                        
                        GetTree().ChangeScene(scenePath);

                        if (disablePause)
                        {
                            GetTree().Paused = false;
                            disablePause = false;
                        }
                    }
                }
                else
                {
                    transition -= step * delta;
                    if (transition < 0.0f)
                    {
                        transition = 0.0f;
                        doChange = false;
                    }
                }

                rectTransition.Color = new Color(transitionColor.r, transitionColor.g, transitionColor.b, transition);
            }
        }

        
    }
}