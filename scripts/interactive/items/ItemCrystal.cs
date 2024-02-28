using Godot;
using System;

namespace MozarellaHills
{
    public class ItemCrystal : ItemBase
    {
        public override float DisappearTime => 1.0f;

        protected override void Make()
        {
            //if (mainCharacter != null) mainCharacter.WearJacket = true;
        }
    }
}