using OfficeOpenXml;

using System;
using System.IO;

namespace Taiko3DS3.Test
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            if (args.Length > 0) CombineXlsx(args);

            Console.WriteLine("按任意键继续!");
            Console.ReadKey();
        }

        /// <summary>
        /// 合并Excel文件
        /// </summary>
        /// <param name="args"></param>
        private static void CombineXlsx(string[] args)
        {
            using (var newPackage = new ExcelPackage())
            {
                File.Delete($@"{AppDomain.CurrentDomain.BaseDirectory}\CombineXlsx.xlsx");
                var i = 0;
                foreach (var arg in args)
                {
                    if (Path.GetExtension(arg.ToLower()) == ".xlsx")
                    {
                        i++;
                        Console.WriteLine($"正在处理: {arg}");
                        using (var package = new ExcelPackage(new FileInfo(arg)))
                        {
                            foreach (var worksheet in package.Workbook.Worksheets)
                            {
                                newPackage.Workbook.Worksheets.Add(worksheet.Name, worksheet);
                            }
                        }
                    }
                }
                Console.WriteLine($"已合并 {i} 个文件");
                newPackage.SaveAs(new FileInfo($@"{AppDomain.CurrentDomain.BaseDirectory}\CombineXlsx.xlsx"));
            }
        }
    }
}