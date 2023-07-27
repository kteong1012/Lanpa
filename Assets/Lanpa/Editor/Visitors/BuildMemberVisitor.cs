using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

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
            NormalProcess(builder, target, label, memberInfo);
        }

        public void Accept(LTextBuilder builder, object target, string label, MemberInfo memberInfo)
        {
            NormalProcess(builder, target, label, memberInfo);
        }

        public void Accept(LDropDownBuilder builder, object target, string label, MemberInfo memberInfo)
        {
            NormalProcess(builder, target, label, memberInfo);
        }

        public void Accept(LMultiDropDownBuilder builder, object target, string label, MemberInfo memberInfo)
        {
            NormalProcess(builder, target, label, memberInfo);
        }

        public void Accept(LDictionaryBuilder builder, object target, string label, MemberInfo memberInfo)
        {
            NormalProcess(builder, target, label, memberInfo);
        }

        public void Accept(LUnityObjectBuilder builder, object target, string label, MemberInfo memberInfo)
        {
            NormalProcess(builder, target, label, memberInfo);
        }

        public void Accept(LSerializedObjectBuilder builder, object target, string label, MemberInfo memberInfo)
        {
            NormalProcess(builder, target, label, memberInfo);
        }

        public void Accept(LListBuilder builder, object target, string label, MemberInfo memberInfo)
        {
            NormalProcess(builder, target, label, memberInfo);
        }

        private void NormalProcess(LanpaBuilderBase builder,object target, string label, MemberInfo memberInfo)
        {
            EditorGUILayout.BeginHorizontal();
            //label的宽度为100
            EditorGUILayout.LabelField(label, GUILayout.Width(100));
            var obj = builder.Apply(BuildValueVisitor.Instance, memberInfo.GetValue(target), 0);
            EditorGUILayout.EndHorizontal();
            memberInfo.SetValue(target, obj);
        }
    }
}