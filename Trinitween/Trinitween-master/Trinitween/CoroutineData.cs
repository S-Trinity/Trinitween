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

        public static IEnumerator MoveEase(Transform transform, Vector3 newValue, float smooth, System.Action<TriTween> tweener)
        {
            TriTween tween = new TriTween() { smooth = smooth };
            tweener.Invoke(tween);
            yield return null;

            Vector3 origPos = transform.position;
            Vector3 newPos = Vector3.zero;
            float t = 0;
            tween.progress = 0;

            if (smooth != tween.smooth)
                smooth = tween.smooth;

            while (tween.progress < 1)
            {
                if (tween.isDurationBased && !tween.pause)
                {
                    if (smooth == 0)
                        break;

                    tween.progress = t / smooth;
                    newPos.x = Easing.Ease(tween.easeType, origPos.x, newValue.x, tween.progress, tween.curve);
                    newPos.y = Easing.Ease(tween.easeType, origPos.y, newValue.y, tween.progress, tween.curve);
                    newPos.z = Easing.Ease(tween.easeType, origPos.z, newValue.z, tween.progress, tween.curve);

                    transform.position = newPos;

                    if (t == smooth)
                        break;

                    t += Time.fixedDeltaTime;

                    if (t > smooth)
                        t = smooth;
                }
                yield return null;
            }
            transform.position = newValue;
        }
        public static IEnumerator Move(Transform transform, Vector3 newValue, float smooth, System.Action<TriTween> tweener)
        {
            TriTween tween = new TriTween() { smooth = smooth };
            tweener.Invoke(tween);
            yield return null;

            Vector3 origPos = transform.position;
            float progress = 0;
            if (smooth != tween.smooth)
                smooth = tween.smooth;

            while (transform.position.ToString(stopPrecision) != newValue.ToString(stopPrecision) && progress < 1)
            {
                if (tween.isDurationBased)
                {
                    transform.position = Vector3.Lerp(origPos, newValue, progress);
                    progress += Time.fixedUnscaledDeltaTime * 1 / smooth;
                }
                else
                {
                    transform.position = Vector3.Lerp(transform.position, newValue, smooth);
                }
                yield return new WaitForSecondsRealtime(Time.fixedUnscaledDeltaTime);
            }
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
    }
}
