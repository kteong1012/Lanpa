using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Lanpa
{
    public class LWindowCreator<T> : EditorWindow where T : class, new()
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
        private Vector2 _scrollPos;

        private void TryInit()
        {
            if (_target == null)
            {
                _target = new T();
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

        private void OnGUI()
        {
            DrawWindow();
        }

        protected virtual void DrawWindow()
        {
            TryInit();
            //创建一个和窗口一样高的滚动区域
            _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos, GUILayout.Width(position.width),
                               GUILayout.Height(position.height));
            foreach (var info in _builderInfos)
            {
                info.builder.Apply(BuildMemberVisitor.Instance, _target, info.label, info.memberInfo);
            }
            EditorGUILayout.EndScrollView();
        }
    }
}