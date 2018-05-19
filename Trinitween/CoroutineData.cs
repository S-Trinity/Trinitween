using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Trinitween.Coroutines
{
    class CoTrT
    {
        static string stopPrecision = "F3";
        public static IEnumerator SlideValue(Slider slider, float newValue, float smooth)
        {
            yield return null;
            while (slider.value.ToString(stopPrecision) != newValue.ToString(stopPrecision))
            {
                slider.value = Mathf.Lerp(slider.value, newValue, smooth);
                yield return new WaitForSecondsRealtime(Time.deltaTime);
            }
            slider.value = newValue;
        }
        public static IEnumerator Move(Transform transform, Vector3 newValue, float smooth)
        {
            while (transform.position.ToString(stopPrecision) != newValue.ToString(stopPrecision))
            {
                transform.position = Vector3.Lerp(transform.position, newValue, smooth);
                yield return new WaitForSecondsRealtime(Time.deltaTime);
            }
            transform.position = newValue;
			//GameObject.Destroy(transform.GetComponent<EmptyScript>());
        }

        public static IEnumerator Rotate(Transform transform, Vector3 orientation, float smooth)
        {
            while (transform.rotation.ToString(stopPrecision) != orientation.ToString(stopPrecision))
            {
                transform.rotation = Quaternion.Euler(Vector3.Lerp(transform.rotation.eulerAngles, orientation, smooth));
                yield return new WaitForSecondsRealtime(Time.deltaTime);
            }
            transform.rotation = Quaternion.Euler(orientation);
        }

        public static IEnumerator LookAtTransform(Transform transform, Transform lookAtTransform, float smooth)
        {
            while (transform.rotation.ToString(stopPrecision) != Quaternion.LookRotation(transform.position - lookAtTransform.position, Vector3.up).ToString(stopPrecision))
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(transform.position - lookAtTransform.position, Vector3.up), smooth);
                yield return new WaitForSecondsRealtime(Time.deltaTime);
            }
            transform.rotation = Quaternion.LookRotation(transform.position - lookAtTransform.position, Vector3.up);
        }
        
        public static IEnumerator LookAtVector3(Transform transform, Vector3 lookAtV3, float smooth)
        {
            Quaternion rot = Quaternion.LookRotation(transform.position - lookAtV3, Vector3.up);
            while (transform.rotation.ToString(stopPrecision) != rot.ToString(stopPrecision))
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, rot, smooth);
                yield return new WaitForSecondsRealtime(Time.deltaTime);
            }
            transform.rotation = rot;
        }
    }
}
