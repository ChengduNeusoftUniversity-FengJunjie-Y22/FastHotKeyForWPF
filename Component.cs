using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastHotKeyForWPF
{
    public interface Component
    {
        //预制组件
        //PrefabComponent.GetComponent<T>的泛型约束指向的就是此接口下的类型
        //你也可以自定义一个UIElement并实现这个接口
    }
}
