using System;

namespace Lanpa
{
    public static class LanpaUtils
    {
        /// <summary>
        /// 是否为基础类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsBaseType(Type type)
        {
            return type == typeof(string) ||
                   type == typeof(int) ||
                   type == typeof(float) ||
                   type == typeof(double) ||
                   type == typeof(decimal) ||
                   type == typeof(char) ||
                   type == typeof(byte) ||
                   type == typeof(sbyte) ||
                   type == typeof(short) ||
                   type == typeof(ushort) ||
                   type == typeof(uint) ||
                   type == typeof(long) ||
                   type == typeof(ulong);
        }

        /// <summary>
        /// 将字符串转换为指定基础类型
        /// </summary>
        /// <param name="type"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        public static object Convert(Type type, string str)
        {
            if (type == typeof(string))
            {
                return str;
            }
            if (type == typeof(int))
            {
                if (int.TryParse(str, out var result))
                {
                    return result;
                }
                return default(int);
            }
            if (type == typeof(float))
            {
                if (float.TryParse(str, out var result))
                {
                    return result;
                }
                return default(float);
            }
            if (type == typeof(double))
            {
                if (double.TryParse(str, out var result))
                {
                    return result;
                }
                return default(double);
            }
            if (type == typeof(decimal))
            {
                if (decimal.TryParse(str, out var result))
                {
                    return result;
                }
                return default(decimal);
            }
            if (type == typeof(char))
            {
                if (char.TryParse(str, out var result))
                {
                    return result;
                }
                return default(char);
            }
            if (type == typeof(byte))
            {
                if (byte.TryParse(str, out var result))
                {
                    return result;
                }
                return default(byte);
            }
            if (type == typeof(sbyte))
            {
                if (sbyte.TryParse(str, out var result))
                {
                    return result;
                }
                return default(sbyte);
            }
            if (type == typeof(short))
            {
                if (short.TryParse(str, out var result))
                {
                    return result;
                }
                return default(short);
            }
            if (type == typeof(ushort))
            {
                if (ushort.TryParse(str, out var result))
                {
                    return result;
                }
                return default(ushort);
            }
            if (type == typeof(uint))
            {
                if (uint.TryParse(str, out var result))
                {
                    return result;
                }
                return default(uint);
            }
            if (type == typeof(long))
            {
                if (long.TryParse(str, out var result))
                {
                    return result;
                }
                return default(long);
            }
            if (type == typeof(ulong))
            {
                if (ulong.TryParse(str, out var result))
                {
                    return result;
                }
                return default(ulong);
            }
            return null;
        }
    }
}