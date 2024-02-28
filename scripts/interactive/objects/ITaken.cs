using Godot;
using System;

namespace MozarellaHills
{
    public interface ITaken
    {
        Vector2 GlobalPosition { get; set; }
        TakenObjects ObjectType { get; }
        bool IsItem { get; }
        void ToggleNearest(bool can);
        void Take();
    }

    public enum TakenObjects
    {
        None,
        Stone,
        Cobblestone,
        Cheese
    }
}