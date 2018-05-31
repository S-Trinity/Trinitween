using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Trinitween.InternalData
{
    public class TriTween
    {
        public float progress;
        public bool isDurationBased;
        public float smooth;
        public SmoothType smoothType;
    }

}
namespace Trinitween
{
    public enum SmoothType
    {
        Linear,
        EaseOut
    }

}
