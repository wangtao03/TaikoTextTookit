using CharaNameConverter3DS;

using System;
using System.IO;

using TaikoData;

namespace CharaNameConverter
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //args = new string[] { @"C:\Users\WangTao\Desktop\太鼓3ds补丁\_data\system\CharaName.xlsx" };

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
                            var charaNameReader = new CharaNameReader(file);
                            charaNameReader.ReadData();
                            charaNameReader.SaveData(Path.ChangeExtension(file, "xlsx"));
                            break;

                        case ".xlsx":
                            Console.WriteLine(file);
                            var charaNameWriter = new CharaNameWriter();
                            charaNameWriter.LoadData(file);
                            charaNameWriter.WriteData(Path.ChangeExtension(file, "dat"));
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