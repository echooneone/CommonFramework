using CustomGUI.Base;
using UnityEngine;
using UnityEngine.Events;

namespace CustomGUI.Control
{
    public class CustomGUIButton : CustomGUIControl
    {
        public event UnityAction ClickEvent; 
        protected override void StyleOnDraw()
        {
            if (GUI.Button(guiPos.Pos, content, style))
            {
                ClickEvent?.Invoke();
            }
        }

        protected override void StyleOffDraw()
        {
            if (GUI.Button(guiPos.Pos, content))
            {
                ClickEvent?.Invoke();
            }
        }

    }
}
