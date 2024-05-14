using System.Windows.Controls;
using System.Windows.Input;

namespace FastHotKeyForWPF
{
    /// <summary>
    /// 组件☆
    /// <para>功能 接收用户按下的两个按键，激活热键的全自动管理</para>
    /// <para>继承 TextBox类</para>
    /// <para>实现 Component接口</para>
    /// </summary>
    public class KeysSelectBox : KeyBox
    {
        Key key1;
        Key key2;

        public bool Protected = false;

        internal KeysSelectBox()
        {

        }

        private void WhileKeysDown(object sender, KeyEventArgs e)
        {

        }
    }
}
