using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace FastHotKeyForWPF
{
    /// <summary>
    /// 抽象类型，继承自TextBox并实现Component接口
    /// <para>热键设置中，接收用户按下键的组件是基于这个做下去的</para>
    /// </summary>
    public abstract class KeyBox : TextBox, Component
    {
        /// <summary>
        /// KeySelectBox组件是否处于保护中（公共）
        /// </summary>
        internal static bool IsKeySelectBoxProtected = false;
        internal static List<KeySelectBox> keySelectBoxes = new List<KeySelectBox>();

        /// <summary>
        /// KeysSelectBox组件是否处于保护中（公共）
        /// </summary>
        internal static bool IsKeysSelectBoxProtected = false;
        internal static List<KeysSelectBox> keysSelectBoxes = new List<KeysSelectBox>();

        /// <summary>
        /// 是否被保护（独立）
        /// </summary>
        internal bool Protected = false;

        /// <summary>
        /// 是否启用默认变色效果（独立）
        /// </summary>
        public bool IsDefaultColorChange = true;

        /// <summary>
        /// 该组件负责管理的事件之一
        /// </summary>
        internal KeyInvoke_Void? Event_void;
        /// <summary>
        /// 该组件负责管理的事件之一
        /// </summary>
        internal KeyInvoke_Return? Event_return;

        /// <summary>
        /// 获取焦点时的行为
        /// </summary>
        internal TextBoxFocusChange? Focused;
        /// <summary>
        /// 失去焦点时的行为
        /// </summary>
        internal TextBoxFocusChange? UnFocused;

        /// <summary>
        /// 应用父容器的尺寸，并自动调节字体大小
        /// </summary>
        /// <typeparam name="T">父容器类型</typeparam>
        public void UseFatherSize<T>() where T : UIElement
        {
            T? father = Parent as T;
            if (father == null) { return; }

            PropertyInfo? widthProperty = typeof(T).GetProperty("Width");
            PropertyInfo? heightProperty = typeof(T).GetProperty("Height");
            if (widthProperty == null) { return; }
            if (heightProperty == null) { return; }

            object? width = widthProperty.GetValue(father);
            object? height = heightProperty.GetValue(father);
            if (width == null) { return; }
            if (height == null) { return; }

            Width = (double)width;
            Height = (double)height;
            FontSize = (double)height * 0.8;
        }

        /// <summary>
        /// 应用资源样式中的全部属性
        /// </summary>
        /// <param name="styleName">资源样式的Key</param>
        public void UseStyleProperty(string styleName)
        {
            Style? style = (Style)TryFindResource(styleName);
            if (style == null) return;

            if (style.TargetType == typeof(TextBox))
            {
                foreach (SetterBase setterBase in style.Setters)
                {
                    if (setterBase is Setter setter)
                    {
                        SetValue(setter.Property, setter.Value);
                    }
                }
            }
        }

        /// <summary>
        /// 应用资源样式中，指定名称的属性
        /// </summary>
        /// <param name="styleName">资源样式的Key</param>
        /// <param name="targetProperties">属性名</param>
        public void UseStyleProperty(string styleName, string[] targetProperties)
        {
            Style? style = (Style)TryFindResource(styleName);
            if (style == null) return;

            if (style.TargetType == typeof(TextBox))
            {
                foreach (string target in targetProperties)
                {
                    Setter? targetSetter = style.Setters.FirstOrDefault(s => ((Setter)s).Property.Name == target) as Setter;
                    if (targetSetter != null)
                    {
                        SetValue(targetSetter.Property, targetSetter.Value);
                    }
                }
            }
        }

        /// <summary>
        /// 使用你自定义的焦点变色函数，这些函数必须带有一个TextBox参数定义
        /// </summary>
        /// <param name="enter">获取焦点时</param>
        /// <param name="leave">失去焦点时</param>
        public void UseFocusTrigger(TextBoxFocusChange enter, TextBoxFocusChange leave)
        {
            Focused = null;
            UnFocused = null;
            Focused = enter;
            UnFocused = leave;
            IsDefaultColorChange = false;
        }

        internal void WhileMouseEnter(object sender, MouseEventArgs e)
        {
            Focus();
            if (IsDefaultColorChange)
            {
                Background = Brushes.Black;
                Foreground = Brushes.Cyan;
            }
            else
            {
                if (Focused != null) Focused.Invoke(this);
            }
        }

        internal void WhileMouseLeave(object sender, MouseEventArgs e)
        {
            Keyboard.ClearFocus();
            if (IsDefaultColorChange)
            {
                Background = Brushes.Wheat;
                Foreground = Brushes.Black;
            }
            else
            {
                if (UnFocused != null) UnFocused.Invoke(this);
            }
        }

        /// <summary>
        /// 将此对象单独设为保护
        /// </summary>
        public void Protect()
        {
            Protected = true;
        }

        public void UnProtect()
        {
            Protected = false;
        }
    }
}
