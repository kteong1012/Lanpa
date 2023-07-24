using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Lanpa
{
    public class LTextBuilder : LanpaBuilderBase
    {
        public override void Build(object target, MemberInfo memberInfo, LanpaAttribute attribute)
        {
            //检查是否是字段或者属性
            if (memberInfo is not FieldInfo && memberInfo is not PropertyInfo)
            {
                Debug.LogWarning($"类型：{target.GetType().FullName}构建错误,{memberInfo.Name} 不是一个字段或者属性");
                return;
            }
            if (attribute is not LTextAttribute textAttribute)
            {
                Debug.LogWarning($"类型：{target.GetType().FullName}构建错误,{memberInfo.Name} 不是一个LTextAttribute");
                return;
            }
            //检查是否是string,int, float, double, decimal, char, byte, sbyte, short, ushort, uint, long, ulong
            if (memberInfo != null && !LanpaUtils.IsBaseType(memberInfo.GetMemberType()))
            {
                Debug.LogWarning($"类型：{target.GetType().FullName}构建错误,{memberInfo.Name} 不是一个基础数据类型");
                return;
            }
            var label = textAttribute.label ?? memberInfo.Name;

            if (textAttribute.inputText)
            {
                var text = EditorGUILayout.TextField(label, memberInfo.GetValue(target).ToString());
                memberInfo.SetValue(target, LanpaUtils.Convert(memberInfo.GetMemberType(), text));
            }
            else
            {
                EditorGUILayout.BeginHorizontal();

                EditorGUILayout.LabelField(label);
                EditorGUILayout.LabelField(memberInfo.GetValue(target).ToString());

                EditorGUILayout.EndHorizontal();
            }
        }
    }
}