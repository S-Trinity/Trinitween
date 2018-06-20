using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Trinitween;

namespace Trinitween.InternalData
{
    [System.Serializable]
    public class TriTween
    {
        public float progress;
        public float smooth;
        public float timeElapsed;

        public bool isDurationBased;
        public bool pause;
        public bool stop;
        public bool hasEndMethod;

        public EaseType easeType = EaseType.Linear;
        public AnimationCurve curve = null;

        public System.Action method;
    }
}