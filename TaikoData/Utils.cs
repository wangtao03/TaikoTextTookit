using System;
using System.Runtime.InteropServices;
using System.Text;

namespace TaikoData
{
    public static class Utils
    {
        /// <summary>
        /// 将文本转为16进制字符
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string Str2Hex(string text)
        {
            var sb = new StringBuilder();
            foreach (var c in text)
            {
                sb.Append(((byte)c).ToString("X2"));
            }
            return sb.ToString();
        }

        /// <summary>
        /// 替换控制字符
        /// </summary>
        /// <param name="text">需要处理的字符串</param>
        /// <returns>处理后的字符串</returns>
        public static string ReplaceController(string text)
        {
            var index = text.IndexOf("\u001B");
            while (index >= 0)
            {
                var sub = text.Substring(index, 3);
                text = text.Replace(sub, $"{{{Str2Hex(sub)}}}");
                index = text.IndexOf("\u001B");
            }
            return text;
        }

        /// <summary>
        /// 对齐字节数组
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <param name="alignSize">对齐长度</param>
        /// <returns>对齐后的数组</returns>
        public static byte[] ByteAlignment(byte[] bytes, int alignSize = 16)
        {
            var size = AlignSize(bytes, alignSize);         //与源数组长度相加 获得对齐后的数组长度
            if (size == bytes.Length) return bytes;         //长度相等直接返回

            //不相等
            var newBytes = new byte[size];                  //以对齐后的长度创建新数组
            Array.Copy(bytes, newBytes, bytes.Length);      //复制数据到新数组

            return newBytes;                                //返回对齐后的数组
        }

        /// <summary>
        /// 以指定长度创建新数组
        /// </summary>
        /// <param name="oldBytes">旧数组</param>
        /// <param name="length">给定长度</param>
        /// <returns>新数组</returns>
        public static byte[] NewBytes(byte[] oldBytes, int? length)
        {
            if (length == null || oldBytes.Length == (int)length) return oldBytes;         //长度为空或相同返回源数组

            var tmpBytes = new byte[(int)length];                        //创建新数组
            var tmpSize = Math.Min(oldBytes.Length, (int)length);        //获取两数组长度最小的大小
            Array.Copy(oldBytes, tmpBytes, tmpSize);                    //复制数据
            return tmpBytes;
        }

        /// <summary>
        /// 结构体转字节数组
        /// </summary>
        /// <param name="structObj">结构体对象</param>
        /// <returns>转换后的字节数组</returns>
        public static byte[] Struct2Bytes(object structObj)
        {
            var structSize = Marshal.SizeOf(structObj);             //获取结构体大小
            var tmpBytes = new byte[structSize];                    //创建同大小数组
            var structPtr = Marshal.AllocHGlobal(structSize);       //申请内存空间

            Marshal.StructureToPtr(structObj, structPtr, false);    //将结构体拷到分配好的内存空间
            Marshal.Copy(structPtr, tmpBytes, 0, structSize);       //从内存空间拷贝到字节数组
            Marshal.FreeHGlobal(structPtr);                         //释放内存空间
            return tmpBytes;
        }

        /// <summary>
        /// 字节数组转结构体
        /// </summary>
        /// <typeparam name="T">结构体</typeparam>
        /// <param name="bytes">字节数组</param>
        /// <returns>结构体</returns>
        public static T Bytes2Struct<T>(byte[] bytes)
        {
            var structSize = Marshal.SizeOf<T>();
            var structPtr = Marshal.AllocHGlobal(structSize);       //申请内存空间
            Marshal.Copy(bytes, 0, structPtr, structSize);          //从字节数组拷贝到内存空间
            return Marshal.PtrToStructure<T>(structPtr);            //转换为结构体
        }

        /// <summary>
        /// 计算对齐长度
        /// </summary>
        /// <param name="size">当前大小</param>
        /// <param name="alignSize">对齐位置</param>
        /// <returns></returns>
        public static int AlignSize(int size, int alignSize = 16)
        {
            if (size <= alignSize) return alignSize;        //小于或等于给定对齐大小则直接返回对齐大小

            var remainder = size % alignSize;               //求余
            if (remainder == 0) return size;                //被整除说明已经对齐,直接返回长度

            //不被整数
            var newSize = alignSize - remainder;            //计算对齐所缺字节数
            newSize += size;                                //与源长度相加 获得对齐后的长度

            return newSize;
        }

        /// <summary>
        /// 计算对齐长度
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <param name="alignSize">对齐位置</param>
        /// <returns></returns>
        public static int AlignSize(byte[] bytes, int alignSize = 16) => AlignSize(bytes.Length, alignSize);

        /// <summary>
        /// 计算对齐长度
        /// </summary>
        /// <param name="text">文本</param>
        /// <param name="alignSize">对齐位置</param>
        /// <returns></returns>
        public static int AlignSize(string text, int alignSize = 16) => AlignSize(Encoding.UTF8.GetBytes(text), alignSize);
    }
}