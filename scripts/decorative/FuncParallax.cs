using Godot;
using System;

namespace MozarellaHills
{
    public class FuncParallax : Node2D, IFuncNode
    {
        public static FuncParallax Instance = null;

        [Export]
        private bool enableClouds = true;

        private TextureRect textureParallax;
    
        public override void _Ready()
        {
            Instance = this;

            textureParallax = (TextureRect) GetNode("CanvasParallax/TextureParallax");

            if (!enableClouds) textureParallax.Visible = false; else textureParallax.Visible = true; 

            ((ShaderMaterial) textureParallax.Material).SetShaderParam("multiplier_hor", 0.1f);
            ((ShaderMaterial) textureParallax.Material).SetShaderParam("multiplier_ver", 0.4f);
        }

        public void ChangeParallax(float offsetHorizontal, float offsetVertical)
        {
            ((ShaderMaterial) textureParallax.Material).SetShaderParam("offset_hor", offsetHorizontal);
            ((ShaderMaterial) textureParallax.Material).SetShaderParam("offset_ver", offsetVertical);
        }
    }
}