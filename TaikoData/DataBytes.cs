using System;
using System.Collections.Generic;
using System.Text;

namespace TaikoData
{
    public class DataBytes
    {
        /// <summary>
        /// 保存二进制数据的字节列表
        /// </summary>
        private List<Byte> dBytes = new List<byte>();

        /// <summary>
        /// 获取二进制数据
        /// </summary>
        public byte[] Data { get => dBytes.ToArray(); }

        /// <summary>
        /// 获取数据长度
        /// </summary>
        public int Length { get => dBytes.Count; }

        public int Seek { get; set; }

        public DataBytes()
        {
            Seek = 0;
        }

        #region 静态函数

        /// <summary>
        /// 添加字节数组
        /// </summary>
        /// <param name="bList">字节数组列表</param>
        /// <param name="bytes">添加的字节数组</param>
        /// <param name="lenght">添加长度</param>
        /// <returns>插入位置索引</returns>
        public static int AddBytes(List<Byte> bList, byte[] bytes, int? lenght = null)
        {
            var tmpBytes = Utils.NewBytes(bytes, lenght);                     //创建临时数组

            var index = bList.Count > 0 ? bList.Count : 0;              //计算当前索引位置
            bList.AddRange(tmpBytes);                                   //添加字节数组
            return index;
        }

        /// <summary>
        /// 插入字节数组
        /// </summary>
        /// <param name="bList">字节数组列表</param>
        /// <param name="index">插入位置</param>
        /// <param name="bytes">插入的字节数组</param>
        /// <param name="lenght">插入长度</param>
        /// <returns>位置索引</returns>
        public static int InsertBytes(List<Byte> bList, int index, byte[] bytes, int? lenght = null)
        {
            var tmpBytes = Utils.NewBytes(bytes, lenght);                     //创建临时数组

            if (bList.Count > index)                                    //判断索引位置是否合法
            {
                //计算需要移除的长度
                var removeSize = Math.Min(tmpBytes.Length, (bList.Count - index));
                bList.RemoveRange(index, removeSize);                   //删除索引开始的指定长度数据
            }
            else
            {
                var addSize = index - bList.Count;                      //计算需要增加的长度
                if (addSize > 0) bList.AddRange(new byte[addSize]);     //增加数据
            }
            bList.InsertRange(index, tmpBytes);                         //索引位置插入给定数据
            return index;
        }

        /// <summary>
        /// 插入字符串
        /// </summary>
        /// <param name="bList">字节数组列表</param>
        /// <param name="index">插入位置</param>
        /// <param name="text">插入的字符串</param>
        /// <param name="lenght">插入长度</param>
        /// <returns>位置索引</returns>
        public static int InsertString(List<Byte> bList, int index, string text, int? lenght = null)
        {
            var tmpBytes = Encoding.UTF8.GetBytes(text);
            return InsertBytes(bList, index, tmpBytes, lenght);
        }

        /// <summary>
        /// 插入Int16
        /// </summary>
        /// <param name="bList">字节数组列表</param>
        /// <param name="index">插入位置</param>
        /// <param name="int16">Int16</param>
        /// <param name="lenght">插入长度</param>
        /// <returns>位置索引</returns>
        public static int InsertInt16(List<Byte> bList, int index, short int16, int? lenght = null)
        {
            var tmpBytes = BitConverter.GetBytes(int16);
            return InsertBytes(bList, index, tmpBytes, lenght);
        }

        /// <summary>
        /// 插入Int32
        /// </summary>
        /// <param name="bList">字节数组列表</param>
        /// <param name="index">插入位置</param>
        /// <param name="int32">Int32</param>
        /// <param name="lenght">插入长度</param>
        /// <returns>位置索引</returns>
        public static int InsertInt32(List<Byte> bList, int index, int int32, int? lenght = null)
        {
            var tmpBytes = BitConverter.GetBytes(int32);
            return InsertBytes(bList, index, tmpBytes, lenght);
        }

        /// <summary>
        /// 插入Int64
        /// </summary>
        /// <param name="bList">字节数组列表</param>
        /// <param name="index">插入位置</param>
        /// <param name="int64">Int32</param>
        /// <param name="lenght">插入长度</param>
        /// <returns>位置索引</returns>
        public static int InsertInt64(List<Byte> bList, int index, long int64, int? lenght = null)
        {
            var tmpBytes = BitConverter.GetBytes(int64);
            return InsertBytes(bList, index, tmpBytes, lenght);
        }

        /// <summary>
        /// 插入结构体
        /// </summary>
        /// <param name="bList">字节数组列表</param>
        /// <param name="index">插入位置</param>
        /// <param name="structObj">结构体</param>
        /// <param name="lenght">插入长度</param>
        /// <returns>位置索引</returns>
        public static int InsertStruct(List<Byte> bList, int index, object structObj, int? lenght = null)
        {
            var tmpBytes = Utils.Struct2Bytes(structObj);
            return InsertBytes(bList, index, tmpBytes, lenght);
        }

        #endregion 静态函数

        #region 普通函数

        /// <summary>
        /// 插入指定长度的字节数组
        /// </summary>
        /// <param name="index">插入索引</param>
        /// <param name="bytes">字节数组</param>
        /// <param name="lenght">插入长度</param>
        /// <returns>索引位置</returns>
        public int InsertBytes(int index, byte[] bytes, int? lenght = null) => InsertBytes(dBytes, index, bytes, lenght);

        /// <summary>
        /// 插入字符串
        /// </summary>
        /// <param name="index">插入索引</param>
        /// <param name="text">字符串</param>
        /// <param name="lenght">插入长度</param>
        /// <returns>索引位置</returns>
        public int InsertString(int index, string text, int? lenght = null) => InsertString(dBytes, index, text, lenght);

        /// <summary>
        /// 插入Int16
        /// </summary>
        /// <param name="index">插入位置</param>
        /// <param name="int16">Int16</param>
        /// <param name="lenght">插入长度</param>
        /// <returns>位置索引</returns>
        public int InsertInt16(int index, short int16, int? lenght = null) => InsertInt16(dBytes, index, int16, lenght);

        /// <summary>
        /// 插入Int32
        /// </summary>
        /// <param name="index">插入位置</param>
        /// <param name="int32">Int32</param>
        /// <param name="lenght">插入长度</param>
        /// <returns>位置索引</returns>
        public int InsertInt32(int index, int int32, int? lenght = null) => InsertInt32(dBytes, index, int32, lenght);

        /// <summary>
        /// 插入Int64
        /// </summary>
        /// <param name="index">插入位置</param>
        /// <param name="int64">Int32</param>
        /// <param name="lenght">插入长度</param>
        /// <returns>位置索引</returns>
        public int InsertInt64(int index, long int64, int? lenght = null) => InsertInt64(dBytes, index, int64, lenght);

        /// <summary>
        /// 插入结构体
        /// </summary>
        /// <param name="index"></param>
        /// <param name="structObj"></param>
        /// <param name="lenght"></param>
        /// <returns></returns>
        public int InsertStruct(int index, object structObj, int? lenght = null) => InsertStruct(dBytes, index, structObj, lenght);

        #endregion 普通函数

        #region 类似文件指针的操作

        public int InsertBytes(byte[] bytes, int? lenght = null)
        {
            var tmpInt = InsertBytes(Seek, bytes, lenght);
            lenght ??= bytes.Length;
            Seek += (int)lenght;
            return tmpInt;
        }

        public int InsertString(string text, int? lenght = null)
        {
            var tmpBytes = Encoding.UTF8.GetBytes(text);
            var tmpInt = InsertBytes(Seek, tmpBytes, lenght);
            lenght ??= tmpBytes.Length;
            Seek += (int)lenght;
            return tmpInt;
        }

        public int InsertInt16(short int16, int? lenght = null)
        {
            var tmpBytes = BitConverter.GetBytes(int16);
            var tmpInt = InsertBytes(Seek, tmpBytes, lenght);
            lenght ??= tmpBytes.Length;
            Seek += (int)lenght;
            return tmpInt;
        }

        public int InsertInt32(int int32, int? lenght = null)
        {
            var tmpBytes = BitConverter.GetBytes(int32);
            var tmpInt = InsertBytes(Seek, tmpBytes, lenght);
            lenght ??= tmpBytes.Length;
            Seek += (int)lenght;
            return tmpInt;
        }

        public int InsertInt64(long int64, int? lenght = null)
        {
            var tmpBytes = BitConverter.GetBytes(int64);
            var tmpInt = InsertBytes(Seek, tmpBytes, lenght);
            lenght ??= tmpBytes.Length;
            Seek += (int)lenght;
            return tmpInt;
        }

        public int InsertStruct(object structObj, int? lenght = null)
        {
            var tmpBytes = Utils.Struct2Bytes(structObj);
            var tmpInt = InsertBytes(Seek, tmpBytes, lenght);
            lenght ??= tmpBytes.Length;
            Seek += (int)lenght;
            return tmpInt;
        }

        public int AddBytes(byte[] bytes, int? lenght = null)
        {
            var tmpInt = AddBytes(dBytes, bytes, lenght);
            Seek = Length;
            return tmpInt;
        }

        public int AddString(string text, int? lenght = null)
        {
            var tmpBytes = Encoding.UTF8.GetBytes(text);
            var tmpInt = AddBytes(dBytes, tmpBytes, lenght);
            Seek = Length;
            return tmpInt;
        }

        public int AddInt16(short int16, int? lenght = null)
        {
            var tmpBytes = BitConverter.GetBytes(int16);
            var tmpInt = AddBytes(dBytes, tmpBytes, lenght);
            Seek = Length;
            return tmpInt;
        }

        public int AddInt32(int int32, int? lenght = null)
        {
            var tmpBytes = BitConverter.GetBytes(int32);
            var tmpInt = AddBytes(dBytes, tmpBytes, lenght);
            Seek = Length;
            return tmpInt;
        }

        public int AddInt64(long int64, int? lenght = null)
        {
            var tmpBytes = BitConverter.GetBytes(int64);
            var tmpInt = AddBytes(dBytes, tmpBytes, lenght);
            Seek = Length;
            return tmpInt;
        }

        public int AddStruct(object structObj, int? lenght = null)
        {
            var tmpBytes = Utils.Struct2Bytes(structObj);
            var tmpInt = AddBytes(dBytes, tmpBytes, lenght);
            Seek = Length;
            return tmpInt;
        }

        #endregion 类似文件指针的操作
    }
}