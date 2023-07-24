using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Lanpa
{
    public class LCheckBoxBuilder : LanpaBuilderBase
    {
        public override void Build(object target, MemberInfo memberInfo, LanpaAttribute attribute)
        {
            //检查是否是字段或者属性
            if (memberInfo is not FieldInfo && memberInfo is not PropertyInfo)
            {
                Debug.LogWarning($"类型：{target.GetType().FullName}构建错误,{memberInfo.Name} 不是一个字段或者属性");
                return;
            }
            if (attribute is not LCheckBoxAttribute checkBoxAttribute)
            {
                Debug.LogWarning($"类型：{target.GetType().FullName}构建错误,{memberInfo.Name} 不是一个LCheckBoxAttribute");
                return;
            }
            //检查是否是bool类型
            if (memberInfo.GetMemberType() != typeof(bool))
            {
                Debug.LogWarning($"类型：{target.GetType().FullName}构建错误,{memberInfo.Name} 不是一个bool类型");
                return;
            }
            var label = checkBoxAttribute.label ?? memberInfo.Name;
            //绘制toggle
            var value = EditorGUILayout.Toggle(label, (bool)memberInfo.GetValue(target));
            memberInfo.SetValue(target, value);
        }
    }
}