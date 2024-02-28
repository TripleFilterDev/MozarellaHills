using Godot;
using System;

namespace MozarellaHills
{
    public class FuncObjectSpawner : Node2D, IFuncNode
    {
        public static FuncObjectSpawner Instance = null;

        private Node2D parentNode;

        public override void _Ready()
        {
            Instance = this;

            parentNode = GetParentOrNull<Node2D>();
        }

        public void SpawnObject(TakenObjects type, Vector2 position)
        {
            if (parentNode == null) return;

            PackedScene scene;
            string path = "res://";

            switch (type)
            {
                case TakenObjects.Stone: path = "res://prefabs/objects/stone.prfb.tscn"; break;
                case TakenObjects.Cobblestone: path = "res://prefabs/objects/cobblestone.prfb.tscn"; break;
                case TakenObjects.Cheese: path = "res://prefabs/objects/cheese.prfb.tscn"; break;
                default: path = "res://prefabs/objects/none.prfb.tscn"; break;
            }

            scene = ResourceLoader.Load<PackedScene>(path);

            Node2D instance = scene.Instance<Node2D>();
            parentNode.AddChild(instance);
            instance.GlobalPosition = position;
        }
    }
}