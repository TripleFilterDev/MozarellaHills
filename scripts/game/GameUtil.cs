using Godot;
using System;
using System.Linq;

namespace MozarellaHills
{
    public class GameUtil
    {
        public static bool IsHtml5 => OS.GetName() == "HTML5";
        public static bool IsDesktop => (new string[]{"X11", "OSX", "Windows"}).Contains(OS.GetName());
    }
}