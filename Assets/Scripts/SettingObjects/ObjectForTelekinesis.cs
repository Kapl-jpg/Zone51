using UnityEngine;

public class ObjectForTelekinesis : MonoBehaviour
{
    private Rigidbody rb;
    private Renderer renderer;
    private Color originalColor;
    private bool activeChecking = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            originalColor = renderer.material.color;
        }
    }

    private void Update()
    {
        if (activeChecking)
        {
            if (rb.linearVelocity == Vector3.zero)
            {
                rb.isKinematic = true;
                activeChecking = false;
                SettingTransparent(true);
            }
        }     
    }

    public void ActivatorChecking(bool active)
    {
        activeChecking = active;
    }

    public void SettingTransparent(bool activator)
    {
        if (rb != null)
        {
            if (renderer != null)
            {
                Color newColor = originalColor;
                newColor.a = activator ? 1f : 0.5f;
                renderer.material.color = newColor;
            }
        }
    }
}
