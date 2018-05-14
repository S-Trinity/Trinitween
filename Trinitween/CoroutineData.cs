using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Trinitween.Coroutines
{
    class CoTrT
    {
        public static IEnumerator SlideValue(Slider slider, float newValue, float smooth)
        {
            yield return null;
            while (slider.value.ToString("F3") != newValue.ToString("F3"))
            {
                slider.value = Mathf.Lerp(slider.value, newValue, smooth);
                yield return new WaitForSecondsRealtime(Time.deltaTime);
            }
        }
        public static IEnumerator Move(Transform transform, Vector3 newValue, float smooth)
        {
            while (transform.position.ToString("F2") != newValue.ToString("F2"))
            {
                Debug.Log("AAAAAA");
                transform.position = Vector3.Lerp(transform.position, newValue, smooth);
                yield return new WaitForSecondsRealtime(Time.deltaTime);
            }
			GameObject.Destroy(transform.GetComponent<EmptyScript>());
        }
    }
}
