using System;
using System.Collections.Generic;
using System.Text;

namespace SystemTextConverter3DS
{
    internal class SystemTextData
    {
        /// <summary>
        /// 文本标识符地址
        /// </summary>
        public int IdentifierAddr { get; set; }

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
    }
}