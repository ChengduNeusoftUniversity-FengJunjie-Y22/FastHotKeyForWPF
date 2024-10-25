using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FastHotKeyForWPF
{
    /// <summary>
    /// 一次继承即可使得用户控件具备类库控件同款效果
    /// <para>核心依赖属性:</para>
    /// <para>[ CurrentKeyA ] 热键左半部分,可以是Ctrl、Alt、Shift中的一个或多个</para>
    /// <para>[ CurrentKeyB ] 热键右半部分,可以是阿拉伯数字、大写字母、Fx中的一个</para>
    /// <para>[ Handler ] 热键处理事件</para>
    /// <para>非核心依赖属性:</para>
    /// <para>[ HotKeyText ] 热键文本表示</para>
    /// <para>[ ConnectText ] 连接左右按键文本的字符</para>
    /// <para>[ ErrorText ] 注册失败文本表示</para>
    /// </summary>
    public abstract class HotKeyControlBase : UserControl, IAutoHotKeyProperty, IAutoHotKeyUpdate
    {
        public int PoolID { get; set; } = 0;

        /// <summary>
        /// 目前是否已成功注册了热键
        /// </summary>
        public virtual bool IsHotKeyRegistered { get; protected set; } = false;
        /// <summary>
        /// 最近一次注册的热键ID
        /// </summary>
        public virtual int LastHotKeyID { get; protected set; } = -1;

        /// <summary>
        /// 注册失败时的文本
        /// </summary>  
        public virtual string ErrorText
        {
            get { return (string)GetValue(ErrorTextProperty); }
            set { SetValue(ErrorTextProperty, value); }
        }
        public static readonly DependencyProperty ErrorTextProperty =
            DependencyProperty.Register("ErrorText", typeof(string), typeof(HotKeyControlBase), new PropertyMetadata("Error"));

        /// <summary>
        /// 连接两边Key的文本
        /// </summary>
        public virtual string ConnectText
        {
            get { return (string)GetValue(ConnectTextProperty); }
            set { SetValue(ConnectTextProperty, value); }
        }
        public static readonly DependencyProperty ConnectTextProperty =
            DependencyProperty.Register("ConnectText", typeof(string), typeof(HotKeyControlBase), new PropertyMetadata(" + "));

        /// <summary>
        /// 热键文本显示
        /// </summary>
        public virtual string HotKeyText
        {
            get { return (string)GetValue(HotKeyTextProperty); }
            set { SetValue(HotKeyTextProperty, value); }
        }
        public static readonly DependencyProperty HotKeyTextProperty =
            DependencyProperty.Register("HotKeyText", typeof(string), typeof(HotKeyControlBase), new PropertyMetadata(string.Empty));

        /// <summary>
        /// 修饰键,例如Ctrl
        /// </summary>
        public virtual uint CurrentKeyA
        {
            get { return (uint)GetValue(CurrentKeyAProperty); }
            set { SetValue(CurrentKeyAProperty, value); }
        }
        public static readonly DependencyProperty CurrentKeyAProperty =
            DependencyProperty.Register("CurrentKeyA", typeof(uint), typeof(HotKeyControlBase), new PropertyMetadata(new uint(), OnCurrentKeyAChanged));
        public static void OnCurrentKeyAChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var target = (HotKeyControlBase)d;
            target?.RemoveOld();
            target?.UpdateText();
            target?.UpdateHotKey();
        }

        /// <summary>
        /// 按键,例如F1
        /// </summary>
        public virtual Key CurrentKeyB
        {
            get { return (Key)GetValue(CurrentKeyBProperty); }
            set { SetValue(CurrentKeyBProperty, value); }
        }
        public static readonly DependencyProperty CurrentKeyBProperty =
            DependencyProperty.Register("CurrentKeyB", typeof(Key), typeof(HotKeyControlBase), new PropertyMetadata(Key.None, OnCurrentKeyBChanged));
        public static void OnCurrentKeyBChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var target = (HotKeyControlBase)d;
            target?.RemoveOld();
            target?.UpdateText();
            target?.UpdateHotKey();
        }

        private event HotKeyEventHandler? Handlers;
        /// <summary>
        /// 热键处理事件
        /// </summary>
        public virtual event HotKeyEventHandler? Handler
        {
            add { Handlers += value; }
            remove { Handlers -= value; }
        }

        /// <summary>
        /// 去除原有注册项
        /// </summary>
        public virtual void RemoveOld()
        {
            GlobalHotKey.DeleteById(LastHotKeyID);
            IsHotKeyRegistered = false;
        }
        /// <summary>
        /// 更新热键
        /// </summary>
        public virtual void UpdateHotKey()
        {
            var temp = KeyHelper.IsKeyValid(CurrentKeyB);
            if (temp.Item2 == KeyTypes.NormalKey && Handlers != null)
            {
                var result = GlobalHotKey.Add(CurrentKeyA, KeyHelper.KeyToNormalKeys[CurrentKeyB], Handlers);
                if (result != -1)
                {
                    IsHotKeyRegistered = true;
                    LastHotKeyID = result;
                }
                else
                {
                    HotKeyText = ErrorText;
                }
            }
            RemoveSame();
        }
        /// <summary>
        /// 更新处理事件
        /// </summary>
        public virtual void UpdateHandler()
        {
            if (IsHotKeyRegistered)
            {
                var temp = KeyHelper.IsKeyValid(CurrentKeyB);
                if (temp.Item2 == KeyTypes.NormalKey && Handlers != null)
                {
                    GlobalHotKey.EditHandler(CurrentKeyA, KeyHelper.KeyToNormalKeys[CurrentKeyB], Handlers);
                }
            }
        }
        /// <summary>
        /// 更新文本
        /// </summary>
        public virtual void UpdateText()
        {
            List<ModelKeys> models = KeyHelper.UintSplit<List<ModelKeys>>(CurrentKeyA);
            bool pos1 = models.Contains(ModelKeys.ALT);
            bool pos2 = models.Contains(ModelKeys.CTRL);
            bool pos3 = models.Contains(ModelKeys.SHIFT);
            string Left = (pos1 ? "ALT" + (pos2 || pos3 ? ConnectText : string.Empty) : string.Empty)
                        + (pos2 ? "CTRL" + (pos3 ? ConnectText : string.Empty) : string.Empty)
                        + (pos3 ? "SHIFT" : string.Empty);

            string temp = CurrentKeyB.ToString();
            string Right = (temp.Length == 2 && temp[0] == 'D') ? temp[1].ToString() : ((temp == "None") ? string.Empty : temp);

            HotKeyText = Left + ((string.IsNullOrEmpty(Left) || string.IsNullOrEmpty(Right)) ? string.Empty : ConnectText) + Right;
        }
        /// <summary>
        /// 移除已存在相同热键
        /// </summary>
        public virtual void RemoveSame()
        {
            BoxPool.RemoveSame(this);
        }
    }
}
