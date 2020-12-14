using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using JetBrains.Annotations;

namespace MEM_GUI.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        
        [NotNull]
        private static string Main()
        {
            var currentPath = Directory.GetCurrentDirectory();
            var handledPath = GetPrimaryTreeElem("/" + currentPath);

            var dirNames = DirNamesFiller(handledPath);
            var info = new DirectoryInfo(handledPath);

            var renderResult = "";
            
            foreach (var dir in info.GetDirectories("*", SearchOption.AllDirectories))
            {
                var elem = dir.ToString();
                var res = $"{GetDirectorySize("/" + elem)} --- {elem}\n";

                if (dirNames != null)
                    foreach (var dirName in dirNames)
                    {
                        var dirNameSize = GetDirectorySize("/" +  elem);
                        
                        if (elem != dirName) continue;

                        renderResult += res;

                        // Console.WriteLine(res);
                    }
            }

            return renderResult;
        }
        
        [NotNull] public string Greeting => Main();

        [NotNull]
        private static string GetPrimaryTreeElem(string path)
        {
            path = path.Replace("/", " ");

            var res = path.Split();

            return res[1];
        }

        [NotNull]
        private static string GetLastTreeElem(string path)
        {
            path = path.Replace("/", " ");

            var res = path.Split();

            return res[^1];
        }

        private static long GetDirectorySize([NotNull] string path)
        {
            var info = new DirectoryInfo(path);

            return info.GetFiles("*", SearchOption.AllDirectories).Sum(file => file.Length);
        }

        [CanBeNull]
        private static List<string> DirNamesFiller([NotNull] string path)
        {
            var info = new DirectoryInfo(path);
            var handledList = new List<string>();
            
            foreach (var elem in info.GetFiles("*", SearchOption.AllDirectories))            
            {
                handledList.Add(elem.ToString());
            }

            var res = handledList.Distinct().ToList();

            return res;
        }
    }
    
}
