using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

namespace Trinitween.Threads
{
    class ThreadMoveHandle
    {
        Transform _transform;
        Vector3 _newValue;
        float _smooth;
        public ThreadMoveHandle(Transform transform, Vector3 newValue, float smooth)
        {
            _transform = transform;
            _newValue = newValue;
            _smooth = smooth;
        }
        public void ThreadMove()
        {
            while (_transform.position.ToString("F3") != _newValue.ToString("F3"))
            {
                Debug.Log("AAAAAA");
                _transform.position = Vector3.Lerp(_transform.position, _newValue, _smooth);
                Thread.Sleep(Mathf.RoundToInt(Time.deltaTime * 1000f));
            }
        }

    }
}
