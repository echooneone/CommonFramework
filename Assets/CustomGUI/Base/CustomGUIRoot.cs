using UnityEngine;

namespace CustomGUI.Base
{
    [ExecuteAlways]
    public class CustomGUIRoot : MonoBehaviour
    {
        //用于存储 子对象 所有GUI控件的容器
        private CustomGUIControl[] _allControls;
        void Start()
        {
            _allControls = GetComponentsInChildren<CustomGUIControl>();
        }
        //在这同一绘制子对象控件的内容
        private void OnGUI()
        {
            //通过每一次绘制之前 得到所有子对象控件的 父类脚本
            //这句代码 浪费性能 因为每次 gui都会来获取所有的 控件对应的脚本
            //编辑状态下 才会一直执行
            if( !Application.isPlaying )
            {
                _allControls = GetComponentsInChildren<CustomGUIControl>();
            }
            //遍历每一个控件 让其 执行绘制
            for (int i = 0; i < _allControls.Length; i++)
            {
                _allControls[i].DrawGUI();
            }
        }
    }
}