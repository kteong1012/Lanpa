using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
namespace Lanpa
{
    public abstract class LanpaBuilderBase
    {
        public MemberInfo MemberInfo { get; }

        public LanpaBuilderBase(MemberInfo memberInfo)
        {
            MemberInfo = memberInfo;
        }

        public abstract int Order { get; }

        public abstract void Apply<A>(IBuilderActionVisitor<A> visitor, A a);

        public abstract void Apply<A, B>(IBuilderActionVisitor<A, B> visitor, A a, B b);

        public abstract void Apply<A, B, C>(IBuilderActionVisitor<A, B, C> visitor, A a, B b, C c);

        public abstract void Apply<A, B, C, D>(IBuilderActionVisitor<A, B, C, D> visitor, A a, B b, C c, D d);
    }
}