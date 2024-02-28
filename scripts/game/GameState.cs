using Godot;
using System;

namespace MozarellaHills
{
    public static class GameState 
    {
        public static bool StartGame = true;
        public static Vector2 SavedPosition = Vector2.Zero;
        public static string SavedScene = "";

        public static class Current
        {
            public static int CrystalsCount = 0;
            public static int CheesesCount = 0;

            public static bool WearJacket = false;
            public static bool WearCrown = false;

            public static bool AdvancementSinless = false;
            public static bool AdvancementDisco = false;
            public static bool AdvancementKingTalking = false;
            public static bool AdvancementSuit = false;
            public static bool AdvancementGriffinHunter = false;
            public static bool AdvancementSuperCrystal = false;
        }

        public static class Saved
        {
            public static int CrystalsCount = 0;
            public static int CheesesCount = 0;

            public static bool WearJacket = false;
            public static bool WearCrown = false;

            public static bool AdvancementSinless = true;
            public static bool AdvancementDisco = false;
            public static bool AdvancementKingTalking = false;
            public static bool AdvancementSuit = false;
            public static bool AdvancementGriffinHunter = false;
            public static bool AdvancementSuperCrystal = false;
        }

        public static void Save()
        {
            Saved.CrystalsCount = Current.CrystalsCount;
            Saved.CheesesCount = Current.CheesesCount;

            Saved.WearCrown = Current.WearCrown;
            Saved.WearJacket = Current.WearJacket;

            Saved.AdvancementSinless = Current.AdvancementSinless;
            Saved.AdvancementDisco = Current.AdvancementDisco;
            Saved.AdvancementKingTalking = Current.AdvancementKingTalking;
            Saved.AdvancementSuit = Current.AdvancementSuit;
            Saved.AdvancementGriffinHunter = Current.AdvancementGriffinHunter;
            Saved.AdvancementSuperCrystal = Current.AdvancementSuperCrystal;
        }

        public static void Load()
        {
            Current.CrystalsCount = Saved.CrystalsCount;
            Current.CheesesCount = Saved.CheesesCount;

            Current.WearCrown = Saved.WearCrown;
            Current.WearJacket = Saved.WearJacket;

            Current.AdvancementSinless = Saved.AdvancementSinless;
            Current.AdvancementDisco = Saved.AdvancementDisco;
            Current.AdvancementKingTalking = Saved.AdvancementKingTalking;
            Current.AdvancementSuit = Saved.AdvancementSuit;
            Current.AdvancementGriffinHunter = Saved.AdvancementGriffinHunter;
            Current.AdvancementSuperCrystal = Saved.AdvancementSuperCrystal;
        }

        public static void Default()
        {
            Current.CrystalsCount = 0;
            Current.CheesesCount = 0;

            Current.WearCrown = false;
            Current.WearJacket = false;

            Current.AdvancementSinless = true;
            Current.AdvancementDisco = false;
            Current.AdvancementKingTalking = false;
            Current.AdvancementSuit = false;
            Current.AdvancementGriffinHunter = false;
            Current.AdvancementSuperCrystal = false;

            Saved.CrystalsCount = 0;
            Saved.CheesesCount = 0;

            Saved.WearCrown = false;
            Saved.WearJacket = false;

            Saved.AdvancementSinless = true;
            Saved.AdvancementDisco = false;
            Saved.AdvancementKingTalking = false;
            Saved.AdvancementSuit = false;
            Saved.AdvancementGriffinHunter = false;
            Saved.AdvancementSuperCrystal = false;
        }
    }
}