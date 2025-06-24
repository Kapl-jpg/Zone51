using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform camera;
    [SerializeField] private float mouseSensitivity = 0.1f;
    [SerializeField] private float minY = -30;
    [SerializeField] private float maxY = 30;

    private InputMeneger inputMeneger;

    private float xRotation = 0f;
    private float yRotation = 0f;

    private void Awake()
    {
        inputMeneger = GetComponent<InputMeneger>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void RotateCharacter()
    {
        Vector2 mouseInput = inputMeneger.InputMouse();
        float mouseX = mouseInput.x * mouseSensitivity;
        float mouseY = mouseInput.y * mouseSensitivity;

        xRotation += mouseX;
        yRotation -= mouseY;
        yRotation = Mathf.Clamp(yRotation, minY, maxY);

        camera.localEulerAngles = new Vector3(yRotation, 0f, 0f); 

        player.localEulerAngles = new Vector3(0f, xRotation, 0f);
    }
}
