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

        #region TransformTween

        public static IEnumerator Moving(Transform transform, Vector3 newValue)
        {
            TriTween tween = ShortcutExtensions.tritMonoInstance.tweens[0];
            ShortcutExtensions.tritMonoInstance.tweens.Remove(tween);

            Vector3 origValue = transform.position;
            Vector3 tempValue = Vector3.zero;

            tween.progress = 0;

            while (tween.progress < 1 && !tween.stop)
            {
                tween.progress = GetProgress(tween);
                tempValue.x = TweenedFloat(tween.easeType, origValue.x, newValue.x, tween.progress, tween.curve);
                tempValue.y = TweenedFloat(tween.easeType, origValue.y, newValue.y, tween.progress, tween.curve);
                tempValue.z = TweenedFloat(tween.easeType, origValue.z, newValue.z, tween.progress, tween.curve);
                transform.position = tempValue;

                if (!tween.pause)
                    tween.timeElapsed += (Time.fixedDeltaTime * Time.timeScale);

                yield return null;
            }
            if (!tween.stop)
                transform.position = newValue;
        }

        public static IEnumerator Rotate(Transform transform, Vector3 newValue)
        {
            TriTween tween = ShortcutExtensions.tritMonoInstance.tweens[0];
            ShortcutExtensions.tritMonoInstance.tweens.Remove(tween);

            Vector3 origValue = transform.rotation.eulerAngles;
            Vector3 tempValue = Vector3.zero;

            tween.progress = 0;

            while (tween.progress < 1 && !tween.stop)
            {
                tween.progress = GetProgress(tween);
                tempValue.x = TweenedFloat(tween.easeType, origValue.x, newValue.x, tween.progress, tween.curve);
                tempValue.y = TweenedFloat(tween.easeType, origValue.y, newValue.y, tween.progress, tween.curve);
                tempValue.z = TweenedFloat(tween.easeType, origValue.z, newValue.z, tween.progress, tween.curve);
                transform.rotation = Quaternion.Euler(tempValue);

                if (!tween.pause)
                    tween.timeElapsed += (Time.fixedDeltaTime * Time.timeScale);

                yield return null;
            }
            if (!tween.stop)
                transform.rotation = Quaternion.Euler(newValue);
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

        #endregion TransformTween

        #region SliderTween

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

        #endregion SliderTween

        #region AudioSourceTween

        public static IEnumerator AudioVolume(AudioSource aSource, float newValue)
        {
            TriTween tween = ShortcutExtensions.tritMonoInstance.tweens[0];
            ShortcutExtensions.tritMonoInstance.tweens.Remove(tween);

            float origValue = aSource.volume;
            float tempValue = 0.0f;

            tween.progress = 0;

            while (tween.progress < 1 && !tween.stop)
            {
                tween.progress = GetProgress(tween);
                tempValue = TweenedFloat(tween.easeType, origValue, newValue, tween.progress, tween.curve);
                aSource.volume = tempValue;

                if (!tween.pause)
                    tween.timeElapsed += (Time.fixedDeltaTime * Time.timeScale);

                yield return null;
            }
            if (!tween.stop)
                aSource.volume = newValue;
        }

        #endregion AudioSourceTween

        #endregion TweenMethods
    }
}