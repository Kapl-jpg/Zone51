using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform camera;
    [SerializeField] private float mouseSensitivity = 0.1f;
    [SerializeField] private float minY;
    [SerializeField] private float maxY;

    private InputSystem_Actions input;
    private float xRotation = 0f;
    private float yRotation = 0f;

    private void Awake()
    {
        input = new InputSystem_Actions();
        input.Player.Enable();
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void RotateCharacter()
    {
        Vector2 mouseInput = input.Player.Look.ReadValue<Vector2>();
        float mouseX = mouseInput.x * mouseSensitivity;
        float mouseY = mouseInput.y * mouseSensitivity;

        xRotation += mouseX;
        yRotation -= mouseY;
        yRotation = Mathf.Clamp(yRotation, minY, maxY);

        // Поворот камеры только вокруг оси X
        camera.localEulerAngles = new Vector3(yRotation, 0f, 0f); 

        // Поворот игрока только вокруг оси Y
        player.localEulerAngles = new Vector3(0f, xRotation, 0f);
    }
}
