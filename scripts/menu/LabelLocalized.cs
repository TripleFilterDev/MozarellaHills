using Godot;
using System;

namespace MozarellaHills
{
    public class LabelLocalized : Label
    {
        [Export]
        private string locale = "";

        public override void _Ready()
        {
            Text = Tr(locale);
        }

        
    }

}
