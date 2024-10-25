
namespace FastHotKeyForWPF
{
    /// <summary>
    /// 注册信息
    /// </summary>
    public class RegisterInfo
    {
        internal RegisterInfo() { }

        public RegisterInfo(int id, object model, uint key, HotKeyEventHandler? handler)
        {
            RegisterID = id;
            ModelKey = KeyHelper.ValueToUint(model);
            NormalKey = key;
            Handler = handler;
        }

        /// <summary>
        /// [ 注册ID ] 默认-1，表示一个失败的注册消息
        /// </summary>
        public int RegisterID { get; internal set; } = -1;

        /// <summary>
        /// 系统按键
        /// </summary>
        public uint ModelKey { get; internal set; }

        /// <summary>
        /// 普通按键
        /// </summary>
        public uint NormalKey { get; internal set; }

        /// <summary>
        /// 处理事件
        /// </summary>
        public HotKeyEventHandler? Handler { get; internal set; }
    }
}
