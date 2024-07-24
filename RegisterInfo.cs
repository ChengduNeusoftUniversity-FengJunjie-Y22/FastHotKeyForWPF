
namespace FastHotKeyForWPF
{
    /// <summary>
    /// 注册信息
    /// </summary>
    public class RegisterInfo
    {
        internal RegisterInfo() { }

        /// <summary>
        /// 初始化后,外部不允许修改各项属性值
        /// </summary>
        public RegisterInfo(int id, ModelKeys model, NormalKeys key, HotKeyEventHandler handler)
        {
            RegisterID = id;
            ModelKey = model;
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
        public ModelKeys ModelKey { get; internal set; }

        /// <summary>
        /// 普通按键
        /// </summary>
        public NormalKeys NormalKey { get; internal set; }

        /// <summary>
        /// 热键的处理者
        /// </summary>
        public HotKeyEventHandler? Handler { get; internal set; } = null;
    }
}
