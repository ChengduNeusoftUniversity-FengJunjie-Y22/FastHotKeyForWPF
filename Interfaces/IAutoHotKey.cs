using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastHotKeyForWPF
{
    /// <summary>
    /// 所有AutoHotKey接口的基接口
    /// </summary>
    public interface IAutoHotKey
    {
        /// <summary>
        /// 在 控件池 中的唯一标识符
        /// </summary>
        int PoolID { get; set; }
    }
}
