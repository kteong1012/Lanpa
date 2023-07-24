using System;

namespace Lanpa
{
    public abstract class LanpaAttribute : Attribute
    {
        public string label;
        public int order;
    }
}