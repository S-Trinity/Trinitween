using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Trinitween.InternalData;

namespace Trinitween
{
    public static class TweenExtensions
    {
        #region TweenBehaviour

        /// <summary>
        /// Set a duration to the tween.
        /// </summary>
        /// <param name="t">The tween that will be affected.</param>
        /// <para/>
        /// <param name="duration">The duration taken by the tween to complete, default is 1 second.</param>
        public static T SetDurationBased<T>(this T t, float duration = 1f) where T : TriTween
        {
            t.isDurationBased = true;
            t.smooth = duration;
            return t;
        }

        /// <summary>
        /// Set an Easing method to define the tween interpolation behaviour.
        /// </summary>
        /// <param name="t">The tween that will be affected.</param>
        /// <para/>
        /// <param name="type">The type of curve that define the tween.</param>
        public static T SetEase<T>(this T t, EaseType type) where T : TriTween
        {
            t.easeType = type;
            return t;
        }

        /// <summary>
        /// Set a custom AnimationCurve to define the tween interpolation behaviour.
        /// Must have the Easing method set to Easy.Custom.
        /// </summary>
        /// <param name="t">The tween that will be affected.</param>
        /// <para/>
        /// <param name="curve">The AnimationCurve that define the tween.</param>
        public static T SetCurve<T>(this T t, AnimationCurve curve) where T : TriTween
        {
            t.curve = curve;
            return t;
        }

        #endregion TweenBehaviour

        #region TweenLifeMethods

        /// <summary>
        /// Pause a tween during it's execution.
        /// </summary>
        /// <param name="t">The tween that will be affected.</param>
        public static T Pause<T>(this T t) where T : TriTween
        {
            t.pause = true;
            return t;
        }

        /// <summary>
        /// Resume a tween if it's paused.
        /// </summary>
        /// <param name="t">The tween that will be affected.</param>
        public static T Resume<T>(this T t) where T : TriTween
        {
            t.pause = false;
            return t;
        }

        /// <summary>
        /// Kill a tween early.
        /// </summary>
        /// <param name="t">The tween that will be affected.</param>
        /// <para/>
        /// <param name="cancelEndMethod">Prevent the end method to be fired with the early stop, default to false.</param>
        public static T Stop<T>(this T t, bool cancelEndMethod = false) where T : TriTween
        {
            if (cancelEndMethod)
                t.hasEndMethod = false;

            t.stop = true;
            return t;
        }

        /// <summary>
        /// Add a method to execute at the end of the tween
        /// </summary>
        /// <param name="t">The tween that will be affected.</param>
        /// <para/>
        /// <param name="method">The method to execute.</param>

        public static T OnEnd<T>(this T t, System.Action method) where T : TriTween
        {
            t.hasEndMethod = true;
            t.method = method;
            return t;
        }

        #endregion TweenLifeMethods
    }
}
