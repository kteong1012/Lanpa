namespace Lanpa
{
    public class LMultiDropDownAttribute : LanpaAttribute
    {
        public override T Apply<T, A>(IAttributeFuncVisitor<T, A> visitor, A a)
        {
            return visitor.Accept(this, a);
        }

        public override T Apply<T, A, B>(IAttributeFuncVisitor<T, A, B> visitor, A a, B b)
        {
            return visitor.Accept(this, a, b);
        }

        public override T Apply<T, A, B, C>(IAttributeFuncVisitor<T, A, B, C> visitor, A a, B b, C c)
        {
            return visitor.Accept(this, a, b, c);
        }

        public override T Apply<T, A, B, C, D>(IAttributeFuncVisitor<T, A, B, C, D> visitor, A a, B b, C c, D d)
        {
            return visitor.Accept(this, a, b, c, d);
        }
    }
}