using System;
using UnityEngine;

public class AnimatorChecker: MonoBehaviour
{
    [SerializeField] private Animator animator;
    private void Update()
    {
        if (animator.speed == 0f)
        {
            Debug.Log("Animator остановлен (speed = 0)");
        }
        else
        {
            Debug.Log("Animator работает (speed > 0)");
        }
    }
}