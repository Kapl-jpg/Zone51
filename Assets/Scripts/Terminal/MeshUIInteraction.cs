using Terminal;
using UnityEngine;
using UnityEngine.UI;

public class MeshUIInteraction : MonoBehaviour
{
    [SerializeField] private TerminalInput terminalInput;
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

        Ray ray = Camera.main.ScreenPointToRay(terminalInput.MousePosition());
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
                    if (IsInRect(rect, canvasScreenPos, canvasMax))
                    {
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

        Camera cam = uiCanvas.worldCamera ?? Camera.main;

        Vector2 minScreenPos = RectTransformUtility.WorldToScreenPoint(cam, corners[0]) - canvasMax;
        Vector2 maxScreenPos = RectTransformUtility.WorldToScreenPoint(cam, corners[2]) - canvasMax;

        return canvasScreenPos.x >= minScreenPos.x && canvasScreenPos.x <= maxScreenPos.x &&
               canvasScreenPos.y >= minScreenPos.y && canvasScreenPos.y <= maxScreenPos.y;
    }
}