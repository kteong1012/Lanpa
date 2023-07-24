using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lanpa
{
    [AttributeUsage(AttributeTargets.Method)]
    public class LButtonAttribute : LanpaAttribute
    {
        public LButtonAttribute(string label = null)
        {
            this.label = label;
        }
    }
}
