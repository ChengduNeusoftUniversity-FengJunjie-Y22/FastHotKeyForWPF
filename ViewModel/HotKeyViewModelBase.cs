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
        public string Text
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

        public Key CurrentKeyA
        {
            get => _model.CurrentKeyA;
            set
            {
                if (value != _model.CurrentKeyA)
                {
                    _model.CurrentKeyA = value;

                    UpdateText();
                    UpdateHotKey();

                    OnPropertyChanged(nameof(CurrentKeyA));
                }
            }
        }

        public Key CurrentKeyB
        {
            get => _model.CurrentKeyB;
            set
            {
                if (value != _model.CurrentKeyB)
                {
                    _model.CurrentKeyB = value;

                    UpdateText();
                    UpdateHotKey();

                    OnPropertyChanged(nameof(CurrentKeyB));
                }
            }
        }

        public HotKeyEventHandler? HandlerData { get; set; }

        public HotKeyEventHandler? Handler
        {
            set
            {
                _model.Handler = value;
                if (IsHotKeyRegistered)
                {
                    var result = KeyHelper.GetKeysFrom(this);
                    if (result.Item1 && value != null)
                    {
                        GlobalHotKey.EditHandler(result.Item2, result.Item3, value);
                        LastHotKeyID = GlobalHotKey.Registers[result.Item2, result.Item3];
                    }
                }
                OnPropertyChanged(nameof(Handler));
            }
            get => _model.Handler;
        }

        public bool IsHotKeyRegistered
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

        internal int LastHotKeyID
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

        public virtual void UpdateHotKey()
        {
            var Keys = KeyHelper.GetKeysFrom(this);
            if (Keys.Item1 && Handler != null)
            {
                var result = GlobalHotKey.Add(Keys.Item2, Keys.Item3, Handler);
                if (!result.Item1)
                {
                    Text = "Error";
                }
            }
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
