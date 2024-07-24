using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FastHotKeyForWPF
{
    /// <summary>
    /// Model层、ViewModel层、View层 都必须实现此接口
    /// </summary>
    public interface IAutoHotKeyProperty : IAutoHotKey
    {
        /// <summary>
        /// 热键的触发Key之一 , 可以是 CTRL / ALT
        /// </summary>
        Key CurrentKeyA { get; set; }

        /// <summary>
        /// 热键的触发Key之一 , 可以是 数字 / 字母 / 方向键 / Fx键
        /// </summary>
        Key CurrentKeyB { get; set; }

        /// <summary>
        /// 用于注册热键的委托 , 通常不建议手动修改
        /// </summary>
        HotKeyEventHandler? HandlerData { get; set; }

        /// <summary>
        /// 用于Xaml的事件 , 在前端直接指定处理函数
        /// </summary>
        event HotKeyEventHandler? Handler { add { } remove { } }
    }
}
