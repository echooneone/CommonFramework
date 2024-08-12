using UnityEngine;

namespace CustomGUI.Base
{
    /// <summary>
    /// 对齐方式枚举
    /// </summary>
    public enum EAlignmentType
    {
        Up,
        Down,
        Left,
        Right,
        Center,
        LeftUp,
        LeftDown,
        RightUp,
        RightDown,
    }

    /// <summary>
    /// 该类 是用来表示位置 计算位置相关信息的 不需要继承mono
    /// </summary>
    [System.Serializable]
    public class CustomGUIPos
    {
        //主要是处理 控件位置相关的内容
        //要完成 分辨率自适应的相关计算
        //该位置信息 会用来返回给外部 用于绘制控件
        //需要对它进行 计算
        private Rect _rPos = new Rect(0, 0, 100, 100);

        //屏幕九宫格对齐方式
        public EAlignmentType screenAlignmentType = EAlignmentType.Center;

        //控件中心对齐方式
        public EAlignmentType controlCenterAlignmentType = EAlignmentType.Center;

        //偏移位置
        public Vector2 pos;

        //宽高
        public float width = 100;

        public float height = 50;

        //用于计算的 中心点 成员变量
        private Vector2 _centerPos;

        //计算中心点偏移的方法
        private void CalcCenterPos()
        {
            switch (controlCenterAlignmentType)
            {
                case EAlignmentType.Up:
                    _centerPos.x = -width / 2;
                    _centerPos.y = 0;
                    break;
                case EAlignmentType.Down:
                    _centerPos.x = -width / 2;
                    _centerPos.y = -height;
                    break;
                case EAlignmentType.Left:
                    _centerPos.x = 0;
                    _centerPos.y = -height / 2;
                    break;
                case EAlignmentType.Right:
                    _centerPos.x = -width;
                    _centerPos.y = -height / 2;
                    break;
                case EAlignmentType.Center:
                    _centerPos.x = -width / 2;
                    _centerPos.y = -height / 2;
                    break;
                case EAlignmentType.LeftUp:
                    _centerPos.x = 0;
                    _centerPos.y = 0;
                    break;
                case EAlignmentType.LeftDown:
                    _centerPos.x = 0;
                    _centerPos.y = -height;
                    break;
                case EAlignmentType.RightUp:
                    _centerPos.x = -width;
                    _centerPos.y = 0;
                    break;
                case EAlignmentType.RightDown:
                    _centerPos.x = -width;
                    _centerPos.y = -height;
                    break;
            }
        }

        //计算最终相对坐标位置的方法
        private void CalcPos()
        {
            switch (screenAlignmentType)
            {
                case EAlignmentType.Up:
                    _rPos.x = Screen.width / 2 + _centerPos.x + pos.x;
                    _rPos.y = 0 + _centerPos.y + pos.y;
                    break;
                case EAlignmentType.Down:
                    _rPos.x = Screen.width / 2 + _centerPos.x + pos.x;
                    _rPos.y = Screen.height + _centerPos.y - pos.y;
                    break;
                case EAlignmentType.Left:
                    _rPos.x = _centerPos.x + pos.x;
                    _rPos.y = Screen.height / 2 + _centerPos.y + pos.y;
                    break;
                case EAlignmentType.Right:
                    _rPos.x = Screen.width + _centerPos.x - pos.x;
                    _rPos.y = Screen.height / 2 + _centerPos.y + pos.y;
                    break;
                case EAlignmentType.Center:
                    _rPos.x = Screen.width / 2 + _centerPos.x + pos.x;
                    _rPos.y = Screen.height / 2 + _centerPos.y + pos.y;
                    break;
                case EAlignmentType.LeftUp:
                    _rPos.x = _centerPos.x + pos.x;
                    _rPos.y = _centerPos.y + pos.y;
                    break;
                case EAlignmentType.LeftDown:
                    _rPos.x = _centerPos.x + pos.x;
                    _rPos.y = Screen.height + _centerPos.y - pos.y;
                    break;
                case EAlignmentType.RightUp:
                    _rPos.x = Screen.width + _centerPos.x - pos.x;
                    _rPos.y = _centerPos.y + pos.y;
                    break;
                case EAlignmentType.RightDown:
                    _rPos.x = Screen.width + _centerPos.x - pos.x;
                    _rPos.y = Screen.height + _centerPos.y - pos.y;
                    break;
            }
        }

        /// <summary>
        /// 得到 最终绘制的位置 和 宽高
        /// </summary>
        public Rect Pos
        {
            get
            {
                //进行计算
                //计算中心点偏移
                CalcCenterPos();
                //计算 相对屏幕坐标点
                CalcPos();
                //宽高直接赋值 返回给外部 别人直接使用来绘制控件
                _rPos.width = width;
                _rPos.height = height;
                return _rPos;
            }
        }
    }
}