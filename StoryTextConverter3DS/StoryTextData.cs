using System;
using System.Collections.Generic;
using System.Text;

namespace StoryTextConverter3DS
{
    internal class StoryTextData
    {
        /// <summary>
        /// 文本标识符地址
        /// </summary>
        public int IdentifierAddr { get; set; }

        /// <summary>
        /// 文本和假名索引地址
        /// </summary>
        public int SubIndexAddr { get; set; }

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

        /// <summary>
        /// 讲话人标识地址
        /// </summary>
        public int SpeakerAddr { get; set; }

        /// <summary>
        /// 讲话人
        /// </summary>
        public string Speaker { get; set; }

        /// <summary>
        /// 假名数量
        /// </summary>
        public int KanaCount { get; set; }

        /// <summary>
        /// 假名起始地址
        /// </summary>
        public int KanaAddr { get; set; }
    }
}