using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;

public enum NormalKeys : uint
{
    F1 = 0x70,
    F2 = 0x71,
    F3 = 0x72,
    F4 = 0x73,
    F5 = 0x74,
    F6 = 0x75,
    F7 = 0x76,

    F9 = 0x78,
    F10 = 0x79,
    F11 = 0x7A,
    F12 = 0x7B,

    LEFT = 0x25,
    UP = 0x26,
    RIGHT = 0x27,
    DOWN = 0x28,

    Zero = 0x30,
    One = 0x31,
    Two = 0x32,
    Three = 0x33,
    Four = 0x34,
    Five = 0x35,
    Six = 0x36,
    Seven = 0x37,
    Eight = 0x38,
    Nine = 0x39,

    SPACE = 0x20,

    A = 0x41,
    B = 0x42,
    C = 0x43,
    D = 0x44,
    E = 0x45,
    F = 0x46,
    G = 0x47,
    H = 0x48,
    I = 0x49,
    J = 0x4A,
    K = 0x4B,
    L = 0x4C,
    M = 0x4D,
    N = 0x4E,
    O = 0x4F,
    P = 0x50,
    Q = 0x51,
    R = 0x52,
    S = 0x53,
    T = 0x54,
    U = 0x55,
    V = 0x56,
    W = 0x57,
    X = 0x58,
    Y = 0x59,
    Z = 0x5A
}

public enum ModelKeys : uint
{
    ALT = 0x0001,
    CTRL = 0x0002,
}

public enum FunctionTypes
{
    Void,
    Return,
    Focus
}

/// <summary>
/// 热键处理函数类型一
/// </summary>
public delegate void KeyInvoke_Void();

/// <summary>
/// 热键处理函数类型二
/// </summary>
/// <returns>object实例对象，你可以用它做进一步处理</returns>
public delegate object KeyInvoke_Return();

/// <summary>
/// TextBox焦点变色事件
/// </summary>
/// <param name="box">需要做出变动的TextBox,一般指继承自它的其它Box</param>
public delegate void TextBoxChange(object sender);

namespace FastHotKeyForWPF
{
    public class GlobalHotKey
    {
        private static GlobalHotKey? Instance;

        private GlobalHotKey() { }

        /// <summary>
        /// 关联函数，将热键的相关消息与窗口句柄做绑定
        /// </summary>
        /// <param name="hWnd">窗口句柄</param>
        /// <param name="id">快捷键的编号</param>
        /// <param name="fsModifiers">控制模块，例如CTRL和ALT</param>
        /// <param name="vk">按键码</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        internal static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        /// <summary>
        /// 取关函数，解除热键相关消息和窗口句柄间的绑定
        /// </summary>
        /// <param name="hWnd">窗口句柄</param>
        /// <param name="id">快捷键的编号</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        internal static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        public static bool IsDeBug = false;//调试开关

        public static bool IsUpdate = true;//实时传递开关

        public const int WM_HOTKEY = 0x0312; //表示Windows有热键被按下

        public static int HOTKEY_ID = 2004; //从此节点开始注册热键

        private static object? _returnvalue = null;
        public static object? ReturnValue //获取最新接收到的处理函数的返回值
        {
            get { return _returnvalue; }
            set
            {
                _returnvalue = value;
                if (IsDeBug)
                {
                    MessageBox.Show($"发生一次返回值的传递【{_returnvalue}】→【{_returnvalue}】");
                }
                if (IsUpdate)
                {
                    BindingRef.Update(_returnvalue);
                }
            }
        }

        public static List<RegisterInfo>? Registers//获取注册信息表
        {
            get
            {
                if (Instance != null)
                {
                    return Instance.RegisterList;
                }
                if (IsDeBug)//调试模式下，尝试读取注册信息会抛出异常
                {
                    throw new Exception("你无法在未激活的状态下读取注册信息！");
                }
                return new List<RegisterInfo>();//非调试模式下，将尽可能避免程序退出
            }
        }
        public static List<Tuple<ModelKeys, NormalKeys>>? ProtectedHotKeys//获取保护名单
        {
            get
            {
                if (Instance == null)
                {
                    return null;
                }
                else
                {
                    return Instance.ProtectList;
                }
            }
        }

        public static void Awake()//激活
        {
            if (Instance == null)
            {
                Window mainWindow = Application.Current.MainWindow;
                if (mainWindow != null)
                {
                    Instance = new GlobalHotKey();
                    Instance.WindowhWnd = new WindowInteropHelper(mainWindow).Handle;
                    Instance.Install();
                    BindingRef.Awake();
                }
                else
                {
                    MessageBox.Show("激活失败，未能找到主窗口！");
                }
            }
            else
            {
                MessageBox.Show("请不要重复激活操作！");
            }
        }
        public static void Destroy()//销毁
        {
            if (Instance != null)
            {
                BindingRef.Destroy();
                Instance.Uninstall();
                Instance.RemoveHotKeys();
                Instance = null;
            }
        }

        public static (bool, string) Add(ModelKeys mode, NormalKeys key, KeyInvoke_Void work)//注册
        {
            if (Instance != null)
            {
                return Instance.AddHotKey_Void(mode, key, work);
            }
            return (false, "热键功能尚未激活，请参考文档。");
        }
        public static (bool, string) Add(ModelKeys mode, NormalKeys key, KeyInvoke_Return work)//注册
        {
            if (Instance != null)
            {
                return Instance.AddHotKey_Return(mode, key, work);
            }
            return (false, "热键功能尚未激活，请参考文档。");
        }

        public static void EditHotKey_Function(ModelKeys mode, NormalKeys key, KeyInvoke_Void work)//修改触发函数
        {
            if (Instance != null)
            {
                Instance.EditHotKey_NewFunctionVoid(mode, key, work);
            }
        }
        public static void EditHotKey_Function(ModelKeys mode, NormalKeys key, KeyInvoke_Return work)//修改触发函数
        {
            if (Instance != null)
            {
                Instance.EditHotKey_NewFunctionReturn(mode, key, work);
            }
        }
        public static void EditHotKey_Keys(KeyInvoke_Void work, ModelKeys mode, NormalKeys key)//修改快捷键
        {
            if (Instance != null)
            {
                Instance.EditHotKey_NewKeysVoid(work, mode, key);
            }
        }
        public static void EditHotKey_Keys(KeyInvoke_Return work, ModelKeys mode, NormalKeys key)//修改快捷键
        {
            if (Instance != null)
            {
                Instance.EditHotKey_NewKeysReturn(work, mode, key);
            }
        }

        public static void Clear()//清空热键，但不卸载钩子
        {
            if (Instance != null)
            {
                Instance.RemoveHotKeys();
            }
        }
        public static void DeleteById(int id)//删除指定编号的热键
        {
            if (Instance != null)
            {
                Instance.RemoveExistRegisterByID(id);
            }
        }
        public static void DeleteByFunction(KeyInvoke_Void work)//清除一个函数的所有触发快捷键
        {
            if (Instance != null)
            {
                Instance.RemoveExistRegisterByFunction_Void(work);
            }
        }
        public static void DeleteByFunction(KeyInvoke_Return work)//清除一个函数的所有触发快捷键
        {
            if (Instance != null)
            {
                Instance.RemoveExistRegisterByFunction_Return(work);
            }
        }
        public static void DeleteByKeys(ModelKeys modelKeys, NormalKeys normalKeys)//依据组合键移除热键
        {
            if (Instance != null)
            {
                Instance.RemoveExistRegisterByKeys(modelKeys, normalKeys);
            }
        }

        public static void ProtectHotKeyByKeys(ModelKeys modelKeys, NormalKeys normalKeys)//增加受保护的热键
        {
            Instance?.AddProtectedHotKey(modelKeys, normalKeys);
        }
        public static void ProtectHotKeyById(int id)//增加受保护的热键
        {
            Instance?.AddProtectedHotKey(id);
        }
        public static void UnProtectHotKeyByKeys(ModelKeys modelKeys, NormalKeys normalKeys)//删除受保护的热键
        {
            Instance?.RemoveProtectedHotKey(modelKeys, normalKeys);
        }
        public static void UnProtectHotKeyById(int id)//删除受保护的热键
        {
            Instance?.RemoveProtectedHotKey(id);
        }
        public static bool IsHotKeyProtected(ModelKeys modelKeys, NormalKeys normalKeys)//仅检查是否处于保护态
        {
            bool result = false;

            if (Instance != null)
            {
                result = Instance.CheckInProtectList(modelKeys, normalKeys);
            }

            return result;
        }
        public static bool IsHotKeyProtected(int id)//仅检查是否处于保护态
        {
            bool result = false;

            if (Instance != null)
            {
                result = Instance.CheckInProtectList(id);
            }

            return result;
        }


        #region 动态资源
        IntPtr WindowhWnd; //主窗体句柄☆

        private int Counter = 0; //计数器，表示历史中一共注册过多少个热键

        private Dictionary<int, KeyInvoke_Void> Trigger_Void = new Dictionary<int, KeyInvoke_Void>();//处理函数映射表

        private Dictionary<int, KeyInvoke_Return> Trigger_Return = new Dictionary<int, KeyInvoke_Return>();//处理函数映射表

        private List<RegisterInfo> RegisterList = new List<RegisterInfo>();//注册在列的热键

        private List<Tuple<ModelKeys, NormalKeys>> ProtectList = new List<Tuple<ModelKeys, NormalKeys>>();//受保护的热键名单

        /// <summary>
        /// 系统消息处理函数
        /// </summary>
        /// <param name="hwnd">主窗口的句柄</param>
        /// <param name="msg">系统发送的消息</param>
        /// <param name="wParam">被触发的热键所对应的ID</param>
        /// <param name="lParam">与键鼠相关的额外信息</param>
        /// <param name="handled">对应事件是否已处理完毕</param>
        /// <returns></returns>
        private IntPtr WhileKeyInvoked(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {
                case WM_HOTKEY:
                    int id = wParam.ToInt32();
                    try
                    {
                        Trigger_Void[id].Invoke();
                    }
                    catch (KeyNotFoundException)
                    {
                        try
                        {
                            ReturnValue = Trigger_Return[id].Invoke();
                        }
                        catch (KeyNotFoundException)
                        {
                            MessageBox.Show("未能找到对应的事件处理函数！");
                        }
                    }
                    handled = true;
                    break;

            }
            return IntPtr.Zero;
        }

        private void Install()//安装键盘钩子
        {
            HwndSource source = HwndSource.FromHwnd(WindowhWnd);
            source.AddHook(new HwndSourceHook(WhileKeyInvoked));
        }

        private void Uninstall()//卸载键盘钩子
        {
            HwndSource source = HwndSource.FromHwnd(WindowhWnd);
            source.RemoveHook(new HwndSourceHook(WhileKeyInvoked));
        }

        /// <summary>
        /// 热键注册函数【void型处理函数】
        /// </summary>
        /// <param name="mode">修饰键</param>
        /// <param name="key">一般按键</param>
        /// <param name="work">处理函数的函数名</param>
        /// <returns>元组Tuple（bool是否注册成功,string注册的热键）</returns>
        private (bool, string) AddHotKey_Void(ModelKeys mode, NormalKeys key, KeyInvoke_Void work)
        {
            if (CheckInProtectList(mode, key)) { return (false, "⚠受保护的热键不允许执行任何操作！"); }
            int id = HOTKEY_ID + Counter;
            RemoveExistRegisterByID(id);
            RemoveExistRegisterByKeys(mode, key);
            bool result;
            result = RegisterHotKey(WindowhWnd, id, (uint)mode, (uint)key);
            if (result)
            {
                Trigger_Void.Add(id, work);
                RegisterInfo info = new RegisterInfo(id, mode, key, work);
                RegisterList.Add(info);
                Counter++;
                return (result, info.SuccessRegistration());
            }
            return (result, RegisterInfo.LoseRegistration(mode, key, work));
        }

        /// <summary>
        /// 热键注册函数【return型处理函数】
        /// </summary>
        /// <param name="mode">修饰键</param>
        /// <param name="key">一般按键</param>
        /// <param name="work">处理函数的函数名</param>
        /// <returns>元组Tuple（bool是否注册成功,string注册的热键）</returns>
        private (bool, string) AddHotKey_Return(ModelKeys mode, NormalKeys key, KeyInvoke_Return work)
        {
            if (CheckInProtectList(mode, key)) { return (false, "⚠受保护的热键不允许执行任何操作！"); }
            int id = HOTKEY_ID + Counter;
            RemoveExistRegisterByID(id);
            RemoveExistRegisterByKeys(mode, key);
            bool result;
            result = RegisterHotKey(WindowhWnd, id, (uint)mode, (uint)key);
            if (result)
            {
                Trigger_Return.Add(id, work);
                RegisterInfo info = new RegisterInfo(id, mode, key, work);
                RegisterList.Add(info);
                Counter++;
                return (result, info.SuccessRegistration());
            }
            return (result, RegisterInfo.LoseRegistration(mode, key, work));
        }

        /// <summary>
        /// 修改指定函数签名所对应的组合键--无参、无返回值
        /// </summary>
        /// <param name="work">函数签名</param>
        /// <param name="mode">修饰键</param>
        /// <param name="key">一般按键</param>
        private void EditHotKey_NewKeysVoid(KeyInvoke_Void work, ModelKeys mode, NormalKeys key)
        {
            if (CheckInProtectList(mode, key)) { return; }
            foreach (RegisterInfo info in RegisterList)
            {
                if (info.FunctionVoid == work)
                {
                    RemoveExistRegisterByID(info.RegisterID);
                    RegisterHotKey(WindowhWnd, info.RegisterID, (uint)mode, (uint)key);
                    RegisterInfo result = new RegisterInfo(info.RegisterID, mode, key, work);
                    RegisterList.Add(result);
                    Trigger_Void.Add(result.RegisterID, work);
                    break;
                }
            }
        }

        /// <summary>
        /// 修改指定函数签名所对应的组合键--无参、有返回值
        /// </summary>
        /// <param name="work">函数签名</param>
        /// <param name="mode">修饰键</param>
        /// <param name="key">一般按键</param>
        private void EditHotKey_NewKeysReturn(KeyInvoke_Return work, ModelKeys mode, NormalKeys key)
        {
            if (CheckInProtectList(mode, key)) { return; }
            foreach (RegisterInfo info in RegisterList)
            {
                if (info.FunctionReturn == work)
                {
                    RemoveExistRegisterByID(info.RegisterID);
                    RegisterHotKey(WindowhWnd, info.RegisterID, (uint)mode, (uint)key);
                    RegisterInfo result = new RegisterInfo(info.RegisterID, mode, key, work);
                    RegisterList.Add(result);
                    Trigger_Return.Add(result.RegisterID, work);
                    break;
                }
            }
        }

        /// <summary>
        /// 依据组合键查找注册信息，并更新它对应的处理事件--无参、无返回值
        /// </summary>
        /// <param name="mode">修饰键</param>
        /// <param name="key">一般按键</param>
        /// <param name="work">新的处理事件</param>
        private void EditHotKey_NewFunctionVoid(ModelKeys mode, NormalKeys key, KeyInvoke_Void work)
        {
            if (CheckInProtectList(mode, key)) { return; }
            foreach (RegisterInfo info in RegisterList)
            {
                if (info.Model == mode && info.Normal == key)
                {
                    RemoveExistRegisterByID(info.RegisterID);
                    RegisterHotKey(WindowhWnd, info.RegisterID, (uint)mode, (uint)key);
                    RegisterInfo result = new RegisterInfo(info.RegisterID, mode, key, work);
                    RegisterList.Add(result);
                    Trigger_Void.Add(result.RegisterID, work);
                    break;
                }
            }
        }

        /// <summary>
        /// 依据组合键查找注册信息，并更新它对应的处理事件--无参、有返回值
        /// </summary>
        /// <param name="mode">修饰键</param>
        /// <param name="key">一般按键</param>
        /// <param name="work">新的处理事件</param>
        private void EditHotKey_NewFunctionReturn(ModelKeys mode, NormalKeys key, KeyInvoke_Return work)
        {
            if (CheckInProtectList(mode, key)) { return; }
            foreach (RegisterInfo info in RegisterList)
            {
                if (info.Model == mode && info.Normal == key)
                {
                    RemoveExistRegisterByID(info.RegisterID);
                    RegisterHotKey(WindowhWnd, info.RegisterID, (uint)mode, (uint)key);
                    RegisterInfo result = new RegisterInfo(info.RegisterID, mode, key, work);
                    RegisterList.Add(result);
                    Trigger_Return.Add(result.RegisterID, work);
                    break;
                }
            }
        }

        private void RemoveHotKeys()//销毁所有热键
        {
            while (Counter >= 0)
            {
                int id = HOTKEY_ID + Counter;
                RemoveExistRegisterByID(id);
                Counter--;
            }
        }

        private void RemoveExistRegisterByID(int id)//依据热键注册时的编号，删除热键
        {
            if (CheckInProtectList(id)) { return; }
            if (UnregisterHotKey(WindowhWnd, id))
            {
                if (Trigger_Void.ContainsKey(id)) { Trigger_Void.Remove(id); };
                if (Trigger_Return.ContainsKey(id)) { Trigger_Return.Remove(id); };

                RegisterInfo? target = null;
                foreach (RegisterInfo registerInfo in RegisterList)
                {
                    if (registerInfo.RegisterID == id)
                    {
                        target = registerInfo;
                        break;
                    }
                }
                if (target != null) RegisterList.Remove(target);
            }
        }

        private int RemoveExistRegisterByKeys(ModelKeys mode, NormalKeys key)//依据组合键，删除热键
        {
            if (CheckInProtectList(mode, key)) { return -1; }
            foreach (RegisterInfo info in RegisterList)
            {
                if (info.Model == mode && info.Normal == key)
                {
                    RemoveExistRegisterByID(info.RegisterID);
                    return info.RegisterID;
                }
            }
            return -1;
        }

        private void RemoveExistRegisterByFunction_Void(KeyInvoke_Void work)//依据函数清除所有组合键
        {
            List<int> target = new List<int>();
            foreach (RegisterInfo info in RegisterList)
            {
                if (info.FunctionVoid == work)
                {
                    target.Add(info.RegisterID);
                }
            }
            foreach (int id in target)
            {
                if (!CheckInProtectList(id)) { RemoveExistRegisterByID(id); }
            }
        }

        private void RemoveExistRegisterByFunction_Return(KeyInvoke_Return work)//依据函数清除所有组合键
        {
            List<int> target = new List<int>();
            foreach (RegisterInfo info in RegisterList)
            {
                if (info.FunctionReturn == work)
                {
                    target.Add(info.RegisterID);
                }
            }
            foreach (int id in target)
            {
                if (!CheckInProtectList(id)) { RemoveExistRegisterByID(id); }
            }
        }

        private void AddProtectedHotKey(ModelKeys modelKey, NormalKeys normalKey)//依据组合键添加保护名单
        {
            foreach (var item in ProtectList)
            {
                if (item.Item1 == modelKey && item.Item2 == normalKey)
                {
                    return;
                }
            }
            ProtectList.Add(Tuple.Create(modelKey, normalKey));
        }

        private void AddProtectedHotKey(int id)//依据注册id添加保护名单
        {
            foreach (RegisterInfo info in RegisterList)
            {
                if (info.RegisterID == id)
                {
                    AddProtectedHotKey(info.Model, info.Normal);
                    return;
                }
            }
        }

        private void RemoveProtectedHotKey(ModelKeys modelKey, NormalKeys normalKey)//依据组合键删除保护名单
        {
            foreach (var item in ProtectList)
            {
                if (item.Item1 == modelKey && item.Item2 == normalKey)
                {
                    ProtectList.Remove(item);
                    return;
                }
            }
        }

        private void RemoveProtectedHotKey(int id)//依据注册id删除保护名单
        {
            foreach (RegisterInfo info in RegisterList)
            {
                if (info.RegisterID == id)
                {
                    RemoveProtectedHotKey(info.Model, info.Normal);
                    return;
                }
            }
        }

        private bool CheckInProtectList(ModelKeys modelKey, NormalKeys normalKey)//仅检查组合键是否处于保护态，不修改
        {
            bool result = false;

            foreach (var item in ProtectList)
            {
                if (item.Item1 == modelKey && item.Item2 == normalKey)
                {
                    result = true;
                    break;
                }
            }

            return result;
        }

        private bool CheckInProtectList(int id)//仅检查指定id对应的组合键是否处于保护态，不修改
        {
            bool result = false;

            foreach (RegisterInfo info in RegisterList)
            {
                if (info.RegisterID == id)
                {
                    result = CheckInProtectList(info.Model, info.Normal);
                    break;
                }
            }

            return result;
        }
        #endregion
    }
}
