using Godot;
using System;

namespace MozarellaHills
{
    public class CharacterWear : AnimatedSprite
    {
        AnimatedSprite characterSprite;

        public override void _Ready()
        {
            characterSprite = (AnimatedSprite) GetParent();
        }

        public override void _Process(float delta)
        {
            this.Animation = characterSprite.Animation;
            this.Frame = characterSprite.Frame;
            this.FlipH = characterSprite.FlipH;
        }
    }

}
