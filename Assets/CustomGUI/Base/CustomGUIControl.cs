using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomGUI.Base
{
    public enum EStyleOnOff
    {
        On,
        Off,
    }

    public abstract class CustomGUIControl : MonoBehaviour
    {
        //提取控件的共同表现
        //位置信息
        public CustomGUIPos guiPos;

        //显示内容信息
        public GUIContent content;

        //自定义样式
        public GUIStyle style;

        //自定义样式是否启用的开关
        public EStyleOnOff styleOnOrOff = EStyleOnOff.Off;

        //提供给外部 绘制GUI控件的方法
        public void DrawGUI()
        {
            switch (styleOnOrOff)
            {
                case EStyleOnOff.On:
                    StyleOnDraw();
                    break;
                case EStyleOnOff.Off:
                    StyleOffDraw();
                    break;
            }
        }

        protected abstract void StyleOnDraw();

        protected abstract void StyleOffDraw();
    }
}