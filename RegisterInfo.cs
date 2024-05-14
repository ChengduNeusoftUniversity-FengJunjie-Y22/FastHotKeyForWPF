
namespace FastHotKeyForWPF
{
    /// <summary>
    /// 注册信息，由一个注册编号\一个键盘组合\一个函数签名构成
    /// </summary>
    public class RegisterInfo
    {
        public RegisterInfo(int id, ModelKeys model, NormalKeys key, KeyInvoke_Void work)
        {
            _registerid = id;
            _model = model;
            _normal = key;
            _name = work.Method.Name;
            _functiontype = FunctionTypes.Void;
            FunctionVoid = work;
        }
        public RegisterInfo(int id, ModelKeys model, NormalKeys key, KeyInvoke_Return work)
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
        /// 注册编号，默认为2004开始累加
        /// </summary>
        public int RegisterID { get { return _registerid; } }

        private ModelKeys _model;
        /// <summary>
        /// CTRL\ALT特殊按键
        /// </summary>
        public ModelKeys Model { get { return _model; } }

        private NormalKeys _normal;
        /// <summary>
        /// 非CTRL\ALT的普通按键
        /// </summary>
        public NormalKeys Normal { get { return _normal; } }

        private FunctionTypes _functiontype;
        /// <summary>
        /// 函数的类型（主要是有无返回值的区别）
        /// </summary>
        public FunctionTypes FunctionType { get { return _functiontype; } }

        private string _name = string.Empty;
        /// <summary>
        /// 函数签名,初始化的时候就决定了
        /// </summary>
        public string Name { get { return _name; } }

        public KeyInvoke_Void? FunctionVoid;
        public KeyInvoke_Return? FunctionReturn;

        /// <summary>
        /// 成功注册的热键才会存在于列表中，于是可以直接调用此方法打印具体信息
        /// </summary>
        /// <returns></returns>
        public string SuccessRegistration()
        {
            string result = string.Empty;

            result += "【成功注册的热键√】\n";
            result += $"注册编号: {RegisterID}\n";
            result += $"触发组合: {Model} + {Normal}\n";
            result += $"函数类型: {FunctionType}型\n";
            result += $"函数签名: {Name}";

            return result;
        }

        public static string LoseRegistration(ModelKeys modelKeys, NormalKeys normalKeys, KeyInvoke_Void work)
        {
            string result = string.Empty;

            result += "【⚠注册失败】\n";
            result += $"触发组合: {modelKeys} + {normalKeys}\n";
            result += $"函数类型: {FunctionTypes.Void}型\n";
            result += $"函数签名: {work.Method.Name}";

            return result;
        }
        public static string LoseRegistration(ModelKeys modelKeys, NormalKeys normalKeys, KeyInvoke_Return work)
        {
            string result = string.Empty;

            result += "【⚠注册失败】\n";
            result += $"触发组合: {modelKeys} + {normalKeys}\n";
            result += $"函数类型: {FunctionTypes.Return}型\n";
            result += $"函数签名: {work.Method.Name}";

            return result;
        }
        //一般是便于DeBug时打印失败消息
    }
}
