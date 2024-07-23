
namespace FastHotKeyForWPF
{
    /// <summary>
    /// 注册信息，由一个注册编号\一个键盘组合\一个函数签名构成
    /// </summary>
    public class RegisterInfo
    {
        internal RegisterInfo(int id, uint model, uint key, Action work)
        {
            _registerid = id;
            _model = model;
            _normal = key;
            _name = work.Method.Name;
            _functiontype = FunctionTypes.Void;
            FunctionVoid = work;
        }
        internal RegisterInfo(int id, uint model, uint key, Func<object> work)
        {
            _registerid = id;
            _model = model;
            _normal = key;
            _name = work.Method.Name;
            _functiontype = FunctionTypes.Return;
            FunctionReturn = work;
        }

        private int _registerid = 2004;
        /// <summary>
        /// 注册编号，默认从2004开始累加
        /// </summary>
        public int RegisterID => _registerid;

        private uint _model;
        /// <summary>
        /// 系统按键
        /// </summary>
        public uint Model => _model;

        private uint _normal;
        /// <summary>
        /// 普通按键
        /// </summary>
        public uint Normal => _normal;

        private FunctionTypes _functiontype;
        /// <summary>
        /// 函数的类型（主要是有无返回值的区别）
        /// </summary>
        public FunctionTypes FunctionType => _functiontype;

        private string _name = string.Empty;
        /// <summary>
        /// 函数签名,初始化时决定
        /// </summary>
        public string Name => _name;

        /// <summary>
        /// 热键可能对应的处理函数
        /// </summary>
        public Action? FunctionVoid { internal set; get; } = null;

        /// <summary>
        /// 热键可能对应的处理函数
        /// </summary>
        public Func<object>? FunctionReturn { internal set; get; } = null;

    }
}
