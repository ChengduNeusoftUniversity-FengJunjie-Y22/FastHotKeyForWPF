using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Input;

namespace FastHotKeyForWPF
{
    /// <summary>
    /// 控件池 , 基于【IAutoHotKeyProperty接口】与【IAutoHotKeyUpdate接口】，提供在View层保持热键唯一性的通用方案
    /// </summary>
    public static class BoxPool
    {
        internal static int ItemID = 0;

        internal static List<IAutoHotKeyProperty> PropertyItems = new List<IAutoHotKeyProperty>();
        internal static List<IAutoHotKeyUpdate> ModelItems = new List<IAutoHotKeyUpdate>();

        /// <summary>
        /// 向控件池添加控件
        /// </summary>
        /// <param name="prop">控件自身引用</param>
        /// <param name="model">控件ViewModel</param>
        /// <returns>int 在控件池中的唯一标识ID</returns>
        public static int Add(IAutoHotKeyProperty prop, IAutoHotKeyUpdate model)
        {
            ItemID++;
            model.PoolID = ItemID;
            PropertyItems.Add(prop);
            ModelItems.Add(model);
            return ItemID;
        }

        /// <summary>
        /// 从控件池中移除控件
        /// </summary>
        /// <param name="prop">控件自身引用</param>
        /// <param name="model">控件ViewModel</param>
        public static void Remove(IAutoHotKeyProperty prop, IAutoHotKeyUpdate model)
        {
            PropertyItems.Remove(prop);
            ModelItems.Remove(model);
        }

        /// <summary>
        /// 依据ViewModel,更新控件池中其它与此同Keys的控件
        /// </summary>
        /// <param name="prop">控件自身引用</param>
        public static void RemoveSame(IAutoHotKeyProperty prop)
        {
            List<IAutoHotKeyProperty> target = new List<IAutoHotKeyProperty>();
            List<int> position = new List<int>();

            int Counter = 0;

            foreach (var item in PropertyItems)
            {
                if ((prop.CurrentKeyA == item.CurrentKeyA) &&
                    (prop.CurrentKeyB == item.CurrentKeyB) &&
                    (prop.PoolID != ModelItems[Counter].PoolID))
                {
                    target.Add(item);
                    position.Add(Counter);
                    break;
                }
                Counter++;
            }


            for (int i = 0; i < target.Count; i++)
            {
                target[i].CurrentKeyA = new Key();
                target[i].CurrentKeyB = new Key();
                ModelItems[position[i]].UpdateText();
            }
        }
    }
}
