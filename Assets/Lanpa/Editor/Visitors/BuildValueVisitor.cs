using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Lanpa
{
    public class BuildValueVisitor : IBuilderFuncVisitor<object, object, int>
    {
        public static BuildValueVisitor Instance { get; } = new BuildValueVisitor();
        public object Accept(LButtonBuilder builder, object value, int depth)
        {
            throw new NotSupportedException();
        }

        public object Accept(LCheckBoxBuilder builder, object value, int depth)
        {
            var b = false;
            if (value != null)
            {
                b = (bool)value;
            }
            //绘制toggle
            return EditorGUILayout.Toggle(b);
        }

        public object Accept(LTextBuilder builder, object value, int depth)
        {
            if (builder.InputText)
            {
                //如果输入框还在焦点就返回原值
                GUI.SetNextControlName(builder.GetHashCode().ToString());
                var text = EditorGUILayout.TextField(value?.ToString());
                return LanpaUtils.Convert(builder.Type, text);
            }
            else
            {
                EditorGUILayout.LabelField(value?.ToString());
                return value;
            }
        }

        public object Accept(LDropDownBuilder builder, object value, int depth)
        {
            Enum.GetValues(builder.Type);
            var enumValue = value == null ? (Enum)Enum.GetValues(builder.Type).GetValue(0) : (Enum)value;

            enumValue = EditorGUILayout.EnumPopup(enumValue);
            return enumValue;
        }

        public object Accept(LMultiDropDownBuilder builder, object value, int depth)
        {
            var enumValue = value == null ? (Enum)Enum.GetValues(builder.Type).GetValue(0) : (Enum)value;
            enumValue = EditorGUILayout.EnumFlagsField(enumValue);
            return enumValue;
        }

        public object Accept(LDictionaryBuilder builder, object value, int depth)
        {
            var dict = value == null ? (IDictionary)Activator.CreateInstance(builder.Type) : (IDictionary)value;
            if (builder.Keys == null || builder.Values == null)
            {
                builder.Keys = new List<object>();
                builder.Values = new List<object>();

                var iter = dict.GetEnumerator();
                while (iter.MoveNext())
                {
                    builder.Keys.Add(iter.Key);
                    builder.Values.Add(iter.Value);
                }
            }
            //获取Keys的所有重复项
            var duplicateKeys = builder.Keys.Select((element, index) => new { Element = element, Index = index })
                            .GroupBy(item => item.Element)
                            .Where(group => group.Count() > 1)
                            .SelectMany(group => group.Skip(1).Select(item => item.Index))
                            .ToList();
            EditorGUILayout.BeginVertical();
            for (int i = 0; i < builder.Keys.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                if (duplicateKeys.Contains(i))
                {
                    GUI.color = Color.red;
                }
                builder.Keys[i] = builder.KeyBuilder.Apply(this, builder.Keys[i], depth + 1);
                GUI.color = Color.white;
                if (GUILayout.Button("-", GUILayout.Width(25), GUILayout.Height(25)))
                {
                    builder.Keys.RemoveAt(i);
                    builder.Values.RemoveAt(i);
                    i--;
                }
                builder.Values[i] = builder.ValueBuilder.Apply(this, builder.Values[i], depth + 1);
                EditorGUILayout.EndHorizontal();
            }
            if (GUILayout.Button("+", GUILayout.Width(25), GUILayout.Height(25)))
            {
                builder.Keys.Add(null);
                builder.Values.Add(null);
            }
            EditorGUILayout.EndVertical();
            //用linq判断keys有没有重复项
            if (builder.Keys.Count != builder.Keys.Distinct().Count())
            {
                return dict;
            }
            dict.Clear();
            for (int i = 0; i < builder.Keys.Count; i++)
            {
                if (builder.Keys[i] == null)
                {
                    continue;
                }
                dict.Add(builder.Keys[i], builder.Values[i]);
            }
            return dict;
        }

        public object Accept(LUnityObjectBuilder builder, object value, int depth)
        {
            var obj = value == null ? null : (UnityEngine.Object)value;
            obj = EditorGUILayout.ObjectField(obj, builder.Type, true);
            return obj;
        }

        public object Accept(LSerializedObjectBuilder builder, object value, int depth)
        {
            var obj = value ?? Activator.CreateInstance(builder.Type);
            EditorGUILayout.BeginVertical();
            foreach (var (label, memberInfo, fieldBuilder) in builder.Builders)
            {
                EditorGUILayout.BeginHorizontal();
                fieldBuilder.Apply(BuildMemberVisitor.Instance, obj, label, memberInfo);
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndVertical();
            return obj;
        }
    }
}