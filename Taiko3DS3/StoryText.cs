using OfficeOpenXml;

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

using Taiko3DS3.IO;

namespace Taiko3DS3
{
    public class StoryText : IDisposable
    {
        /// <summary>
        /// 文件头结构
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public sealed class Header
        {
            public int indexOffset;             //主索引地址
            public int indexCount;              //主索引数量
        }

        /// <summary>
        /// 主索引结构
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public sealed class Index
        {
            public int identifierOffset;        //文本标识地址
            public int textIndexOffset;         //文本索引地址
            public int unknow;                  //总为1
        }

        /// <summary>
        /// 文本索引结构
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public sealed class TextIndex
        {
            public int textOffset;              //文本地址
            public int speakerNameOffset;       //讲话人标识名地址
            public int hiraganaCount;           //平假名数量
            public int hiraganaIndexOffset;     //平假名索引地址
        }

        /// <summary>
        /// 文本类存储用结构
        /// </summary>
        public sealed class Label
        {
            public string Identifier { get; set; }      //文本标识
            public string OriginalText { get; set; }    //原文
            public string TranslateText { get; set; }   //译文
            public string SpeakerName { get; set; }     //说话人标识

            /// <summary>
            /// Label解析函数
            /// </summary>
            public Label()
            {
                Identifier = string.Empty;
                OriginalText = string.Empty;
                TranslateText = string.Empty;
                SpeakerName = "NAME_NONE";
            }
        }

        public Header header;
        public List<Index> indexs;
        public List<TextIndex> textIndexs;
        public List<Label> labels;

        /// <summary>
        /// 控制码对应关系字典
        /// </summary>
        public static Dictionary<string, string> controlList = new Dictionary<string, string>
        {
            //颜色控制码
            {"\u001B\u0001\u0002","{红色}" },
            {"\u001B\u0001\u0003","{绿色}" },
            {"\u001B\u0001\u0004","{蓝色}" },
            {"\u001B\u0001\u0005","{黄色}" },
            {"\u001B\u0001\u0007","{黑色}" },
            //占位控制码
        {"\u001B\u0002\u0001",string.Empty},
        {"\u001B\u0002\u0002",string.Empty},
        {"\u001B\u0002\u0003",string.Empty},
        {"\u001B\u0002\u0004",string.Empty},
        {"\u001B\u0002\u0005",string.Empty},
        {"\u001B\u0002\u0006",string.Empty},
        {"\u001B\u0002\u0007",string.Empty},
        {"\u001B\u0002\u0008",string.Empty},
        {"\u001B\u0002\u0009",string.Empty},
        {"\u001B\u0002\u000A",string.Empty},
        {"\u001B\u0002\u000B",string.Empty}
        };

        public StoryText()
        {
            Init();
        }

        public StoryText(string path)
        {
            Init();
            Reader(path);
        }

        public StoryText(byte[] bytes)
        {
            Init();
            Reader(bytes);
        }

        /// <summary>
        /// 初始化结构与变量
        /// </summary>
        private void Init()
        {
            indexs = new List<Index>();
            textIndexs = new List<TextIndex>();
            labels = new List<Label>();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 替换控制字符
        /// </summary>
        /// <param name="str">要处理的文本</param>
        /// <param name="replaceCode">true 为控制符转文本, false 为文本转控制符</param>
        /// <returns>处理后的字符串</returns>
        public static string ReplaceControlCode(string str, bool replaceCode = true)
        {
            var temp = str;
            foreach (var item in controlList)
            {
                temp = replaceCode ? temp.Replace(item.Key, item.Value) :
                    (item.Value == string.Empty) ? temp : temp.Replace(item.Value, item.Key);
            }
            return temp;
        }

        /// <summary>
        /// 使用 ReaderX 填充结构和变量
        /// </summary>
        /// <param name="rx">ReaderX</param>
        public void Reader(ReaderX rx)
        {
            //读取文件头
            header = rx.ReadStruct<Header>();
            //跳转到主索引地址
            rx.Seek(header.indexOffset);

            for (int i = 0; i < header.indexCount; i++)
            {
                //读入主索引
                indexs.Add(rx.ReadStruct<Index>());
                //读取文本索引
                textIndexs.Add(rx.PeekStruct<TextIndex>(indexs[i].textIndexOffset));

                labels.Add(new Label
                {
                    //读取并设置文本标识
                    Identifier = rx.PeekStringByZero(indexs[i].identifierOffset, indexs[i].textIndexOffset - indexs[i].identifierOffset),
                    //读取并设置原文
                    OriginalText = rx.PeekStringByZero(textIndexs[i].textOffset, textIndexs[i].speakerNameOffset - textIndexs[i].textOffset),
                    //读取并设置讲话人标识
                    SpeakerName = rx.PeekStringByZero(textIndexs[i].speakerNameOffset)
                });

                //处理控制字符
                labels[i].OriginalText = ReplaceControlCode(labels[i].OriginalText);
            }
        }

        /// <summary>
        /// 以文件路径创建 ReaderX 并填充结构和变量
        /// </summary>
        /// <param name="filename">文件路径</param>
        public void Reader(string path)
        {
            using (var rx = new ReaderX(path))
            {
                Reader(rx);
            }
        }

        /// <summary>
        /// 以字节数组创建 ReaderX 并填充结构和变量
        /// </summary>
        /// <param name="bytes"></param>
        public void Reader(byte[] bytes)
        {
            using (var rx = new ReaderX(bytes))
            {
                Reader(rx);
            }
        }

        /// <summary>
        /// 保存到文件
        /// </summary>
        /// <param name="path">文件路径</param>
        public void Save(string path)
        {
            using (var wx = new WriterX())
            {
                var newHeader = new Header()
                {                          //创建新文件头结构
                    indexCount = labels.Count                           //设置文本数量
                };

                wx.WriterStructAlignment(newHeader);                    //写入文件头
                newHeader.indexOffset = wx.Position;                    //设置主索引地址

                wx.WriterStruct(newHeader, 0, true);                    //写入计算后的文件头

                wx.SkipAlignment<Index>(labels.Count);                  //跳过主索引位置

                var newIndexs = new List<Index>();                      //创建新主索引
                var newTextIndexs = new List<TextIndex>();              //创建新文本索引

                for (int i = 0; i < labels.Count; i++)
                {
                    newIndexs.Add(new Index                             //添加主索引并设置默认值
                    {
                        identifierOffset = wx.Position,                 //获取并设置标识地址
                        textIndexOffset = 0,                            //临时设置文本索引地址
                        unknow = 1                                      //恒为 1
                    });
                    wx.WriterStringAlignment(labels[i].Identifier);     //写入文本标识
                    newIndexs[i].textIndexOffset = wx.Position;         //获取并设置文本索引地址

                    wx.SkipAlignment<TextIndex>();                      //跳过文本索引
                    newTextIndexs.Add(new TextIndex
                    {
                        textOffset = wx.Position,                       //获取并设置文本地址
                        speakerNameOffset = 0,                          //临时设置讲话人标识地址
                        hiraganaCount = 0,                              //抛弃假名 假名数量恒为0
                        hiraganaIndexOffset = 0                         //抛弃假名 恒为文件尾
                    });

                    //写入原文或译文
                    wx.WriterStringAlignment(ReplaceControlCode((labels[i].TranslateText.Length > 0) ? labels[i].TranslateText : labels[i].OriginalText, false));
                    //获取并设置讲话人标识地址
                    newTextIndexs[i].speakerNameOffset = wx.Position;
                    //写入讲话人标识
                    wx.WriterStringAlignment(labels[i].SpeakerName);
                    //写入文本索引
                    wx.WriterStructAlignment(newTextIndexs[i], newIndexs[i].textIndexOffset, true);
                }
                //写入主索引
                wx.WriterStruct<Index>(newIndexs, newHeader.indexOffset, true);
                //保存到文件
                wx.Save2File(path);
            }
        }

        /// <summary>
        /// 从Excel文件导入
        /// </summary>
        /// <param name="path">Excel文件路径</param>
        /// <returns>新的labels</returns>
        public static List<Label> Import(string path)
        {
            var newLabels = new List<Label>();
            using (ExcelPackage package = new ExcelPackage(new FileInfo(path)))
            {
                var worksheet = package.Workbook.Worksheets[1];
                if (worksheet != null)
                {
                    for (int i = worksheet.Dimension.Start.Row; i < worksheet.Dimension.End.Row; i++)
                    {
                        if (worksheet.Cells[i + 1, 1].Value != null)
                        {
                            newLabels.Add(new Label
                            {
                                Identifier = worksheet.Cells[i + 1, 1].Value.ToString().Replace("\r\n", "\n"),

                                OriginalText = worksheet.Cells[i + 1, 2].Value == null ? "" : worksheet.Cells[i + 1, 2].Value.ToString().Replace("\r\n", "\n"),
                                TranslateText = worksheet.Cells[i + 1, 3].Value == null ? worksheet.Cells[i + 1, 2].Value == null ? "" : worksheet.Cells[i + 1, 2].Value.ToString().Replace("\r\n", "\n") : worksheet.Cells[i + 1, 3].Value.ToString().Replace("\r\n", "\n"),
                                SpeakerName = worksheet.Cells[i + 1, 4].Value.ToString().Replace("\r\n", "\n")
                            });
                        }
                    }
                }
            }
            return newLabels;
        }

        /// <summary>
        /// 从Excel文件导入并更新labels
        /// </summary>
        /// <param name="path">Excel文件路径</param>
        /// <returns>返回是否成功</returns>
        public bool ImportAndUpdata(string path)
        {
            var newlabels = Import(path);
            if (newlabels.Count > 0)
            {
                labels = newlabels;
                return true;
            }
            return false;
        }

        /// <summary>
        /// 从Excel文件导入并保存为dat文件
        /// </summary>
        /// <param name="path">Excel文件路径</param>
        /// <param name="savePath">dat保存路径</param>
        /// <returns>返回是否成功</returns>
        public bool ImportAndSave(string path, string savePath = "")
        {
            var result = ImportAndUpdata(path);
            if (result)
            {
                savePath = (savePath.Length > 0) ? savePath : Path.ChangeExtension(path, "dat");
                Save(savePath);
            }

            return result;
        }

        /// <summary>
        /// 导出到Excel文件
        /// </summary>
        /// <param name="path">Exccel文件路径</param>
        public void Export(string path)
        {
            try
            {
                if (labels.Count > 0)
                {
                    using (var package = new ExcelPackage())
                    {
                        var worksheet = package.Workbook.Worksheets.Add(Path.GetFileNameWithoutExtension(path));
                        worksheet.Cells["A1"].Value = "文本标识";
                        worksheet.Cells["B1"].Value = "原文";
                        worksheet.Cells["C1"].Value = "译文";
                        worksheet.Cells["D1"].Value = "讲话人标识";
                        for (int i = 0; i < labels.Count; i++)
                        {
                            worksheet.Cells[i + 2, 1].Value = labels[i].Identifier;
                            worksheet.Cells[i + 2, 2].Value = labels[i].OriginalText;
                            worksheet.Cells[i + 2, 3].Value = labels[i].TranslateText;
                            worksheet.Cells[i + 2, 4].Value = labels[i].SpeakerName;
                        }
                        worksheet.Column(1).AutoFit(20);
                        worksheet.Column(2).AutoFit(50);
                        worksheet.Column(3).AutoFit(50);
                        worksheet.Column(4).AutoFit(50);
                        package.SaveAs(new FileInfo(path));
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}