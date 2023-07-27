using System.Reflection;

namespace Lanpa
{
    public class MemberBuilderFactoryVisitor : IAttributeFuncVisitor<LanpaBuilderBase, MemberInfo>
    {
        public static MemberBuilderFactoryVisitor Instance { get; } = new MemberBuilderFactoryVisitor();

        public LanpaBuilderBase Accept(LButtonAttribute attribute, MemberInfo memberInfo)
        {
            return new LButtonBuilder(memberInfo.GetMemberType(), memberInfo as MethodInfo, attribute.order);
        }

        public LanpaBuilderBase Accept(LCheckBoxAttribute attribute, MemberInfo memberInfo)
        {
            return new LCheckBoxBuilder(memberInfo.GetMemberType(), attribute.order);
        }

        public LanpaBuilderBase Accept(LTextAttribute attribute, MemberInfo memberInfo)
        {
            return new LTextBuilder(memberInfo.GetMemberType(), attribute.order);
        }

        public LanpaBuilderBase Accept(LDropDownAttribute attribute, MemberInfo memberInfo)
        {
            return new LDropDownBuilder(memberInfo.GetMemberType(), attribute.order);
        }

        public LanpaBuilderBase Accept(LMultiDropDownAttribute attribute, MemberInfo memberInfo)
        {
            return new LMultiDropDownBuilder(memberInfo.GetMemberType(), attribute.order);
        }

        public LanpaBuilderBase Accept(LDictionaryAttribute attribute, MemberInfo memberInfo)
        {
            return new LDictionaryBuilder(memberInfo.GetMemberType(), attribute.order);
        }

        public LanpaBuilderBase Accept(LUnityObjectAttribute attribute, MemberInfo memberInfo)
        {
            return new LUnityObjectBuilder(memberInfo.GetMemberType(), attribute.order, attribute.allowSceneObject);
        }

        public LanpaBuilderBase Accept(LSerializedObjectAttribute attribute, MemberInfo memberInfo)
        {
            return new LSerializedObjectBuilder(memberInfo.GetMemberType(), attribute.order);
        }

        public LanpaBuilderBase Accept(LListAttribute attribute, MemberInfo memberInfo)
        {
            return new LListBuilder(memberInfo.GetMemberType(), attribute.order);
        }
    }
}