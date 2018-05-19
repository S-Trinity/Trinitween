using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using Trinitween.Threads;
using Trinitween.Coroutines;


namespace Trinitween
{
    public static class ShortcutExtensions
    {  
        /// <summary>
        /// Tween a slider value.
        /// </summary>
        /// <param name="slider">The slider that will move.</param>
        /// <param name="newValue">The new value of the slider</param>
        /// <param name="smooth">A linear interpolation parameter. The closer to 0, the smaller the steps will be.</param>
        public static void TritSlideValue(this Slider slider, float newValue, float smooth = .1f)
        {
            slider.StartCoroutine(CoTrT.SlideValue(slider, newValue, smooth));
        }
        /// <summary>
        /// Tween a movement to the newValue.
        /// </summary>
        /// <param name="transform">The transform that will move.</param>
        /// <param name="newValue">The world position towards which the transform will move.</param>
        /// <param name="smooth">A linear interpolation parameter. The closer to 0, the smaller the steps will be.</param>
        public static void TritMove(this Transform transform, Vector3 newValue, float smooth = .1f)
        {
            tritMonoInstance.StartCoroutine(CoTrT.Move(transform, newValue, smooth));
        }
        /// <summary>
        /// Tween a Rotation to the orientation.
        /// </summary>
        /// <param name="transform">The transform that will rotate.</param>
        /// <param name="orientation">The rotation in euler angles which the transform will tween to</param>
        /// <param name="smooth">A linear interpolation parameter. The closer to 0, the smaller the steps will be.</param>
        public static void TritRotate(this Transform transform, Vector3 orientation, float smooth = .1f)
        {
            tritMonoInstance.StartCoroutine(CoTrT.Rotate(transform, orientation, smooth));
        }
        /// <summary>
        /// Tween a "Look At" to a world position in Vector 3.
        /// </summary>
        /// <param name="transform">The transform that will look at.</param>
        /// <param name="lookAtTransform">The transform toward which the designated transform will look at.</param>
        /// <param name="smooth">A linear interpolation parameter. The closer to 0, the smaller the steps will be.</param>
        public static void TritLookAt(this Transform transform, Transform lookAtTransform, float smooth = .1f)
        {
            tritMonoInstance.StartCoroutine(CoTrT.LookAtTransform(transform, lookAtTransform, smooth));
        }
        /// <summary>
        /// Tween a "Look At" to a world position in Vector 3.
        /// </summary>
        /// <param name="transform">The transform that will look at.</param>
        /// <param name="lookAtPosition">The world position toward which the designated transform will look at.</param>
        /// <param name="smooth">A linear interpolation parameter. The closer to 0, the smaller the steps will be.</param>
        /// <returns></returns>
        public static void TritLookAt(this Transform transform, Vector3 lookAtPosition, float smooth = .1f)
        {
            tritMonoInstance.StartCoroutine(CoTrT.LookAtVector3(transform, lookAtPosition, smooth));
        }
        static MonoBehaviour inst;
        static MonoBehaviour tritMonoInstance
        {
            get
            {
                if (inst != null)
                    return inst;
                else
                {
                    if (EmptyScript.Instance != null)
                        inst = EmptyScript.Instance;
                    else
                        inst = GameObject.Instantiate(new GameObject()).AddComponent(typeof(EmptyScript)) as MonoBehaviour;
                }
                return inst;
            }
            set
            {
                inst = value;
            }
        }
    }
}
