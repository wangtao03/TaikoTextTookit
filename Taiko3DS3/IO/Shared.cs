using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Taiko3DS3.IO
{
    public enum ByteOrder : ushort
    {
        LittleEndian = 0xFEFF,
        BigEndian = 0xFFFE
    }

    public static class Utils
    {
        /// <summary>
        /// 按指定大小创建字节数组
        /// </summary>
        /// <param name="size">指定大小</param>
        /// <returns>指定大小的字节数组</returns>
        public static byte[] ZeroBytes(int size) => new byte[size];

        /// <summary>
        /// 计算当前与按指定字节量对齐后的差值
        /// </summary>
        /// <param name="size">当前大小</param>
        /// <param name="AlignmentAmount">规定的对齐字节量</param>
        /// <returns>原大小与对齐后的差值</returns>
        public static int GetAlignmentDifference(int size, int AlignmentAmount = 16)
        {
            var remainder = size % AlignmentAmount;

            return (remainder == 0) ? 0 : AlignmentAmount - remainder;
        }

        /// <summary>
        /// 计算当前按指定字节量对齐后的大小
        /// </summary>
        /// <param name="size">当前大小</param>
        /// <param name="AlignmentAmount">规定的对齐字节量</param>
        /// <returns>对齐后的大小</returns>
        public static int GetAlignmentSize(int size, int AlignmentAmount = 16)
        {
            var difference = GetAlignmentDifference(size, AlignmentAmount);
            return size + difference;
        }

        public static int GetAlignmentDifference(object obj) => GetAlignmentDifference(Marshal.SizeOf(obj));

        public static int GetAlignmentDifference<T>(int count = 1) => GetAlignmentDifference(Marshal.SizeOf(typeof(T)) * count);

        public static int GetAlignmentDifference<T>(List<T> obj) => GetAlignmentDifference(Marshal.SizeOf(typeof(T)) * obj.Count);

        public static int GetAlignmentSize(object obj) => GetAlignmentSize(Marshal.SizeOf(obj));

        public static int GetAlignmentSize<T>(int count = 1) => GetAlignmentSize(Marshal.SizeOf(typeof(T)) * count);

        public static int GetAlignmentSize<T>(List<T> obj) => GetAlignmentSize(Marshal.SizeOf(typeof(T)) * obj.Count);
    }
}