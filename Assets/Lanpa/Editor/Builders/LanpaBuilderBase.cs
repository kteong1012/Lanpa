using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
namespace Lanpa
{
    public abstract class LanpaBuilderBase
    {
        public Type Type { get; }
        public int Order { get; }

        public LanpaBuilderBase(Type type,int order)
        {
            Type = type;
            Order = order;
        }


        public abstract void Apply<A>(IBuilderActionVisitor<A> visitor, A a);

        public abstract void Apply<A, B>(IBuilderActionVisitor<A, B> visitor, A a, B b);

        public abstract void Apply<A, B, C>(IBuilderActionVisitor<A, B, C> visitor, A a, B b, C c);

        public abstract void Apply<A, B, C, D>(IBuilderActionVisitor<A, B, C, D> visitor, A a, B b, C c, D d);

        public abstract T Apply<T>(IBuilderFuncVisitor<T> visitor);

        public abstract T Apply<T, A>(IBuilderFuncVisitor<T, A> visitor, A a);

        public abstract T Apply<T, A, B>(IBuilderFuncVisitor<T, A, B> visitor, A a, B b);

        public abstract T Apply<T, A, B, C>(IBuilderFuncVisitor<T, A, B, C> visitor, A a, B b, C c);

        public abstract T Apply<T, A, B, C, D>(IBuilderFuncVisitor<T, A, B, C, D> visitor, A a, B b, C c, D d);
    }
}