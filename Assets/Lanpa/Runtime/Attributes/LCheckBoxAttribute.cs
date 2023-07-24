using System;

namespace Lanpa
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class LCheckBoxAttribute : LanpaAttribute
    {
        public LCheckBoxAttribute(string label = null)
        {
            this.label = label;
        }
    }
}
