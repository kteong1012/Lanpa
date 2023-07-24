using System;
using System.Linq;
using System.Reflection;
using UnityEditor;

namespace Lanpa
{
    public class LWindowCreator<T> : EditorWindow where T : class, new()
    {
        protected Type _type;
        protected T _target;

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
        }

        private void OnGUI()
        {
            TryInit();
            var members = _type.GetLanpaMembers()
                .OrderByDescending(pair => pair.attribute.order);

            foreach (var (member, attribute) in members)
            {
                var builder = attribute.Apply(BuilderFactoryVisitor.Instance, member);
                builder.Apply(NormalBuilderVisitor.Instance, _target, member);
            }
        }
    }
}