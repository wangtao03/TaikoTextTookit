using OfficeOpenXml;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using TaikoData;

using ToolKits;

namespace StoryTextConverter3DS
{
    internal class StoryTextWriter : DataWriter
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

        public StoryTextWriter() : base()
        {
            bar = new ConsoleProgressBar();
        }

        public StoryTextWriter(DataBytes indexs, DataBytes content) : base(indexs, content)
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
                var workSheet = excelPackage.Workbook.Worksheets["StoryText"];
                var dimension = workSheet.Dimension;

                for (int i = dimension.Start.Row; i <= dimension.End.Row; i++)
                {
                    bar.Update(i, dimension.End.Row);
                    dataList.Add(new Data()
                    {
                        Identifier = workSheet.Cells[i, 2].Text + "\0",
                        //Text = workSheet.Cells[i, 3].Text + "\0",
                        Text = Utils.RehabilitateController(workSheet.Cells[i, 4].Text) + "\0",
                        Speaker = workSheet.Cells[i, 5].Text + "\0"
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

            indexs.AddInt32(0X10);                  //添加文件头
            indexs.AddInt32(dataList.Count);        //添加文件数量
            indexs.AddBytes(new byte[8]);           //手动对齐

            var indexSize = Utils.AlignSize(dataList.Count * (4 + 4 + 4) + indexs.Length);   //计算索引头对齐后大小

            for (int i = 0; i < dataList.Count; i++)
            {
                bar.Update(i + 1, dataList.Count);

                var size = Utils.AlignSize(dataList[i].Identifier);                                         //计算文本对齐后长度
                dataList[i].IdentifierAddr = content.AddString(dataList[i].Identifier, size) + indexSize;   //保存标识索引地址

                dataList[i].SubIndexAddr = content.AddBytes(new byte[16]);                                  //添加空的子索引 !!未加索引头偏移量

                size = Utils.AlignSize(dataList[i].Text);                                                   //计算文本对齐后长度
                dataList[i].TextAddr = content.AddString(dataList[i].Text, size) + indexSize;               //保存文本索引地址

                size = Utils.AlignSize(dataList[i].Speaker);                                                //计算文本对齐后长度
                dataList[i].SpeakerAddr = content.AddString(dataList[i].Speaker, size) + indexSize;         //保存讲话人索引

                content.InsertInt32(dataList[i].SubIndexAddr, dataList[i].TextAddr);                        //向子索引添加文本索引
                content.InsertInt32(dataList[i].SubIndexAddr + 0x4, dataList[i].SpeakerAddr);               //向子索引添加讲话人索引
                content.InsertInt32(dataList[i].SubIndexAddr + 0x8, 0);                                     //向子索引添加假名数量 0
                content.InsertInt32(dataList[i].SubIndexAddr + 0xC, content.Length + indexSize);            //向子索引添加下条标识索引

                dataList[i].SubIndexAddr += indexSize;                                                      //子索引地址增加头偏移量

                indexs.AddInt32(dataList[i].IdentifierAddr);                                                //写入标识索引
                indexs.AddInt32(dataList[i].SubIndexAddr);                                                  //写入子索引索引
                indexs.AddInt32(0x01);                                                                      //写入固定值1
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