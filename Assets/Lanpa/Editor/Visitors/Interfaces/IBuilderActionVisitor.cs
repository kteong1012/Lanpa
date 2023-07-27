namespace Lanpa
{
    public interface IBuilderActionVisitor<A>
    {
        void Accept(LButtonBuilder builder, A a);
        void Accept(LCheckBoxBuilder builder, A a);
        void Accept(LTextBuilder builder, A a);
        void Accept(LDropDownBuilder builder, A a);
        void Accept(LMultiDropDownBuilder builder, A a);
        void Accept(LDictionaryBuilder builder, A a);
        void Accept(LUnityObjectBuilder builder, A a);
        void Accept(LSerializedObjectBuilder builder, A a);
    }

    public interface IBuilderActionVisitor<A, B>
    {
        void Accept(LButtonBuilder builder, A a, B b);
        void Accept(LCheckBoxBuilder builder, A a, B b);
        void Accept(LTextBuilder builder, A a, B b);
        void Accept(LDropDownBuilder builder, A a, B b);
        void Accept(LMultiDropDownBuilder builder, A a, B b);
        void Accept(LDictionaryBuilder builder, A a, B b);
        void Accept(LUnityObjectBuilder builder, A a, B b);
        void Accept(LSerializedObjectBuilder builder, A a, B b);
    }

    public interface IBuilderActionVisitor<A, B, C>
    {
        void Accept(LButtonBuilder builder, A a, B b, C c);
        void Accept(LCheckBoxBuilder builder, A a, B b, C c);
        void Accept(LTextBuilder builder, A a, B b, C c);
        void Accept(LDropDownBuilder builder, A a, B b, C c);
        void Accept(LMultiDropDownBuilder builder, A a, B b, C c);
        void Accept(LDictionaryBuilder builder, A a, B b, C c);
        void Accept(LUnityObjectBuilder builder, A a, B b, C c);
        void Accept(LSerializedObjectBuilder builder, A a, B b, C c);
    }

    public interface IBuilderActionVisitor<A, B, C, D>
    {
        void Accept(LButtonBuilder builder, A a, B b, C c, D d);
        void Accept(LCheckBoxBuilder builder, A a, B b, C c, D d);
        void Accept(LTextBuilder builder, A a, B b, C c, D d);
        void Accept(LDropDownBuilder builder, A a, B b, C c, D d);
        void Accept(LMultiDropDownBuilder builder, A a, B b, C c, D d);
        void Accept(LDictionaryBuilder builder, A a, B b, C c, D d);
        void Accept(LUnityObjectBuilder builder, A a, B b, C c, D d);
        void Accept(LSerializedObjectBuilder builder, A a, B b, C c, D d);
    }
}