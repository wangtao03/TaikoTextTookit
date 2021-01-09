using System;
using System.IO;

using TaikoData;

namespace SystemDataSwitchExport
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            foreach (var file in args)
            {
                if (File.Exists(file))
                {
                    var datFile = new DataFile(file);
                    var magic = datFile.GetString(8, 12).ToUpper();
                    if (magic.IndexOf("WORDLIST") >= 0)
                    {
                        Console.WriteLine($"{magic}: {file}");
                        var wordData = new WordDataReader(datFile);

                        wordData.ReadData();
                        wordData.SaveData(Path.ChangeExtension(file, "xlsx"));
                        continue;
                    }
                    var indexs = datFile.IndexOf(new byte[] { 0x10, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });
                    if (indexs.Count > 0 && indexs[0] == 0)
                    {
                        Console.WriteLine($"StoryText: {file}");
                        var storyText = new StoryTextReader(datFile);

                        storyText.ReadData();
                        storyText.SaveData(Path.ChangeExtension(file, "xlsx"));
                        continue;
                    }
                }
            }

            Console.WriteLine("\r\n导出完毕，按任意键继续！");
            Console.ReadKey();
        }
    }
}