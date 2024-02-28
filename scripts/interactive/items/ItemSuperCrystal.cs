using Godot;
using System;

namespace MozarellaHills
{
    public class ItemSuperCrystal : ItemBase
    {
        public override float DisappearTime => 0.5f;

        protected override void Make()
        {
            GameState.Current.AdvancementSuperCrystal = true;
        }
    }
}