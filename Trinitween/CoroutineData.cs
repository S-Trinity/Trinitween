using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Trinitween.InternalData;
using Trinitween;
using System.Reflection;

namespace Trinitween.Coroutines
{
    class CoTrT
    {
        static string stopPrecision = "F2";
        public static IEnumerator LookAtVector3(Transform transform, Vector3 lookAtV3, float smooth, System.Action<TriTween> tweener)
        {
            //Recover eventual Settings from extensions
            TriTween tween = new TriTween() { smooth = smooth };
            tweener.Invoke(tween);
            Vector3 origRot = transform.rotation.eulerAngles;
            Vector3 newRot = Vector3.zero;
            Vector3 orientation = Quaternion.LookRotation(lookAtV3 - transform.position, Vector3.up).eulerAngles;
            tween.progress = 0;
            yield return null;

            while (tween.progress < 1)
            {
                tween.timeElapsed += Time.fixedDeltaTime;
                tween.progress = GetProgress(tween);
                newRot.x = TweenedFloat(tween.easeType, origRot.x, orientation.x, tween.progress, tween.curve);
                newRot.y = TweenedFloat(tween.easeType, origRot.y, orientation.y, tween.progress, tween.curve);
                newRot.z = TweenedFloat(tween.easeType, origRot.z, orientation.z, tween.progress, tween.curve);
                transform.rotation = Quaternion.Euler(newRot);
                yield return new WaitForFixedUpdate();
            }
            transform.rotation = Quaternion.Euler(orientation);
        }
        public static IEnumerator LookAtTransform(Transform transform, Transform lookAtTransform, float smooth, System.Action<TriTween> tweener)
        {
            //Recover eventual Settings from extensions
            TriTween tween = new TriTween() { smooth = smooth };
            tweener.Invoke(tween);
            Vector3 origRot = transform.position;
            Vector3 newRot = Vector3.zero;
            Vector3 orientation = Quaternion.LookRotation(lookAtTransform.position - transform.position, Vector3.up).eulerAngles;
            tween.progress = 0;
            yield return null;

            while (tween.progress < 1)
            {
                tween.timeElapsed += Time.fixedDeltaTime;
                tween.progress = GetProgress(tween);
                orientation = Quaternion.LookRotation(lookAtTransform.position - transform.position, Vector3.up).eulerAngles;
                newRot.x = TweenedFloat(tween.easeType, origRot.x, orientation.x, tween.progress, tween.curve);
                newRot.y = TweenedFloat(tween.easeType, origRot.y, orientation.y, tween.progress, tween.curve);
                newRot.z = TweenedFloat(tween.easeType, origRot.z, orientation.z, tween.progress, tween.curve);
                transform.rotation = Quaternion.Euler(newRot);
                yield return new WaitForFixedUpdate();
            }
            transform.rotation = Quaternion.LookRotation(lookAtTransform.position - transform.position, Vector3.up);
        }
        public static IEnumerator Rotate(Transform transform, Vector3 orientation, float smooth, System.Action<TriTween> tweener)
        {

            //Recover eventual Settings from extensions
            TriTween tween = new TriTween() { smooth = smooth };
            tweener.Invoke(tween);
            Vector3 origRot = transform.rotation.eulerAngles;
            Vector3 newRot = Vector3.zero;
            tween.progress = 0;
            yield return null; ;


            while (tween.progress < 1)
            {
                tween.timeElapsed += Time.fixedDeltaTime;
                tween.progress = GetProgress(tween);
                newRot.x = TweenedFloat(tween.easeType, origRot.x, orientation.x, tween.progress, tween.curve);
                newRot.y = TweenedFloat(tween.easeType, origRot.y, orientation.y, tween.progress, tween.curve);
                newRot.z = TweenedFloat(tween.easeType, origRot.z, orientation.z, tween.progress, tween.curve);
                transform.rotation = Quaternion.Euler(newRot);
                yield return new WaitForFixedUpdate();
            }
            transform.rotation = Quaternion.Euler(orientation);
        }
        public static IEnumerator SlideValue(Slider slider, float newValue, float smooth, System.Action<TriTween> tweener)
        {
            TriTween tween = new TriTween() { smooth = smooth };
            tweener.Invoke(tween);
            float origValue = slider.value;
            tween.progress = 0;
            yield return null;
            while (tween.progress < 1)
            {
                tween.timeElapsed += Time.fixedDeltaTime;
                tween.progress = GetProgress(tween);
                slider.value = TweenedFloat(tween.easeType, origValue, newValue, tween.progress, tween.curve);
                yield return new WaitForFixedUpdate();
            }
            slider.value = newValue;
        }
        public static IEnumerator Move(Transform transform, Vector3 newValue, float smooth, System.Action<TriTween> tweener)
        {
            TriTween tween = new TriTween() { smooth = smooth };
            tweener.Invoke(tween);
            Vector3 origPos = transform.position;
            Vector3 newPos = Vector3.zero;
            tween.progress = 0;
            yield return null;
            while (tween.progress < 1)
            {
                tween.timeElapsed += Time.fixedDeltaTime;
                tween.progress = GetProgress(tween);
                newPos.x = TweenedFloat(tween.easeType, origPos.x, newValue.x, tween.progress, tween.curve);
                newPos.y = TweenedFloat(tween.easeType, origPos.y, newValue.y, tween.progress, tween.curve);
                newPos.z = TweenedFloat(tween.easeType, origPos.z, newValue.z, tween.progress, tween.curve);
                transform.position = newPos;
                yield return new WaitForFixedUpdate();
            }
            transform.position = newValue;
        }
        public static IEnumerator TweenCall(IEnumerator method, float smooth, System.Action<TriTween> tweener)
        {
            TriTween tween = new TriTween() { smooth = smooth };
            tweener.Invoke(tween);
            yield return null;
            Coroutine co;
            tween.progress = 0;

            if (smooth != tween.smooth)
                smooth = tween.smooth;

            co = ShortcutExtensions.tritMonoInstance.StartCoroutine(method);
            //Wait for Tween to end
            yield return co;
            //There we may call any function after like a OnEnd() thing. That is the only reason why this coroutine isn't terminated.
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





        
    }
}
