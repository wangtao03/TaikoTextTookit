using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TaikoData
{
    /// <summary>
    /// Dat文件对象
    /// </summary>
    public class DataFile
    {
        /// <summary>
        /// 存储文件内容的字节数组
        /// </summary>
        private readonly byte[] bFile;

        /// <summary>
        /// 数组读取位置
        /// </summary>
        public long Seek { get; set; }

        /// <summary>
        /// 文件长度
        /// </summary>
        public int Length { get => bFile.Length; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="path">文件路径</param>
        public DataFile(string path)
        {
            bFile = File.ReadAllBytes(path);    //从文件读取全部字节
        }

        #region 静态函数

        /// <summary>
        /// 从源字节数组的指定位置获取指定长度的字节数组
        /// </summary>
        /// <param name="source">源字节数组</param>
        /// <param name="sourceIndex">源字节数组起始位置</param>
        /// <param name="length">目标字节数组长度</param>
        /// <returns>目标字节数组</returns>
        public static byte[] GetBytes(byte[] source, long sourceIndex, int length)
        {
            if (source.Length <= 0) return source;                    //检查源数组是否合法
            var tmpBytes = new byte[length];                          //创建临时字节数组
            Array.Copy(source, sourceIndex, tmpBytes, 0, length);     //从源数组指定位置复制指定长度的数据
            return tmpBytes;
        }

        /// <summary>
        /// 从字节数组的指定位置获取指定长度的字符串
        /// </summary>
        /// <param name="source">源字节数组</param>
        /// <param name="sourceIndex">源字节数组的位置</param>
        /// <param name="length">字符串的字节长度</param>
        /// <param name="trimEndNul">是否清除字符串结尾的0</param>
        /// <returns></returns>
        public static string GetString(byte[] source, long sourceIndex, int length, bool trimEndNul = true)
        {
            if (source.Length <= 0) return string.Empty;                //判断源数组是否合法
            var tmpBytes = GetBytes(source, sourceIndex, length);       //从源数组指定位置获取指定长度的新数组
            var tmpString = Encoding.UTF8.GetString(tmpBytes);          //将临时字节数组转为字符串
            return trimEndNul ? tmpString.TrimEnd('\0') : tmpString;    //根据选择替换字符串结尾0字符
        }

        /// <summary>
        /// 从字节数组的指定位置获取指Int16
        /// </summary>
        /// <param name="source">源字节数组</param>
        /// <param name="sourceIndex">源字节数组的位置</param>
        /// <returns></returns>
        public static short GetInt16(byte[] source, long sourceIndex)
        {
            if (source.Length <= 0) return 0;                       //判断源数组是否合法
            var tmpBytes = GetBytes(source, sourceIndex, 2);        //从源数组指定位置获取指定长度的新数组
            return BitConverter.ToInt16(tmpBytes);                  //从字节数组转为int32
        }

        /// <summary>
        /// 从字节数组的指定位置获取指Int32
        /// </summary>
        /// <param name="source">源字节数组</param>
        /// <param name="sourceIndex">源字节数组的位置</param>
        /// <returns></returns>
        public static int GetInt32(byte[] source, long sourceIndex)
        {
            if (source.Length <= 0) return 0;                       //判断源数组是否合法
            var tmpBytes = GetBytes(source, sourceIndex, 4);        //从源数组指定位置获取指定长度的新数组
            return BitConverter.ToInt32(tmpBytes);                  //从字节数组转为int32
        }

        /// <summary>
        /// 从字节数组的指定位置获取指long
        /// </summary>
        /// <param name="source">源字节数组</param>
        /// <param name="sourceIndex">源字节数组的位置</param>
        /// <returns></returns>
        public static long GetInt64(byte[] source, long sourceIndex)
        {
            if (source.Length <= 0) return 0;                       //判断源数组是否合法
            var tmpBytes = GetBytes(source, sourceIndex, 8);        //从源数组指定位置获取指定长度的新数组
            return BitConverter.ToInt64(tmpBytes);                  //从字节数组转为int64
        }

        /// <summary>
        /// 从字节数组获取指定结构体
        /// </summary>
        /// <typeparam name="T">结构体</typeparam>
        /// <param name="source">源字节数组</param>
        /// <param name="sourceIndex">源字节数组的位置</param>
        /// <param name="size">结构体的大小</param>
        /// <returns>结构体</returns>
        public static T GetStruct<T>(byte[] source, long sourceIndex, int size)
        {
            if (source.Length <= 0) return default;                //判断源数组是否合法
            var tmpBytes = GetBytes(source, sourceIndex, size);  //从源数组指定位置获取指定长度的新数组
            return Utils.Bytes2Struct<T>(tmpBytes);
        }

        /// <summary>
        /// 从字节数组搜索指定字节
        /// </summary>
        /// <param name="data">被搜索的字节数组</param>
        /// <param name="pattern">搜索的内容</param>
        /// <returns></returns>
        public static List<int> IndexOf(byte[] data, byte[] pattern)
        {
            List<int> matchedPos = new List<int>();

            if (data.Length == 0 || data.Length < pattern.Length) return matchedPos;

            int end = data.Length - pattern.Length;
            bool matched = false;

            for (int i = 0; i <= end; i++)
            {
                for (int j = 0; j < pattern.Length || !(matched = (j == pattern.Length)); j++)
                {
                    if (data[i + j] != pattern[j]) break;
                }
                if (matched)
                {
                    matched = false;
                    matchedPos.Add(i);
                }
            }
            return matchedPos;
        }

        #endregion 静态函数

        #region 普通函数

        /// <summary>
        /// 从源字节数组的指定位置获取指定长度的字节数组
        /// </summary>
        /// <param name="sourceIndex">源字节数组起始位置</param>
        /// <param name="length">目标字节数组长度</param>
        /// <returns>目标字节数组</returns>
        public byte[] GetBytes(long sourceIndex, int length) => GetBytes(bFile, sourceIndex, length);

        /// <summary>
        /// 从字节数组的指定位置获取指定长度的字符串
        /// </summary>
        /// <param name="sourceIndex">源字节数组的位置</param>
        /// <param name="length">字符串的字节长度</param>
        /// <param name="trimEndNul">是否清除字符串结尾的0</param>
        /// <returns></returns>
        public string GetString(long sourceIndex, int length, bool trimEndNul = true) => GetString(bFile, sourceIndex, length, trimEndNul);

        /// <summary>
        /// 从字节数组的指定位置获取指Int16
        /// </summary>
        /// <param name="sourceIndex">源字节数组的位置</param>
        /// <returns></returns>
        public short GetInt16(long sourceIndex) => GetInt16(bFile, sourceIndex);

        /// <summary>
        /// 从字节数组的指定位置获取指Int32
        /// </summary>
        /// <param name="sourceIndex">源字节数组的位置</param>
        /// <returns></returns>
        public int GetInt32(long sourceIndex) => GetInt32(bFile, sourceIndex);

        /// <summary>
        /// 从字节数组的指定位置获取指long
        /// </summary>
        /// <param name="sourceIndex">源字节数组的位置</param>
        /// <returns></returns>
        public long GetInt64(long sourceIndex) => GetInt64(bFile, sourceIndex);

        /// <summary>
        /// 从字节数组获取指定结构体
        /// </summary>
        /// <typeparam name="T">结构体</typeparam>
        /// <param name="sourceIndex">源字节数组的位置</param>
        /// <param name="size">结构体的大小</param>
        /// <returns>结构体</returns>
        public T GetStruct<T>(long sourceIndex, int size) => GetStruct<T>(bFile, sourceIndex, size);

        /// <summary>
        /// 从字节数组搜索指定字节
        /// </summary>
        /// <param name="pattern">搜索的内容</param>
        /// <returns></returns>
        public List<int> IndexOf(byte[] pattern) => IndexOf(bFile, pattern);

        #endregion 普通函数

        #region 类似文件指针的操作

        /// <summary>
        /// 从源字节数组的指定位置获取指定长度的字节数组
        /// </summary>
        /// <param name="length">目标字节数组长度</param>
        /// <returns>目标字节数组</returns>
        public byte[] GetBytes(int length)
        {
            var tmpBytes = GetBytes(Seek, length);                          //获取字节数组
            Seek += length;                                                 //移动读取位置

            return tmpBytes;
        }

        /// <summary>
        /// 从字节数组的指定位置获取指定长度的字符串
        /// </summary>
        /// <param name="sourceIndex">源字节数组的位置</param>
        /// <param name="length">字符串的字节长度</param>
        /// <param name="trimEndNul">是否清除字符串结尾的0</param>
        /// <returns></returns>
        public string GetString(int length, bool trimEndNul = true)
        {
            var tmpStr = GetString(Seek, length, trimEndNul);               //获取文本
            Seek += length;                                                 //移动读取位置

            return tmpStr;
        }

        /// <summary>
        /// 从字节数组的指定位置获取指Int16
        /// </summary>
        /// <param name="sourceIndex">源字节数组的位置</param>
        /// <returns></returns>
        public short GetInt16(int jump = 2)
        {
            var tmpInt32 = GetInt16(Seek);                                  //获取short
            Seek += jump;                                                   //移动读取位置
            return tmpInt32;
        }

        /// <summary>
        /// 从字节数组的指定位置获取指Int32
        /// </summary>
        /// <param name="sourceIndex">源字节数组的位置</param>
        /// <returns></returns>
        public int GetInt32(int jump = 4)
        {
            var tmpInt32 = GetInt32(Seek);                                  //获取int
            Seek += jump;                                                   //移动读取位置
            return tmpInt32;
        }

        /// <summary>
        /// 从字节数组的指定位置获取指long
        /// </summary>
        /// <param name="sourceIndex">源字节数组的位置</param>
        /// <returns></returns>
        public long GetInt64(int jump = 8)
        {
            var tmpInt64 = GetInt64(Seek);                                  //获取long
            Seek += jump;                                                   //移动读取位置
            return tmpInt64;
        }

        /// <summary>
        /// 从字节数组获取指定结构体
        /// </summary>
        /// <typeparam name="T">结构体</typeparam>
        /// <param name="size">结构体的大小</param>
        /// <returns>结构体</returns>
        public T GetStruct<T>(int size)
        {
            var tmpStruct = GetStruct<T>(Seek, size);
            Seek += size;                                                   //移动读取位置
            return tmpStruct;
        }

        #endregion 类似文件指针的操作
    }
}