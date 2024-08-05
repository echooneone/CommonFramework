using UnityEngine;
using UnityEngine.EventSystems;

public class UIDragHandle : MonoBehaviour, IDragHandler, IBeginDragHandler
{
    private Vector3 offset;
    private RectTransform rectTransform;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //将屏幕坐标转换成世界坐标
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, eventData.position, null, out var globalMousePos))
        {
            //计算UI和指针之间的位置偏移量
            offset = rectTransform.position - globalMousePos;
            
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, eventData.position, null, out var globalMousePos))
        {
            rectTransform.position = offset + globalMousePos;
        }
    }
}