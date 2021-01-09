using OfficeOpenXml;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using TaikoData;

using ToolKits;

namespace SystemTextConverter3DS
{
    internal class SystemTextWriter : DataWriter
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

        public SystemTextWriter() : base()
        {
            bar = new ConsoleProgressBar();
        }

        public SystemTextWriter(DataBytes indexs, DataBytes content) : base(indexs, content)
        {
            bar = new ConsoleProgressBar();
        }

        public override void LoadData(string excelPath)
        {
            Console.WriteLine("\r\n从Excel读取数据!");
            bar.Start();
            dataList = new List<Data>();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var excelPackage = new ExcelPackage(new FileInfo(excelPath)))
            {
                var workSheet = excelPackage.Workbook.Worksheets[0];
                titleName = workSheet.Name + "\0";
                var dimension = workSheet.Dimension;

                for (int i = dimension.Start.Row; i <= dimension.End.Row; i++)
                {
                    bar.Update(i, dimension.End.Row);
                    dataList.Add(new Data()
                    {
                        Identifier = workSheet.Cells[i, 2].Text + "\0",
                        //Text = workSheet.Cells[i, 3].Text + "\0",
                        Text = workSheet.Cells[i, 4].Text + "\0",
                    });
                }
                bar.End();
            }
        }

        public override void WriteData(string datPath)
        {
            Console.WriteLine("\r\n创建内容索引!");
            bar.Start();

            if (dataList.Count <= 0) return;

            indexs.AddInt32(dataList.Count);                    //添加文件数量
            indexs.AddInt32(0X10);                              //添加标题地址
            var indexAddr = Utils.AlignSize(titleName);         //计算索引地址
            indexs.AddInt32(indexAddr + 0x10);                  //添加索引地址
            indexs.AddBytes(new byte[4]);                       //手动对齐
            indexs.AddString(titleName, indexAddr);             //添加标题

            var indexSize = Utils.AlignSize(dataList.Count * (4 + 4)) + Utils.AlignSize(indexs.Length);   //计算索引头对齐后大小

            for (int i = 0; i < dataList.Count; i++)
            {
                bar.Update(i + 1, dataList.Count);
                var size = Utils.AlignSize(dataList[i].Identifier);     //计算文本对齐后长度
                dataList[i].IdentifierAddr = content.AddString(dataList[i].Identifier, size) + indexSize;   //保存标识索引地址

                size = Utils.AlignSize(dataList[i].Text);               //计算文本对齐后长度
                dataList[i].TextAddr = content.AddString(dataList[i].Text, size) + indexSize;               //保存文本索引地址

                indexs.AddInt32(dataList[i].IdentifierAddr);            //写入标识索引
                indexs.AddInt32(dataList[i].TextAddr);                  //写入文本索引
            }
            bar.End();

            if (File.Exists(datPath)) File.Delete(datPath);             //文件存在则删除

            Console.WriteLine("\r\n写出到Dat文件!");
            using (var binaryWriter = new BinaryWriter(File.Open(datPath, FileMode.OpenOrCreate)))
            {
                binaryWriter.Write(Utils.ByteAlignment(indexs.Data));                        //将索引表写入文件
                binaryWriter.Write(Utils.ByteAlignment(content.Data));                       //将文本表写入文件
            }
        }
    }
}