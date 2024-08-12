using CustomGUI.Base;
using UnityEngine;

namespace CustomGUI.Control
{
    public class CustomGUITexture : CustomGUIControl
    {
        public ScaleMode scaleMode = ScaleMode.StretchToFill;
        protected override void StyleOnDraw()
        {
            GUI.DrawTexture(guiPos.Pos,content.image,scaleMode);
        }

        protected override void StyleOffDraw()
        {
            GUI.DrawTexture(guiPos.Pos,content.image,scaleMode);
        }
    }
}
