using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Lanpa
{
    public class LButtonBuilder : LanpaBuilderBase
    {
        public override void Build(object target, MemberInfo memberInfo, LanpaAttribute attribute)
        {
            if (memberInfo is not MethodInfo methodInfo)
            {
                Debug.LogWarning($"类型：{target.GetType().FullName}构建错误,{memberInfo.Name} 不是一个方法");
                return;
            }
            if (attribute is not LButtonAttribute buttonAttribute)
            {
                Debug.LogWarning($"类型：{target.GetType().FullName}构建错误,{memberInfo.Name} LButtonAttribute 缺失");
                return;
            }
            var label = attribute.label ?? methodInfo.Name;
            if (GUILayout.Button(label))
            {
                methodInfo.Invoke(target, null);
            }
        }
    }
}