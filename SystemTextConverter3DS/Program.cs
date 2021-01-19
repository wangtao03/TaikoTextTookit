using System;
using System.IO;

namespace SystemTextConverter3DS
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //args = new string[] { @"C:\Users\WangTao\Desktop\PackEnglishV12\PackHack\ExtractedRomFS\_data\system\text\BossText_00.xlsx" };

            if (args.Length > 0 && Directory.Exists(args[0]))
            {
                args = Directory.GetFiles(args[0], "*.dat");
            }
            var count = 0;

            foreach (var file in args)
            {
                if (File.Exists(file))
                {
                    Console.Clear();
                    Console.Write($"{++count}/{args.Length}: ");

                    switch (Path.GetExtension(file).ToLower())
                    {
                        case ".dat":
                            Console.WriteLine(file);
                            var systemTextReader = new SystemTextReader(file);
                            systemTextReader.ReadData();
                            systemTextReader.SaveData(Path.ChangeExtension(file, "xlsx"));
                            break;

                        case ".xlsx":
                            Console.WriteLine(file);
                            var systemTextWriter = new SystemTextWriter();
                            systemTextWriter.LoadData(file);
                            systemTextWriter.WriteData(Path.ChangeExtension(file, "dat"));
                            break;

                        default:
                            break;
                    }
                }
            }
            Console.WriteLine("\r\n转换完成，按任意键继续！");
            Console.ReadKey();
        }
    }
}