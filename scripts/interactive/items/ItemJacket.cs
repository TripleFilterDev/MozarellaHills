using Godot;
using System;

namespace MozarellaHills
{
    public class ItemJacket : ItemBase
    {
        public override float DisappearTime => 0.5f;

        protected override void Make()
        {
            if (mainCharacter != null) mainCharacter.WearJacket = true;
            GameState.Current.WearJacket = true;
            GameState.Current.AdvancementSuit = true;
        }
    }
}