using Doozy.Runtime.UIManager.Containers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using static PInvoke;
[RequireComponent(typeof(Graphic))]
public class WindowMoveHandlerInputSystem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    public WindowScript ws;
    static bool isDrag = false;
    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        isDrag = Mouse.current.leftButton.isPressed;
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData) => isDrag = false;
    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        isDrag = Mouse.current.leftButton.isPressed;
    }
    
    private void Update()
    {
        if (!Application.isEditor && isDrag&&!ws.Maximized)
        {
            DragWindow();
        }
    }


}
    

