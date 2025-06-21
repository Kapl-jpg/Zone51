using UnityEngine;

public class SettingAnimations : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
   
    public void ActiveAnimationsDoor(bool active)
    {
        animator.SetBool("ActivatorDoor", active);
    }
}
