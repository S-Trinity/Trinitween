using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Trinitween;

namespace Trinitween.InternalData
{
    public class TriTween
    {
        public float progress;
        public bool isDurationBased;
        public bool pause;
        public float smooth;
        public EaseType easeType = EaseType.Linear;
        public AnimationCurve curve = null;
    }
}