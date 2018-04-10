using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace Taiko3DS3
{
    public class SystemText
    {
        //文件内文本数量
        private int count;

        //用于存放文件索引之前的信息
        private byte[] fileBytes;

        private int indexAddr;

        //用于存放文本信息的数据表
        private DataTable textTable;

        //文本标题名
        private string textTitle = string.Empty;

        private int textTitleAddr;  //标题名的地址

        /// <summary>
        /// 构造函数
        /// </summary>
        public SystemText()
        {
            InitDataTable();
        }

        /// <summary>
        /// 构造函数(解析文本)
        /// </summary>
        /// <param name="bytes">读取到的二进制文本</param>
        public SystemText(byte[] bytes)
        {
            InitDataTable();
            Read(bytes);
        }

        /// <summary>
        /// 析构函数
        /// </summary>
        ~SystemText()
        {
            Dispose();
        }

        public int Count { get => count; }

        //索引的地址
        public string TextTitle { get => textTitle; }

        /// <summary>
        /// 释放DataTable资源
        /// </summary>
        public void Dispose()
        {
            textTable.Dispose();
        }

        /// <summary>
        /// 导出数据表到Excel文件
        /// </summary>
        /// <param name="filename">文件名称</param>
        /// <returns>是否成功导出</returns>
        public bool Export(string filename)
        {
            try
            {
                if (textTable.Rows.Count > 0)
                {
                    using (var package = new ExcelPackage())
                    {
                        var worksheet = package.Workbook.Worksheets.Add(textTitle);
                        worksheet.Cells["A1"].Value = "标识";
                        worksheet.Cells["B1"].Value = "原文";
                        worksheet.Cells["C1"].Value = "译文";
                        for (int i = 0; i < textTable.Rows.Count; i++)
                        {
                            worksheet.Cells[i + 2, 1].Value = GetTextNoZero(i, TextType.identifier);
                            worksheet.Cells[i + 2, 2].Value = GetTextNoZero(i, TextType.content);
                            worksheet.Cells[i + 2, 3].Value = GetTextNoZero(i, TextType.translation);
                        }
                        worksheet.Column(1).AutoFit(20);
                        worksheet.Column(2).AutoFit(50);
                        worksheet.Column(3).AutoFit(50);
                        package.SaveAs(new FileInfo(@filename));
                    }
                    return true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return false;
        }

        /// <summary>
        /// 获取内容文本的起始地址
        /// </summary>
        /// <param name="index">row索引号</param>
        /// <returns></returns>
        public int GetAddr(int index)
        {
            if (index < textTable.Rows.Count)
            {
                return (int)textTable.Rows[index]["content_address"];
            }
            return 0;
        }

        /// <summary>
        /// 获取DataTable中文本相关内容(默认获取原文)
        /// </summary>
        /// <param name="index">row索引</param>
        /// <param name="type">文本类型枚举</param>
        /// <returns>数据表中对应的文本</returns>
        public string GetText(int index, TextType type = TextType.content)
        {
            if (index < textTable.Rows.Count)
            {
                switch (type)
                {
                    case TextType.identifier:
                        return textTable.Rows[index]["identifier"].ToString();

                    case TextType.content:
                        return textTable.Rows[index]["content"].ToString();

                    case TextType.translation:
                        return textTable.Rows[index]["translation"].ToString();
                }
            }
            return string.Empty;
        }

        public string GetTextNoZero(int index, TextType type = TextType.content)
        {
            return GetText(index, type).Replace("\0", string.Empty);
        }

        /// <summary>
        /// 从Excel导入译文
        /// </summary>
        /// <param name="filename">Excel文件名</param>
        /// <returns>返回结果 0为正常 -5为文本不符</returns>
        public int Import(string filename)
        {
            using (ExcelPackage package = new ExcelPackage(new FileInfo(filename)))
            {
                var worksheet = package.Workbook.Worksheets[1];
                if (worksheet.Name == textTitle)
                {
                    for (int i = 0; i < textTable.Rows.Count; i++)
                    {
                        SetText(i, worksheet.Cells[i + 2, 3].Value.ToString().Replace("\r\n", "\n"));
                    }
                    package.Dispose();
                    return 0;
                }
                else
                {
                    package.Dispose();
                    return -5;
                }
            }
        }

        public bool Import4Excel(string filename)
        {
            try
            {
                textTable.Rows.Clear();
                using (ExcelPackage package = new ExcelPackage(new FileInfo(filename)))
                {
                    var worksheet = package.Workbook.Worksheets[1];
                    textTitle = worksheet.Name;
                    count = worksheet.Dimension.End.Row-1;
                    for (int i = worksheet.Dimension.Start.Row; i < worksheet.Dimension.End.Row; i++)
                    {
                        textTable.Rows.Add(new object[] { 0, 0, worksheet.Cells[i + 1, 1].Value, worksheet.Cells[i + 1, 2].Value, worksheet.Cells[i + 1, 3].Value });
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        /// <summary>
        /// 从字节数组填充数DataTable
        /// </summary>
        /// <param name="bytes">从文本读取的二进制文本</param>
        public void Read(byte[] bytes)
        {
            fileBytes = bytes;
            count = BitConverter.ToInt32(bytes, 0);
            if (count > 0)
            {
                textTable.Rows.Clear(); //清空数据表

                textTitleAddr = BitConverter.ToInt32(bytes, 4); //获取文本标题地址
                indexAddr = BitConverter.ToInt32(bytes, 8); //获取文本索引地址

                textTitle = GetString(textTitleAddr, indexAddr - textTitleAddr).Replace("\0", string.Empty); //获取文本标题

                PaddingData(indexAddr);  //填充第一项数据到DataTable

                for (int i = 1; i < count; i++)
                {
                    if (i == count - 1)
                    {
                        PaddingData(indexAddr + i * 8, bytes.Length);    //填充最后一项
                    }
                    else
                    {
                        PaddingData(indexAddr + i * 8);  //填充当前项
                    }
                    SetText(i - 1, GetContent(bytes, i - 1), TextType.content);   //为上一项数据填充内容文本
                }
            }
        }

        /// <summary>
        /// 将译文替换原文位置并保存(译文超长部分被截取)
        /// </summary>
        /// <param name="filename">要保存的文件名</param>
        public void Save(string filename)
        {
            var saveBytes = fileBytes;
            for (int i = 0; i < textTable.Rows.Count; i++)
            {
                if (GetText(i, TextType.translation).Length > 0)
                {
                    var size = Encoding.UTF8.GetBytes(GetText(i, TextType.content)).Length;
                    var translation = Encoding.UTF8.GetBytes(GetText(i, TextType.translation));
                    Array.Copy(NewBytes(translation, size), 0, saveBytes, GetAddr(i), size);
                }
            }
            File.WriteAllBytes(@filename, saveBytes);
        }

        /// <summary>
        /// 依据数据格式从新生成文本文件()
        /// </summary>
        /// <param name="filename">要保存的文件名</param>
        /// <param name="title">文本文件的标题名</param>
        public void SaveAs(string filename, string title)
        {
            var header = new List<byte>();  //用于文件头数据
            var index = new List<byte>();   //用于文本索引数据
            var text = new List<byte>();    //用于文本数据

            var temp = NewBytes(Encoding.UTF8.GetBytes(title), 16);

            header.AddRange(BitConverter.GetBytes(count));  //文本数量
            header.AddRange(BitConverter.GetBytes(0x10));   //标题地址
            header.AddRange(BitConverter.GetBytes(0x20));   //索引地址
            header.AddRange(BitConverter.GetBytes(0x00));   //0无作用
            header.AddRange(temp);                          //文本标题名

            var offset = AlignmentSize(header.Count + textTable.Rows.Count * 8); //计算文本数据的起始位置

            for (int i = 0; i < textTable.Rows.Count; i++)
            {
                var size = AddBytes2List(text, GetText(i, TextType.identifier));
                index.AddRange(BitConverter.GetBytes(offset));
                offset += size;

                size = GetText(i, TextType.translation).Length > 0 ? AddBytes2List(text, GetText(i, TextType.translation)) : AddBytes2List(text, GetText(i, TextType.content));
                index.AddRange(BitConverter.GetBytes(offset));
                offset += size;
            }

            var difference = DifferenceSize(textTable.Rows.Count * 8);  //计算索引数据与对齐后的差值
            index.AddRange(new byte[difference]);                       //根据差值增加填充数据
            File.WriteAllBytes(@filename, header.Concat(index).Concat(text).ToArray());  //连接并保存到文件
        }

        /// <summary>
        /// 设置DataTable中文本相关内容
        /// </summary>
        /// <param name="index">row索引</param>
        /// <param name="text">设置的文本</param>
        /// <param name="type">文本类型枚举</param>
        public void SetText(int index, string text, TextType type = TextType.translation)
        {
            if (index < textTable.Rows.Count)
            {
                switch (type)
                {
                    case TextType.identifier:
                        textTable.Rows[index]["identifier"] = text;
                        break;

                    case TextType.content:
                        textTable.Rows[index]["content"] = text;
                        break;

                    case TextType.translation:
                        textTable.Rows[index]["translation"] = text;
                        break;
                }
            }
        }

        /// <summary>
        /// 将文本转化为数组并对齐添加到List
        /// </summary>
        /// <param name="list">要添加到的List</param>
        /// <param name="text">要添加的文本</param>
        /// <returns>对齐后的大小</returns>
        private static int AddBytes2List(List<byte> list, string text)
        {
            var temp = Encoding.UTF8.GetBytes(text.Replace("\r\n", "\n"));
            var size = AlignmentSize(temp.Length);
            temp = NewBytes(temp, size);
            list.AddRange(temp);

            return size;
        }

        /// <summary>
        /// 计算对齐后的值
        /// </summary>
        /// <param name="size">被对齐的值</param>
        /// <param name="mod">对齐量</param>
        /// <returns>对齐后的值</returns>
        private static int AlignmentSize(int size, int mod = 16)
        {
            if (size <= 16) return mod;
            var i = size / mod; //求整
            var r = size % mod; //求余
            if (r != 0) i++;
            return i * mod;
        }

        /// <summary>
        /// 计算当前值与对齐后的差值
        /// </summary>
        /// <param name="size">要被对齐的值</param>
        /// <param name="mod">对齐量</param>
        /// <returns>与对齐后的差值</returns>
        private static int DifferenceSize(int size, int mod = 16)
        {
            return AlignmentSize(size, mod) - size;
        }

        /// <summary>
        /// 从二进制文本中获取字符串
        /// </summary>
        /// <param name="bytes">二进制文本</param>
        /// <param name="start">起始位置</param>
        /// <param name="len">文本长度</param>
        /// <returns></returns>
        private static string GetString(byte[] bytes, int start, int len)
        {
            var temp = bytes.Skip(start).Take(len).ToArray();
            return System.Text.Encoding.UTF8.GetString(temp);
        }

        /// <summary>
        /// 生成指定大小的新字节数组
        /// </summary>
        /// <param name="bytes">目标字节数组</param>
        /// <param name="size">要生成的数组大小</param>
        /// <returns></returns>
        private static byte[] NewBytes(byte[] bytes, int size)
        {
            var temp = new byte[size];
            if (bytes.Length < size) size = bytes.Length;
            Array.Copy(bytes, 0, temp, 0, size);
            return temp;
        }

        /// <summary>
        /// 从二进制文本中获取内容
        /// </summary>
        /// <param name="bytes">二进制文本</param>
        /// <param name="i">row索引号</param>
        /// <returns></returns>
        private string GetContent(byte[] bytes, int i)
        {
            if (i < textTable.Rows.Count - 1)
            {
                return GetString(bytes, (int)textTable.Rows[i]["content_address"], (int)textTable.Rows[i + 1]["identifier_address"] - (int)textTable.Rows[i]["content_address"]);
            }
            return string.Empty;
        }

        private string GetContent(int i)
        {
            return GetContent(fileBytes, i);
        }

        private string GetString(int start, int len)
        {
            return GetString(fileBytes, start, len);
        }

        /// <summary>
        /// 初始化数据表
        /// </summary>
        private void InitDataTable()
        {
            textTable = new DataTable();
            textTable.Columns.Add("identifier_address", Type.GetType("System.Int32"));
            textTable.Columns.Add("content_address", Type.GetType("System.Int32"));
            textTable.Columns.Add("identifier", Type.GetType("System.String"));
            textTable.Columns.Add("content", Type.GetType("System.String"));
            textTable.Columns.Add("translation", Type.GetType("System.String"));
        }

        /// <summary>
        /// 填充DataTable
        /// </summary>
        /// <param name="bytes">读取到的二进制文本</param>
        /// <param name="identifierIndex">当前标识文本索引</param>
        /// <param name="contentIndex">当前内容文本索引</param>
        /// <param name="nextIdentifierIndex">下一条标识文本的索引</param>
        private void PaddingData(byte[] bytes, int identifierIndex, int contentIndex, int nextIdentifierIndex = 0)
        {
            var identifierAddress = BitConverter.ToInt32(bytes, identifierIndex);
            var contentAddress = BitConverter.ToInt32(bytes, contentIndex);
            var identifier = GetString(bytes, identifierAddress, contentAddress - identifierAddress);
            var content = String.Empty;

            if (nextIdentifierIndex > 0)
            {
                content = GetString(bytes, contentAddress, nextIdentifierIndex - contentAddress);
            }
            textTable.Rows.Add(new object[] { identifierAddress, contentAddress, identifier, content, null });
        }

        /// <summary>
        /// 为数据表添加内容(自动计算内容索引)
        /// </summary>
        /// <param name="bytes">读取到的二进制文本</param>
        /// <param name="identifierIndex">当前标识文本索引</param>
        /// <param name="nextIdentifierIndex">标识文本索引</param>
        private void PaddingData(byte[] bytes, int identifierIndex, int nextIdentifierIndex = 0)
        {
            PaddingData(bytes, identifierIndex, identifierIndex + 4, nextIdentifierIndex);
        }

        private void PaddingData(int identifierIndex, int contentIndex, int nextIdentifierIndex = 0)
        {
            PaddingData(fileBytes, identifierIndex, contentIndex, nextIdentifierIndex);
        }

        private void PaddingData(int identifierIndex, int nextIdentifierIndex = 0)
        {
            PaddingData(fileBytes, identifierIndex, identifierIndex + 4, nextIdentifierIndex);
        }
    }
}