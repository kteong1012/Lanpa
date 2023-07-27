using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Lanpa
{
    public class LInspectorCreator<T> : Editor where T : class, new()
    {
        private class LanpaMemberInfo
        {
            public string label;
            public MemberInfo memberInfo;
            public LanpaBuilderBase builder;
        }
        protected Type _type;
        protected T _target;
        private List<LanpaMemberInfo> _builderInfos;

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
            if (_builderInfos == null)
            {
                _builderInfos = _type.GetLanpaMembers()
                    .OrderByDescending(pair => pair.attribute.order)
                    .Select(pair =>
                    {
                        return new LanpaMemberInfo()
                        {
                            label = pair.attribute.label ?? pair.memberInfo.Name,
                            memberInfo = pair.memberInfo,
                            builder = pair.attribute.Apply(MemberBuilderFactoryVisitor.Instance, pair.memberInfo)
                        };
                    }).ToList();
            }
        }
        public override void OnInspectorGUI()
        {
            //记录当前的target状态，如果有改变就SetDirty
            EditorGUI.BeginChangeCheck();
            DrawWindow();
            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(target);
            }
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
            foreach (var info in _builderInfos)
            {
                info.builder.Apply(BuildMemberVisitor.Instance, _target, info.label, info.memberInfo);
            }
        }
    }
}