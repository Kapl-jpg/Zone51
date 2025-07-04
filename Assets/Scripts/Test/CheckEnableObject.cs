using System;
using UnityEngine;

namespace Test
{
    public class CheckEnableObject: MonoBehaviour
    {
        void OnEnable()
        {
            Debug.Log($"[{Time.frameCount}] {gameObject.name} включён. Стек:\n" + Environment.StackTrace);
        }
    }
}