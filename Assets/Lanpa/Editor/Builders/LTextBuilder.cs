using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Lanpa
{
    public class LTextBuilder : LanpaBuilderBase
    {
        public override int Order => Attribute.order;

        public LTextAttribute Attribute { get; }

        public LTextBuilder(MemberInfo memberInfo, LTextAttribute lTextAttribute) : base(memberInfo)
        {
            Attribute = lTextAttribute;
        }

        public override void Apply<A>(IBuilderActionVisitor<A> visitor, A a)
        {
            visitor.Accept(this, a);
        }

        public override void Apply<A, B>(IBuilderActionVisitor<A, B> visitor, A a, B b)
        {
            visitor.Accept(this, a, b);
        }

        public override void Apply<A, B, C>(IBuilderActionVisitor<A, B, C> visitor, A a, B b, C c)
        {
            visitor.Accept(this, a, b, c);
        }

        public override void Apply<A, B, C, D>(IBuilderActionVisitor<A, B, C, D> visitor, A a, B b, C c, D d)
        {
            visitor.Accept(this, a, b, c, d);
        }
    }
}