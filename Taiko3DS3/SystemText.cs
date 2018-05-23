using OfficeOpenXml;

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

using Taiko3DS3.IO;

namespace Taiko3DS3
{
    public class SystemText : IDisposable
    {
        /// <summary>
        /// 文件头结构
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public sealed class Header
        {
            public int textCount;               //文本数量
            public int titleOffset;             //文本标题地址
            public int textIndexOffset;         //文本索引地址
        }

        /// <summary>
        /// 文本索引结构
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public sealed class TextIndex
        {
            public int identifierOffset;        //文本标识地址
            public int textOffset;              //文本索引地址
        }

        /// <summary>
        /// 文字存储用结构
        /// </summary>
        public sealed class Label
        {
            public string Identifier { get; set; }      //文本标识
            public string OriginalText { get; set; }    //原文
            public string TranslateText { get; set; }   //译文

            public Label()
            {
                Identifier = string.Empty;
                OriginalText = string.Empty;
                TranslateText = string.Empty;
            }
        }

        /// <summary>
        /// 标识与文件名对应关系
        /// </summary>
        public static Dictionary<string, string> identifierList = new Dictionary<string, string> {
            { "AREAPOINT", "AreaPointText_00.dat" },
            { "BATTLE", "BattleText_00.dat" },
            { "BOSS", "BossText_00.dat" },
            { "CEC", "CecText_00.dat" },
            { "CHARA", "CharaText_00.dat" },
            { "COMMONUI", "CommonUIText_00.dat" },
            { "COSTUME", "CostumeText_00.dat" },
            { "DEGREE", "DegreeText_00.dat" },
            { "DIALOG", "DialogText_00.dat" },
            { "DIARY", "DiaryText_00.dat" },
            { "DONCARD", "DonCardText_00.dat" },
            { "ENCOUNT", "EncountText_00.dat" },
            { "ERROR", "ErrorText_00.dat" },
            { "HELP", "HelpText_00.dat" },
            { "HINT", "HintText_00.dat" },
            { "INFOMATION", "InfomationText_00.dat" },
            { "INFO", "InfoText_00.dat" },
            { "INITAP", "InitAppText_00.dat" },
            { "ITEM", "ItemText_00.dat" },
            { "LEVEL", "LevelText_00.dat" },
            { "MAGIC", "MagicText_00.dat" },
            { "NAVIMAP", "NaviMapText_00.dat" },
            { "NET", "NetText_00.dat" },
            { "QR", "QrText_00.dat" },
            { "QUEST", "QuestText_00.dat" },
            { "SKILL", "SkillText_00.dat" },
            { "STAMP", "StampText_00.dat" },
            { "STORE", "StoreText_00.dat" },
            { "STORYSYSTEM", "StorySystemText_00.dat" },
            { "SYSTEM", "SystemText_00.dat" },
            { "WORLD", "WorldText_00.dat" }
        };

        //文本标题
        private string textTitle;

        public string TextTitle { get => textTitle; }

        public Header header;
        public List<TextIndex> textIndexs;
        public List<Label> labels;

        public SystemText()
        {
            Init();
        }

        public SystemText(string path)
        {
            Init();
            Reader(path);
        }

        public SystemText(byte[] bytes)
        {
            Init();
            Reader(bytes);
        }

        /// <summary>
        /// 初始结构和变量
        /// </summary>
        private void Init()
        {
            textTitle = string.Empty;
            textIndexs = new List<TextIndex>();
            labels = new List<Label>();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 使用 ReaderX 填充结构和变量
        /// </summary>
        /// <param name="rx">ReaderX</param>
        public void Reader(ReaderX rx)
        {
            //读取文件头
            header = rx.ReadStruct<Header>();
            //读取并设置文本标题
            textTitle = rx.PeekStringByZero(header.titleOffset, header.textIndexOffset - header.titleOffset);
            //跳转到文本索引地址
            rx.Seek(header.textIndexOffset);
            for (int i = 0; i < header.textCount; i++)
            {
                //读取文本索引
                textIndexs.Add(rx.ReadStruct<TextIndex>());

                labels.Add(new Label
                {
                    //添加标识
                    Identifier = rx.PeekStringByZero(textIndexs[i].identifierOffset, textIndexs[i].textOffset - textIndexs[i].identifierOffset),
                    //添加原文
                    OriginalText = rx.PeekStringByZero(textIndexs[i].textOffset)
                });
            }
        }

        /// <summary>
        /// 以文件路径创建 ReaderX 并填充结构和变量
        /// </summary>
        /// <param name="path">文件路径</param>
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
                {
                    textCount = labels.Count
                };

                wx.WriterStructAlignment(newHeader);                    //写入文件头

                newHeader.titleOffset = wx.Position;                    //计算文件标题地址
                wx.WriterStringAlignment(textTitle);                    //写入文件标题

                newHeader.textIndexOffset = wx.Position;                //计算文本索引地址
                wx.WriterStruct(newHeader, 0, true);                    //写入计算后的文件头

                wx.SkipAlignment<TextIndex>(labels.Count);              //跳过索引位置

                var newIndexs = new List<TextIndex>();                  //创建新的文本索引
                for (int i = 0; i < labels.Count; i++)
                {
                    newIndexs.Add(new TextIndex());
                    newIndexs[i].identifierOffset = wx.Position;        //计算新的文本标识地址
                    wx.WriterStringAlignment(labels[i].Identifier);     //写入文本标识
                    newIndexs[i].textOffset = wx.Position;              //计算新的文本地址
                    wx.WriterStringAlignment((labels[i].TranslateText.Length > 0) ? labels[i].TranslateText : labels[i].OriginalText);                                //写入译文或原文
                }
                wx.WriterStruct<TextIndex>(newIndexs, newHeader.textIndexOffset, true);   //写入文本索引
                wx.Save2File(path);                                                    //保存到文件
            }
        }

        /// <summary>
        /// 从Excel文件导入
        /// </summary>
        /// <param name="path">Excel文件路径</param>
        /// <param name="title">指定的文本标题</param>
        /// <returns>新的labels</returns>
        public List<Label> Import(string path, string title = "")
        {
            var newLabels = new List<Label>();
            using (ExcelPackage package = new ExcelPackage(new FileInfo(path)))
            {
                var worksheet = (title.Length > 0) ? package.Workbook.Worksheets[title] : package.Workbook.Worksheets[1];
                if (worksheet != null)
                {
                    textTitle = worksheet.Name;
                    for (int i = worksheet.Dimension.Start.Row; i < worksheet.Dimension.End.Row; i++)
                    {
                        if (worksheet.Cells[i + 1, 1].Value != null)
                        {
                            newLabels.Add(new Label
                            {
                                Identifier = worksheet.Cells[i + 1, 1].Value.ToString().Replace("\r\n", "\n"),
                                //原文为为空 则输入为 ""
                                OriginalText = worksheet.Cells[i + 1, 2].Value == null ? "" : worksheet.Cells[i + 1, 2].Value.ToString().Replace("\r\n", "\n"),
                                //译文为空 则输入为 原文
                                TranslateText = worksheet.Cells[i + 1, 3].Value == null ? worksheet.Cells[i + 1, 2].Value == null ? "" : worksheet.Cells[i + 1, 2].Value.ToString().Replace("\r\n", "\n") : worksheet.Cells[i + 1, 3].Value.ToString().Replace("\r\n", "\n"),
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
        /// <param name="title">指定的文本标题</param>
        /// <returns>返回是否成功</returns>
        public bool ImportAndUpdata(string path, string title = "")
        {
            var newlabels = Import(path, title);
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
        /// <param name="title">指定的文本标题</param>
        /// <param name="savePath">dat保存路径</param>
        /// <returns>返回是否成功</returns>
        public bool ImportAndSave(string path, string savePath = "")
        {
            var result = ImportAndUpdata(path);
            if (result)
            {
                savePath = (savePath.Length > 0) ? savePath : $@"{ Path.GetDirectoryName(path)}\{identifierList[textTitle]}";
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
                        var worksheet = package.Workbook.Worksheets.Add(textTitle);
                        worksheet.Cells["A1"].Value = "文本标识";
                        worksheet.Cells["B1"].Value = "原文";
                        worksheet.Cells["C1"].Value = "译文";

                        for (int i = 0; i < labels.Count; i++)
                        {
                            worksheet.Cells[i + 2, 1].Value = labels[i].Identifier;
                            worksheet.Cells[i + 2, 2].Value = labels[i].OriginalText;
                            worksheet.Cells[i + 2, 3].Value = labels[i].TranslateText;
                        }
                        worksheet.Column(1).AutoFit(20);
                        worksheet.Column(2).AutoFit(50);
                        worksheet.Column(3).AutoFit(50);
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