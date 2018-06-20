using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Trinitween.InternalData;
using Trinitween;

namespace Trinitween.Coroutines
{
    class CoTrT
    {
        static string stopPrecision = "F2";

        #region CoreMethods

        public static IEnumerator TweenCall(IEnumerator method, float smooth, System.Action<TriTween> tweener)
        {
            TriTween tween = new TriTween() { smooth = smooth };
            tweener.Invoke(tween);
            yield return null;
            Coroutine co;

            tween.progress = 0;
            if (smooth != tween.smooth)
                smooth = tween.smooth;

            ShortcutExtensions.tritMonoInstance.tweens.Add(tween);

            co = ShortcutExtensions.tritMonoInstance.StartCoroutine(method);

            //Wait for Tween to end
            yield return co;

            //Call end method if there's one
            if (tween.hasEndMethod)
                tween.method.Invoke();
        }

        public static float TweenedFloat(EaseType type, float from, float to, float t, AnimationCurve curve = null)
        {
            return Easing.Ease(type, from, to, t, curve);
        }

        private static float GetProgress(TriTween tween)
        {
            if (tween.pause)
                return tween.progress;
            if (tween.smooth == 0)
                return 1f;
            if (tween.isDurationBased)
            {
                if (tween.timeElapsed > tween.smooth)
                {
                    tween.timeElapsed = tween.smooth;
                    return 1f;
                }
                return tween.progress = tween.timeElapsed / tween.smooth;
            }
            else
            {
                return tween.progress += tween.smooth;
            }
        }

        #endregion CoreMethods

        #region TweenMethods

        public static IEnumerator SlideValue(Slider slider, float newValue, float smooth)
        {
            yield return null;
            while (slider.value.ToString(stopPrecision) != newValue.ToString(stopPrecision))
            {
                slider.value = Mathf.Lerp(slider.value, newValue, smooth);
                yield return new WaitForSecondsRealtime(Time.fixedUnscaledDeltaTime);
            }
            slider.value = newValue;
        }


        public static IEnumerator Moving(Transform transform, Vector3 newValue, float smooth, System.Action<TriTween> tweener)
        {
            TriTween tween = ShortcutExtensions.tritMonoInstance.tweens[0];
            ShortcutExtensions.tritMonoInstance.tweens.Remove(tween);

            Vector3 origPos = transform.position;
            Vector3 newPos = Vector3.zero;

            tween.progress = 0;

            while (tween.progress < 1 && !tween.stop)
            {
                tween.progress = GetProgress(tween);
                newPos.x = TweenedFloat(tween.easeType, origPos.x, newValue.x, tween.progress, tween.curve);
                newPos.y = TweenedFloat(tween.easeType, origPos.y, newValue.y, tween.progress, tween.curve);
                newPos.z = TweenedFloat(tween.easeType, origPos.z, newValue.z, tween.progress, tween.curve);
                transform.position = newPos;
                if (!tween.pause)
                    tween.timeElapsed += Time.fixedDeltaTime;
                yield return null;
            }
            if (!tween.stop)
                transform.position = newValue;
        }

        public static IEnumerator Rotate(Transform transform, Vector3 orientation, float smooth, System.Action<TriTween> tweener)
        {
            //Recover eventual Settings from extensions
            TriTween tween = new TriTween() { smooth = smooth };
            tweener.Invoke(tween);
            yield return null;

            float progress = 0;
            Quaternion origRot = transform.rotation;
            if (smooth != tween.smooth)
                smooth = tween.smooth;


            while (transform.rotation.ToString(stopPrecision) != orientation.ToString(stopPrecision) && progress < 1)
            {
                if (tween.isDurationBased)
                {
                    transform.rotation = Quaternion.Euler(Vector3.Lerp(transform.rotation.eulerAngles, orientation, progress));
                    progress += Time.fixedUnscaledDeltaTime * 1 / smooth;
                }
                else
                {
                    transform.rotation = Quaternion.Euler(Vector3.Lerp(transform.rotation.eulerAngles, orientation, smooth));
                }
                yield return new WaitForSecondsRealtime(Time.fixedUnscaledDeltaTime);
            }
            transform.rotation = Quaternion.Euler(orientation);
        }

        public static IEnumerator LookAtTransform(Transform transform, Transform lookAtTransform, float smooth, System.Action<TriTween> tweener)
        {
            //Recover eventual Settings from extensions
            TriTween tween = new TriTween() { smooth = smooth };
            tweener.Invoke(tween);
            yield return null;

            float progress = 0;
            Quaternion origRot = transform.rotation;
            if (smooth != tween.smooth)
                smooth = tween.smooth;

            while (transform.rotation.ToString(stopPrecision) != Quaternion.LookRotation(lookAtTransform.position - transform.position, Vector3.up).ToString(stopPrecision) && progress < 1)
            {
                if (tween.isDurationBased)
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookAtTransform.position - transform.position, Vector3.up), progress);
                    progress += Time.fixedUnscaledDeltaTime * 1 / smooth;
                }
                else
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookAtTransform.position - transform.position, Vector3.up), smooth);
                }

                yield return new WaitForSecondsRealtime(Time.fixedUnscaledDeltaTime);
            }
            transform.rotation = Quaternion.LookRotation(lookAtTransform.position - transform.position, Vector3.up);
        }

        public static IEnumerator LookAtVector3(Transform transform, Vector3 lookAtV3, float smooth, System.Action<TriTween> tweener)
        {
            //Recover eventual Settings from extensions
            TriTween tween = new TriTween() { smooth = smooth };
            tweener.Invoke(tween);
            yield return null;

            float progress = 0;
            Quaternion origRot = transform.rotation;
            if (smooth != tween.smooth)
                smooth = tween.smooth;

            Quaternion rot = Quaternion.LookRotation(transform.position - lookAtV3, Vector3.up);
            while (transform.rotation.ToString(stopPrecision) != rot.ToString(stopPrecision))
            {
                if (tween.isDurationBased)
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation, rot, progress);
                    progress += Time.fixedUnscaledDeltaTime * 1 / smooth;
                }
                else
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation, rot, smooth);
                }
                yield return new WaitForSecondsRealtime(Time.fixedUnscaledDeltaTime);
            }
            transform.rotation = rot;
        }

        #endregion TweenMethods
    }
}