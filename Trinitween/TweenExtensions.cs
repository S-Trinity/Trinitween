using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Trinitween.InternalData;

namespace Trinitween
{
    public static class TweenExtensions
    {
        public static T SetEase<T>(this T t, EaseType type) where T : TriTween
        {
            t.easeType = type;
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

        public static T Pause<T>(this T t) where T : TriTween
        {
            t.pause = true;
            return t;
        }

        public static T Resume<T>(this T t) where T : TriTween
        {
            t.pause = false;
            return t;
        }
        
        public static T Stop<T>(this T t) where T : TriTween
        {
            t.stop = true;
            return t;
        }

        public static T SetCurve<T>(this T t, AnimationCurve curve) where T : TriTween
        {
            t.curve = curve;
            return t;
        }
    }
}
