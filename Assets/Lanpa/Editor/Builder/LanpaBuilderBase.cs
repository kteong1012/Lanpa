using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
namespace Lanpa
{
    public abstract class LanpaBuilderBase
    {
        public abstract void Build(object target, MemberInfo memberInfo, LanpaAttribute attribute);
    }
}