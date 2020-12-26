using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace MEM_GUI.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {

        private static string Add()
        {

            var currentPath = Directory.GetCurrentDirectory();
            var handledPath = "/" + GetPrimaryTreeElem("/" + currentPath);
            Console.WriteLine(handledPath);

            var info = new DirectoryInfo(handledPath);

            var container = new HashSet<string>();
            var addContainer = new HashSet<string>();

            try
            {

                Parallel.ForEach(info.GetDirectories("*", SearchOption.AllDirectories), (dir) =>
                {

                    var elem = dir.ToString();
                    var lastElem = GetLastTreeElem(elem);
                    var lastElemSize = DirSize(new DirectoryInfo(elem));

                    var dirname = dir.ToString().Split("/")[^1];

                    if (dirname == "root" || addContainer.Contains(dirname) || lastElemSize <= 1000000) return;
                    container.Add($"{lastElem,30} --- {lastElemSize}");
                    addContainer.Add(lastElem);
                    
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            var renderResult = "";
            var counter = 0;

            foreach (var elem in container)
            {
                counter += 1;
                
                if (counter > 30) continue;

                renderResult += (elem + "\n");
            }

            renderResult += (container.Count + "\n");
            
            return renderResult;

        }


        [NotNull] public string Greeting => Add();

        [CanBeNull]
        private static string GetPrimaryTreeElem(string path)
        {
            var res = path.Split("/");

            return res[2];
        }

        [NotNull]
        private static string GetLastTreeElem(string path)
        {
            path = path.Replace("/", " ");

            var res = path.Split();

            return res[^1];
        }

        private static long DirSize([NotNull] DirectoryInfo d, long aLimit = 0)
        {
            long size = 0;
            var fis = d.GetFiles();

            foreach (var fi in fis)
            {
                size += fi.Length;
                if (aLimit > 0 && size > aLimit)
                    return size;
            }

            var dis = d.GetDirectories();
            foreach (var di in dis)
            {
                size += DirSize(di, aLimit);
                if (aLimit > 0 && size > aLimit)
                    return size;
            }

            return size;
        }

    }
}
