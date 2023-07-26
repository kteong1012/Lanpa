using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Lanpa
{
    public class NormalValueBuildVisitor : IBuilderActionVisitor<object>
    {
        public static NormalValueBuildVisitor Instance { get; } = new NormalValueBuildVisitor();
        public void Accept(LButtonBuilder builder, object target)
        {
            throw new NotSupportedException();
        }

        public void Accept(LCheckBoxBuilder builder, object target)
        {
            var memberInfo = builder.MemberInfo;
            //绘制toggle
            var value = EditorGUILayout.Toggle((bool)memberInfo.GetValue(target));
            memberInfo.SetValue(target, value);
        }

        public void Accept(LTextBuilder builder, object target)
        {
            var memberInfo = builder.MemberInfo;
            if (builder.Attribute.inputText)
            {
                var text = EditorGUILayout.TextField(memberInfo.GetValue(target).ToString());
                memberInfo.SetValue(target, LanpaUtils.Convert(memberInfo.GetMemberType(), text));
            }
            else
            {
                EditorGUILayout.LabelField(memberInfo.GetValue(target).ToString());
            }
        }

        public void Accept(LDropDownBuilder builder, object target)
        {
            var memberInfo = builder.MemberInfo;
            var enumValue = (Enum)memberInfo.GetValue(target);
            enumValue = EditorGUILayout.EnumPopup(enumValue);
            memberInfo.SetValue(target, enumValue);
        }

        public void Accept(LMultiDropDownBuilder builder, object target)
        {
            var memberInfo = builder.MemberInfo;
            var enumValue = (Enum)memberInfo.GetValue(target);
            enumValue = EditorGUILayout.EnumFlagsField(enumValue);
            memberInfo.SetValue(target, enumValue);
        }
    }
}