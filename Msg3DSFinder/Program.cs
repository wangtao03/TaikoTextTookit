using OfficeOpenXml;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

using ToolKits;

namespace Msg3DSFinder
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //args = new string[] { @"E:\yuzu-windows-msvc-early-access\user\dump\0100383012646000\romfs\3DS3\SystemData\StoryText\" };
            //args = new string[] { "R", "MsgID_3ds.xlsx", @"E:\3DSTaiko\_data\script_b\story" };

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            if (args.Length > 0)
            {
                if (Directory.Exists(args[0]))
                {
                    args = Directory.GetFiles(args[0], "*.xlsx");
                }
                else if (args[0].Equals("R", StringComparison.OrdinalIgnoreCase))
                {
                    try
                    {
                        Console.Clear();
                        var xlsx = args[1];
                        var dir = args[2];

                        Console.WriteLine("1、从xlsx 读取 MsgID");
                        var keyValues2 = LoadDic4Xlsx(xlsx);
                        Console.WriteLine("\r\n2.将 lua 中的 MsgID 替换为 文本");
                        var result = ReplaceMsgKey2Value(keyValues2, dir);
                        Console.WriteLine($"成功替换 {result} 个文件");
                        return;
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("参数错误!");
                        return;
                    }
                    finally
                    {
                        Console.WriteLine("\r\n处理完毕，按任意键继续！");
                        Console.ReadKey();
                    }
                }

                Console.Clear();

                Console.WriteLine("1、查找 xlsx 中的 MsgID");
                var keyValues = FindMsg3DS(args);

                Console.WriteLine("2、将 MsgID 保存到xlsx");
                var path = $@"{Environment.CurrentDirectory}\MsgID_3ds.xlsx";
                SaveDic2Xlsx(keyValues, path);
            }

            Console.WriteLine("\r\n处理完毕，按任意键继续！");
            Console.ReadKey();
        }

        private static int ReplaceMsgKey2Value(Dictionary<string, string> keyValues, string directory)
        {
            if (!Directory.Exists(directory)) return -1;
            int count = 0;
            var bar = new ConsoleProgressBar(50);
            bar.Start();
            foreach (var item in keyValues)
            {
                var dirPaths = item.Key.Split("_");
                if (dirPaths.Length != 6) continue;
                var file = $@"{directory}\{dirPaths[2]}\{dirPaths[3]}\{dirPaths[4]}\{dirPaths[5]}.lua";
                if (!File.Exists(file))
                {
                    file = Path.ChangeExtension(file, "bin");
                    if (!File.Exists(file)) continue;
                }
                var lua = File.ReadAllText(file);

                if (lua.IndexOf(item.Key) >= 0)
                {
                    bar.Update(++count, keyValues.Count);
                    Console.WriteLine($"{count}: {item.Key}\t{file}");
                    lua = lua.Replace(item.Key, item.Value.Replace("\n", @"\n"));
                    File.WriteAllText(file, lua);
                }
            }
            bar.End();
            return count;
        }

        private static Dictionary<string, string> LoadDic4Xlsx(string path)
        {
            var keyValues = new Dictionary<string, string>();
            var bar = new ConsoleProgressBar(50);

            using (var excelPackage = new ExcelPackage(new FileInfo(path)))
            {
                var worksheet = excelPackage.Workbook.Worksheets[0];
                var dimension = worksheet.Dimension;
                bar.Start();
                for (int i = dimension.Start.Row; i <= dimension.End.Row; i++)
                {
                    bar.Update(i, dimension.End.Row);
                    var key = worksheet.Cells[i, 2].Text;
                    var value = worksheet.Cells[i, 3].Text;
                    keyValues.TryAdd(key, value);
                }
                bar.End();
            }
            Console.WriteLine($"读取MsgID {keyValues.Count} 条");
            return keyValues;
        }

        private static void SaveDic2Xlsx(Dictionary<string, string> keyValues, string path)
        {
            if (File.Exists(path)) File.Delete(path);
            var bar = new ConsoleProgressBar(50);

            using (var excelPackage = new ExcelPackage(new FileInfo(path)))
            {
                var worksheet = excelPackage.Workbook.Worksheets.Add("MsgID_3ds");
                worksheet.Cells.Style.WrapText = true;
                var index = 0;

                bar.Start();
                foreach (var item in keyValues)
                {
                    bar.Update(++index, keyValues.Count);
                    worksheet.Cells[index, 1].Value = index;
                    worksheet.Cells[index, 2].Value = item.Key;
                    worksheet.Cells[index, 3].Value = item.Value;
                }
                bar.End();
                excelPackage.Save();
            }
        }

        private static Dictionary<string, string> FindMsg3DS(params string[] files)
        {
            var regex = new Regex(@"MsgID_3ds\d_\S+_\d{4}");
            var keyValues = new Dictionary<string, string>();
            var bar = new ConsoleProgressBar(50);
            var count = 0;
            bar.Start();
            foreach (var file in files)
            {
                bar.Update(++count, files.Length);
                using (var excelPackage = new ExcelPackage(new FileInfo(file)))
                {
                    var worksheet = excelPackage.Workbook.Worksheets[0];
                    var dimension = worksheet.Dimension;

                    for (int i = dimension.Start.Row; i <= dimension.End.Row; i++)
                    {
                        var key = worksheet.Cells[i, 2].Text;

                        if (regex.IsMatch(key))
                        {
                            var value = worksheet.Cells[i, 3].Text;
                            keyValues.TryAdd(key, value);
                        }
                    }
                }
            }
            bar.End();
            return keyValues;
        }
    }
}