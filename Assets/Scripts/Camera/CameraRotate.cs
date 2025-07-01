using System.Collections;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    [SerializeField] private float minAngle;
    [SerializeField] private float maxAngle;
    [SerializeField] private float translateTime;
    [SerializeField] private float delay;
    [SerializeField] private bool clockwise;

    private bool _right;
    private float _angle;

    private void Start()
    {
        StartCoroutine(clockwise ? ClockwiseRotate() : CounterClockwiseRotate());
    }

    private void Rotate(float angle)
    {
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, angle, transform.localEulerAngles.z);
    }

    private IEnumerator ClockwiseRotate()
    {
        yield return new WaitForSeconds(delay);

        var t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / translateTime;
            Rotate(Mathf.Lerp(minAngle, maxAngle, t));
            yield return null;
        }
        
        StartCoroutine(CounterClockwiseRotate());
    }

    private IEnumerator CounterClockwiseRotate()
    {
        yield return new WaitForSeconds(delay);

        var t = 1f;
        while (t > 0)
        {
            t -= Time.deltaTime / translateTime;
            Rotate(Mathf.Lerp(minAngle, maxAngle, t));
            yield return null;
        }

        StartCoroutine(ClockwiseRotate());
    }
}
