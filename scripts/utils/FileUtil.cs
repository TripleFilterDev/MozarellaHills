using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MozarellaHills
{
    public static class FileUtil
    {
        public static bool IsFilePathValid(string path)
        {
            File file = new File();
            var err = file.Open(path, Godot.File.ModeFlags.Read);
            if (err == Error.Ok)
            {
                file.Close();
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string LoadText(string path)
        {
            File file = new File();
            var err = file.Open(path, Godot.File.ModeFlags.Read);

            if (err == Error.Ok)
            {
                string content = file.GetAsText();
                file.Close();
                return content;
            }
            else
            {
                return "";
            }
        }

        public static bool SaveText(string path, string content)
        {
            File file = new File();
            var err = file.Open(path, Godot.File.ModeFlags.Write);

            if (err == Error.Ok)
            {
                file.StoreString(content);
                file.Close();
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string LoadText(string path, string password)
        {
            File file = new File();
            var err = file.OpenEncryptedWithPass(path, Godot.File.ModeFlags.Read, password);

            if (err == Error.Ok)
            {
                string content = file.GetAsText();
                file.Close();
                return content;
            }
            else
            {
                return "";
            }
        }

        public static bool SaveText(string path, string password, string content)
        {
            File file = new File();
            var err = file.OpenEncryptedWithPass(path, Godot.File.ModeFlags.Write, password);

            if (err == Error.Ok)
            {
                file.StoreString(content);
                file.Close();
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void MakeDir(string path, string folder)
        {
            var dir = new Directory();
            dir.Open(path);
            dir.MakeDir(folder);
        }
    }
}