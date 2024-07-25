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

        public virtual Key CurrentKeyA
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
                    var result = KeyHelper.GetKeysFrom(this);
                    if (result.Item1 && value != null)
                    {
                        GlobalHotKey.EditHandler(result.Item2, result.Item3, value);
                        LastHotKeyID = GlobalHotKey.Registers[result.Item2, result.Item3].RegisterID;
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
        /// 去除原有注册项
        /// </summary>
        public virtual void RemoveOld()
        {
            GlobalHotKey.DeleteById(LastHotKeyID);
            IsHotKeyRegistered = false;
        }

        public virtual void UpdateHotKey()
        {
            var Keys = KeyHelper.GetKeysFrom(this);
            if (Keys.Item1 && HandlerData != null)
            {
                var result = GlobalHotKey.Add(Keys.Item2, Keys.Item3, HandlerData);
                if (result.Item1)
                {
                    IsHotKeyRegistered = true;
                    LastHotKeyID = result.Item2;
                }
                else
                {
                    Text = "Error";
                }
            }
            RemoveSame();
        }

        public virtual void UpdateText()
        {
            Text = CurrentKeyA.ToString() + "  +  " + CurrentKeyB.ToString();
        }

        public virtual void RemoveSame()
        {
            BoxPool.RemoveSame(this);
        }
    }
}
