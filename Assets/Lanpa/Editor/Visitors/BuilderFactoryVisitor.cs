using System.Reflection;

namespace Lanpa
{
    public class BuilderFactoryVisitor : IAttributeFuncVisitor<LanpaBuilderBase, MemberInfo>
    {
        public static BuilderFactoryVisitor Instance { get; } = new BuilderFactoryVisitor();

        public LanpaBuilderBase Accept(LButtonAttribute attribute, MemberInfo memberInfo)
        {
            return new LButtonBuilder(memberInfo, attribute);
        }

        public LanpaBuilderBase Accept(LCheckBoxAttribute attribute, MemberInfo memberInfo)
        {
            return new LCheckBoxBuilder(memberInfo, attribute);
        }

        public LanpaBuilderBase Accept(LTextAttribute attribute, MemberInfo memberInfo)
        {
            return new LTextBuilder(memberInfo, attribute);
        }

        public LanpaBuilderBase Accept(LDropDownAttribute lDropDownAttribute, MemberInfo a)
        {
            return new LDropDownBuilder(a, lDropDownAttribute);
        }

        public LanpaBuilderBase Accept(LMultiDropDownAttribute lMultiDropDownAttribute, MemberInfo a)
        {
            return new LMultiDropDownBuilder(a, lMultiDropDownAttribute);
        }
    }
}