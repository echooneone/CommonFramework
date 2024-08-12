using CustomGUI.Base;
using UnityEngine;

namespace CustomGUI.Control
{
    public class CustomGUILabel : CustomGUIControl
    {
        protected override void StyleOnDraw()
        {
            GUI.Label(guiPos.Pos,content,style);
        }

        protected override void StyleOffDraw()
        {
            GUI.Label(guiPos.Pos,content);
        }
    }
}
