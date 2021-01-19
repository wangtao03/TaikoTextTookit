using OfficeOpenXml;

using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace ToolKits
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var bar = new ConsoleProgressBar();
            var regex = new Regex(@"\{\S+\}");
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            //args = new string[] { @"E:\3DSTaiko\_data\system\text\CharaText_00.xlsx" };

            if (args.Length > 0 && Directory.Exists(args[0]))
            {
                args = Directory.GetFiles(args[0], "*.xlsx");
            }
            var count = 0;

            foreach (var file in args)
            {
                Console.Clear();
                Console.Write($"{++count}/{args.Length}: ");

                Console.WriteLine($"\r\n{file}");
                if (File.Exists(file) && file.ToLower().EndsWith(".xlsx"))
                {
                    File.AppendAllText("CheckList.log", $"{file}\r\n");
                    using (var excelPackage = new ExcelPackage(new FileInfo(file)))
                    {
                        foreach (var workSheet in excelPackage.Workbook.Worksheets)
                        {
                            Console.WriteLine(workSheet.Name);
                            bar.Start();
                            var dimension = workSheet.Dimension;
                            for (int i = dimension.Start.Row; i <= dimension.End.Row; i++)
                            {
                                for (int j = dimension.Start.Column; j <= dimension.End.Column; j++)
                                {
                                    bar.Update((i - 1) * dimension.End.Column + j, dimension.End.Row * dimension.End.Column);
                                    var cell = workSheet.Cells[i, j];
                                    if (cell.Text.Length >= 4 && regex.IsMatch(cell.Text))
                                    {
                                        Console.WriteLine($"{workSheet.Name}: {cell.Address}");
                                        File.AppendAllText("CheckList.log", $"{workSheet.Name}: {cell.Address}\r\n");
                                    }
                                }
                            }
                            bar.End();
                        }
                    }
                }
            }

            Console.WriteLine("\r\n检查完毕，按任意键继续！");
            Console.ReadKey();
        }
    }
}