using Godot;
using System;

namespace MozarellaHills
{
    public class ItemCheese : ItemBase
    {
        public override float DisappearTime => 1.0f;

        protected override void Make()
        {
            if (mainCharacter != null) mainCharacter.AddCheese();
        }
    }
}