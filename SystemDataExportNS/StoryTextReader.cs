using OfficeOpenXml;

using System;
using System.Collections.Generic;
using System.IO;

using TaikoData;

using ToolKits;

namespace SystemDataSwitchExport
{
    internal class StoryTextReader : DataReader
    {
        private readonly ConsoleProgressBar bar;

        /// <summary>
        /// 文件单条数据结构
        /// </summary>
        public class Data
        {
            /// <summary>
            /// 文本标识符地址
            /// </summary>
            public int IdentifierAddr { get; set; }

            /// <summary>
            /// 文本和假名索引地址
            /// </summary>
            public int IndexAddr { get; set; }

            /// <summary>
            /// 文本标识符
            /// </summary>
            public string Identifier { get; set; }

            /// <summary>
            /// 文本地址
            /// </summary>
            public int TextAddr { get; set; }

            /// <summary>
            /// 文本内容
            /// </summary>
            public string Text { get; set; }

            /// <summary>
            /// 讲话人标识地址
            /// </summary>
            public int SpeakerAddr { get; set; }

            /// <summary>
            /// 讲话人
            /// </summary>
            public string Speaker { get; set; }

            /// <summary>
            /// 假名数量
            /// </summary>
            public int KanaCount { get; set; }

            /// <summary>
            /// 假名起始地址
            /// </summary>
            public int KanaAddr { get; set; }
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

            datFile.Seek = 8;
            var count = datFile.GetInt32(8);

            bar.Start();
            for (int i = 0; i < count; i++)
            {
                bar.Update(i + 1, count);
                dList.Add(new Data()
                {
                    IdentifierAddr = datFile.GetInt32(8),                     //读取标识符地址
                    IndexAddr = datFile.GetInt32(8),                          //读取索引串地址
                });
                _ = datFile.GetInt32(8);                                      //固定是1???

                dList[i].TextAddr = datFile.GetInt32((long)dList[i].IndexAddr);           //从索引串获得文本地址
                dList[i].SpeakerAddr = datFile.GetInt32((long)dList[i].IndexAddr + 8);    //从索引串获得讲述人地址
                dList[i].KanaCount = datFile.GetInt32((long)dList[i].IndexAddr + 16);     //从索引串获得假名数
                dList[i].KanaAddr = datFile.GetInt32((long)dList[i].IndexAddr + 24);      //从索引串获得假名

                //取得标识符
                dList[i].Identifier = datFile.GetString(dList[i].IdentifierAddr, dList[i].IndexAddr - dList[i].IdentifierAddr);
                //取得文本内容
                dList[i].Text = Utils.ReplaceController(datFile.GetString(dList[i].TextAddr, dList[i].SpeakerAddr - dList[i].TextAddr));
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
                var workSheet = excelPack.Workbook.Worksheets.Add("Data");
                workSheet.Cells.Style.WrapText = true;              //自动换行
                for (int i = 0; i < dataList.Count; i++)
                {
                    bar.Update(i + 1, dataList.Count);

                    workSheet.Cells[i + 1, 1].Value = i + 1;
                    workSheet.Cells[i + 1, 2].Value = dataList[i].Identifier;
                    workSheet.Cells[i + 1, 3].Value = dataList[i].Text;
                    workSheet.Cells[i + 1, 4].Value = dataList[i].Speaker;
                }
                bar.End();

                excelPack.Save();
            }
        }
    }
}