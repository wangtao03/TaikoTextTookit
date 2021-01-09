using System;
using System.Diagnostics;

namespace ToolKits

{
    /// <summary>
    /// 控制台进度条
    /// </summary>
    public class ConsoleProgressBar
    {
        private int cursorTop, width;

        private static readonly object locker = new object();

        private bool isStart = false;

        public ConsoleProgressBar(int width)
        {
            this.width = width;
        }

        public ConsoleProgressBar()
        {
            width = 50;
        }

        public void Start()
        {
            cursorTop = Console.CursorTop;
            isStart = true;
        }

        public void Update(int value, string text = null)
        {
            if (!isStart) return;

            if (value < 0) value = 0;
            if (value > 100) value = 100;

            var percent = (int)((float)width / 100 * (float)value);
            Debug.WriteLine(value);
            lock (locker)
            {
                var l = Console.CursorLeft;
                var t = Console.CursorTop;
                Console.SetCursorPosition(0, cursorTop);
                Console.Write($"\0[{new string('*', percent)}{new string('\0', width - percent)}]\0");
                text ??= $"{value:##}%\0";
                Console.WriteLine(text);

                Console.SetCursorPosition(l, t);
            }
        }

        public void Update(int now, int max, string text = null)
        {
            var i = (float)now / (float)max * 100;
            Update((int)i, text);
        }

        public void End()
        {
            isStart = false;
        }
    }
}