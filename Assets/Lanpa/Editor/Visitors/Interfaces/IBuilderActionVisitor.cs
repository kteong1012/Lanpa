namespace Lanpa
{
    public interface IBuilderActionVisitor<A>
    {
        void Accept(LButtonBuilder lButtonBuilder, A a);
        void Accept(LCheckBoxBuilder lCheckBoxBuilder, A a);
        void Accept(LTextBuilder lTextBuilder, A a);
    }

    public interface IBuilderActionVisitor<A, B>
    {
        void Accept(LButtonBuilder lButtonBuilder, A a, B b);
        void Accept(LCheckBoxBuilder lCheckBoxBuilder, A a, B b);
        void Accept(LTextBuilder lTextBuilder, A a, B b);
    }

    public interface IBuilderActionVisitor<A, B, C>
    {
        void Accept(LButtonBuilder lButtonBuilder, A a, B b, C c);
        void Accept(LCheckBoxBuilder lCheckBoxBuilder, A a, B b, C c);
        void Accept(LTextBuilder lTextBuilder, A a, B b, C c);
    }

    public interface IBuilderActionVisitor<A, B, C, D>
    {
        void Accept(LButtonBuilder lButtonBuilder, A a, B b, C c, D d);
        void Accept(LCheckBoxBuilder lCheckBoxBuilder, A a, B b, C c, D d);
        void Accept(LTextBuilder lTextBuilder, A a, B b, C c, D d);
    }
}