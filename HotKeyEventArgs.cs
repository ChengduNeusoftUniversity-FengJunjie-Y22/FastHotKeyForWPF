using FastHotKeyForWPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 热键的处理函数
/// </summary>
public delegate void HotKeyEventHandler(object sender, HotKeyEventArgs e);

namespace FastHotKeyForWPF
{
    /// <summary>
    /// 热键触发过程中的信息传递
    /// </summary>
    public class HotKeyEventArgs : EventArgs
    {
        public HotKeyEventArgs() { }

        /// <summary>
        /// 热键信息
        /// </summary>
        public RegisterInfo RegisterInfo { get; set; } = new RegisterInfo();
    }
}
