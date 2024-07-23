using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;

/// <summary>
/// 普通按键
/// </summary>
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

/// <summary>
/// 系统按键
/// </summary>
[Flags]
public enum ModelKeys : uint
{
    ALT = 0x0001,
    CTRL = 0x0002,
    SHIFT = 0x0004,
    WIN = 0x0008,
    NOREPEAT = 0x4000
}

/// <summary>
/// 处理函数的类别
/// </summary>
public enum FunctionTypes
{
    Void,
    Return,
    Focus
}

namespace FastHotKeyForWPF
{
    /// <summary>
    /// 全局热键
    /// </summary>
    public class GlobalHotKey
    {
        private static GlobalHotKey? Instance;

        private GlobalHotKey() { }

        internal const int WM_HOTKEY = 0x0312; //表示Windows有热键被按下

        [DllImport("user32.dll")]
        internal static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll")]
        internal static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        /// <summary>
        /// 是否实时更新热键处理函数返回的值
        /// </summary>
        public static bool IsUpdate { get; set; } = true;

        /// <summary>
        /// 第一个热键注册的ID号
        /// </summary>
        public static int HOTKEY_ID { get; set; } = 2004;

        private static object? ReturnValue
        {
            set
            {
                if (IsUpdate)
                {
                    ReturnValueMonitor.Update(value);
                }
            }
        }

        /// <summary>
        /// 注册信息表
        /// </summary>
        public static RegisterCollection Registers
        {
            get
            {
                if (Instance != null)
                {
                    return Instance.RegisterList;
                }
                return new RegisterCollection();
            }
        }

        /// <summary>
        /// 保护名单
        /// </summary>
        public static List<Tuple<uint, uint>>? ProtectedHotKeys
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

        /// <summary>
        /// 激活热键功能
        /// </summary>
        public static void Awake()
        {
            if (Instance == null)
            {
                Window mainWindow = Application.Current.MainWindow;
                if (mainWindow != null)
                {
                    Instance = new GlobalHotKey();
                    Instance.WindowhWnd = new WindowInteropHelper(mainWindow).Handle;
                    Instance.Install();
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

        /// <summary>
        /// 销毁所有热键
        /// </summary>
        public static void Destroy()
        {
            if (Instance != null)
            {
                Instance.Uninstall();
                Instance.RemoveHotKeys();
                Instance = null;
            }
        }

        /// <summary>
        /// 添加一个热键
        /// </summary>
        /// <returns>元组，Item1表示是否成功注册热键，Item2表示注册ID</returns>
        public static (bool, int) Add(ICollection<ModelKeys> mode, NormalKeys key, Action work)
        {
            if (Instance != null)
            {
                uint uMode = (uint)mode.Aggregate((current, next) => current | next);
                uint uKey = (uint)key;
                return Instance.AddHotKey_Void(uMode, uKey, work);
            }
            return (false, -1);
        }

        /// <summary>
        /// 添加一个热键
        /// </summary>
        /// <returns>元组，Item1表示是否成功注册热键，Item2表示注册ID</returns>
        public static (bool, int) Add(ICollection<ModelKeys> mode, NormalKeys key, Func<object> work)
        {
            if (Instance != null)
            {
                uint uMode = (uint)mode.Aggregate((current, next) => current | next);
                uint uKey = (uint)key;
                return Instance.AddHotKey_Return(uMode, uKey, work);
            }
            return (false, -1);
        }

        /// <summary>
        /// 修改热键的处理函数
        /// </summary>
        public static void EditHotKey_Function(ICollection<ModelKeys> mode, NormalKeys key, Action work)
        {
            if (Instance != null)
            {
                uint uMode = (uint)mode.Aggregate((current, next) => current | next);
                uint uKey = (uint)key;
                Instance.EditHotKey_NewFunctionVoid(uMode, uKey, work);
            }
        }

        /// <summary>
        /// 修改热键的处理函数
        /// </summary>
        public static void EditHotKey_Function(ICollection<ModelKeys> mode, NormalKeys key, Func<object> work)
        {
            if (Instance != null)
            {
                uint uMode = (uint)mode.Aggregate((current, next) => current | next);
                uint uKey = (uint)key;
                Instance.EditHotKey_NewFunctionReturn(uMode, uKey, work);
            }
        }

        /// <summary>
        /// 修改热键的热键组合
        /// </summary>
        public static void EditHotKey_Keys(Action work, ICollection<ModelKeys> mode, NormalKeys key)
        {
            if (Instance != null)
            {
                uint uMode = (uint)mode.Aggregate((current, next) => current | next);
                uint uKey = (uint)key;
                Instance.EditHotKey_NewKeysVoid(work, uMode, uKey);
            }
        }

        /// <summary>
        /// 修改热键的热键组合
        /// </summary>
        public static void EditHotKey_Keys(Func<object> work, ICollection<ModelKeys> mode, NormalKeys key)
        {
            if (Instance != null)
            {
                uint uMode = (uint)mode.Aggregate((current, next) => current | next);
                uint uKey = (uint)key;
                Instance.EditHotKey_NewKeysReturn(work, uMode, uKey);
            }
        }

        /// <summary>
        /// 清空热键，但不卸载钩子
        /// </summary>
        public static void Clear()
        {
            if (Instance != null)
            {
                Instance.RemoveHotKeys();
            }
        }

        /// <summary>
        /// 删除指定编号的热键
        /// </summary>
        public static void DeleteById(int id)
        {
            if (Instance != null)
            {
                Instance.RemoveExistRegisterByID(id);
            }
        }

        /// <summary>
        /// 清除与指定函数关联的热键
        /// </summary>
        public static void DeleteByFunction(Action work)//
        {
            if (Instance != null)
            {
                Instance.RemoveExistRegisterByFunction_Void(work);
            }
        }

        /// <summary>
        /// 清除与指定函数关联的热键
        /// </summary>
        public static void DeleteByFunction(Func<object> work)
        {
            if (Instance != null)
            {
                Instance.RemoveExistRegisterByFunction_Return(work);
            }
        }

        /// <summary>
        /// 清除与指定热键组合关联的热键
        /// </summary>
        public static void DeleteByKeys(uint modelKeys, uint normalKeys)
        {
            if (Instance != null)
            {
                Instance.RemoveExistRegisterByKeys(modelKeys, normalKeys);
            }
        }

        /// <summary>
        /// 依据热键组合，增加受保护的热键
        /// </summary>
        public static void ProtectHotKeyByKeys(uint modelKeys, uint normalKeys)
        {
            Instance?.AddProtectedHotKey(modelKeys, normalKeys);
        }

        /// <summary>
        /// 依据注册ID，增加受保护的热键
        /// </summary>
        /// <param name="id"></param>
        public static void ProtectHotKeyById(int id)
        {
            Instance?.AddProtectedHotKey(id);
        }

        /// <summary>
        /// 依据热键组合，解除热键的保护态
        /// </summary>
        public static void UnProtectHotKeyByKeys(ICollection<ModelKeys> modelKeys, NormalKeys normalKeys)
        {
            uint uMode = (uint)modelKeys.Aggregate((current, next) => current | next);
            uint uKey = (uint)normalKeys;
            Instance?.RemoveProtectedHotKey(uMode, uKey);
        }

        /// <summary>
        /// 依据注册ID，解除热键的保护态
        /// </summary>
        public static void UnProtectHotKeyById(int id)
        {
            Instance?.RemoveProtectedHotKey(id);
        }

        /// <summary>
        /// 检查热键组合是否处于保护态
        /// </summary>
        public static bool IsHotKeyProtected(uint modelKeys, uint normalKeys)
        {
            bool result = false;

            if (Instance != null)
            {
                result = Instance.CheckInProtectList(modelKeys, normalKeys);
            }

            return result;
        }

        /// <summary>
        /// 检查指定ID的热键是否处于保护态
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool IsHotKeyProtected(int id)
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

        private Dictionary<int, Action> Trigger_Void = new Dictionary<int, Action>();//处理函数映射表

        private Dictionary<int, Func<object>> Trigger_Return = new Dictionary<int, Func<object>>();//处理函数映射表

        private RegisterCollection RegisterList = new RegisterCollection();//注册在列的热键

        private List<Tuple<uint, uint>> ProtectList = new List<Tuple<uint, uint>>();//受保护的热键名单

        private IntPtr WhileKeyInvoked(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)//系统消息处理函数
        {
            switch (msg)
            {
                case WM_HOTKEY:
                    int id = wParam.ToInt32();

                    if (Trigger_Void.TryGetValue(id, out Action? action))
                    {
                        action.Invoke();
                    }
                    else if (Trigger_Return.TryGetValue(id, out Func<object>? func))
                    {
                        ReturnValue = Trigger_Return[id].Invoke();
                    }
                    else
                    {
                        MessageBox.Show("未能找到对应的事件处理函数！");
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

        private (bool, int) AddHotKey_Void(uint mode, uint key, Action work)
        {
            if (CheckInProtectList(mode, key)) { return (false, -1); }
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
                return (result, info.RegisterID);
            }
            return (result, -1);
        }
        private (bool, int) AddHotKey_Return(uint mode, uint key, Func<object> work)
        {
            if (CheckInProtectList(mode, key)) { return (false, -1); }
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
                return (result, info.RegisterID);
            }
            return (result, -1);
        }

        private void EditHotKey_NewKeysVoid(Action work, uint mode, uint key)
        {
            if (CheckInProtectList(mode, key)) { return; }
            foreach (RegisterInfo info in RegisterList.RegisterList)
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
        private void EditHotKey_NewKeysReturn(Func<object> work, uint mode, uint key)
        {
            if (CheckInProtectList(mode, key)) { return; }
            foreach (RegisterInfo info in RegisterList.RegisterList)
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
        private void EditHotKey_NewFunctionVoid(uint mode, uint key, Action work)
        {
            if (CheckInProtectList(mode, key)) { return; }
            foreach (RegisterInfo info in RegisterList.RegisterList)
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
        private void EditHotKey_NewFunctionReturn(uint mode, uint key, Func<object> work)
        {
            if (CheckInProtectList(mode, key)) { return; }
            foreach (RegisterInfo info in RegisterList.RegisterList)
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

        private void RemoveHotKeys()
        {
            while (Counter >= 0)
            {
                int id = HOTKEY_ID + Counter;
                RemoveExistRegisterByID(id);
                Counter--;
            }
        }
        private void RemoveExistRegisterByID(int id)
        {
            if (CheckInProtectList(id)) { return; }
            if (UnregisterHotKey(WindowhWnd, id))
            {
                if (Trigger_Void.ContainsKey(id)) { Trigger_Void.Remove(id); };
                if (Trigger_Return.ContainsKey(id)) { Trigger_Return.Remove(id); };

                RegisterInfo? target = null;
                foreach (RegisterInfo registerInfo in RegisterList.RegisterList)
                {
                    if (registerInfo.RegisterID == id)
                    {
                        target = registerInfo;
                        break;
                    }
                }
                if (target != null) RegisterList.Remove(target.RegisterID);
            }
        }
        private int RemoveExistRegisterByKeys(uint mode, uint key)
        {
            if (CheckInProtectList(mode, key)) { return -1; }
            foreach (RegisterInfo info in RegisterList.RegisterList)
            {
                if (info.Model == mode && info.Normal == key)
                {
                    RemoveExistRegisterByID(info.RegisterID);
                    return info.RegisterID;
                }
            }
            return -1;
        }
        private void RemoveExistRegisterByFunction_Void(Action work)
        {
            List<int> target = new List<int>();
            foreach (RegisterInfo info in RegisterList.RegisterList)
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
        private void RemoveExistRegisterByFunction_Return(Func<object> work)
        {
            List<int> target = new List<int>();
            foreach (RegisterInfo info in RegisterList.RegisterList)
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

        private void AddProtectedHotKey(uint modelKey, uint normalKey)
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
        private void AddProtectedHotKey(int id)
        {
            foreach (RegisterInfo info in RegisterList.RegisterList)
            {
                if (info.RegisterID == id)
                {
                    AddProtectedHotKey(info.Model, info.Normal);
                    return;
                }
            }
        }

        private void RemoveProtectedHotKey(uint modelKey, uint normalKey)
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
        private void RemoveProtectedHotKey(int id)
        {
            foreach (RegisterInfo info in RegisterList.RegisterList)
            {
                if (info.RegisterID == id)
                {
                    RemoveProtectedHotKey(info.Model, info.Normal);
                    return;
                }
            }
        }

        private bool CheckInProtectList(uint modelKey, uint normalKey)
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
        private bool CheckInProtectList(int id)
        {
            bool result = false;

            foreach (RegisterInfo info in RegisterList.RegisterList)
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
