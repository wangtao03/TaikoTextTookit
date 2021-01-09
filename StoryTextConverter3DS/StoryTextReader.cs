using OfficeOpenXml;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

using TaikoData;

using ToolKits;

namespace StoryTextConverter3DS
{
    internal class StoryTextReader : DataReader
    {
        private readonly ConsoleProgressBar bar;

        /// <summary>
        /// 文件单条数据结构
        /// </summary>
        public class Data : StoryTextData
        {
        }

        /// <summary>
        /// 数据对象列表
        /// </summary>
        public List<Data> dataList;

        public StoryTextReader() : base()
        {
            bar = new ConsoleProgressBar();
        }

        public StoryTextReader(string path) : base(path)
        {
            bar = new ConsoleProgressBar();
        }

        public StoryTextReader(DataFile datFile) : base(datFile)
        {
            bar = new ConsoleProgressBar();
        }

        /// <summary>
        /// 读取数据
        /// </summary>
        public override void ReadData()
        {
            Console.WriteLine("\r\n读取文件内容!");
            var dList = new List<Data>();

            var regex = new Regex(@"{1B02\d\d}");

            datFile.Seek = 4;
            var count = datFile.GetInt32(12);

            bar.Start();
            for (int i = 0; i < count; i++)
            {
                bar.Update(i + 1, count);
                dList.Add(new Data()
                {
                    IdentifierAddr = datFile.GetInt32(),                      //读取标识符地址
                    SubIndexAddr = datFile.GetInt32(),                           //读取索引串地址
                });
                _ = datFile.GetInt32();                                       //固定是1???

                dList[i].TextAddr = datFile.GetInt32((long)dList[i].SubIndexAddr);           //从索引串获得文本地址
                dList[i].SpeakerAddr = datFile.GetInt32((long)dList[i].SubIndexAddr + 4);    //从索引串获得讲述人地址
                dList[i].KanaCount = datFile.GetInt32((long)dList[i].SubIndexAddr + 8);      //从索引串获得假名数
                dList[i].KanaAddr = datFile.GetInt32((long)dList[i].SubIndexAddr + 12);      //从索引串获得假名

                //取得标识符
                dList[i].Identifier = datFile.GetString(dList[i].IdentifierAddr, dList[i].SubIndexAddr - dList[i].IdentifierAddr);
                //取得文本内容
                dList[i].Text = Utils.ReplaceController(datFile.GetString(dList[i].TextAddr, dList[i].SpeakerAddr - dList[i].TextAddr));
                dList[i].Text = regex.Replace(dList[i].Text, "");   //替换假名占位控制符
                //取得讲述人标识
                dList[i].Speaker = datFile.GetString(dList[i].SpeakerAddr, dList[i].KanaAddr - dList[i].SpeakerAddr);
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
                var workSheet = excelPack.Workbook.Worksheets.Add("StoryText");
                workSheet.Cells.Style.WrapText = true;              //自动换行
                for (int i = 0; i < dataList.Count; i++)
                {
                    bar.Update(i + 1, dataList.Count);

                    workSheet.Cells[i + 1, 1].Value = i + 1;
                    workSheet.Cells[i + 1, 2].Value = dataList[i].Identifier;
                    workSheet.Cells[i + 1, 3].Value = dataList[i].Text;
                    workSheet.Cells[i + 1, 5].Value = dataList[i].Speaker;
                }
                bar.End();

                excelPack.Save();
            }
        }
    }
}