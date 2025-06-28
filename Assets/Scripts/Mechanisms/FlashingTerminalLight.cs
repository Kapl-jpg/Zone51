using UnityEngine;

public class FlashingTerminalLight : Subscriber
{
    [SerializeField] private float flashSpeed = 1;
    [SerializeField] private bool isOpen;
    private Renderer _rend;
    private float _timer = 0;
    

    private void Start()
    {
        _rend = GetComponent<Renderer>();
    }

    [Event("FlashingLight")]

    private void FlashingLight(bool isOpen)
    {
        if (isOpen)
        {
            _rend.material.color = Color.green;
        }
        else
        {
            _timer += Time.deltaTime * flashSpeed;
            float t = Mathf.PingPong(_timer, 1f);
            _rend.material.color = Color.Lerp(Color.white, Color.red, t);
        }
    }
}
