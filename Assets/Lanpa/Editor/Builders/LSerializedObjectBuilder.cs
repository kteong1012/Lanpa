using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Lanpa
{
    public class LSerializedObjectBuilder : LanpaBuilderBase
    {
        public override bool MixedValue => false;
        public List<(string label, MemberInfo memberInfo, LanpaBuilderBase builder)> Builders { get; }
        public LSerializedObjectBuilder(Type type, int order = 0) : base(type, order)
        {
            Builders = new List<(string label, MemberInfo memberInfo, LanpaBuilderBase builder)>();
            var members = type.GetMembers(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            //反射获取所有字段，并创建对应的LanpaBuilderBase
            foreach (var memberInfo in members)
            {
                //LIgnoreSerializedAttribute的字段不创建LFieldBuilder
                if (memberInfo.IsDefined(typeof(LIgnoreSerializedAttribute), true))
                {
                    continue;
                }
                //如果带有继承自LanpaAttribute特性的字段，创建对应的LFieldBuilder
                if (memberInfo.IsDefined(typeof(LanpaAttribute), true))
                {
                    var attribute = memberInfo.GetCustomAttribute<LanpaAttribute>(true);
                    var builder = attribute.Apply(MemberBuilderFactoryVisitor.Instance, memberInfo);
                    var label = attribute.label ?? memberInfo.Name;
                    Builders.Add((label, memberInfo, builder));
                }
                else
                {
                    if (memberInfo is FieldInfo fieldInfo && !fieldInfo.IsPublic)
                    {
                        continue;
                    }
                    if (memberInfo is PropertyInfo)
                    {
                        continue;
                    }

                    //如果没有带有继承自LanpaAttribute特性的字段，创建对应的LFieldBuilder
                    var builder = LanpaEditorUtils.CreateElementBuilder(memberInfo.GetMemberType());
                    if (builder != null)
                    {
                        var label = memberInfo.Name;
                        Builders.Add((label, memberInfo, builder));
                    }
                }
            }
            Builders = Builders
                .OrderByDescending(pair => pair.builder.Order)
                .ToList();
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