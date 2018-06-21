using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using Trinitween.Coroutines;
using Trinitween.InternalData;

namespace Trinitween
{
    public static class ShortcutExtensions
    {
        #region Instance
        static EmptyScript inst;
        public static EmptyScript tritMonoInstance
        {
            get
            {
                if (inst != null)
                    return inst;
                else
                {
                    inst = new GameObject().AddComponent(typeof(EmptyScript)) as EmptyScript;
                }
                return inst;
            }
        }

        #endregion Instance

        #region TweenExtensionMethods

        #region TransformExtension

        /// <summary>
        /// Tween a movement to the newValue.
        /// </summary>
        /// <param name="transform">    The transform that will move.</param>
        /// <para/>
        /// <param name="newValue"> The world position towards which the transform will move.</param>
        /// <para/>
        /// <param name="smooth">   A linear interpolation parameter. The closer to 0, the smaller the steps will be.</param>
        public static TriTween TritMove(this Transform transform, Vector3 newValue, float smooth = .1f)
        {
            TriTween tween = new TriTween();
            tritMonoInstance.StartCoroutine(CoTrT.TweenCall(CoTrT.Moving(transform, newValue), smooth, value => tween = value));
            return tween;
        }

        /// <summary>
        /// Tween a Rotation to the orientation.
        /// </summary>
        /// <param name="transform">The transform that will rotate.</param>
        /// <para/>
        /// <param name="orientation">The rotation in euler angles which the transform will tween to</param>
        /// <para/>
        /// <param name="smooth">A linear interpolation parameter. The closer to 0, the smaller the steps will be.</param>
        public static TriTween TritRotate(this Transform transform, Vector3 orientation, float smooth = .1f)
        {
            TriTween tween = new TriTween();
            tritMonoInstance.StartCoroutine(CoTrT.TweenCall(CoTrT.Rotate(transform, orientation), smooth, value => tween = value));
            return tween;
            // TriTween tween = new TriTween();
            // tritMonoInstance.StartCoroutine(CoTrT.Rotate(transform, orientation, smooth, value => tween = value));
            // return tween;
        }
        /// <summary>
        /// Tween a "Look At" to a world position in Vector 3.
        /// </summary>
        /// <param name="transform">The transform that will look at.</param>
        /// <para/>
        /// <param name="lookAtTransform">The transform toward which the designated transform will look at.</param>
        /// <para/>
        /// <param name="smooth">A linear interpolation parameter. The closer to 0, the smaller the steps will be.</param>
        public static TriTween TritLookAt(this Transform transform, Transform lookAtTransform, float smooth = .1f)
        {
            TriTween tween = new TriTween();
            tritMonoInstance.StartCoroutine(CoTrT.LookAtTransform(transform, lookAtTransform, smooth, value => tween = value));
            return tween;
        }
        /// <summary>
        /// Tween a "Look At" to a world position in Vector 3.
        /// </summary>
        /// <param name="transform">The transform that will look at.</param>
        /// <para/>
        /// <param name="lookAtPosition">The world position toward which the designated transform will look at.</param>
        /// <para/>
        /// <param name="smooth">A linear interpolation parameter. The closer to 0, the smaller the steps will be.</param>
        /// <returns></returns>
        public static TriTween TritLookAt(this Transform transform, Vector3 lookAtPosition, float smooth = .1f)
        {
            TriTween tween = new TriTween();
            tritMonoInstance.StartCoroutine(CoTrT.LookAtVector3(transform, lookAtPosition, smooth, value => tween = value));
            return tween;
        }

        #endregion TransformExtension

        #region SliderExtension

        /// <summary>
        /// Tween a slider value.
        /// </summary>
        /// <param name="slider">The slider that will move.</param>
        /// <para/>
        /// <param name="newValue">The new value of the slider</param>
        /// <para/>
        /// <param name="smooth">A linear interpolation parameter. The closer to 0, the smaller the steps will be.</param>
        public static void TritSlideValue(this Slider slider, float newValue, float smooth = .1f)
        {
            //TriTween tween = new TriTween();
            slider.StartCoroutine(CoTrT.SlideValue(slider, newValue, smooth));
            //return tween;
        }

        #endregion SliderExtension

        #region AudioExtension

        /// <summary>
        /// Tween an AudioSource volume.
        /// </summary>
        /// <param name="aSource">The AudioSource that will be affected.</param>
        /// <para/>
        /// <param name="newValue">The new value of the AudioSource volume.</param>
        /// <para/>
        /// <param name="smooth">A linear interpolation parameter. The closer to 0, the smaller the steps will be.</param>
        public static TriTween TritVol(this AudioSource aSource, float newValue, float smooth = .1f)
        {
            TriTween tween = new TriTween();
            tritMonoInstance.StartCoroutine(CoTrT.TweenCall(CoTrT.AudioVolume(aSource, newValue), smooth, value => tween = value));
            return tween;
        }

        #endregion AudioExtension

        #endregion TweenExtensionMethods
    }
}