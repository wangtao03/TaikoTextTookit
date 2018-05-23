using System;
using System.IO;
using Taiko3DS3;
using System.Collections.Generic;
using System.Text;

namespace TaikoTextConsole
{
    internal class Program
    {
        //默认类型为转换系统文本
        private bool sysMode = true;

        //默认模式为转换到xlsx
        private bool toXlsx = true;

        private string outPath = string.Empty;

        private List<string> filePaths = new List<string>();

        /// <summary>
        /// 显示使用方法
        /// </summary>
        private static void GetUsaeg()
        {
            var sb = new StringBuilder();
            sb.Append("用法:\n");
            sb.Append("类型:\t可选，默认为系统文本\n");
            sb.Append("\t-s\t转换故事文本\n");
            sb.Append("\t--s\t转换系统文本\n\n");

            sb.Append("模式:\t可选，默认为将dat转xlsx\n");
            sb.Append("\t-d\t将xlsx转换为dat\n");
            sb.Append("\t-x\t将dat转换为xlsx\n\n");

            sb.Append("输入输出:\n");
            sb.Append("\t-o [目录]\t可选，默认与输入文件同目录\n");
            sb.Append("\t-f [文件]\t必须，要被转换的文件,可多个之间用空格隔开\n");

            Console.Write(sb.ToString());
        }

        /// <summary>
        /// 解析命令行参数
        /// </summary>
        /// <param name="args">命令行参数</param>
        private void CmdParser(string[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i].ToLower())
                {
                    case "-s":
                        sysMode = false;
                        break;
                    case "--s":
                        sysMode = true;
                        break;
                    case "-d":
                        toXlsx = false;
                        break;
                    case "-x":
                        toXlsx = true;
                        break;
                    case "-o":
                        i++;
                        outPath = args[i];
                        break;
                    case "-f":
                        i++;
                        filePaths.Clear();
                        for (int j = i; j < args.Length; j++)
                        {
                            if (args[j].StartsWith("-"))
                            {
                                i = --j;
                                break;
                            }
                            filePaths.Add(args[j]);
                        }
                        break;
                    default:
                        GetUsaeg();
                        return;
                }
            }
        }

        private void RunCmd()
        {
            var value = 0;
            value = sysMode ? 0 : 1;
            value += toXlsx ? 2 : 4;

            switch (value)
            {
                case 2:
                    SystemDat2Xlsx();
                    break;
                case 4:
                    SystemXlsx2Dat();
                    break;
                case 3:
                    StoryDat2Xlsx();
                    break;
                case 5:
                    StoryXlsx2Dat();
                    break;
                default:
                    break;
            }

        }

        private void SystemDat2Xlsx()
        {
            using (var text = new SystemText())
            {
                foreach (var file in filePaths)
                {
                    var savePath = (outPath.Length > 0) ? $@"{outPath}\{Path.GetFileNameWithoutExtension(file)}.xslx" : Path.ChangeExtension(file, "xslx");
                    text.Reader(file);
                    text.Export(savePath);
                }
            }
        }

        private void SystemXlsx2Dat()
        {
            using (var text = new SystemText())
            {
                foreach (var file in filePaths)
                {
                    var savePath = (outPath.Length > 0) ? $@"{outPath}\{Path.GetFileNameWithoutExtension(file)}.dat" : Path.ChangeExtension(file, "dat");
                    text.ImportAndSave(file, "", savePath);
                }
            }
        }

        private void StoryDat2Xlsx()
        {
            using (var text = new StoryText())
            {
                foreach (var file in filePaths)
                {
                    var savePath = (outPath.Length > 0) ? $@"{outPath}\{Path.GetFileNameWithoutExtension(file)}.xslx" : Path.ChangeExtension(file, "xslx");
                    text.Reader(file);
                    text.Export(savePath);
                }
            }

        }

        private void StoryXlsx2Dat()
        {
            using (var text = new StoryText())
            {
                foreach (var file in filePaths)
                {
                    var savePath = (outPath.Length > 0) ? $@"{outPath}\{Path.GetFileNameWithoutExtension(file)}.dat" : Path.ChangeExtension(file, "dat");
                    text.ImportAndSave(file, savePath);
                }
            }
        }

        private static void Main(string[] args)
        {
            var p = new Program();
            if (args.Length > 0)
            {
                p.CmdParser(args);
                p.RunCmd();
            }
            GetUsaeg();
        }
    }
}