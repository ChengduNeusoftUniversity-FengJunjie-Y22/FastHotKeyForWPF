using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastHotKeyForWPF
{
    /// <summary>
    /// 注册信息的集合
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
        public RegisterInfo? this[int ID]
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
                return null;
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
