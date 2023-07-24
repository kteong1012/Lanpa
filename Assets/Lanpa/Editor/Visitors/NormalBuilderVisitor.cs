using System.Reflection;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace Lanpa
{
    public class NormalBuilderVisitor : IBuilderActionVisitor<object, MemberInfo>
    {
        public static NormalBuilderVisitor Instance = new NormalBuilderVisitor();
        public void Accept(LButtonBuilder builder, object target, MemberInfo memberInfo)
        {
            if (memberInfo is not MethodInfo methodInfo)
            {
                Debug.LogWarning($"类型：{target.GetType().FullName}构建错误,{memberInfo.Name} 不是一个方法");
                return;
            }
            var label = builder.Attribute.label ?? methodInfo.Name;
            if (GUILayout.Button(label))
            {
                methodInfo.Invoke(target, null);
            }
        }

        public void Accept(LCheckBoxBuilder builder, object target, MemberInfo memberInfo)
        {
            //检查是否是字段或者属性
            if (memberInfo is not FieldInfo && memberInfo is not PropertyInfo)
            {
                Debug.LogWarning($"类型：{target.GetType().FullName}构建错误,{memberInfo.Name} 不是一个字段或者属性");
                return;
            }
            //检查是否是bool类型
            if (memberInfo.GetMemberType() != typeof(bool))
            {
                Debug.LogWarning($"类型：{target.GetType().FullName}构建错误,{memberInfo.Name} 不是一个bool类型");
                return;
            }
            var label = builder.Attribute.label ?? memberInfo.Name;
            //绘制toggle
            var value = EditorGUILayout.Toggle(label, (bool)memberInfo.GetValue(target));
            memberInfo.SetValue(target, value);
        }

        public void Accept(LTextBuilder builder, object target, MemberInfo memberInfo)
        {
            //检查是否是字段或者属性
            if (memberInfo is not FieldInfo && memberInfo is not PropertyInfo)
            {
                Debug.LogWarning($"类型：{target.GetType().FullName}构建错误,{memberInfo.Name} 不是一个字段或者属性");
                return;
            }
            //检查是否是string,int, float, double, decimal, char, byte, sbyte, short, ushort, uint, long, ulong
            if (memberInfo != null && !LanpaUtils.IsBaseType(memberInfo.GetMemberType()))
            {
                Debug.LogWarning($"类型：{target.GetType().FullName}构建错误,{memberInfo.Name} 不是一个基础数据类型");
                return;
            }
            var label = builder.Attribute.label ?? memberInfo.Name;

            if (builder.Attribute.inputText)
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