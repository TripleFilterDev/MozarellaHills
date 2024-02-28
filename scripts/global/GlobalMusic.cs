using Godot;
using System;
using System.Collections.Generic;

namespace MozarellaHills
{
    public class GlobalMusic : Node2D
    {
        public static GlobalMusic Instance;

        public enum Tracks
        {
            None,
            LoopAmbient,
            LoopBattle,
            LoopChill,
            LoopDanger,
            LoopEnnui,
            LoopTremor
        }

        private void MusicPaths()
        {
            musicPaths.Add(Tracks.None, "res://sources/music/none.ogg");
            musicPaths.Add(Tracks.LoopAmbient, "res://sources/music/loop_ambient.ogg");
            musicPaths.Add(Tracks.LoopBattle, "res://sources/music/loop_battle.ogg");
            musicPaths.Add(Tracks.LoopChill, "res://sources/music/loop_chill.ogg");
            musicPaths.Add(Tracks.LoopDanger, "res://sources/music/loop_danger.ogg");
            musicPaths.Add(Tracks.LoopEnnui, "res://sources/music/loop_ennui.ogg");
            musicPaths.Add(Tracks.LoopTremor, "res://sources/music/loop_tremor.ogg");
        }

        private Tracks trackMusic = Tracks.None;
        public Tracks TrackMusic
        {
            get => trackMusic;
        }

        private AudioStreamPlayer streamPlayer;

        private Dictionary<Tracks, string> musicPaths = new Dictionary<Tracks, string>();
        private Dictionary<Tracks, AudioStream> musicDict = new Dictionary<Tracks, AudioStream>();

        private float minVolume = -100.0f;
        private float maxVolume = 0.0f;
        private float curVolume = 0.0f;
        private float targetVolume = 0.0f;
        private float stepChange = 108.0f;

        private bool changing = false;
        private bool moving = true;

        public override void _Ready()
        {
            Instance = this;
            
            MusicPaths();
            MusicStreams();
            streamPlayer = (AudioStreamPlayer) GetNode("AudioMusic");
        }

        public override void _Process(float delta)
        {
            if (changing)
            {
                if (!moving)
                {
                    curVolume += stepChange * delta;
                    if (curVolume > maxVolume)
                    {
                        curVolume = maxVolume;
                        changing = false;
                    }
                }
                else
                {
                    curVolume -= stepChange * delta;
                    if (curVolume < minVolume)
                    {
                        curVolume = minVolume;
                        //changing = false;
                        moving = false;

                        streamPlayer.Stop();
                        streamPlayer.Stream = musicDict[trackMusic];
                        streamPlayer.Play();
                    }
                }
            }

            streamPlayer.VolumeDb = curVolume;
        }

        public void Play(Tracks track)
        {
            if (track == TrackMusic) return;

            changing = true;
            moving = true;

            trackMusic = track;
        }

        private void MusicStreams()
        {
            foreach (var pair in musicPaths)
            {
                
                var music = GD.Load(pair.Value) as AudioStream;
                musicDict.Add(pair.Key, music);
                
            }
        }
    }
}

