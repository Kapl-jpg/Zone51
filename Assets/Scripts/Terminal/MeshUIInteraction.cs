using Terminal;
using UnityEngine;
using UnityEngine.UI;

public class MeshUIInteraction : MonoBehaviour
{
    [SerializeField] private TerminalInput terminalInput;
    [SerializeField] private TMPro.TMP_Text text;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Camera uiCamera;
    [SerializeField] private Canvas uiCanvas;

    private Collider _meshCollider;

    void Start()
    {
        _meshCollider = GetComponent<Collider>();

        if (!uiCamera || !uiCanvas || !_meshCollider)
        {
            enabled = false;
        }
    }

    void Update()
    {
        if (!terminalInput.Click()) return;

        Ray ray = mainCamera.ScreenPointToRay(terminalInput.MousePosition());
        RaycastHit hit;

        if (!_meshCollider.Raycast(ray, out hit, Mathf.Infinity)) return;

        Vector2 uv = hit.textureCoord;

        uiCanvas.TryGetComponent(out RectTransform canvasRect);

        Vector2 canvasMin = canvasRect.rect.min;
        Vector2 canvasMax = canvasRect.rect.max;

        Vector2 canvasScreenPos = new Vector2(
            Mathf.Lerp(canvasMin.x, canvasMax.x, uv.x),
            Mathf.Lerp(canvasMin.y, canvasMax.y, uv.y)
        );

        bool isInCanvas = canvasScreenPos.x >= canvasMin.x && canvasScreenPos.x <= canvasMax.x &&
                          canvasScreenPos.y >= canvasMin.y && canvasScreenPos.y <= canvasMax.y;
        
        if (!isInCanvas)
        {
            return;
        }

        int objectCount = 0;
        RectTransform[] rectTransforms = uiCanvas.GetComponentsInChildren<RectTransform>(true);

        foreach (RectTransform rect in rectTransforms)
        {
            if (!rect.gameObject.activeInHierarchy) continue;

            objectCount++;

            if (rect.TryGetComponent(out Button button))
            {
                if (button.enabled && button.interactable)
                {
                    Vector2 screenPos = terminalInput.MousePosition();
                    if (IsInRect(rect, screenPos, canvasMax))
                    {
                        Debug.LogError("5");
                        button.onClick.Invoke();
                        return;
                    }
                }
            }
        }
    }

    private bool IsInRect(RectTransform rect, Vector2 canvasScreenPos, Vector2 canvasMax)
    {
        Vector3[] corners = new Vector3[4];
        rect.GetWorldCorners(corners);

        Camera cam = uiCanvas.worldCamera ?? mainCamera;

        if (cam == null)
        {
            Debug.LogError("IsInRect: Camera is NULL");
            return false;
        }

        // Преобразуем углы в экранные координаты
        Vector2 screenMin = RectTransformUtility.WorldToScreenPoint(cam, corners[0]);
        Vector2 screenMax = RectTransformUtility.WorldToScreenPoint(cam, corners[2]);

        // Логируем всё для понимания
        Debug.LogError($"[IsInRect] rect = {rect.name}");
        Debug.LogError($"  ScreenMin = {screenMin}, ScreenMax = {screenMax}");
        Debug.LogError($"  canvasScreenPos = {canvasScreenPos}");
        Debug.LogError($"  canvasMax = {canvasMax}");

        bool isInside = canvasScreenPos.x >= screenMin.x && canvasScreenPos.x <= screenMax.x &&
                        canvasScreenPos.y >= screenMin.y && canvasScreenPos.y <= screenMax.y;

        Debug.LogError($"  → Click is inside: {isInside}");

        return isInside;
    }
}