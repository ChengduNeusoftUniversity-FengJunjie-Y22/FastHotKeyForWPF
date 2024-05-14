
namespace FastHotKeyForWPF
{
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
        public int RegisterID { get { return _registerid; } }

        private ModelKeys _model;
        public ModelKeys Model { get { return _model; } }

        private NormalKeys _normal;
        public NormalKeys Normal { get { return _normal; } }

        private FunctionTypes _functiontype;
        public FunctionTypes FunctionType { get { return _functiontype; } }

        private string _name = string.Empty;
        public string Name { get { return _name; } }

        public KeyInvoke_Void? FunctionVoid;
        public KeyInvoke_Return? FunctionReturn;

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

    }
}
