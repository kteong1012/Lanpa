using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lanpa
{
    public static class LanpaBuilders
    {
        public static Dictionary<Type, LanpaBuilderBase> builders = new Dictionary<Type, LanpaBuilderBase>()
        {
            { typeof(LButtonAttribute), new LButtonBuilder() },
            { typeof(LTextAttribute), new LTextBuilder() },
            { typeof(LCheckBoxAttribute), new LCheckBoxBuilder() },
        };
    }
}