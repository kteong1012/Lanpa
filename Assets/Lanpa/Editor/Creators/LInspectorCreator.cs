﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Lanpa
{
    public class LInspectorCreator<T> : Editor where T : Component
    {
        private class LanpaMemberInfo
        {
            public string label;
            public MemberInfo memberInfo;
            public LanpaBuilderBase builder;
        }
        protected Type _type;
        protected T _target;
        private LSerializedObjectBuilder _builder;

        private void TryInit()
        {
            if (_target == null)
            {
                _target = target as T;
            }
            if (_type == null)
            {
                _type = typeof(T);
            }
            if (_builder == null)
            {
                _builder = new LSerializedObjectBuilder(_type);
            }
        }
        public override void OnInspectorGUI()
        {
            //记录撤回状态
            Undo.RecordObject(target, "Inspector");

            serializedObject.Update();
            DrawInspector();
            serializedObject.ApplyModifiedProperties();

            if (GUI.changed)
            {
                EditorUtility.SetDirty(target);
            }
        }


        protected virtual void DrawInspector()
        {
            TryInit();
            if (Event.current.type == EventType.MouseDown && Event.current.button == 0)
            {
                GUI.FocusControl(null);
                EditorGUI.FocusTextInControl(null);
                GUI.FocusWindow(GetHashCode());
                Repaint();
            }
            //创建一个控件，不可编辑的脚本，指向T类型，双击可以打开脚本
            //字体颜色为蓝色
            GUI.enabled = false;
            EditorGUILayout.ObjectField("Script", MonoScript.FromMonoBehaviour(target as MonoBehaviour), typeof(MonoScript), false);
            GUI.enabled = true;
            BuildValueVisitor.Instance.Accept(_builder, targets);
        }
    }
}