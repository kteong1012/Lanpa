using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Lanpa
{
    public static class CreatorUtils
    {
        public static IEnumerable<(MemberInfo memberInfo, LanpaAttribute attribute)> GetLanpaMembers(this Type type)
        {
            var members = type.GetMembers(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (var memberInfo in members)
            {
                var attribute = memberInfo.GetCustomAttribute<LanpaAttribute>(true);
                if (attribute != null && attribute.Apply(AttributeCheckVisitor.Instance, memberInfo))
                {
                    yield return (memberInfo, attribute);
                }
            }
        }
    }
}