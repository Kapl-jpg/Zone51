using System.Collections;
using UnityEngine;

public class CharacterStepsRotate : MonoBehaviour
{
    [SerializeField] private float rotateDelay;
    [SerializeField] private float angleStep;
    
    private float _angle;

    private IEnumerator Start()
    {
        _angle = transform.eulerAngles.y;
        
        while (true)
        {
            yield return new WaitForSeconds(rotateDelay);
            _angle += angleStep;
            transform.eulerAngles = new Vector3(0, _angle, 0);
        }
    }
}
