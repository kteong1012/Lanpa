using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace Lanpa
{
    public class BuildValueVisitor : IBuilderFuncVisitor<object, object>
    {
        public static BuildValueVisitor Instance { get; } = new BuildValueVisitor();
        private GUIStyle _boxStyle;

        private BuildValueVisitor()
        {
            _boxStyle = new GUIStyle(GUI.skin.box);
            _boxStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node1.png") as Texture2D;
            _boxStyle.normal.textColor = Color.white;
            _boxStyle.padding = new RectOffset(10, 10, 10, 10);

        }
        public object Accept(LButtonBuilder builder, object value)
        {
            throw new NotSupportedException();
        }

        public object Accept(LCheckBoxBuilder builder, object value)
        {
            var b = false;
            if (value != null)
            {
                b = (bool)value;
            }
            //绘制toggle
            return EditorGUILayout.Toggle(b);
        }

        public object Accept(LTextBuilder builder, object value)
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

        public object Accept(LDropDownBuilder builder, object value)
        {
            Enum.GetValues(builder.Type);
            var enumValue = value == null ? (Enum)Enum.GetValues(builder.Type).GetValue(0) : (Enum)value;

            enumValue = EditorGUILayout.EnumPopup(enumValue);
            return enumValue;
        }

        public object Accept(LMultiDropDownBuilder builder, object value)
        {
            var enumValue = value == null ? (Enum)Enum.GetValues(builder.Type).GetValue(0) : (Enum)value;
            enumValue = EditorGUILayout.EnumFlagsField(enumValue);
            return enumValue;
        }

        public object Accept(LDictionaryBuilder builder, object value)
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
            //加底框，灰色边框线
            EditorGUILayout.BeginVertical(_boxStyle);
            for (int i = 0; i < builder.Keys.Count; i++)
            {

                EditorGUILayout.BeginHorizontal();
                if (duplicateKeys.Contains(i))
                {
                    GUI.color = Color.red;
                }
                builder.Keys[i] = builder.KeyBuilder.Apply(this, builder.Keys[i]);
                GUI.color = Color.white;
                var clickRemove = GUILayout.Button("-", GUILayout.Width(25), GUILayout.Height(25));
                builder.Values[i] = builder.ValueBuilder.Apply(this, builder.Values[i]);
                if (clickRemove)
                {
                    builder.Keys.RemoveAt(i);
                    builder.Values.RemoveAt(i);
                    i--;
                }
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

        public object Accept(LUnityObjectBuilder builder, object value)
        {
            var obj = value == null ? null : (UnityEngine.Object)value;
            obj = EditorGUILayout.ObjectField(obj, builder.Type, true);
            return obj;
        }

        public object Accept(LSerializedObjectBuilder builder, object value)
        {
            var targets = value as UnityEngine.Object[];
            if (targets != null && targets.Length > 1)
            {
                var firstObj = targets[0];
                EditorGUILayout.BeginVertical(_boxStyle);
                foreach (var (label, memberInfo, fieldBuilder) in builder.Builders)
                {
                    if (!fieldBuilder.MixedValue)
                    {
                        continue;
                    }
                    EditorGUILayout.BeginHorizontal();
                    //label的宽度为100
                    EditorGUILayout.LabelField(label, GUILayout.Width(100));
                    if (targets.All(x => memberInfo.GetValue(x).Equals(memberInfo.GetValue(firstObj))))
                    {
                        EditorGUI.showMixedValue = false;
                    }
                    else
                    {
                        EditorGUI.showMixedValue = true;
                    }

                    EditorGUI.BeginChangeCheck();
                    var val = fieldBuilder.Apply(this, memberInfo.GetValue(firstObj));
                    if (EditorGUI.EndChangeCheck())
                    {
                        foreach (var obj in targets)
                        {
                            memberInfo.SetValue(obj, val);
                        }
                    }
                    EditorGUI.showMixedValue = false;
                    EditorGUILayout.EndHorizontal();
                }
                EditorGUILayout.EndVertical();
                return firstObj;
            }
            else
            {
                object obj = null;
                if (targets != null)
                {
                    obj = targets[0];
                }
                else
                {
                    obj = value ?? Activator.CreateInstance(builder.Type);
                }
                EditorGUILayout.BeginVertical(_boxStyle);
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

        public object Accept(LListBuilder builder, object value)
        {
            if (builder.IsArray)
            {
                var array = (Array)value;
                if (value == null)
                {
                    array = Array.CreateInstance(builder.Type.GetElementType(), 0);
                }
                if (builder.Elements == null)
                {
                    builder.Elements = new List<object>();
                    if (value != null)
                    {
                        for (int i = 0; i < array.Length; i++)
                        {
                            builder.Elements.Add(array.GetValue(i));
                        }
                    }
                }
                EditorGUILayout.BeginVertical(_boxStyle);
                for (int i = 0; i < builder.Elements.Count; i++)
                {
                    EditorGUILayout.BeginHorizontal();
                    //显示索引[i]
                    EditorGUILayout.LabelField($"[{i}]", GUILayout.Width(25));
                    builder.Elements[i] = builder.ElementBuilder.Apply(this, builder.Elements[i]);
                    if (GUILayout.Button("-", GUILayout.Width(25), GUILayout.Height(25)))
                    {
                        builder.Elements.RemoveAt(i);
                        i--;
                    }
                    EditorGUILayout.EndHorizontal();
                }
                if (GUILayout.Button("+", GUILayout.Width(25), GUILayout.Height(25)))
                {
                    builder.Elements.Add(null);
                }
                EditorGUILayout.EndVertical();
                if (builder.Elements.Count != array.Length)
                {
                    array = Array.CreateInstance(builder.Type.GetElementType(), builder.Elements.Count);
                }
                for (int i = 0; i < builder.Elements.Count; i++)
                {
                    array.SetValue(builder.Elements[i], i);
                }
                return array;
            }
            else
            {
                var list = (IList)value;
                if (value == null)
                {
                    list = (IList)Activator.CreateInstance(builder.Type);
                }
                if (builder.Elements == null)
                {
                    builder.Elements = new List<object>();
                    if (value != null)
                    {
                        for (int i = 0; i < list.Count; i++)
                        {
                            builder.Elements.Add(list[i]);
                        }
                    }
                }
                EditorGUILayout.BeginVertical(_boxStyle);
                for (int i = 0; i < builder.Elements.Count; i++)
                {
                    EditorGUILayout.BeginHorizontal();
                    //显示索引[i]
                    EditorGUILayout.LabelField($"[{i}]", GUILayout.Width(25));
                    builder.Elements[i] = builder.ElementBuilder.Apply(this, builder.Elements[i]);
                    if (GUILayout.Button("-", GUILayout.Width(25), GUILayout.Height(25)))
                    {
                        builder.Elements.RemoveAt(i);
                        i--;
                    }
                    EditorGUILayout.EndHorizontal();
                }
                if (GUILayout.Button("+", GUILayout.Width(25), GUILayout.Height(25)))
                {
                    builder.Elements.Add(null);
                }
                EditorGUILayout.EndVertical();
                if (builder.Elements.Count != list.Count)
                {
                    list.Clear();
                    for (int i = 0; i < builder.Elements.Count; i++)
                    {
                        list.Add(builder.Elements[i]);
                    }
                }
                else
                {
                    for (int i = 0; i < builder.Elements.Count; i++)
                    {
                        list[i] = builder.Elements[i];
                    }
                }
                return list;
            }
        }
    }
}