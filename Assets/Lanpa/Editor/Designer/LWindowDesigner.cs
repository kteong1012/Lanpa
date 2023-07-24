using System;
using System.Linq;
using System.Reflection;
using UnityEditor;

namespace Lanpa
{
    public class LWindowDesigner<T> : EditorWindow where T : class, new()
    {
        protected Type _type;
        protected T _target;

        private void TryInit()
        {
            if(_target == null)
            {
                _target = new T();
            }
            if(_type == null)
            {
                _type = typeof(T);
            }
        }

        private void OnGUI()
        {
            TryInit();
            var members = _type.GetMembers(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .Where(m => m.GetCustomAttribute<LanpaAttribute>(true) != null)
                .OrderByDescending(m => m.GetCustomAttribute<LanpaAttribute>(true)?.order ?? 0);
            foreach (var member in members)
            {
                var attribute = member.GetCustomAttribute<LanpaAttribute>(true);
                if (attribute != null)
                {
                    var builder = LanpaBuilders.builders[attribute.GetType()];
                    builder.Build(_target, member, attribute);
                }
            }
        }
    }
}