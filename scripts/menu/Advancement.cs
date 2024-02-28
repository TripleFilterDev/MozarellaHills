using Godot;
using System;

namespace MozarellaHills
{
    public class Advancement : Control
    {
        public enum Advancements
        {
            Sinless,
            Disco,
            KingTalking,
            Suit,
            GriffinHunter,
            SuperCrystal,
        }

        [Export]
        public Advancements Current = Advancements.Sinless;

        private TextureRect textureAdvancement;
        private Label labelName;
        private Label labelText;

        private bool isCompleted = false;

        public override void _Ready()
        {
            if (Current == Advancements.Sinless && GameState.Saved.AdvancementSinless) isCompleted = true;
            if (Current == Advancements.Disco && GameState.Saved.AdvancementDisco) isCompleted = true;
            if (Current == Advancements.KingTalking && GameState.Saved.AdvancementKingTalking) isCompleted = true;
            if (Current == Advancements.GriffinHunter && GameState.Saved.AdvancementGriffinHunter) isCompleted = true;
            if (Current == Advancements.Suit && GameState.Saved.AdvancementSuit) isCompleted = true;
            if (Current == Advancements.SuperCrystal && GameState.Saved.AdvancementSuperCrystal) isCompleted = true;

            textureAdvancement = (TextureRect) GetNode("TextureAdvancement");
            labelName = (Label) GetNode("LabelName");
            labelText = (Label) GetNode("LabelText");

            if (!isCompleted)
            {
                textureAdvancement.Modulate = new Color(1, 1, 1, 0.5f);
                labelName.Modulate = new Color(1, 1, 1, 0.5f);
                labelText.Modulate = new Color(1, 1, 1, 0.5f);
            }

            if (Current == Advancements.Sinless) { textureAdvancement.Texture = LoadImageTexture("res://sources/images/advancements/sinless.png"); labelName.Text = Tr("AD_SINLESS_NAME"); labelText.Text = Tr("AD_SINLESS_DESC"); }
            if (Current == Advancements.Disco) { textureAdvancement.Texture = LoadImageTexture("res://sources/images/advancements/disco.png"); labelName.Text = Tr("AD_DISCO_NAME"); labelText.Text = Tr("AD_DISCO_DESC"); }
            if (Current == Advancements.KingTalking) { textureAdvancement.Texture = LoadImageTexture("res://sources/images/advancements/king.png"); labelName.Text = Tr("AD_KING_NAME"); labelText.Text = Tr("AD_KING_DESC"); }
            if (Current == Advancements.Suit) { textureAdvancement.Texture = LoadImageTexture("res://sources/images/advancements/suit.png"); labelName.Text = Tr("AD_SUIT_NAME"); labelText.Text = Tr("AD_SUIT_DESC"); }
            if (Current == Advancements.GriffinHunter) { textureAdvancement.Texture = LoadImageTexture("res://sources/images/advancements/hunter.png"); labelName.Text = Tr("AD_GHUNTER_NAME"); labelText.Text = Tr("AD_GHUNTER_DESC"); }
            if (Current == Advancements.SuperCrystal) { textureAdvancement.Texture = LoadImageTexture("res://sources/images/advancements/supercrystal.png"); labelName.Text = Tr("AD_SCRYSTAL_NAME"); labelText.Text = Tr("AD_SCRYSTAL_DESC"); }
        }

        private Texture LoadImageTexture(string path)
        {
            var texture = ResourceLoader.Load<Texture>(path);
            return texture;
        }
    }
}