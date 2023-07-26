using System;
using System.Collections.Generic;
using System.Reflection;

namespace Lanpa
{
    public class LDictionaryBuilder : LanpaBuilderBase
    {
        public LDictionaryAttribute Attribute { get; }
        public Type KeyType { get; }
        public Type ValueType { get; }
        public List<object> Keys { get; } = new List<object>();
        public List<object> Values { get; } = new List<object>();

        public LDictionaryBuilder(MemberInfo memberInfo, LDictionaryAttribute attribute) : base(memberInfo)
        {
            Attribute = attribute;
            KeyType = memberInfo.GetMemberType().GetGenericArguments()[0];
            ValueType = memberInfo.GetMemberType().GetGenericArguments()[1];
        }

        public override int Order => Attribute.order;

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
        public override T Apply<T>(IBuilderFuncVisitor<T> visitor)
        {
            return visitor.Accept(this);
        }

        public override T Apply<T, A>(IBuilderFuncVisitor<T, A> visitor, A a)
        {
            return visitor.Accept(this, a);
        }

        public override T Apply<T, A, B>(IBuilderFuncVisitor<T, A, B> visitor, A a, B b)
        {
            return visitor.Accept(this, a, b);
        }

        public override T Apply<T, A, B, C>(IBuilderFuncVisitor<T, A, B, C> visitor, A a, B b, C c)
        {
            return visitor.Accept(this, a, b, c);
        }

        public override T Apply<T, A, B, C, D>(IBuilderFuncVisitor<T, A, B, C, D> visitor, A a, B b, C c, D d)
        {
            return visitor.Accept(this, a, b, c, d);
        }
    }
}