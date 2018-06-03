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
        public bool stop;
        public float smooth;
        public EaseType easeType = EaseType.Linear;
        public AnimationCurve curve = null;
        public float timeElapsed;
    }
}
