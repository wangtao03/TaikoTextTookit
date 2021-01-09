namespace CharaNameConverter3DS
{
    internal class CharaNameData
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

        /// <summary>
        /// 索引号?
        /// </summary>
        public short Index { get; set; }

        /// <summary>
        /// 未知信息 固定 40 00
        /// </summary>
        public short Unknow { get; set; }
    }
}