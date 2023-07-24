using System;

namespace Lanpa
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class LTextAttribute : LanpaAttribute
    {
        public bool inputText = false;

        public LTextAttribute(string label = null, bool inputText = false)
        {
            this.label = label;
            this.inputText = inputText;
        }
    }
}
