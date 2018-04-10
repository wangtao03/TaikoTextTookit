using System;
using System.IO;
using Taiko3DS3;

namespace TaikoTextConsole
{
    internal class Program
    {
        private static void PrintUsage()
        {
            Console.WriteLine("用法: TaikoTextConsole.exe <文件路径>");
        }

        private static void Excel2Dat(string filename)
        {
            var taiko = new SystemText();
            Console.WriteLine("正在处理: {0}", filename);
            taiko.Import4Excel(filename);            
            taiko.SaveAs(Path.ChangeExtension(filename, "dat"), taiko.TextTitle);
            taiko.Dispose();
        }

        private static void Dat2Excel(string filename)
        {
            var taiko = new SystemText(File.ReadAllBytes(filename));
            Console.WriteLine("正在处理: {0}", filename);
            taiko.Export(Path.ChangeExtension(filename, "xlsx"));
            taiko.Dispose();
        }

        private static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                PrintUsage();
                return;
            }
            foreach (var arg in args)
            {
                var extension = Path.GetExtension(arg);
                switch (extension)
                {
                    case ".dat":
                        {
                            Dat2Excel(arg);
                            break;
                        }
                    case ".xlsx":
                        {
                            Excel2Dat(arg);
                            break;
                        }
                    default:
                        {
                            PrintUsage();
                            break;
                        }
                }
            }
        }
    }
}