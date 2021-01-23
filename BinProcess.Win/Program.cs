using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinProcess.Win
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                if (Directory.Exists(args[0]))
                {
                    args = GetFiles(args[0], "*.lua");
                }
                var count = 0;
                foreach (var file in args)
                {
                    try
                    {        
                        Console.WriteLine($"{++count} : {file}");
                        var lua = LoadLua(file);
                        File.WriteAllText(file, lua);
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
            }
            Console.WriteLine("处理完毕,按任意键继续!");
            Console.ReadKey();
        }



        private static string LoadLua(string path)
        {
            var lines = File.ReadAllLines(path);
            return LuaAnalysis(lines);
        }

        private static string LuaAnalysis(string[] lines)
        {
            var sb = new StringBuilder();
            var varDic = new Dictionary<string, string>();
            for (int i = 0; i < lines.Length; i++)

            {
                var words = lines[i].Trim().Split(' ');
                if (words.Length <= 0) continue;

                switch (words[0])
                {
                    case "local":
                        varDic = InitVariable(words);
                        continue;
                    case "function":

                        break;
                    default:
                        if (words.Length == 3 && words[1] == "=")
                        {
                            var key = words[0];
                            var value = Replace(varDic, words[2]);
                            varDic[key] = value;
                            continue;
                        }
                        var newLine = Replace(varDic, lines[i]);
                        sb.AppendLine(newLine);
                        break;
                }
            }
            return sb.ToString();
        }

        private static string Replace(Dictionary<string, string> varDic, string line)
        {
            string tmpLine = line;
            foreach (var item in varDic)
            {
                tmpLine = tmpLine.Replace(item.Key, item.Value);
            }
            return tmpLine;
        }

        private static Dictionary<string, string> InitVariable(string[] variables)
        {
            var varDic = new Dictionary<string, string>();

            for (int i = 1; i < variables.Length; i++)
            {
                var varKey = variables[i].Trim().Trim(',');
                if (varKey.Length > 0) varDic[varKey] = null;
            }

            return varDic;
        }

        private static string[] GetFiles(string dir, string pattern)
        {
            var files = new List<string>();
            files.AddRange(Directory.GetFiles(dir, pattern));
            var subDirs = Directory.GetDirectories(dir);
            foreach (var subDir in subDirs)
            {
                var subFiles = GetFiles(subDir, pattern);
                if (subFiles.Length > 0) files.AddRange(subFiles);
            }
            return files.ToArray();
        }
    }
}
