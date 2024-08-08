using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows;

namespace FastHotKeyForWPF
{
    /// <summary>
    /// 为 HotKey用户控件 的设计提供便利
    /// </summary>
    public abstract class HotKeyViewModelBase : ViewModelBase, IAutoHotKeyUpdate, IAutoHotKeyProperty
    {
        public HotKeyBoxModel _model = new HotKeyBoxModel();

        public int PoolID { get; set; }

        /// <summary>
        /// 控件中的文本
        /// </summary>
        public virtual string Text
        {
            get => _model.Text;
            set
            {
                if (_model.Text != value)
                {
                    _model.Text = value;
                    OnPropertyChanged(nameof(Text));
                }
            }
        }

        public virtual uint CurrentKeyA
        {
            get => _model.CurrentKeyA;
            set
            {
                RemoveOld();

                _model.CurrentKeyA = value;

                UpdateText();
                UpdateHotKey();

                OnPropertyChanged(nameof(CurrentKeyA));
            }
        }

        public virtual Key CurrentKeyB
        {
            get => _model.CurrentKeyB;
            set
            {
                RemoveOld();

                _model.CurrentKeyB = value;

                UpdateText();
                UpdateHotKey();

                OnPropertyChanged(nameof(CurrentKeyB));
            }
        }

        public HotKeyEventHandler? HandlerData
        {
            get => _model.HandlerData;
            set
            {
                _model.HandlerData = value;
                if (IsHotKeyRegistered)
                {
                    var temp = KeyHelper.IsKeyValid(CurrentKeyB);
                    if (temp.Item2 == KeyTypes.NormalKey && value != null)
                    {
                        GlobalHotKey.EditHandler(CurrentKeyA, KeyHelper.KeyToNormalKeys[CurrentKeyB], value);
                    }
                }
                OnPropertyChanged(nameof(HandlerData));
            }
        }

        /// <summary>
        /// 目前是否已成功注册了热键
        /// </summary>
        public virtual bool IsHotKeyRegistered
        {
            get => _model.IsHotKeyRegistered;
            set
            {
                if (value != _model.IsHotKeyRegistered)
                {
                    _model.IsHotKeyRegistered = value;
                    OnPropertyChanged(nameof(IsHotKeyRegistered));
                }
            }
        }

        /// <summary>
        /// 最近一次注册的热键ID
        /// </summary>
        public virtual int LastHotKeyID
        {
            get => _model.LastHotKeyID;
            set
            {
                if (value != _model.LastHotKeyID)
                {
                    _model.LastHotKeyID = value;
                    OnPropertyChanged(nameof(LastHotKeyID));
                }
            }
        }

        /// <summary>
        /// 注册失败时的文本
        /// </summary>
        public string ErrorText
        {
            get => _model.ErrorText;
            set
            {
                if (_model.ErrorText != value)
                {
                    _model.ErrorText = value;
                    OnPropertyChanged(nameof(ErrorText));
                }
            }
        }

        /// <summary>
        /// 连接两个Key的文本
        /// </summary>
        public string ConnectText
        {
            get => _model.ConnectText;
            set
            {
                if (value != _model.ConnectText)
                {
                    _model.ConnectText = value;
                    OnPropertyChanged(nameof(ConnectText));
                }
            }
        }

        /// <summary>
        /// 去除原有注册项
        /// </summary>
        public virtual void RemoveOld()
        {
            GlobalHotKey.DeleteById(LastHotKeyID);
            IsHotKeyRegistered = false;
        }

        public virtual void UpdateHotKey()
        {
            var temp = KeyHelper.IsKeyValid(CurrentKeyB);
            if (temp.Item2 == KeyTypes.NormalKey && HandlerData != null)
            {
                var result = GlobalHotKey.Add(CurrentKeyA, KeyHelper.KeyToNormalKeys[CurrentKeyB], HandlerData);
                if (result != -1)
                {
                    IsHotKeyRegistered = true;
                    LastHotKeyID = result;
                }
                else
                {
                    Text = ErrorText;
                }
            }
            RemoveSame();
        }

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

            Text = Left + ((string.IsNullOrEmpty(Left) || string.IsNullOrEmpty(Right)) ? string.Empty : ConnectText) + Right;
        }

        public virtual void RemoveSame()
        {
            BoxPool.RemoveSame(this);
        }
    }
}
