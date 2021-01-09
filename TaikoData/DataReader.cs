namespace TaikoData
{
    public abstract class DataReader
    {
        /// <summary>
        /// dat文件对象
        /// </summary>
        protected DataFile datFile;

        /// <summary>
        /// 构造函数
        /// </summary>
        public DataReader()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="path">文件路径</param>
        public DataReader(string path)
        {
            LoadDat(path);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="datFile">dat文件对象</param>
        public DataReader(DataFile datFile)
        {
            this.datFile = datFile;
        }

        /// <summary>
        /// 载入dat文件
        /// </summary>
        /// <param name="path">dat文件路径</param>
        public void LoadDat(string path)
        {
            datFile = new DataFile(path);
        }

        /// <summary>
        /// 读取数据
        /// </summary>
        public abstract void ReadData();

        /// <summary>
        /// 保存数据到excel文件
        /// </summary>
        /// <param name="xlsxPath">excel文件路径</param>
        public abstract void SaveData(string xlsxPath);
    }
}