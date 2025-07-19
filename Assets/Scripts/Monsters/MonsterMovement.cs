using System.Collections;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    [SerializeField] private float offsetY = 0.1f;
    [SerializeField] private float moveSpeed;
    private bool _moveUp;
    private Vector3 _startPos;

    private IEnumerator Start()
    {
        _moveUp = Random.value > 0.5f;
        _startPos = transform.position;

        while (true)
        {
            if (_moveUp)
            {
                while (transform.position.y < _startPos.y + offsetY)
                {
                    transform.position = Vector3.MoveTowards(transform.position, transform.position + Vector3.up,
                        moveSpeed * Time.deltaTime);
                    yield return null;
                }
            }
            else
            {
                while (transform.position.y > _startPos.y - offsetY)
                {
                    transform.position = Vector3.MoveTowards(transform.position, transform.position + Vector3.down,
                        moveSpeed * Time.deltaTime);
                    yield return null;
                }
            }
        }
    }
}
