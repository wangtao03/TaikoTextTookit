namespace TaikoData
{
    public abstract class DataWriter
    {
        /// <summary>
        /// 索引和内容
        /// </summary>
        protected DataBytes indexs, content;

        public DataWriter()
        {
            indexs = new DataBytes();
            content = new DataBytes();
        }

        public DataWriter(DataBytes indexs, DataBytes content)
        {
            this.indexs = indexs;
            this.content = content;
        }

        /// <summary>
        /// 从Excel加载数据
        /// </summary>
        /// <param name="excelPath">xlsx文件路径</param>
        public abstract void LoadData(string excelPath);

        /// <summary>
        /// 创建Dat文件
        /// </summary>
        /// <param name="datPath">dat文件路径</param>
        public abstract void WriteData(string datPath);
    }
}