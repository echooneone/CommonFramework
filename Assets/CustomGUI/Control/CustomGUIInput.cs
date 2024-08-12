using CustomGUI.Base;
using UnityEngine;
using UnityEngine.Events;

namespace CustomGUI.Control
{
    public class CustomGUIInput : CustomGUIControl
    {
        public event UnityAction<string> TextChange;

        private string _oldStr = "";
        protected override void StyleOffDraw()
        {
            content.text = GUI.TextField(guiPos.Pos, content.text);
            if(_oldStr != content.text)
            {
                TextChange?.Invoke(_oldStr);
                _oldStr = content.text;
            }
        }

        protected override void StyleOnDraw()
        {
            content.text = GUI.TextField(guiPos.Pos, content.text, style);
            if (_oldStr != content.text)
            {
                TextChange?.Invoke(_oldStr);
                _oldStr = content.text;
            }
        }
    }
}
