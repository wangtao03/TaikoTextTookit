using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HexViewer
{
    public class Rows
    {
        private readonly List<string>[] hexList = new List<string>[3];

        public int Count { get => hexList[0].Count; }

        public string[] this[int index]
        {
            get
            {
                return new string[] { hexList[0][index].ToString(), hexList[1][index].ToString(), hexList[2][index].ToString() };
            }
            set
            {
                hexList[0][index] = value[0];
                hexList[1][index] = value[1];
                hexList[2][index] = value[2];
            }
        }

        public Rows()
        {
            hexList[0] = new List<string>();
            hexList[1] = new List<string>();
            hexList[2] = new List<string>();
        }

        public Rows(int num)
        {
            hexList[0] = new List<string>(num);
            hexList[1] = new List<string>(num);
            hexList[2] = new List<string>(num);
        }

        public void Add(string addr, string hex, string text)
        {
            hexList[0].Add(addr);
            hexList[1].Add(hex);
            hexList[2].Add(text);
        }

        public void Add(string[] hex)
        {
            if (hex.Count() >= 3)
            {
                Add(hex[0], hex[1], hex[2]);
            }
        }

        public void Insert(int index, string addr, string hex, string text)
        {
            hexList[0].Insert(index, addr);
            hexList[1].Insert(index, hex);
            hexList[2].Insert(index, text);
        }

        public void Insert(int index, string[] hex)
        {
            hexList[0].Insert(index, hex[0]);
            hexList[1].Insert(index, hex[1]);
            hexList[2].Insert(index, hex[2]);
        }

        public void RemoveAt(int index)
        {
            hexList[0].RemoveAt(index);
            hexList[1].RemoveAt(index);
            hexList[2].RemoveAt(index);
        }

        public void Clear()
        {
            hexList[0].Clear();
            hexList[1].Clear();
            hexList[2].Clear();
        }

        public object[] ToArray(int start = 0, int size = 0)
        {
            if (size == 0) size = this.Count;
            var addr = hexList[0].ToArray().Skip(start).Take(size).ToArray();
            var hex = hexList[1].ToArray().Skip(start).Take(size).ToArray();
            var text = hexList[2].ToArray().Skip(start).Take(size).ToArray();

            return new object[3] { addr, hex, text };
        }
    }

    public class HexView
    {
        public Rows rows;

        public int Count { get => rows.Count; }

        public HexView()
        {
            rows = new Rows();
        }

        public HexView(byte[] bytes, int columns = 16)
        {
            rows = new Rows();
            Analysis(bytes, columns);
        }

        public void Analysis(byte[] bytes, int columns = 16)
        {
            rows.Clear();
            var addLine = 1;
            if (bytes.Length % columns == 0) addLine = 0;
            var lines = bytes.Length / columns + addLine;

            for (int i = 0; i < lines; i++)
            {
                var temp = bytes.Skip(i * columns).Take(columns).ToArray();
                rows.Add((i * columns).ToString("X8"), BitConverter.ToString(temp).Replace("-", " "), @Encoding.UTF8.GetString(temp).Replace("\0", ".").TrimEnd());
            }
        }
    }
}