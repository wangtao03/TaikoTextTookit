using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Taiko3DS3.IO
{
    public class WriterX : IDisposable
    {
        //用于存放二进制数据的字节数组
        public List<Byte> BaseBytes { get; set; }

        //用于存放当前读写位置的指针
        public int Position { get; set; }

        //大小端模式
        public ByteOrder ByteOrder { get; set; }

        //编码格式
        public Encoding Encoding { get; set; }

        /// <summary>
        /// 解析函数
        /// </summary>
        public WriterX()
        {
            Init();
        }

        /// <summary>
        /// 初始化变量
        /// </summary>
        private void Init()
        {
            BaseBytes = new List<byte>();
            Position = 0;   //设置读写位置位置为0
            ByteOrder = ByteOrder.LittleEndian; //设置默认为小端模式
            Encoding = Encoding.UTF8;   //默认为UTF8编码
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 结构体转字节数组
        /// </summary>
        /// <param name="structObj">结构体对象</param>
        /// <returns>转换后的字节数组</returns>
        public static byte[] StructToBytes(object structObj)
        {
            var size = Marshal.SizeOf(structObj);

            var bytes = new byte[size];
            var structPtr = Marshal.AllocHGlobal(size);
            //将结构体拷到分配好的内存空间
            Marshal.StructureToPtr(structObj, structPtr, false);
            //从内存空间拷贝到byte 数组
            Marshal.Copy(structPtr, bytes, 0, size);
            //释放内存空间
            Marshal.FreeHGlobal(structPtr);
            return bytes;
        }

        /// <summary>
        /// 扩展字节数组大小
        /// </summary>
        /// <param name="size">新的大小</param>
        public void DilatationList(int size)
        {
            var listSize = BaseBytes.ToArray().Length;

            if (size > listSize) BaseBytes.AddRange(new byte[size - listSize]);
        }

        /// <summary>
        /// 写入字节数组
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <param name="offset">偏移量</param>
        /// <param name="peek">是否移动读写指针</param>
        /// <returns>写入的大小</returns>
        public int WriterBytes(byte[] bytes, int offset, bool peek)
        {
            DilatationList(offset);
            if (peek)
            {
                BaseBytes.RemoveRange(offset, bytes.Count());
                BaseBytes.InsertRange(offset, bytes);
            }
            else
            {
                Seek(offset + bytes.Count());
                BaseBytes.AddRange(bytes);
            }
            return bytes.Count();
        }

        /// <summary>
        /// 写入结构体
        /// </summary>
        /// <param name="structObj">结构体</param>
        /// <param name="offset">偏移量</param>
        /// <param name="peek">是否移动读写指针</param>
        /// <returns>写入的大小</returns>
        public int WriterStruct(object structObj, int offset, bool peek)
        {
            if (offset < 0) offset = 0;
            var bytes = (ByteOrder == ByteOrder.LittleEndian) ? StructToBytes(structObj) : StructToBytes(structObj).Reverse().ToArray();

            return WriterBytes(bytes, offset, peek);
        }

        public int WriterStruct<T>(List<T> listObj, int offset, bool peek)
        {
            if (offset < 0) offset = 0;
            for (int i = 0; i < listObj.Count(); i++)
            {
                var bytes = (ByteOrder == ByteOrder.LittleEndian) ? StructToBytes(listObj[i]) : StructToBytes(listObj[i]).Reverse().ToArray();
                WriterBytes(bytes, offset, peek);
                offset += bytes.Count();
            }
            return Marshal.SizeOf(typeof(T)) * listObj.Count;
        }

        /// <summary>
        /// 写入文本
        /// </summary>
        /// <param name="str">文本</param>
        /// <param name="offset">偏移量</param>
        /// <param name="peek">是否移动读写指针</param>
        /// <returns>写入的大小</returns>
        public int WriterString(string str, int offset, bool peek)
        {
            //如果字符串为空,默认写入16个空字节
            if (str.Length <= 0) return WriterBytes(new byte[16], offset, peek);

            var bytes = Encoding.GetBytes(str);
            return WriterBytes(bytes, offset, peek);
        }

        /// <summary>
        /// 写入结构体并对齐
        /// </summary>
        /// <param name="structObj">结构体</param>
        /// <param name="offset">偏移量</param>
        /// <param name="peek">是否移动读写指针</param>
        /// <param name="AlignmentAmount">对其量</param>
        /// <returns>写入的大小</returns>
        public int WriterStructAlignment(object structObj, int offset, bool peek, int AlignmentAmount = 16)
        {
            var size = WriterStruct(structObj, offset, peek);
            var difference = Utils.GetAlignmentDifference(size, AlignmentAmount);
            BaseBytes.InsertRange(offset + size, new byte[difference]);
            if (!peek) Position += difference;
            return size + difference;
        }

        public int WriterBytesAlignment(byte[] bytes, int offset, bool peek, int AlignmentAmount = 16)
        {
            var size = WriterBytes(bytes, offset, peek);
            var difference = Utils.GetAlignmentDifference(size, AlignmentAmount);
            BaseBytes.InsertRange(offset + size, new byte[difference]);
            if (!peek) Position += difference;
            return size + difference;
        }

        /// <summary>
        /// 写入文本并对齐
        /// </summary>
        /// <param name="str">文本</param>
        /// <param name="offset">偏移量</param>
        /// <param name="peek">是否移动读写指针</param>
        /// <param name="AlignmentAmount">对其量</param>
        /// <returns>写入的大小</returns>
        public int WriterStringAlignment(string str, int offset, bool peek, int AlignmentAmount = 16)
        {
            var size = WriterString($"{str.TrimEnd('\0')}\0", offset, peek);
            var difference = Utils.GetAlignmentDifference(size, AlignmentAmount);
            BaseBytes.InsertRange(offset + size, new byte[difference]);
            if (!peek) Position += difference;
            return size + difference;
        }

        public int WriterStructAlignment<T>(List<T> listObj, int offset, bool peek, int AlignmentAmount = 16)
        {
            var size = WriterStruct<T>(listObj, offset, peek);
            var difference = Utils.GetAlignmentDifference(size, AlignmentAmount);
            BaseBytes.InsertRange(offset + size, new byte[difference]);
            if (!peek) Seek(offset + size + difference);
            return size + difference;
        }

        /// <summary>
        /// 设置读写指针绝对位置
        /// </summary>
        /// <param name="offset">偏移量</param>
        public void Seek(int offset)
        {
            if (offset >= 0) Position = offset;
        }

        /// <summary>
        /// 保存数据到文件
        /// </summary>
        /// <param name="path">保存到的位置</param>
        public void Save2File(string path)
        {
            File.WriteAllBytes(path, BaseBytes.ToArray());
        }

        /// <summary>
        /// 设置读写指针相对位置
        /// </summary>
        /// <param name="offset">偏移量</param>
        public void Skip(int offset) => Seek(Position + offset);

        public void Skip<T>(int count = 1) => Seek(Position + Marshal.SizeOf(typeof(T)) * count);

        public void Skip<T>(List<T> listobj) => Seek(Position + (Marshal.SizeOf(typeof(T)) * listobj.Count));

        public void SkipAlignment<T>(int count = 1) => Seek(Position + Utils.GetAlignmentSize<T>(count));

        public void SkipAlignment<T>(List<T> listobj) => Seek(Position + Utils.GetAlignmentSize<T>(listobj));

        public int WriterStruct(object structObj) => WriterStruct(structObj, Position, false);

        public int WriterBytes(byte[] bytes) => WriterBytes(bytes, Position, false);

        public int WriterString(string str) => WriterString(str, Position, false);

        public int WriterStruct(object structObj, int offset) => WriterStruct(structObj, offset, false);

        public int WriterBytes(byte[] bytes, int offset) => WriterBytes(bytes, offset, false);

        public int WriterString(string str, int offset) => WriterString(str, offset, false);

        public int PeekStruct(object structObj) => WriterStruct(structObj, Position, true);

        public int PeekBytes(byte[] bytes) => WriterBytes(bytes, Position, true);

        public int PeekString(string str) => WriterString(str, Position, true);

        public int PeekStruct(object structObj, int offset) => WriterStruct(structObj, offset, true);

        public int PeekBytes(byte[] bytes, int offset) => WriterBytes(bytes, offset, true);

        public int PeekString(string str, int offset) => WriterString(str, offset, true);

        public int WriterStructAlignment(object structObj) => WriterStructAlignment(structObj, Position, false);

        public int WriterBytesAlignment(byte[] bytes) => WriterBytesAlignment(bytes, Position, false);

        public int WriterStringAlignment(string str) => WriterStringAlignment(str, Position, false);

        public int WriterStructAlignment(object structObj, int offset) => WriterStructAlignment(structObj, offset, false);

        public int WriterBytesAlignment(byte[] bytes, int offset) => WriterBytesAlignment(bytes, offset, false);

        public int WriterStringAlignment(string str, int offset) => WriterStringAlignment(str, offset, false);

        public int PeekStructAlignment(object structObj) => WriterStructAlignment(structObj, Position, true);

        public int PeekBytesAlignment(byte[] bytes) => WriterBytesAlignment(bytes, Position, true);

        public int PeekStringAlignment(string str) => WriterStringAlignment(str, Position, true);

        public int PeekStructAlignment(object structObj, int offset) => WriterStructAlignment(structObj, offset, true);

        public int PeekBytesAlignment(byte[] bytes, int offset) => WriterBytesAlignment(bytes, offset, true);

        public int PeekStringAlignment(string str, int offset) => WriterStringAlignment(str, offset, true);
    }
}