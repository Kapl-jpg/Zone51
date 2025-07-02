using UnityEngine;

public class ObjectForTelekinesis : MonoBehaviour
{
    private Rigidbody rb;
    private Renderer renderer;
    private Color originalColor;

    private bool activeCheckingMovement = false;
    private bool activeCheckingGravity = false;
    private float massObject;
    private float massForDeductions;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        renderer = GetComponent<Renderer>();

        massObject = rb.mass;
        massForDeductions = massObject * 100;
        rb.mass = massForDeductions;

        if (renderer != null)
        {
            originalColor = renderer.material.color;
        }
    }

    private void Update()
    {
        if (activeCheckingMovement)
        {
            if (rb.velocity == Vector3.zero)
            {
                rb.mass = massForDeductions;
                SettingTransparent(true);
                activeCheckingMovement = false;
            }
        } 
        
        if (activeCheckingGravity)
        {
            if (rb.useGravity == true)
            {
                activeCheckingMovement = true;
                activeCheckingGravity = false;
            }
        }
    }

    public void ActivatorCheckingGravity(bool active)
    {
        activeCheckingGravity = active;
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

    public void PurposeRealMass()
    {
        rb.mass = massObject;
    }
}
