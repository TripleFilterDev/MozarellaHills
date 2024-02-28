using Godot;
using System;

namespace MozarellaHills
{
    public class ItemHelp : ItemBase
    {
        public override float DisappearTime => 1.0f;

        [Export]
        private bool localized = true;

        [Export]
        private string textShow = "";

        protected override void Make()
        {
            if (FuncItemHelp.Instance != null) FuncItemHelp.Instance.ShowHelp(localized, textShow);
        }
    }
}