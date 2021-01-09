using OfficeOpenXml;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using TaikoData;

using ToolKits;

namespace SystemTextConverter3DS
{
    internal class SystemTextReader : DataReader

    {
        private readonly ConsoleProgressBar bar;

        private string titleName;

        public class Data : SystemTextData
        {
        }

        /// <summary>
        /// 数据对象列表
        /// </summary>
        public List<Data> dataList;

        public SystemTextReader() : base()
        {
            bar = new ConsoleProgressBar();
        }

        public SystemTextReader(string path) : base(path)
        {
            bar = new ConsoleProgressBar();
        }

        public SystemTextReader(DataFile datFile) : base(datFile)
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
            var count = datFile.GetInt32();
            var titleAddr = datFile.GetInt32();
            var indexAddr = datFile.GetInt32();

            titleName = datFile.GetString(titleAddr, indexAddr - titleAddr);

            datFile.Seek = indexAddr;

            bar.Start();
            for (int i = 0; i < count; i++)
            {
                bar.Update(i + 1, count);
                dList.Add(new Data()
                {
                    IdentifierAddr = datFile.GetInt32(),
                    TextAddr = datFile.GetInt32(),
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
                var workSheet = excelPackage.Workbook.Worksheets.Add(titleName);
                workSheet.Cells.Style.WrapText = true;              //自动换行
                for (int i = 0; i < dataList.Count; i++)
                {
                    bar.Update(i + 1, dataList.Count);
                    workSheet.Cells[i + 1, 1].Value = i + 1;
                    workSheet.Cells[i + 1, 2].Value = dataList[i].Identifier;
                    workSheet.Cells[i + 1, 3].Value = dataList[i].Text;
                }
                bar.End();
                excelPackage.Save();
            }
        }
    }
}