using UnityEngine;

namespace CustomGUI.Control
{
    [ExecuteAlways]
    public class CustomGUIToggleGroup : MonoBehaviour
    {
        public CustomGUIToggle[] toggles;

        //记录上一次为true的 toggle
        private CustomGUIToggle _frontTrueToggle;

        void Start()
        {
            if (toggles.Length == 0)
                return;

            //通过遍历 来为多个 多选框 添加 监听事件函数
            //在函数中做 处理 
            //当一个为true时 另外两个变成false
            for (int i = 0; i < toggles.Length; i++)
            {
                CustomGUIToggle toggle = toggles[i];
                toggle.ChangeValue += (value) =>
                {
                    //当传入的 value  是ture时 需要把另外的toggle 
                    //变成false
                    if( value )
                    {
                        //意味其他要变成false
                        foreach (var t in toggles)
                        {
                            //这里有闭包  toggle就是上一个函数中申明的变量
                            //改变了它的生命周期
                            if( t != toggle )
                            {
                                t.isSel = false;
                            }
                        }
                        //记录上一次为true的toggle
                        _frontTrueToggle = toggle;
                    }
                    //来判断 当前变成false的这个toggle是不是上一次为true
                    //如果是 就不应该让它变成false
                    else if( toggle == _frontTrueToggle)
                    {
                        //强制改成 true
                        toggle.isSel = true;
                    }
                };
            }
        }

    }
}
