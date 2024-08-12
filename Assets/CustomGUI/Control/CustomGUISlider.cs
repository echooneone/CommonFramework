using System;
using System.Collections;
using System.Collections.Generic;
using CustomGUI.Base;
using UnityEngine;
using UnityEngine.Events;

namespace CustomGUI.Control
{
    public enum ESliderType
    {
        Horizontal,
        Vertical,
    }

    public class CustomGUISlider : CustomGUIControl
    {
        //最小值
        public float minValue = 0;
        //最大值
        public float maxValue = 1;
        //当前值
        public float nowValue = 0;
        //水平还是竖直样式
        public ESliderType type = ESliderType.Horizontal;
        //小按钮的 style
        public GUIStyle styleThumb;

        public event UnityAction<float> ChangeValue;

        private float _oldValue = 0;

        protected override void StyleOffDraw()
        {
            switch (type)
            {
                case ESliderType.Horizontal:
                    nowValue = GUI.HorizontalSlider(guiPos.Pos, nowValue, minValue, maxValue);
                    break;
                case ESliderType.Vertical:
                    nowValue = GUI.VerticalSlider(guiPos.Pos, nowValue, minValue, maxValue);
                    break;
            }

            if(_oldValue != nowValue)
            {
                ChangeValue?.Invoke(nowValue);
                _oldValue = nowValue;
            }
        
        }

        protected override void StyleOnDraw()
        {
            switch (type)
            {
                case ESliderType.Horizontal:
                    nowValue = GUI.HorizontalSlider(guiPos.Pos, nowValue, minValue, maxValue, style, styleThumb);
                    break;
                case ESliderType.Vertical:
                    nowValue = GUI.VerticalSlider(guiPos.Pos, nowValue, minValue, maxValue, style, styleThumb);
                    break;
            }

            if (_oldValue != nowValue)
            {
                ChangeValue?.Invoke(nowValue);
                _oldValue = nowValue;
            }
        }
    }
}