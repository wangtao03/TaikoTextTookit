using System;
using System.IO;

namespace StoryTextConverter3DS
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //args = new string[] { @"C:\Users\WangTao\Desktop\PackEnglishV12\PackHack\ExtractedRomFS\_data\system\storytext\000_NPC_SET_A.xlsx" };
            foreach (var file in args)
            {
                if (File.Exists(file))
                {
                    switch (Path.GetExtension(file).ToLower())
                    {
                        case ".dat":
                            Console.WriteLine(file);
                            var storyTextReader = new StoryTextReader(file);
                            storyTextReader.ReadData();
                            storyTextReader.SaveData(Path.ChangeExtension(file, "xlsx"));
                            break;

                        case ".xlsx":
                            Console.WriteLine(file);
                            var storyTextWriter = new StoryTextWriter();
                            storyTextWriter.LoadData(file);
                            storyTextWriter.WriteData(Path.ChangeExtension(file, "dat"));
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