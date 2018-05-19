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
        public static void TritSlideValue(this Slider slider, float newValue, float smooth = .1f)
        {
            slider.StartCoroutine(CoTrT.SlideValue(slider, newValue, smooth));
        }

        public static void TritMove(this Transform transform, Vector3 newValue, float smooth = .1f)
        {
            /*ThreadMoveHandle tmh = new ThreadMoveHandle(transform, newValue, smooth);
            Thread t = new Thread(new ThreadStart(tmh.ThreadMove));
            t.Start();*/

            TritMonoInstance.StartCoroutine(CoTrT.Move(transform, newValue, smooth));
            //CoTrT.Move(transform, newValue, smooth);
            //MonoBehaviour mono = new MonoBehaviour();
            //mono.StartCoroutine();
        }

        public static void TritRotate(this Transform transform, Vector3 orientation, float smooth = .1f)
        {
            TritMonoInstance.StartCoroutine(CoTrT.Rotate(transform, orientation, smooth));
        }

        public static void TritLookAt(this Transform transform, Transform lookAtTransform, float smooth = .1f)
        {
            TritMonoInstance.StartCoroutine(CoTrT.LookAtTransform(transform, lookAtTransform, smooth));
        }
        public static void TritLookAt(this Transform transform, Vector3 lookAtPosition, float smooth = .1f)
        {
            TritMonoInstance.StartCoroutine(CoTrT.LookAtVector3(transform, lookAtPosition, smooth));
        }
        static MonoBehaviour inst;

        static MonoBehaviour TritMonoInstance
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
