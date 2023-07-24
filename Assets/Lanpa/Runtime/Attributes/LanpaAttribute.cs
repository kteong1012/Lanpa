using System;

namespace Lanpa
{
    public abstract class LanpaAttribute : Attribute
    {
        public string label;
        public int order;

        public abstract T Apply<T, A>(IAttributeFuncVisitor<T, A> visitor, A a);
        public abstract T Apply<T, A, B>(IAttributeFuncVisitor<T, A, B> visitor, A a, B b);
        public abstract T Apply<T, A, B, C>(IAttributeFuncVisitor<T, A, B, C> visitor, A a, B b, C c);
        public abstract T Apply<T, A, B, C, D>(IAttributeFuncVisitor<T, A, B, C, D> visitor, A a, B b, C c, D d);
    }
}