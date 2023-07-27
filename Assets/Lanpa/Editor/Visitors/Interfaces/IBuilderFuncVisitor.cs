namespace Lanpa
{
    public interface IBuilderFuncVisitor<T>
    {
        T Accept(LButtonBuilder builder);
        T Accept(LCheckBoxBuilder builder);
        T Accept(LTextBuilder builder);
        T Accept(LDropDownBuilder builder);
        T Accept(LMultiDropDownBuilder builder);
        T Accept(LDictionaryBuilder builder);
        T Accept(LUnityObjectBuilder builder);
        T Accept(LSerializedObjectBuilder builder);
        T Accept(LListBuilder builder);
    }

    public interface IBuilderFuncVisitor<T, A>
    {
        T Accept(LButtonBuilder builder, A a);
        T Accept(LCheckBoxBuilder builder, A a);
        T Accept(LTextBuilder builder, A a);
        T Accept(LDropDownBuilder builder, A a);
        T Accept(LMultiDropDownBuilder builder, A a);
        T Accept(LDictionaryBuilder builder, A a);
        T Accept(LUnityObjectBuilder builder, A a);
        T Accept(LSerializedObjectBuilder builder, A a);
        T Accept(LListBuilder builder, A a);
    }

    public interface IBuilderFuncVisitor<T, A, B>
    {
        T Accept(LButtonBuilder builder, A a, B b);
        T Accept(LCheckBoxBuilder builder, A a, B b);
        T Accept(LTextBuilder builder, A a, B b);
        T Accept(LDropDownBuilder builder, A a, B b);
        T Accept(LMultiDropDownBuilder builder, A a, B b);
        T Accept(LDictionaryBuilder builder, A a, B b);
        T Accept(LUnityObjectBuilder builder, A a, B b);
        T Accept(LSerializedObjectBuilder builder, A a, B b);
        T Accept(LListBuilder builder, A a, B b);
    }

    public interface IBuilderFuncVisitor<T, A, B, C>
    {
        T Accept(LButtonBuilder builder, A a, B b, C c);
        T Accept(LCheckBoxBuilder builder, A a, B b, C c);
        T Accept(LTextBuilder builder, A a, B b, C c);
        T Accept(LDropDownBuilder builder, A a, B b, C c);
        T Accept(LMultiDropDownBuilder builder, A a, B b, C c);
        T Accept(LDictionaryBuilder builder, A a, B b, C c);
        T Accept(LUnityObjectBuilder builder, A a, B b, C c);
        T Accept(LSerializedObjectBuilder builder, A a, B b, C c);
        T Accept(LListBuilder builder, A a, B b, C c);
    }

    public interface IBuilderFuncVisitor<T, A, B, C, D>
    {
        T Accept(LButtonBuilder builder, A a, B b, C c, D d);
        T Accept(LCheckBoxBuilder builder, A a, B b, C c, D d);
        T Accept(LTextBuilder builder, A a, B b, C c, D d);
        T Accept(LDropDownBuilder builder, A a, B b, C c, D d);
        T Accept(LMultiDropDownBuilder builder, A a, B b, C c, D d);
        T Accept(LDictionaryBuilder builder, A a, B b, C c, D d);
        T Accept(LUnityObjectBuilder builder, A a, B b, C c, D d);
        T Accept(LSerializedObjectBuilder builder, A a, B b, C c, D d);
        T Accept(LListBuilder builder, A a, B b, C c, D d);
    }
}