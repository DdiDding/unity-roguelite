using UnityEngine;
using UnityEngine.Rendering;

[DisallowMultipleComponent]
public sealed class ObjectLayerSetter : MonoBehaviour
{
    [SerializeField] private Transform sortPoint;

    private SortingGroup sortingGroup;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        if (TryGetComponent(out sortingGroup) || TryGetComponent(out spriteRenderer))
            return;

        Debug.LogError("ObjectLayerSetter에는 SortingGroup 또는 SpriteRenderer가 필요합니다.", this);
        enabled = false;
    }

    private void LateUpdate()
    {
        float y = sortPoint != null ? sortPoint.position.y : transform.position.y;
        int order = Mathf.Clamp(
            Mathf.RoundToInt(-y * 100f), short.MinValue, short.MaxValue);

        if (sortingGroup != null)
        {
            if (sortingGroup.sortingOrder != order)
                sortingGroup.sortingOrder = order;
        }
        else if (spriteRenderer.sortingOrder != order)
        {
            spriteRenderer.sortingOrder = order;
        }
    }
}