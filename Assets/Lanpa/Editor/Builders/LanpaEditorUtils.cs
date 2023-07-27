using System;
using System.Collections.Generic;
using System.Reflection;

namespace Lanpa
{
    internal static class LanpaEditorUtils
    {
        public static object GetValue(this MemberInfo memberInfo, object target)
        {
            if (memberInfo is FieldInfo fieldInfo)
            {
                return fieldInfo.GetValue(target);
            }
            if (memberInfo is PropertyInfo propertyInfo)
            {
                return propertyInfo.GetValue(target);
            }
            return null;
        }

        public static Type GetMemberType(this MemberInfo memberInfo)
        {
            if (memberInfo is FieldInfo fieldInfo)
            {
                return fieldInfo.FieldType;
            }
            if (memberInfo is PropertyInfo propertyInfo)
            {
                return propertyInfo.PropertyType;
            }
            return null;
        }

        public static void SetValue(this MemberInfo memberInfo, object target, object value)
        {
            if (memberInfo is FieldInfo fieldInfo)
            {
                fieldInfo.SetValue(target, value);
            }
            if (memberInfo is PropertyInfo propertyInfo)
            {
                if (propertyInfo.CanWrite)
                {
                    propertyInfo.SetValue(target, value);
                }
            }
        }

        public static LanpaBuilderBase CreateElementBuilder(Type type)
        {
            if (type == null)
            {
                return null;
            }
            if (LanpaUtils.IsBaseType(type))
            {
                return new LTextBuilder(type);
            }
            if (type.IsEnum)
            {
                if (type.GetCustomAttribute<FlagsAttribute>() != null)
                {
                    return new LMultiDropDownBuilder(type);
                }
                else
                {
                    return new LDropDownBuilder(type);
                }
            }
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Dictionary<,>))
            {
                return new LDictionaryBuilder(type);
            }
            //if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>))
            //{
            //    return new LListBuilder(type);
            //}
            if (type == typeof(bool))
            {
                return new LCheckBoxBuilder(type);
            }
            //UnityObject
            if (typeof(UnityEngine.Object).IsAssignableFrom(type))
            {
                return new LUnityObjectBuilder(type, 0, false);
            }
            //SerializedAttribute
            if (type.GetCustomAttribute<SerializableAttribute>() != null)
            {
                return new LSerializedObjectBuilder(type);
            }
            return null;
        }
    }
}