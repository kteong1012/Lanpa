using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Lanpa
{
    public abstract class LWindowCreator<T> : EditorWindow
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
        private Vector2 _scrollPos;

        private void TryInit()
        {
            if (_target == null)
            {
                _target = LoadTarget();
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

        protected abstract T LoadTarget();

        private void OnGUI()
        {
            DrawWindow();
        }

        protected virtual void DrawWindow()
        {
            TryInit();
            if (Event.current.type == EventType.MouseDown && Event.current.button == 0)
            {
                GUI.FocusControl(null);
                EditorGUI.FocusTextInControl(null);
                GUI.FocusWindow(GetHashCode());
                Repaint();
            }
            //创建一个和窗口一样高的滚动区域
            _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos, GUILayout.Width(position.width),
                               GUILayout.Height(position.height));
            _builder.Apply(BuildValueVisitor.Instance, _target);
            EditorGUILayout.EndScrollView();
        }
    }
}