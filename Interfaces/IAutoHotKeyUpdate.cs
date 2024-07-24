using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastHotKeyForWPF
{
    /// <summary>
    /// ViewModel层 必须实现此接口
    /// </summary>
    public interface IAutoHotKeyUpdate : IAutoHotKey
    {
        /// <summary>
        /// 必须包含 热键的注册（更新）逻辑
        /// </summary>
        void UpdateHotKey();

        /// <summary>
        /// 必须包含 ViewModel层 Text 数据更新逻辑
        /// </summary>
        void UpdateText();

        /// <summary>
        /// 必须包含 从BoxPool中移除已有重复热键逻辑
        /// </summary>
        void RemoveSame();
    }
}
