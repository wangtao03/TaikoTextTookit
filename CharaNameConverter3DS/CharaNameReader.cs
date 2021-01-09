using CharaNameConverter3DS;

using OfficeOpenXml;

using System;
using System.Collections.Generic;
using System.IO;

using TaikoData;

using ToolKits;

namespace CharaNameConverter
{
    internal class CharaNameReader : DataReader
    {
        private readonly ConsoleProgressBar bar;

        public class Data : CharaNameData
        {
        }

        /// <summary>
        /// 数据对象列表
        /// </summary>
        public List<Data> dataList;

        public CharaNameReader() : base()
        {
            bar = new ConsoleProgressBar();
        }

        public CharaNameReader(string path) : base(path)
        {
            bar = new ConsoleProgressBar();
        }

        public CharaNameReader(DataFile datFile) : base(datFile)
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
            datFile.Seek = 4;
            var count = datFile.GetInt32();
            datFile.Seek = 16;

            bar.Start();
            for (int i = 0; i < count; i++)
            {
                bar.Update(i + 1, count);
                dList.Add(new Data()
                {
                    IdentifierAddr = datFile.GetInt32(),
                    TextAddr = datFile.GetInt32(),
                    Index = datFile.GetInt16(),
                    Unknow = datFile.GetInt16()
                });
            }
            bar.End();

            Console.WriteLine("\r\n读取文件内容!");
            bar.Start();
            for (int i = 0; i < dList.Count; i++)
            {
                bar.Update(i + 1, dList.Count);
                var length = 0;                     //默认文本长度为0
                try
                {
                    length = dList[i + 1].IdentifierAddr - dList[i].TextAddr;   //由下条文本地址来计算当前文本长度
                }
                catch (ArgumentOutOfRangeException)
                {
                    length = datFile.Length - dList[i].TextAddr;                //以文件结尾到当前文本地址为长度
                }
                finally
                {
                    dList[i].Identifier = datFile.GetString(dList[i].IdentifierAddr, dList[i].TextAddr - dList[i].IdentifierAddr);
                    dList[i].Text = datFile.GetString(dList[i].TextAddr, length);
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
            using (var excelPackage = new ExcelPackage(new FileInfo(xlsxPath)))
            {
                var workSheet = excelPackage.Workbook.Worksheets.Add("CharaName");
                workSheet.Cells.Style.WrapText = true;              //自动换行

                for (int i = 0; i < dataList.Count; i++)
                {
                    bar.Update(i + 1, dataList.Count);
                    workSheet.Cells[i + 1, 1].Value = i + 1;
                    workSheet.Cells[i + 1, 2].Value = dataList[i].Identifier;
                    workSheet.Cells[i + 1, 3].Value = dataList[i].Text;
                    workSheet.Cells[i + 1, 5].Value = dataList[i].Index;
                    workSheet.Cells[i + 1, 6].Value = dataList[i].Unknow;
                }
                bar.End();

                excelPackage.Save();
            }
        }
    }
}