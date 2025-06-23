using UnityEngine;

public class DoorAnimation : MonoBehaviour
{
    private static readonly int Open = Animator.StringToHash("Open");
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }
   
    public void OpenDoor()
    {
        _animator.SetTrigger(Open);
    }
}
