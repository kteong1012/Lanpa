using System;
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
                propertyInfo.SetValue(target, value);
            }
        }
    }
}