using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;

namespace Lanpa
{
    public class LWindowCreator<T> : EditorWindow where T : class, new()
    {
        protected Type _type;
        protected T _target;
        protected IEnumerable<LanpaBuilderBase> _builders;

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
            if (_builders == null)
            {
                _builders = _type.GetLanpaMembers()
                    .OrderByDescending(pair => pair.attribute.order)
                    .Select(pair => pair.attribute.Apply(BuilderFactoryVisitor.Instance, pair.memberInfo));
            }
        }

        private void OnGUI()
        {
            DrawWindow();
        }

        protected virtual void DrawWindow()
        {
            TryInit();
            foreach (var builder in _builders)
            {
                builder.Apply(NormalBuildVisitor.Instance, _target);
            }
        }
    }
}