using Godot;
using System;

namespace MozarellaHills
{
    public class RichLabelLocalized : RichTextLabel
    {
        [Export]
        private string locale = "";

        public override void _Ready()
        {
            //BbcodeEnabled = true;
            string text = Tr(locale);

            Text = String.Join("\n", text.Split("%"));
        }
    }

}
