using UnityEngine;
using UnityEngine.UI;

public class MeshUIInteraction : MonoBehaviour
{
    public Camera uiCamera;     // Камера, рендерящая UI
    public Canvas uiCanvas;     // Canvas с UI элементами

    private Collider _meshCollider;

    void Start()
    {
        _meshCollider = GetComponent<Collider>();

        // Проверка компонентов
        if (!uiCamera || !uiCanvas || !_meshCollider)
        {
            enabled = false;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Raycast на меш
            if (_meshCollider.Raycast(ray, out hit, Mathf.Infinity))
            {
                Vector2 uv = hit.textureCoord;
                
                // Получение экранных границ Canvas
                RectTransform canvasRect = uiCanvas.GetComponent<RectTransform>();
                
                Vector2 canvasMin = canvasRect.rect.min;
                Vector2 canvasMax = canvasRect.rect.max;

                // Преобразование UV в экранные координаты Canvas
                Vector2 canvasScreenPos = new Vector2(
                    Mathf.Lerp(canvasMin.x, canvasMax.x, uv.x),
                    Mathf.Lerp(canvasMin.y, canvasMax.y, uv.y)
                );

                // Проверка области Canvas
                bool isInCanvas = canvasScreenPos.x >= canvasMin.x && canvasScreenPos.x <= canvasMax.x &&
                                  canvasScreenPos.y >= canvasMin.y && canvasScreenPos.y <= canvasMax.y;

                if (!isInCanvas)
                {
                    return;
                }

                // Проверка дочерних RectTransform
                int objectCount = 0;
                RectTransform[] rectTransforms = uiCanvas.GetComponentsInChildren<RectTransform>(true);

                foreach (RectTransform rect in rectTransforms)
                {
                    if (rect.gameObject.activeInHierarchy)
                    {
                        objectCount++;

                        // Получение углов RectTransform в мировых координатах
                        Vector3[] corners = new Vector3[4];
                        rect.GetWorldCorners(corners);

                        // Преобразование мировых координат в экранные
                        Camera cam = uiCanvas.worldCamera ?? Camera.main; // Используем камеру канваса или основную
                        
                        Vector2 minScreenPos = RectTransformUtility.WorldToScreenPoint(cam, corners[0]) - canvasMax; // Нижний левый угол
                        Vector2 maxScreenPos = RectTransformUtility.WorldToScreenPoint(cam, corners[2]) - canvasMax; // Верхний правый угол

                        // Проверка попадания точки в экранных координатах
                        bool isInRect = canvasScreenPos.x >= minScreenPos.x && canvasScreenPos.x <= maxScreenPos.x &&
                                        canvasScreenPos.y >= minScreenPos.y && canvasScreenPos.y <= maxScreenPos.y;

                        // Проверка кнопки
                        if (isInRect)
                        {
                            if (rect.TryGetComponent(out Button button))
                            {
                                //Debug.Log($"Button hit: Min = {minScreenPos}, Max = {maxScreenPos}, Object = {button.gameObject.name}");
                                button.onClick.Invoke();
                            }
                        }
                    }
                }
            }
        }
    }
}