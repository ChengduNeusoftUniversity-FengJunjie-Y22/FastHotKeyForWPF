using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;

namespace FastHotKeyForWPF
{
    /// <summary>
    /// 全局热键
    /// </summary>
    public class GlobalHotKey
    {
        private static GlobalHotKey? Instance;

        private GlobalHotKey() { }

        internal const int WM_HOTKEY = 0x0312;

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
        public static List<Tuple<uint, NormalKeys>>? ProtectedHotKeys
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

                    for (int i = 0; i < BoxPool.ModelItems.Count; i++)
                    {
                        BoxPool.ModelItems[i].UpdateText();
                        BoxPool.ModelItems[i].UpdateHotKey();
                    }
                }
                else
                {
                    throw new Exception("窗口句柄不存在 ！");
                }
            }
            else
            {
                throw new Exception("重复激活 GlobalHotKey ！");
            }
        }
        /// <summary>
        /// 销毁所有热键
        /// </summary>
        public static void Destroy()
        {
            if (Instance != null)
            {
                Instance.UnInstall();
                Instance.RemoveAllHotKeys();
                Instance = null;
            }
        }

        /// <summary>
        /// 添加一个热键
        /// </summary>
        /// <returns>元组，Item1表示是否成功注册热键，Item2表示注册ID</returns>
        public static int Add(ModelKeys mode, NormalKeys key, HotKeyEventHandler handler)
        {
            if (Instance != null)
            {
                return Instance.AddHotKey(mode, key, handler);
            }
            return -1;
        }
        /// <summary>
        /// 添加一个热键
        /// </summary>
        /// <returns>元组，Item1表示是否成功注册热键，Item2表示注册ID</returns>
        public static int Add(ICollection<ModelKeys> modes, NormalKeys key, HotKeyEventHandler handler)
        {
            if (Instance != null && modes.Count > 0)
            {
                return Instance.AddHotKey(modes, key, handler);
            }
            return -1;
        }
        /// <summary>
        /// 添加一个热键
        /// </summary>
        /// <returns>元组，Item1表示是否成功注册热键，Item2表示注册ID</returns>
        public static int Add(uint modes, NormalKeys key, HotKeyEventHandler handler)
        {
            if (Instance != null)
            {
                return Instance.AddHotKey(modes, key, handler);
            }
            return -1;
        }

        /// <summary>
        /// 修改热键的处理函数
        /// </summary>
        public static void EditHandler(ModelKeys mode, NormalKeys key, HotKeyEventHandler handler)
        {
            if (Instance != null)
            {
                Instance.EditExistHandler(mode, key, handler);
            }
        }
        /// <summary>
        /// 修改热键的处理函数
        /// </summary>
        public static void EditHandler(ICollection<ModelKeys> modes, NormalKeys key, HotKeyEventHandler handler)
        {
            if (Instance != null && modes.Count > 0)
            {
                Instance.EditExistHandler(modes, key, handler);
            }
        }
        /// <summary>
        /// 修改热键的处理函数
        /// </summary>
        public static void EditHandler(uint modes, NormalKeys key, HotKeyEventHandler handler)
        {
            if (Instance != null)
            {
                Instance.EditExistHandler(modes, key, handler);
            }
        }
        /// <summary>
        /// 修改热键的热键组合
        /// </summary>
        public static void EditKeys(HotKeyEventHandler handler, ModelKeys mode, NormalKeys key)
        {
            if (Instance != null)
            {
                Instance.EditExistKeys(handler, mode, key);
            }
        }
        /// <summary>
        /// 修改热键的热键组合
        /// </summary>
        public static void EditKeys(HotKeyEventHandler handler, ICollection<ModelKeys> modes, NormalKeys key)
        {
            if (Instance != null && modes.Count > 0)
            {
                Instance.EditExistKeys(handler, modes, key);
            }
        }
        /// <summary>
        /// 修改热键的热键组合
        /// </summary>
        public static void EditKeys(HotKeyEventHandler handler, uint modes, NormalKeys key)
        {
            if (Instance != null)
            {
                Instance.EditExistKeys(handler, modes, key);
            }
        }

        /// <summary>
        /// 清空热键，但不卸载钩子
        /// </summary>
        public static void Clear()
        {
            if (Instance != null)
            {
                Instance.RemoveAllHotKeys();
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
        public static void DeleteByHandler(HotKeyEventHandler handler)
        {
            if (Instance != null)
            {
                Instance.RemoveExistRegisterByHandler(handler);
            }
        }
        /// <summary>
        /// 清除与指定热键组合关联的热键
        /// </summary>
        public static void DeleteByKeys(ModelKeys modelKeys, NormalKeys normalKeys)
        {
            if (Instance != null)
            {
                Instance.RemoveExistRegisterByKeys(modelKeys, normalKeys);
            }
        }
        /// <summary>
        /// 清除与指定热键组合关联的热键
        /// </summary>
        public static void DeleteByKeys(ICollection<ModelKeys> modelKeys, NormalKeys normalKey)
        {
            if (Instance != null && modelKeys.Count > 0)
            {
                Instance.RemoveExistRegisterByKeys(modelKeys, normalKey);
            }
        }
        /// <summary>
        /// 清除与指定热键组合关联的热键
        /// </summary>
        public static void DeleteByKeys(uint modelKeys, NormalKeys normalKey)
        {
            if (Instance != null)
            {
                Instance.RemoveExistRegisterByKeys(modelKeys, normalKey);
            }
        }

        /// <summary>
        /// 依据热键组合，增加受保护的热键
        /// </summary>
        public static void ProtectHotKeyByKeys(ModelKeys modelKeys, NormalKeys normalKeys)
        {
            Instance?.AddProtectedHotKey(modelKeys, normalKeys);
        }
        /// <summary>
        /// 依据热键组合，增加受保护的热键
        /// </summary>
        public static void ProtectHotKeyByKeys(ICollection<ModelKeys> modelKeys, NormalKeys normalKey)
        {
            if (modelKeys.Count < 1) { return; }
            Instance?.AddProtectedHotKey(modelKeys, normalKey);
        }
        /// <summary>
        /// 依据热键组合，增加受保护的热键
        /// </summary>
        public static void ProtectHotKeyByKeys(uint modelKeys, NormalKeys normalKey)
        {
            Instance?.AddProtectedHotKey(modelKeys, normalKey);
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
        /// 解除热键的保护态
        /// </summary>
        public static void UnProtectHotKeyByKeys(ModelKeys modelKeys, NormalKeys normalKey)
        {
            Instance?.RemoveProtectedHotKey(modelKeys, normalKey);
        }
        /// <summary>
        /// 解除热键的保护态
        /// </summary>
        public static void UnProtectHotKeyByKeys(ICollection<ModelKeys> modelKeys, NormalKeys normalKey)
        {
            if (modelKeys.Count < 1) { return; }
            Instance?.RemoveProtectedHotKey(modelKeys, normalKey);
        }
        /// <summary>
        /// 解除热键的保护态
        /// </summary>
        public static void UnProtectHotKeyByKeys(uint modelKeys, NormalKeys normalKey)
        {
            Instance?.RemoveProtectedHotKey(modelKeys, normalKey);
        }
        /// <summary>
        /// 解除热键的保护态
        /// </summary>
        public static void UnProtectHotKeyById(int id)
        {
            Instance?.RemoveProtectedHotKey(id);
        }

        /// <summary>
        /// 检查热键是否处于保护态
        /// </summary>
        public static bool IsHotKeyProtected(ModelKeys modelKeys, NormalKeys normalKey)
        {
            bool result = false;

            if (Instance != null)
            {
                result = Instance.CheckInProtectList(modelKeys, normalKey);
            }

            return result;
        }
        /// <summary>
        /// 检查热键是否处于保护态
        /// </summary>
        public static bool IsHotKeyProtected(ICollection<ModelKeys> modelKeys, NormalKeys normalKey)
        {
            bool result = false;

            if (Instance != null && modelKeys.Count > 0)
            {
                result = Instance.CheckInProtectList(modelKeys, normalKey);
            }

            return result;
        }
        /// <summary>
        /// 检查热键是否处于保护态
        /// </summary>
        public static bool IsHotKeyProtected(uint modelKeys, NormalKeys normalKey)
        {
            bool result = false;

            if (Instance != null)
            {
                result = Instance.CheckInProtectList(modelKeys, normalKey);
            }

            return result;
        }
        /// <summary>
        /// 检查热键是否处于保护态
        /// </summary>
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

        IntPtr WindowhWnd;

        private int Counter = 0;

        private Dictionary<int, HotKeyEventHandler> Handlers = new Dictionary<int, HotKeyEventHandler>();

        private RegisterCollection RegisterList = new RegisterCollection();

        private List<Tuple<uint, NormalKeys>> ProtectList = new List<Tuple<uint, NormalKeys>>();

        private IntPtr WhileKeyInvoked(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {
                case WM_HOTKEY:
                    int id = wParam.ToInt32();
                    try
                    {
                        HotKeyEventArgs Args = new HotKeyEventArgs();
                        Args.RegisterInfo = RegisterList[id];
                        Handlers[id].Invoke(this, Args);
                    }
                    catch (KeyNotFoundException)
                    {
                        throw new Exception($"Handlers[{id}] Not Be Found !");
                    }
                    handled = true;
                    break;

            }
            return IntPtr.Zero;
        }

        private void Install()
        {
            HwndSource source = HwndSource.FromHwnd(WindowhWnd);
            source.AddHook(new HwndSourceHook(WhileKeyInvoked));
        }

        private void UnInstall()
        {
            HwndSource source = HwndSource.FromHwnd(WindowhWnd);
            source.RemoveHook(new HwndSourceHook(WhileKeyInvoked));
        }

        private int AddHotKey(ModelKeys mode, NormalKeys key, HotKeyEventHandler handler)
        {
            if (CheckInProtectList(mode, key)) { return -1; }
            int id = HOTKEY_ID + Counter;
            RemoveExistRegisterByID(id);
            RemoveExistRegisterByKeys(mode, key);
            bool result;
            result = RegisterHotKey(WindowhWnd, id, (uint)mode, (uint)key);
            if (result)
            {
                Handlers.Add(id, handler);
                RegisterInfo info = new RegisterInfo(id, mode, key, handler);
                RegisterList.Add(info);
                Counter++;
                return info.RegisterID;
            }
            return -1;
        }
        private int AddHotKey(ICollection<ModelKeys> modes, NormalKeys key, HotKeyEventHandler handler)
        {
            uint target = (uint)modes.Aggregate((current, next) => current | next);

            if (CheckInProtectList(target, key)) { return -1; }
            int id = HOTKEY_ID + Counter;
            RemoveExistRegisterByID(id);
            RemoveExistRegisterByKeys(target, key);
            bool result;
            result = RegisterHotKey(WindowhWnd, id, target, (uint)key);
            if (result)
            {
                Handlers.Add(id, handler);
                RegisterInfo info = new RegisterInfo(id, target, key, handler);
                RegisterList.Add(info);
                Counter++;
                return info.RegisterID;
            }
            return -1;
        }
        private int AddHotKey(uint mode, NormalKeys key, HotKeyEventHandler handler)
        {
            if (CheckInProtectList(mode, key)) { return -1; }
            int id = HOTKEY_ID + Counter;
            RemoveExistRegisterByID(id);
            RemoveExistRegisterByKeys(mode, key);
            bool result;
            result = RegisterHotKey(WindowhWnd, id, mode, (uint)key);
            if (result)
            {
                Handlers.Add(id, handler);
                RegisterInfo info = new RegisterInfo(id, mode, key, handler);
                RegisterList.Add(info);
                Counter++;
                return info.RegisterID;
            }
            return -1;
        }

        private void EditExistKeys(HotKeyEventHandler handler, ModelKeys mode, NormalKeys key)
        {
            if (CheckInProtectList(mode, key)) { return; }

            foreach (RegisterInfo info in RegisterList.RegisterList)
            {
                if (info.Handler == handler)
                {
                    RemoveExistRegisterByID(info.RegisterID);
                    RegisterHotKey(WindowhWnd, info.RegisterID, (uint)mode, (uint)key);
                    RegisterInfo result = new RegisterInfo(info.RegisterID, mode, key, handler);
                    RegisterList.Add(result);
                    Handlers.Add(result.RegisterID, handler);
                    break;
                }
            }
        }
        private void EditExistKeys(HotKeyEventHandler handler, ICollection<ModelKeys> modes, NormalKeys key)
        {
            uint target = (uint)modes.Aggregate((current, next) => current | next);

            if (CheckInProtectList(target, key)) { return; }

            foreach (RegisterInfo info in RegisterList.RegisterList)
            {
                if (info.Handler == handler)
                {
                    RemoveExistRegisterByID(info.RegisterID);
                    RegisterHotKey(WindowhWnd, info.RegisterID, target, (uint)key);
                    RegisterInfo result = new RegisterInfo(info.RegisterID, target, key, handler);
                    RegisterList.Add(result);
                    Handlers.Add(result.RegisterID, handler);
                    break;
                }
            }
        }
        private void EditExistKeys(HotKeyEventHandler handler, uint mode, NormalKeys key)
        {
            if (CheckInProtectList(mode, key)) { return; }

            foreach (RegisterInfo info in RegisterList.RegisterList)
            {
                if (info.Handler == handler)
                {
                    RemoveExistRegisterByID(info.RegisterID);
                    RegisterHotKey(WindowhWnd, info.RegisterID, mode, (uint)key);
                    RegisterInfo result = new RegisterInfo(info.RegisterID, mode, key, handler);
                    RegisterList.Add(result);
                    Handlers.Add(result.RegisterID, handler);
                    break;
                }
            }
        }
        private void EditExistHandler(ModelKeys mode, NormalKeys key, HotKeyEventHandler handler)
        {
            if (CheckInProtectList(mode, key)) { return; }

            foreach (RegisterInfo info in RegisterList.RegisterList)
            {
                if (info.ModelKey == (uint)mode && info.NormalKey == key)
                {
                    RemoveExistRegisterByID(info.RegisterID);
                    RegisterHotKey(WindowhWnd, info.RegisterID, (uint)mode, (uint)key);
                    RegisterInfo result = new RegisterInfo(info.RegisterID, mode, key, handler);
                    RegisterList.Add(result);
                    Handlers.Add(result.RegisterID, handler);
                    break;
                }
            }
        }
        private void EditExistHandler(ICollection<ModelKeys> modes, NormalKeys key, HotKeyEventHandler handler)
        {
            uint target = (uint)modes.Aggregate((current, next) => current | next);

            if (CheckInProtectList(target, key)) { return; }

            foreach (RegisterInfo info in RegisterList.RegisterList)
            {
                if (info.ModelKey == target && info.NormalKey == key)
                {
                    RemoveExistRegisterByID(info.RegisterID);
                    RegisterHotKey(WindowhWnd, info.RegisterID, target, (uint)key);
                    RegisterInfo result = new RegisterInfo(info.RegisterID, target, key, handler);
                    RegisterList.Add(result);
                    Handlers.Add(result.RegisterID, handler);
                    break;
                }
            }
        }
        private void EditExistHandler(uint mode, NormalKeys key, HotKeyEventHandler handler)
        {
            if (CheckInProtectList(mode, key)) { return; }

            foreach (RegisterInfo info in RegisterList.RegisterList)
            {
                if (info.ModelKey == mode && info.NormalKey == key)
                {
                    RemoveExistRegisterByID(info.RegisterID);
                    RegisterHotKey(WindowhWnd, info.RegisterID, mode, (uint)key);
                    RegisterInfo result = new RegisterInfo(info.RegisterID, mode, key, handler);
                    RegisterList.Add(result);
                    Handlers.Add(result.RegisterID, handler);
                    break;
                }
            }
        }

        private void RemoveAllHotKeys()
        {
            while (Counter >= 2004)
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
                if (Handlers.ContainsKey(id)) { Handlers.Remove(id); };

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
        private int RemoveExistRegisterByKeys(uint mode, NormalKeys key)
        {
            if (CheckInProtectList(mode, key)) { return -1; }
            foreach (RegisterInfo info in RegisterList.RegisterList)
            {
                if (info.ModelKey == mode && info.NormalKey == key)
                {
                    RemoveExistRegisterByID(info.RegisterID);
                    return info.RegisterID;
                }
            }
            return -1;
        }
        private int RemoveExistRegisterByKeys(ModelKeys mode, NormalKeys key)
        {
            if (CheckInProtectList(mode, key)) { return -1; }
            foreach (RegisterInfo info in RegisterList.RegisterList)
            {
                if (info.ModelKey == (uint)mode && info.NormalKey == key)
                {
                    RemoveExistRegisterByID(info.RegisterID);
                    return info.RegisterID;
                }
            }
            return -1;
        }
        private int RemoveExistRegisterByKeys(ICollection<ModelKeys> modes, NormalKeys key)
        {
            uint target = (uint)modes.Aggregate((current, next) => current | next);
            if (CheckInProtectList(target, key)) { return -1; }

            foreach (RegisterInfo info in RegisterList.RegisterList)
            {
                if (info.ModelKey == target && info.NormalKey == key)
                {
                    RemoveExistRegisterByID(info.RegisterID);
                    return info.RegisterID;
                }
            }
            return -1;
        }
        private void RemoveExistRegisterByHandler(HotKeyEventHandler handler)
        {
            List<int> target = new List<int>();
            foreach (RegisterInfo info in RegisterList.RegisterList)
            {
                if (info.Handler == handler)
                {
                    target.Add(info.RegisterID);
                }
            }
            foreach (int id in target)
            {
                if (!CheckInProtectList(id)) { RemoveExistRegisterByID(id); }
            }
        }

        private void AddProtectedHotKey(ModelKeys modelKey, NormalKeys normalKey)
        {
            foreach (var item in ProtectList)
            {
                if (item.Item1 == (uint)modelKey && item.Item2 == normalKey)
                {
                    return;
                }
            }
            ProtectList.Add(Tuple.Create((uint)modelKey, normalKey));
        }
        private void AddProtectedHotKey(ICollection<ModelKeys> modelKeys, NormalKeys normalKey)
        {
            uint target = (uint)modelKeys.Aggregate((current, next) => current | next);
            foreach (var item in ProtectList)
            {
                if (item.Item1 == target && item.Item2 == normalKey)
                {
                    return;
                }
            }
            ProtectList.Add(Tuple.Create(target, normalKey));
        }
        private void AddProtectedHotKey(uint modelKey, NormalKeys normalKey)
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
                    AddProtectedHotKey(info.ModelKey, info.NormalKey);
                    return;
                }
            }
        }

        private void RemoveProtectedHotKey(ModelKeys modelKey, NormalKeys normalKey)
        {
            foreach (var item in ProtectList)
            {
                if (item.Item1 == (uint)modelKey && item.Item2 == normalKey)
                {
                    ProtectList.Remove(item);
                    return;
                }
            }
        }
        private void RemoveProtectedHotKey(ICollection<ModelKeys> modelKeys, NormalKeys normalKey)
        {
            uint target = (uint)modelKeys.Aggregate((current, next) => current | next);

            foreach (var item in ProtectList)
            {
                if (item.Item1 == target && item.Item2 == normalKey)
                {
                    ProtectList.Remove(item);
                    return;
                }
            }
        }
        private void RemoveProtectedHotKey(uint modelKey, NormalKeys normalKey)
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
                    RemoveProtectedHotKey(info.ModelKey, info.NormalKey);
                    return;
                }
            }
        }

        private bool CheckInProtectList(ModelKeys modelKey, NormalKeys normalKey)
        {
            bool result = false;

            foreach (var item in ProtectList)
            {
                if (item.Item1 == (uint)modelKey && item.Item2 == normalKey)
                {
                    result = true;
                    break;
                }
            }

            return result;
        }
        private bool CheckInProtectList(ICollection<ModelKeys> modelKeys, NormalKeys normalKey)
        {
            bool result = false;
            uint target = (uint)modelKeys.Aggregate((current, next) => current | next);

            foreach (var item in ProtectList)
            {
                if (item.Item1 == target && item.Item2 == normalKey)
                {
                    result = true;
                    break;
                }
            }

            return result;
        }
        private bool CheckInProtectList(uint modelKey, NormalKeys normalKey)
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
                    result = CheckInProtectList(info.ModelKey, info.NormalKey);
                    break;
                }
            }

            return result;
        }

        #endregion
    }
}
