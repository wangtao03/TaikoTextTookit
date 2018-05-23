using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Taiko3DS3.IO
{
    public class ReaderX : IDisposable
    {
        //用于存放二进制数据的字节数组
        public byte[] BaseBytes { get; set; }

        //用于存放当前读写位置的指针
        public int Position { get; set; }

        //大小端模式
        public ByteOrder ByteOrder { get; set; }

        //编码格式
        public Encoding Encoding { get; set; }

        /// <summary>
        /// 解析函数 以文件创建
        /// </summary>
        /// <param name="path">文件路径</param>
        public ReaderX(string path)
        {
            BaseBytes = File.Exists(path) ? File.ReadAllBytes(path) : new byte[0];
            Init();
        }

        /// <summary>
        /// 解析函数 以字节数组创建
        /// </summary>
        /// <param name="bytes">字节数组</param>
        public ReaderX(byte[] bytes)
        {
            BaseBytes = bytes;
            Init();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        private void Init()
        {
            Position = 0;   //设置读写位置位置为0
            ByteOrder = ByteOrder.LittleEndian; //设置默认为小端模式
            Encoding = Encoding.UTF8;   //默认为UTF8编码
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
        /// 设置读写指针相对位置
        /// </summary>
        /// <param name="offset">偏移量</param>
        public void Skip(int offset) => Seek(Position + offset);

        /// <summary>
        /// 读取并填充结构体
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="offset">偏移量</param>
        /// <param name="peek">是否保持读写指针位置</param>
        /// <returns>结构体</returns>
        public T ReadStruct<T>(int offset, bool peek)
        {
            var size = Marshal.SizeOf(typeof(T));

            if (offset < 0) offset = 0;

            var bytes = (ByteOrder == ByteOrder.LittleEndian) ? BaseBytes.Skip(offset).Take(size).ToArray() : BaseBytes.Skip(offset).Take(size).Reverse().ToArray();

            if (!peek) Seek(offset + size);

            var structPtr = Marshal.AllocHGlobal(size);
            Marshal.Copy(bytes, 0, structPtr, size);
            return Marshal.PtrToStructure<T>(structPtr);
        }

        /// <summary>
        /// 读取指定字节数的字节数组
        /// </summary>
        /// <param name="offset">偏移量</param>
        /// <param name="length">字节数</param>
        /// <param name="peek">是否保持指针</param>
        /// <returns>指定的字节数组</returns>
        public byte[] ReadBytes(int offset, int length, bool peek)
        {
            if (offset < 0) offset = 0;
            if (length < 1) length = 1;

            var bytes = BaseBytes.Skip(offset).Take(length).ToArray();

            if (!peek) Seek(offset + length);

            return bytes;
        }

        /// <summary>
        /// 读取指定字节数的字符串
        /// </summary>
        /// <param name="offset">偏移量</param>
        /// <param name="length">字节数</param>
        /// <param name="peek">是否保持读写指针</param>
        /// <returns></returns>
        public string ReadString(int offset, int length, bool peek)
        {
            var bytes = ReadBytes(offset, length, peek);
            return Encoding.GetString(bytes).TrimEnd('\0');
        }

        /// <summary>
        /// 根据\0位置读取字符串
        /// </summary>
        /// <param name="offset">偏移地址</param>
        /// <param name="size">读取的缓冲器大小</param>
        /// <returns>字符串</returns>
        public string PeekStringByZero(int offset, int size = 255)
        {
            var bytes = PeekBytes(offset, size);
            for (int i = 0; i < bytes.Length; i++)
            {
                if (bytes[i] == 0)
                {
                    bytes = bytes.Take(i).ToArray();
                    break;
                }
            }
            return Encoding.GetString(bytes).TrimEnd('\0');
        }

        public T ReadStruct<T>() => ReadStruct<T>(Position, false);

        public bool ReadBool() => ReadStruct<bool>(Position, false);

        public byte ReadByte() => ReadStruct<byte>(Position, false);

        public Int16 ReadInt16() => ReadStruct<Int16>(Position, false);

        public Int32 ReadInt32() => ReadStruct<Int32>(Position, false);

        public Int64 ReadInt64() => ReadStruct<Int64>(Position, false);

        public UInt16 ReadUInt16() => ReadStruct<UInt16>(Position, false);

        public UInt32 ReadUInt32() => ReadStruct<UInt32>(Position, false);

        public UInt64 ReadUInt64() => ReadStruct<UInt64>(Position, false);

        public T ReadStruct<T>(int offset) => ReadStruct<T>(offset, false);

        public bool ReadBool(int offset) => ReadStruct<bool>(offset, false);

        public byte ReadByte(int offset) => ReadStruct<byte>(offset, false);

        public Int16 ReadInt16(int offset) => ReadStruct<Int16>(offset, false);

        public Int32 ReadInt32(int offset) => ReadStruct<Int32>(offset, false);

        public Int64 ReadInt64(int offset) => ReadStruct<Int64>(offset, false);

        public UInt16 ReadUInt16(int offset) => ReadStruct<UInt16>(offset, false);

        public UInt32 ReadUInt32(int offset) => ReadStruct<UInt32>(offset, false);

        public UInt64 ReadUInt64(int offset) => ReadStruct<UInt64>(offset, false);

        public T PeekStruct<T>() => ReadStruct<T>(Position, true);

        public bool PeekBool() => ReadStruct<bool>(Position, true);

        public byte PeekByte() => ReadStruct<byte>(Position, true);

        public Int16 PeekInt16() => ReadStruct<Int16>(Position, true);

        public Int32 PeekInt32() => ReadStruct<Int32>(Position, true);

        public Int64 PeekInt64() => ReadStruct<Int64>(Position, true);

        public UInt16 PeekUInt16() => ReadStruct<UInt16>(Position, true);

        public UInt32 PeekUInt32() => ReadStruct<UInt32>(Position, true);

        public UInt64 PeekUInt64() => ReadStruct<UInt64>(Position, true);

        public T PeekStruct<T>(int offset) => ReadStruct<T>(offset, true);

        public bool PeekBool(int offset) => ReadStruct<bool>(offset, true);

        public byte PeekByte(int offset) => ReadStruct<byte>(offset, true);

        public Int16 PeekInt16(int offset) => ReadStruct<Int16>(offset, true);

        public Int32 PeekInt32(int offset) => ReadStruct<Int32>(offset, true);

        public Int64 PeekInt64(int offset) => ReadStruct<Int64>(offset, true);

        public UInt16 PeekUInt16(int offset) => ReadStruct<UInt16>(offset, true);

        public UInt32 PeekUInt32(int offset) => ReadStruct<UInt32>(offset, true);

        public UInt64 PeekUInt64(int offset) => ReadStruct<UInt64>(offset, true);

        public byte[] ReadBytes(int lenght) => ReadBytes(Position, lenght, false);

        public byte[] ReadBytes(int offset, int lenght) => ReadBytes(offset, lenght, false);

        public byte[] PeekBytes(int lenght) => ReadBytes(Position, lenght, true);

        public byte[] PeekBytes(int offset, int lenght) => ReadBytes(offset, lenght, true);

        public string ReadString(int lenght) => ReadString(Position, lenght, false);

        public string ReadString(int offset, int lenght) => ReadString(offset, lenght, false);

        public string PeekString(int lenght) => ReadString(Position, lenght, true);

        public string PeekString(int offset, int lenght) => ReadString(offset, lenght, true);
    }
}