using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Trinitween.InternalData;

namespace Trinitween
{
    public static class TweenExtensions
    {
        public static T SetEase<T>(this T t, SmoothType type) where T : TriTween
        {
            t.smoothType = type;
            return t;
        }
        public static T SetDurationBased<T>(this T t) where T : TriTween
        {
            t.isDurationBased = true;
            return t;
        }
        public static T SetDurationBased<T>(this T t, float duration) where T : TriTween
        {
            t.isDurationBased = true;
            t.smooth = duration;
            return t;
        }
    }
}
