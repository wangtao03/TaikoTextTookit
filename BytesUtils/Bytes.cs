using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BytesUtils
{
    public static class Bytes
    {
        /// <summary>
        /// KMP算法
        /// </summary>
        /// <param name="bytes">被搜索的字节</param>
        /// <param name="searchBytes">要搜索的字节</param>
        /// <param name="startIndex">起始索引</param>
        /// <returns></returns>
        public static int IndexOf(byte[] bytes, byte[] searchBytes, int startIndex = 0)
        {
            var bSize = bytes.Count();
            var sSize = searchBytes.Count();

            if (sSize > bSize) return -1;

            var cSize = bSize - sSize;

            var i = startIndex > cSize ? cSize : startIndex;
            var j = 0;

            var next = GetNextVal(searchBytes);

            while (i < bSize && j < sSize)
            {
                if (j == -1 || bytes[i] == searchBytes[j])
                {
                    i++;
                    j++;
                }
                else
                {
                    j = next[j];
                }
            }
            return j >= sSize ? i - sSize : -1;
        }

        /// <summary>
        /// KMP算法(查找全部版)
        /// </summary>
        /// <param name="bytes">被搜索的字节</param>
        /// <param name="searchBytes">要搜索的字节</param>
        /// <param name="startIndex">起始索引</param>
        /// <returns></returns>
        public static int[] IndexOfAll(byte[] bytes, byte[] searchBytes, int startIndex = 0)
        {
            var list = new List<int>();
            var i = IndexOf(bytes, searchBytes, startIndex);
            while (i >= 0)
            {
                list.Add(i);
                i = IndexOf(bytes, searchBytes, i + 1);
            }
            return list.ToArray();
        }

        /// <summary>
        /// 获取KMP算法所需的next数组
        /// </summary>
        /// <param name="searchBytes">搜索字节</param>
        /// <returns>next数组</returns>
        private static int[] GetNextVal(byte[] searchBytes)
        {
            var j = 0;
            var k = -1;
            var nextVal = new int[searchBytes.Count()];
            nextVal[0] = -1;
            while (j < searchBytes.Count() - 1)
            {
                if (k == -1 || searchBytes[j] == searchBytes[k])
                {
                    j++;
                    k++;
                    nextVal[j] = searchBytes[j] != searchBytes[k] ? k : nextVal[k];
                }
                else
                {
                    k = nextVal[k];
                }
            }
            return nextVal;
        }

        /// <summary>
        /// 数组替换
        /// </summary>
        /// <param name="bytes">源数组</param>
        /// <param name="oldBytes">旧数组</param>
        /// <param name="newBytes">新数组</param>
        /// <returns>替换后的数组</returns>
        public static byte[] Replace(byte[] bytes, byte[] oldBytes, byte[] newBytes)
        {
            var offset = IndexOfAll(bytes, oldBytes);
            var tmp = bytes;
            var oSize = oldBytes.Count();
            var nSize = newBytes.Count();
            foreach (var i in offset)
            {
                if (i < 0) return tmp;
                if (oSize == nSize)
                {
                    Array.Copy(newBytes, 0, tmp, i, newBytes.Count());
                }
                else
                {
                    var list = new List<byte>();
                    var skip = i + oSize;
                    list.AddRange(tmp.Take(i));
                    list.AddRange(newBytes);
                    list.AddRange(tmp.Skip(skip).Take(tmp.Count() - skip));
                    tmp = list.ToArray<byte>();
                }
            }

            return tmp;
        }
        public static byte[] Replace(byte[] bytes, string oldStr, string newStr)
        {
            return Replace(bytes, Encoding.UTF8.GetBytes(oldStr), Encoding.UTF8.GetBytes(newStr));
        }
        public static byte[] Replace(byte[] bytes, byte[] oldBytes, string newStr)
        {
            return Replace(bytes, oldBytes, Encoding.UTF8.GetBytes(newStr));
        }
        public static byte[] Replace(byte[] bytes, string oldStr, byte[] newBytes)
        {
            return Replace(bytes, Encoding.UTF8.GetBytes(oldStr), newBytes);
        }
    }
}