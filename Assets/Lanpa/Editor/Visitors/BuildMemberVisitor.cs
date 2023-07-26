using System;
using System.Collections;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Lanpa
{
    public class BuildMemberVisitor : IBuilderActionVisitor<object, string, MemberInfo>
    {
        public static BuildMemberVisitor Instance { get; } = new BuildMemberVisitor();

        public void Accept(LButtonBuilder builder, object target, string label, MemberInfo memberInfo)
        {
            var methodInfo = builder.MethodInfo;
            if (GUILayout.Button(label))
            {
                methodInfo.Invoke(target, null);
            }
        }

        public void Accept(LCheckBoxBuilder builder, object target, string label, MemberInfo memberInfo)
        {
            //绘制toggle
            var value = EditorGUILayout.Toggle(label, (bool)memberInfo.GetValue(target));
            memberInfo.SetValue(target, value);
        }

        public void Accept(LTextBuilder builder, object target, string label, MemberInfo memberInfo)
        {
            if (builder.InputText)
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

        public void Accept(LDropDownBuilder builder, object target, string label, MemberInfo memberInfo)
        {
            var enumValue = (Enum)memberInfo.GetValue(target);
            enumValue = EditorGUILayout.EnumPopup(label, enumValue);
            memberInfo.SetValue(target, enumValue);
        }

        public void Accept(LMultiDropDownBuilder builder, object target, string label, MemberInfo memberInfo)
        {
            var enumValue = (Enum)memberInfo.GetValue(target);
            enumValue = EditorGUILayout.EnumFlagsField(label, enumValue);
            memberInfo.SetValue(target, enumValue);
        }

        public void Accept(LDictionaryBuilder builder, object target, string label, MemberInfo memberInfo)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(label);
            //反射获取字典的Keys和Values
            var dict = builder.Apply(BuildValueVisitor.Instance, memberInfo.GetValue(target), 0);
            EditorGUILayout.EndHorizontal();
            memberInfo.SetValue(target, dict);
        }
    }
}