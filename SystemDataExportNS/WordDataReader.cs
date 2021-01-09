using OfficeOpenXml;

using System;
using System.Collections.Generic;
using System.IO;

using TaikoData;

using ToolKits;

namespace SystemDataSwitchExport
{
    internal class WordDataReader : DataReader
    {
        private readonly ConsoleProgressBar bar;

        /// <summary>
        /// 单个数据内容
        /// </summary>
        public class Data
        {
            /// <summary>
            /// 文本标识符
            /// </summary>
            public string Identifier { get; set; }

            /// <summary>
            /// 文本地址
            /// </summary>
            public int Address { get; set; }

            /// <summary>
            /// 文本内容
            /// </summary>
            public string Text { get; set; }
        }

        /// <summary>
        /// 数据对象列表
        /// </summary>
        public List<Data> dataList;

        public WordDataReader() : base()
        {
            bar = new ConsoleProgressBar();
        }

        public WordDataReader(string path) : base(path)
        {
            bar = new ConsoleProgressBar();
        }

        public WordDataReader(DataFile datFile) : base(datFile)
        {
            bar = new ConsoleProgressBar();
        }

        /// <summary>
        /// 读取数据
        /// </summary>
        public override void ReadData()
        {
            Console.WriteLine("\r\n读取文件索引!");
            var dList = new List<Data>();

            datFile.Seek = 0;
            var count = datFile.GetInt32();         //文本数量
            var firstIdAddr = datFile.GetInt32();   //首个标识地址
            datFile.Seek = firstIdAddr;             //转到首个标识地址

            bar.Start();
            for (int i = 0; i < count; i++)         //循环第一次 取出标识和文本地址
            {
                bar.Update(i + 1, count);
                dList.Add(new Data()
                {
                    Identifier = datFile.GetString(128),
                    Address = datFile.GetInt32()
                });
            }
            bar.End();

            Console.WriteLine("\r\n读取文件内容!");
            bar.Start();
            for (int i = 0; i < dList.Count; i++)         //循环第二次 取出文本内容
            {
                bar.Update(i + 1, dList.Count);
                var length = 0;                     //默认文本长度为0
                try
                {
                    length = dList[i + 1].Address - dList[i].Address;   //由下条文本地址来计算当前文本长度
                }
                catch (ArgumentOutOfRangeException)                     //最后一条文本
                {
                    length = datFile.Length - dList[i].Address;         //以文件结尾到当前文本地址为长度
                }
                finally
                {
                    dList[i].Text = Utils.ReplaceController(datFile.GetString(dList[i].Address, length));//取出文本 并替换控制符
                }
            }
            bar.End();

            dataList = dList;
        }

        /// <summary>
        /// 保存数据到excel文件
        /// </summary>
        /// <param name="xlsxPath">excel文件路径</param>
        public override void SaveData(string xlsxPath)
        {
            Console.WriteLine("\r\n写出到Excel!");
            bar.Start();

            if (File.Exists(xlsxPath)) File.Delete(xlsxPath);
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var excelPack = new ExcelPackage(new FileInfo(xlsxPath)))
            {
                var workSheet = excelPack.Workbook.Worksheets.Add("Data");
                workSheet.Cells.Style.WrapText = true;              //自动换行

                for (int i = 0; i < dataList.Count; i++)
                {
                    bar.Update(i + 1, dataList.Count);

                    workSheet.Cells[i + 1, 1].Value = i + 1;
                    workSheet.Cells[i + 1, 2].Value = dataList[i].Identifier;
                    workSheet.Cells[i + 1, 3].Value = dataList[i].Text;
                }
                bar.End();
                excelPack.Save();
            }
        }
    }
}