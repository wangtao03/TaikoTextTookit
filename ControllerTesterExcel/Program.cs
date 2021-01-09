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
            var sb = new StringBuilder();
            var regex = new Regex(@"{\S+}");
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            foreach (var file in args)
            {
                Console.WriteLine($"\r\n{file}");
                if (File.Exists(file) && file.ToLower().EndsWith(".xlsx"))
                {
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