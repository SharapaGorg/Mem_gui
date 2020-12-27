#region

using System;

#endregion

namespace MEM_GUI.Models
{
    public class DirPart
    {
        public DirPart(string name, string path, int size)
        {
            Name = name;
            Path = path;
            Size = size;
        }

        public string Name { get; }
        public string Path { get; }
        public int Size { get; }

        public override string ToString()
        {
            return $"{Name}: {BytesToString(Size)}";
        }

        private static string BytesToString(long byteCount)
        {
            string[] suf = {"B", "KB", "MB", "GB", "TB", "PB", "EB"};
            if (byteCount == 0)
                return "0" + suf[0];
            var bytes = Math.Abs(byteCount);
            var place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
            var num = Math.Round(bytes / Math.Pow(1024, place), 1);
            return Math.Sign(byteCount) * num + suf[place];
        }
    }
}