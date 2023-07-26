namespace Lanpa
{
    public interface IAttributeFuncVisitor<T>
    {
        T Accept(LButtonAttribute attribute);
        T Accept(LCheckBoxAttribute attribute);
        T Accept(LTextAttribute attribute);
    }
    public interface IAttributeFuncVisitor<T, A>
    {
        T Accept(LButtonAttribute attribute, A a);
        T Accept(LCheckBoxAttribute attribute, A a);
        T Accept(LTextAttribute attribute, A a);
        T Accept(LDropDownAttribute attribute, A a);
        T Accept(LMultiDropDownAttribute attribute, A a);
        T Accept(LDictionaryAttribute attribute, A a);
        T Accept(LUnityObjectAttribute attribute, A a);
    }
    public interface IAttributeFuncVisitor<T, A, B>
    {
        T Accept(LButtonAttribute attribute, A a, B b);
        T Accept(LCheckBoxAttribute attribute, A a, B b);
        T Accept(LTextAttribute attribute, A a, B b);
        T Accept(LDropDownAttribute attribute, A a, B b);
        T Accept(LMultiDropDownAttribute attribute, A a, B b);
        T Accept(LDictionaryAttribute attribute, A a, B b);
        T Accept(LUnityObjectAttribute attribute, A a, B b);
    }
    public interface IAttributeFuncVisitor<T, A, B, C>
    {
        T Accept(LButtonAttribute attribute, A a, B b, C c);
        T Accept(LCheckBoxAttribute attribute, A a, B b, C c);
        T Accept(LTextAttribute attribute, A a, B b, C c);
        T Accept(LDropDownAttribute attribute, A a, B b, C c);
        T Accept(LMultiDropDownAttribute attribute, A a, B b, C c);
        T Accept(LDictionaryAttribute attribute, A a, B b, C c);
        T Accept(LUnityObjectAttribute attribute, A a, B b, C c);
    }
    public interface IAttributeFuncVisitor<T, A, B, C, D>
    {
        T Accept(LButtonAttribute attribute, A a, B b, C c, D d);
        T Accept(LCheckBoxAttribute attribute, A a, B b, C c, D d);
        T Accept(LTextAttribute attribute, A a, B b, C c, D d);
        T Accept(LDropDownAttribute attribute, A a, B b, C c, D d);
        T Accept(LMultiDropDownAttribute attribute, A a, B b, C c, D d);
        T Accept(LDictionaryAttribute attribute, A a, B b, C c, D d);
        T Accept(LUnityObjectAttribute attribute, A a, B b, C c, D d);
    }
}