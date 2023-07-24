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
            foreach (var member in members)
            {
                var attribute = member.GetCustomAttribute<LanpaAttribute>(true);
                if (attribute != null)
                {
                    yield return (member, attribute);
                }
            }
        }
    }
}