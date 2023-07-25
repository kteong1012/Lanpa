namespace Lanpa
{
    public interface IAttributeFuncVisitor<T>
    {
        T Accept(LButtonAttribute lButtonAttribute);
        T Accept(LCheckBoxAttribute lCheckBoxAttribute);
        T Accept(LTextAttribute lTextAttribute);
    }
    public interface IAttributeFuncVisitor<T, A>
    {
        T Accept(LButtonAttribute lButtonAttribute, A a);
        T Accept(LCheckBoxAttribute lCheckBoxAttribute, A a);
        T Accept(LTextAttribute lTextAttribute, A a);
        T Accept(LDropDownAttribute lDropDownAttribute, A a);
        T Accept(LMultiDropDownAttribute lMultiDropDownAttribute, A a);
    }
    public interface IAttributeFuncVisitor<T, A, B>
    {
        T Accept(LButtonAttribute lButtonAttribute, A a, B b);
        T Accept(LCheckBoxAttribute lCheckBoxAttribute, A a, B b);
        T Accept(LTextAttribute lTextAttribute, A a, B b);
        T Accept(LDropDownAttribute lDropDownAttribute, A a, B b);
        T Accept(LMultiDropDownAttribute lMultiDropDownAttribute, A a, B b);
    }
    public interface IAttributeFuncVisitor<T, A, B, C>
    {
        T Accept(LButtonAttribute lButtonAttribute, A a, B b, C c);
        T Accept(LCheckBoxAttribute lCheckBoxAttribute, A a, B b, C c);
        T Accept(LTextAttribute lTextAttribute, A a, B b, C c);
        T Accept(LDropDownAttribute lDropDownAttribute, A a, B b, C c);
        T Accept(LMultiDropDownAttribute lMultiDropDownAttribute, A a, B b, C c);
    }
    public interface IAttributeFuncVisitor<T, A, B, C, D>
    {
        T Accept(LButtonAttribute lButtonAttribute, A a, B b, C c, D d);
        T Accept(LCheckBoxAttribute lCheckBoxAttribute, A a, B b, C c, D d);
        T Accept(LTextAttribute lTextAttribute, A a, B b, C c, D d);
        T Accept(LDropDownAttribute lDropDownAttribute, A a, B b, C c, D d);
        T Accept(LMultiDropDownAttribute lMultiDropDownAttribute, A a, B b, C c, D d);
    }
}