using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GridDragHandle : MonoBehaviour, IDragHandler, IBeginDragHandler
{
    public RectTransform Holder;
    private Vector3 offset;
    private RectTransform rectTransform;
    private void Awake()
    {
        GetComponent<Image>().material.SetVector("_Offset", Vector4.zero);
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //将屏幕坐标转换成世界坐标
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, eventData.position, null, out var globalMousePos))
        {
            //计算UI和指针之间的位置偏移量
            //offset = globalMousePos- (Vector3)GetComponent<Image>().material.GetVector("_Offset");
            offset = Holder.position - globalMousePos;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, eventData.position, null, out var globalMousePos))
        {
            GetComponent<Image>().material.SetVector("_Offset", -offset - globalMousePos);
            Holder.position = offset + globalMousePos;
        }
    }
    
    
}