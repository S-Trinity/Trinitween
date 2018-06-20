using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Trinitween.InternalData;

namespace Trinitween
{
    public static class TweenExtensions
    {
        #region TweenBehaviour

        public static T SetDurationBased<T>(this T t, float duration = 1f) where T : TriTween
        {
            t.isDurationBased = true;
            t.smooth = duration;
            return t;
        }

        public static T SetEase<T>(this T t, EaseType type) where T : TriTween
        {
            t.easeType = type;
            return t;
        }

        public static T SetCurve<T>(this T t, AnimationCurve curve) where T : TriTween
        {
            t.curve = curve;
            return t;
        }

        #endregion TweenBehaviour

        #region TweenLifeMethods

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

        public static T OnEnd<T>(this T t, System.Action method) where T : TriTween
        {
            t.hasEndMethod = true;
            t.method = method;
            return t;
        }

        #endregion TweenLifeMethods
    }
}
