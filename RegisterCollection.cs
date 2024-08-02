using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FastHotKeyForWPF
{
    /// <summary>
    /// 注册信息的集合,提供了索引式的查询
    /// </summary>
    public class RegisterCollection
    {
        internal RegisterCollection() { }

        /// <summary>
        /// 注册信息
        /// </summary>
        public List<RegisterInfo> RegisterList { get; internal set; } = new List<RegisterInfo>();

        /// <summary>
        /// 将注册ID作为索引值,查询注册消息
        /// </summary>
        public RegisterInfo this[int ID]
        {
            get
            {
                foreach (RegisterInfo register in RegisterList)
                {
                    if (register.RegisterID == ID)
                    {
                        return register;
                    }
                }
                return new RegisterInfo();
            }
        }

        /// <summary>
        /// 将组合键作为索引,查询注册ID
        /// </summary>
        public RegisterInfo this[ModelKeys key1, NormalKeys key2]
        {
            get
            {
                foreach (RegisterInfo register in RegisterList)
                {
                    if (register.ModelKey == (uint)key1 && register.NormalKey == key2)
                    {
                        return register;
                    }
                }
                return new RegisterInfo();
            }
        }

        /// <summary>
        /// 将组合键作为索引,查询注册ID
        /// </summary>
        public RegisterInfo this[uint key1, NormalKeys key2]
        {
            get
            {
                foreach (RegisterInfo register in RegisterList)
                {
                    if (register.ModelKey == key1 && register.NormalKey == key2)
                    {
                        return register;
                    }
                }
                return new RegisterInfo();
            }
        }

        /// <summary>
        /// 将组合键作为索引,查询注册ID
        /// </summary>
        public RegisterInfo this[ICollection<ModelKeys> keys1, NormalKeys key2]
        {
            get
            {
                uint target = (uint)keys1.Aggregate((current, next) => current | next);

                foreach (RegisterInfo register in RegisterList)
                {
                    if (register.ModelKey == target && register.NormalKey == key2)
                    {
                        return register;
                    }
                }

                return new RegisterInfo();
            }
        }

        /// <summary>
        /// 将Handler作为索引,查询所有注册了此Handler的热键ID
        /// </summary>
        public List<RegisterInfo> this[HotKeyEventHandler handler]
        {
            get
            {
                List<RegisterInfo> list = new List<RegisterInfo>();

                foreach (RegisterInfo register in RegisterList)
                {
                    if (register.Handler == handler)
                    {
                        list.Add(register);
                    }
                }

                return list;
            }
        }

        /// <summary>
        /// 添加注册消息 - 通常由库自动完成
        /// </summary>
        public bool Add(RegisterInfo register)
        {
            foreach (RegisterInfo registerInfo in RegisterList)
            {
                if (registerInfo.RegisterID == register.RegisterID)
                {
                    return false;
                }
            }
            RegisterList.Add(register);
            return true;
        }

        /// <summary>
        /// 依据注册ID从集合中删除信息 - 通常由库自动完成
        /// </summary>
        public bool Remove(int ID)
        {
            RegisterInfo? register = null;

            foreach (RegisterInfo registerInfo in RegisterList)
            {
                if (registerInfo.RegisterID == ID)
                {
                    register = registerInfo;
                    break;
                }
            }

            if (register != null) { RegisterList.Remove(register); return true; }

            return false;
        }
    }
}
