using System;
using System.IO;

using Taiko3DS3;

namespace TaikoSysTextConver
{
    internal class Program
    {
        private static void Dat2Xlsx(string path)
        {
            Console.WriteLine($"正在处理: {path}");
            using (var text = new SystemText(path))
            {
                text.Export(Path.ChangeExtension(path, "xlsx"));
            }
        }

        private static void Xlsx2Dat(string path)
        {
            Console.WriteLine($"正在处理: {path}");
            using (var text = new SystemText())
            {
                text.ImportAndSave(path);
            }
        }

        private static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                foreach (var arg in args)
                {
                    switch (Path.GetExtension(arg.ToLower()))
                    {
                        case ".dat":
                            Dat2Xlsx(arg);
                            break;

                        case ".xlsx":
                            Xlsx2Dat(arg);
                            break;

                        default:
                            break;
                    }
                }
            }
        }
    }
}