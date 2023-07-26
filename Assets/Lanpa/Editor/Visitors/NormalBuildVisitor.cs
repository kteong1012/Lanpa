using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Lanpa
{
    public class NormalBuildVisitor : IBuilderActionVisitor<object>
    {
        public static NormalBuildVisitor Instance { get; } = new NormalBuildVisitor();
        public void Accept(LButtonBuilder builder, object target)
        {
            var methodInfo = builder.MemberInfo as MethodInfo;
            var label = builder.Attribute.label ?? methodInfo.Name;
            if (GUILayout.Button(label))
            {
                methodInfo.Invoke(target, null);
            }
        }

        public void Accept(LCheckBoxBuilder builder, object target)
        {
            var memberInfo = builder.MemberInfo;
            var label = builder.Attribute.label ?? memberInfo.Name;
            //绘制toggle
            var value = EditorGUILayout.Toggle(label, (bool)memberInfo.GetValue(target));
            memberInfo.SetValue(target, value);
        }

        public void Accept(LTextBuilder builder, object target)
        {
            var memberInfo = builder.MemberInfo;
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

        public void Accept(LDropDownBuilder builder, object target)
        {
            var memberInfo = builder.MemberInfo;
            var label = builder.Attribute.label ?? memberInfo.Name;
            var enumValue = (Enum)memberInfo.GetValue(target);
            enumValue = EditorGUILayout.EnumPopup(label, enumValue);
            memberInfo.SetValue(target, enumValue);
        }

        public void Accept(LMultiDropDownBuilder builder, object target)
        {
            var memberInfo = builder.MemberInfo;
            var label = builder.Attribute.label ?? memberInfo.Name;
            var enumValue = (Enum)memberInfo.GetValue(target);
            enumValue = EditorGUILayout.EnumFlagsField(label, enumValue);
            memberInfo.SetValue(target, enumValue);
        }
    }
}