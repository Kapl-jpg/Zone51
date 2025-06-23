using UnityEngine;

public class ModelRotate : MonoBehaviour
{
    [SerializeField] private float rotateSpeed;

    private float _currentAngle = 80f;

    private void Update()
    {
        _currentAngle += rotateSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(-90f, 0f, _currentAngle);
    }
}
