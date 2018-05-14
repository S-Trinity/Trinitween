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
            Debug.Log("BBBBB");
			transform.GetOrAddComponent<EmptyScript>().StartCoroutine(CoTrT.Move(transform, newValue, smooth));
            //CoTrT.Move(transform, newValue, smooth);
            //MonoBehaviour mono = new MonoBehaviour();
            //mono.StartCoroutine();
        }

    }




}
