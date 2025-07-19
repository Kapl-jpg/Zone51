using System.Collections;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    [SerializeField] private float offsetY = 0.1f;
    [SerializeField] private float moveTime = 3f;
    private bool _moveUp;
    private Vector3 _startPos;

    private IEnumerator Start()
    {
        _moveUp = Random.value > 0.5f;
        _startPos = transform.position;
        var upPoint = new Vector3(_startPos.x, _startPos.y + offsetY, _startPos.z);
        var downPoint = new Vector3(_startPos.x, _startPos.y - offsetY, _startPos.z);
        while (true)
        {
            if (_moveUp)
            {
                var t = 0f;
                while (t < 1f)
                {
                    t += Time.deltaTime / moveTime;
                    transform.position = Vector3.Lerp(downPoint, upPoint, t);
                    yield return null;
                }

                _moveUp = false;
            }
            else
            {
                var t = 0f;
                while (t < 1f)
                {
                    t += Time.deltaTime / moveTime;
                    transform.position = Vector3.Lerp(upPoint, downPoint, t);
                    yield return null;
                }
                
                _moveUp = true;
            }

            yield return null;
        }
    }
}
