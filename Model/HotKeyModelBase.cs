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
    /// 若控件用于处理热键增删改,可使用这个数据模型基类
    /// </summary>
    public abstract class HotKeyModelBase : IAutoHotKey, IAutoHotKeyProperty
    {
        public int PoolID { get; set; } = 0;

        public uint CurrentKeyA { get; set; } = new uint();

        public Key CurrentKeyB { get; set; }

        public HotKeyEventHandler? HandlerData { get; set; }

        public HotKeyEventHandler? Handler { get; set; }
    }
}
