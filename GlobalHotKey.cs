using System.Runtime.InteropServices;
using System.Windows;
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
    F8 = 0x77,
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
    Return
}

public delegate void KeyInvoke_Void(); //用于接收处理函数

public delegate object KeyInvoke_Return(); //用于接收处理函数

namespace FastHotKeyForWPF
{
    /// <summary>
    /// 【全局热键】使用说明:
    /// <para>⚠注意:需要在Application.Current.MainWindow中管理热键功能的激活和销毁</para>
    /// <para>Ⅰ 按以下方式激活热键功能</para>
    /// <para>protected override void OnSourceInitialized(EventArgs e)</para>
    /// <para>{</para>       
    /// <para>GlobalHotKey.Awake();</para>
    /// <para>base.OnSourceInitialized(e);</para>    
    /// <para>}</para>
    /// <para>Ⅱ 按以下方式销毁热键功能</para>
    /// <para>protected override void OnClosed(EventArgs e)</para>
    /// <para>{</para>       
    /// <para>GlobalHotKey.Destroy();</para>
    /// <para>base.OnClosed(e);</para>
    /// <para>}</para>
    /// <para>Ⅲ 函数表</para>
    /// <para>Awake() 激活热键功能，在主窗口的句柄已存在时调用</para>
    /// <para>Destroy() 销毁热键功能</para>
    /// <para>Add() 注册快捷键。将自定义的、无参数的【void函数】/【返回值类型为object的函数】的【函数签名】作为参数，这就是此快捷键触发后要做的事情。函数返回（bool,string）元组，表示是否注册成功、注册消息</para>
    /// <para>DeleteById() 删除指定编号的快捷键</para>
    /// <para>Binding() 在外部定义一个object属性并传入此函数中，即可实时向这个属性传入【返回值类型为object的函数】的返回结果</para>
    /// <para>Clear() 清除所有注册的快捷键</para>
    /// <para>HotKeyInfo() 获取一个包括当前所有在册热键的信息合集，它是一个RegisterInfo对象的集合</para>
    /// <para>EditHotKey_Function() 查找并替换组合键对应的处理函数</para>
    /// <para>EditHotKey_Keys() 查找并替换处理函数对应的组合键</para>
    /// <para>Ⅳ 参数调试</para>
    /// <para>(int) HOTKEY_ID 热键的注册编号，建议只修改一次，且修改操作位于你第一个注册快捷键操作之前</para>
    /// <para>(bool) IsDeBug 调试模式开关，默认关闭(false)，若打开，则部分过程会打印值(MessageBox)</para>
    /// <para>(bool) IsUpdate 绑定传递开关，默认打开(true)，若关闭，则不会实时更新绑定的内容(object)</para>
    /// </summary>
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
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        /// <summary>
        /// 取关函数，解除热键相关消息和窗口句柄间的绑定
        /// </summary>
        /// <param name="hWnd">窗口句柄</param>
        /// <param name="id">快捷键的编号</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

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

        public static void Destroy()
        {
            if (Instance != null)
            {
                Instance.Uninstall();
                Instance.RemoveHotKeys();
                Instance = null;
            }
        }

        public static (bool, string) Add(ModelKeys mode, NormalKeys key, KeyInvoke_Void work)
        {
            if (Instance != null)
            {
                return Instance.AddHotKey_Void(mode, key, work);
            }
            return (false, "热键功能尚未激活，请参考文档注释。");
        }
        public static (bool, string) Add(ModelKeys mode, NormalKeys key, KeyInvoke_Return work)
        {
            if (Instance != null)
            {
                return Instance.AddHotKey_Return(mode, key, work);
            }
            return (false, "热键功能尚未激活，请参考文档注释。");
        }

        public static void DeleteById(int id)
        {
            if (Instance != null)
            {
                Instance.RemoveExistRegisterByID(id);
            }
        }

        public static void Clear()
        {
            if (Instance != null)
            {
                Instance.RemoveHotKeys();
            }
        }

        public static List<RegisterInfo> HotKeyInfo()
        {
            if (Instance != null)
            {
                return Instance.RegisterList;
            }
            if (IsDeBug)//调试模式下不允许在未激活此类型时读取注册列表
            {
                throw new Exception("不可以");
            }
            return new List<RegisterInfo>();
        }

        public static void EditHotKey_Function(ModelKeys mode, NormalKeys key, KeyInvoke_Void work)
        {
            if (Instance != null)
            {
                Instance.EditHotKey_NewFunctionVoid(mode, key, work);
            }
        }
        public static void EditHotKey_Function(ModelKeys mode, NormalKeys key, KeyInvoke_Return work)
        {
            if (Instance != null)
            {
                Instance.EditHotKey_NewFunctionReturn(mode, key, work);
            }
        }

        public static void EditHotKey_Keys(KeyInvoke_Void work, ModelKeys mode, NormalKeys key)
        {
            if (Instance != null)
            {
                Instance.EditHotKey_NewKeysVoid(work, mode, key);
            }
        }
        public static void EditHotKey_Keys(KeyInvoke_Return work, ModelKeys mode, NormalKeys key)
        {
            if (Instance != null)
            {
                Instance.EditHotKey_NewKeysReturn(work, mode, key);
            }
        }



        #region 动态资源
        IntPtr WindowhWnd; //主窗体句柄☆

        private int Counter = 0; //计数器，表示历史中一共注册过多少个热键

        private Dictionary<int, KeyInvoke_Void> Trigger_Void = new Dictionary<int, KeyInvoke_Void>();

        private Dictionary<int, KeyInvoke_Return> Trigger_Return = new Dictionary<int, KeyInvoke_Return>();

        private List<RegisterInfo> RegisterList = new List<RegisterInfo>();//注册在列的热键

        /// <summary>
        /// 快捷事件处理函数
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

        private int RemoveExistRegisterByKeys(ModelKeys mode, NormalKeys key)
        {
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
        #endregion
    }
}
