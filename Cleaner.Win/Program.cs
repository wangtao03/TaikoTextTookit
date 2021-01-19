using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cleaner.Win
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            if (args.Length <= 0) args = new string[] { Environment.CurrentDirectory };
            var paths = GetSubPath(args);
            foreach (var path in paths)
            {
                var files = Directory.GetFiles(path, "*.dat");
                if (files.Length > 0) CheckCompleted(files);
            }

            Console.WriteLine("清理完成，按任意键继续！");
            Console.ReadKey();
        }

        public static string[] GetSubPath(params string[] paths)
        {
            List<string> dirs = new List<string>();
            foreach (var path in paths)
            {
                var subPath = Directory.GetDirectories(path);

                if (subPath.Length > 0)
                {
                    dirs.AddRange(subPath);
                    dirs.AddRange(GetSubPath(subPath));
                }
            }
            return dirs.ToArray();
        }

        public static void CheckCompleted(params string[] files)
        {
            foreach (var file in files)
            {
                var excel = Path.ChangeExtension(file, "xlsx");
                if (!File.Exists(excel)) continue;
                var path = $@"{Path.GetDirectoryName(file)}\_Completed";
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                var newExcel = $@"{path}\{Path.GetFileName(excel)}";
                Console.WriteLine($"移动 {excel}");
                File.Move(excel, newExcel);
            }
        }
    }
}