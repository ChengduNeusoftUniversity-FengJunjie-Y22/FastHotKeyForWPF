
namespace FastHotKeyForWPF
{
    /// <summary>
    /// 注册信息，由一个注册编号\一个键盘组合\一个函数签名构成
    /// </summary>
    public class RegisterInfo
    {
        internal RegisterInfo(int id, ModelKeys model, NormalKeys key, KeyInvoke_Void work)
        {
            _registerid = id;
            _model = model;
            _normal = key;
            _name = work.Method.Name;
            _functiontype = FunctionTypes.Void;
            FunctionVoid = work;
        }
        internal RegisterInfo(int id, ModelKeys model, NormalKeys key, KeyInvoke_Return work)
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
        public int RegisterID { get { return _registerid; } }

        private ModelKeys _model;
        /// <summary>
        /// 系统按键
        /// </summary>
        public ModelKeys Model { get { return _model; } }

        private NormalKeys _normal;
        /// <summary>
        /// 普通按键
        /// </summary>
        public NormalKeys Normal { get { return _normal; } }

        private FunctionTypes _functiontype;
        /// <summary>
        /// 函数的类型（主要是有无返回值的区别）
        /// </summary>
        public FunctionTypes FunctionType { get { return _functiontype; } }

        private string _name = string.Empty;
        /// <summary>
        /// 函数签名,初始化时决定
        /// </summary>
        public string Name { get { return _name; } }

        /// <summary>
        /// 热键可能对应的处理函数
        /// </summary>
        public KeyInvoke_Void? FunctionVoid { internal set; get; } = null;

        /// <summary>
        /// 热键可能对应的处理函数
        /// </summary>
        public KeyInvoke_Return? FunctionReturn { internal set; get; } = null;

    }
}
