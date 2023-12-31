﻿using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Lanpa
{
    public class AttributeCheckVisitor : IAttributeFuncVisitor<bool, MemberInfo>
    {
        public static AttributeCheckVisitor Instance { get; } = new AttributeCheckVisitor();

        public bool Accept(LButtonAttribute attribute, MemberInfo memberInfo)
        {
            if (memberInfo is not MethodInfo)
            {
                Debug.LogWarning($"构建错误,{memberInfo.Name} 不是一个方法");
                return false;
            }
            return true;
        }

        public bool Accept(LCheckBoxAttribute attribute, MemberInfo memberInfo)
        {
            //检查是否是字段或者属性
            if (memberInfo is not FieldInfo && memberInfo is not PropertyInfo)
            {
                Debug.LogWarning($"构建错误,{memberInfo.Name} 不是一个字段或者属性");
                return false;
            }
            //检查是否是bool类型
            if (memberInfo.GetMemberType() != typeof(bool))
            {
                Debug.LogWarning($"构建错误,{memberInfo.Name} 不是一个bool类型");
                return false;
            }
            return true;
        }

        public bool Accept(LTextAttribute attribute, MemberInfo memberInfo)
        {
            //检查是否是字段或者属性
            if (memberInfo is not FieldInfo && memberInfo is not PropertyInfo)
            {
                Debug.LogWarning($"构建错误,{memberInfo.Name} 不是一个字段或者属性");
                return false;
            }
            //检查是否是string,int, float, double, decimal, char, byte, sbyte, short, ushort, uint, long, ulong
            if (memberInfo != null && !LanpaUtils.IsBaseType(memberInfo.GetMemberType()))
            {
                Debug.LogWarning($"构建错误,{memberInfo.Name} 不是一个基础数据类型");
                return false;
            }
            //检查是否是只读类型
            if (attribute is not LTextAttribute textAttribute)
            {
                Debug.LogWarning($"构建错误,{memberInfo.Name} 没有LTextAttribute");
                return false;
            }
            if (textAttribute.inputText && memberInfo is PropertyInfo propertyInfo && !propertyInfo.CanWrite)
            {
                Debug.LogWarning($"构建错误,{memberInfo.Name} 是一个只读属性, inputText不能为true");
                return false;
            }
            return true;
        }

        public bool Accept(LDropDownAttribute attribute, MemberInfo memberInfo)
        {
            //检查是否是枚举类型
            if (!memberInfo.GetMemberType().IsEnum)
            {
                Debug.LogWarning($"构建错误,{memberInfo.Name} 不是一个枚举类型");
                return false;
            }
            return true;
        }

        public bool Accept(LMultiDropDownAttribute attribute, MemberInfo memberInfo)
        {
            //检查是否是枚举类型
            if (!memberInfo.GetMemberType().IsEnum)
            {
                Debug.LogWarning($"构建错误,{memberInfo.Name} 不是一个枚举类型");
                return false;
            }
            //检查这个枚举类型是否有Flags特性
            if (!memberInfo.GetMemberType().IsDefined(typeof(FlagsAttribute), false))
            {
                Debug.LogWarning($"构建错误,{memberInfo.Name} 不是一个Flags枚举类型");
                return false;
            }
            return true;
        }

        public bool Accept(LDictionaryAttribute attribute, MemberInfo memberInfo)
        {
            //检查是否是字典类型
            if (!memberInfo.GetMemberType().IsGenericType || memberInfo.GetMemberType().GetGenericTypeDefinition() != typeof(Dictionary<,>))
            {
                Debug.LogWarning($"构建错误,{memberInfo.Name} 不是一个字典类型");
                return false;
            }
            return true;
        }

        public bool Accept(LUnityObjectAttribute attribute, MemberInfo memberInfo)
        {
            //检查是否是UnityObject类型
            if (!memberInfo.GetMemberType().IsSubclassOf(typeof(UnityEngine.Object)))
            {
                Debug.LogWarning($"构建错误,{memberInfo.Name} 不是一个UnityObject类型");
                return false;
            }
            return true;
        }

        public bool Accept(LSerializedObjectAttribute attribute, MemberInfo a)
        {
            //检查是否有SerializeField特性
            if (a is not FieldInfo fieldInfo || !fieldInfo.IsDefined(typeof(SerializeField), false))
            {
                Debug.LogWarning($"构建错误,{a.Name} 没有SerializeField特性");
                return false;
            }
            return true;
        }

        public bool Accept(LListAttribute attribute, MemberInfo memberInfo)
        {
            //检查是否是List<>或者数组类型
            if (memberInfo.GetMemberType().IsGenericType && memberInfo.GetMemberType().GetGenericTypeDefinition() == typeof(List<>))
            {
                return true;
            }
            if (memberInfo.GetMemberType().IsArray)
            {
                return true;
            }
            Debug.LogWarning($"构建错误,{memberInfo.Name} 不是一个List<>或者数组类型");
            return false;
        }
    }
}