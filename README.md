# FastHotKeyForWPF
#### NuGet文档已停止更新，请前往github或gitee查看
- [github][1]
- [gitee][2]

[1]: https://github.com/ChengduNeusoftUniversity-FengJunjie-Y22/FastHotKeyForWPF
[2]: https://gitee.com/CNU-FJj-Y22/FastHotKeyForWPF
### 目录 （带 ☆ 的类型通常提供了大部分的核心功能，较为常用）
- [项目简介](#项目简介)

- [☆ GlobalHotKey类](#GlobalHotKey类)

- [☆ BindingRef类](#BindingRef类)

- [☆ PrefabComponent类](#PrefabComponent类)

- [RegisterInfo类](#RegisterInfo类)

- [ComponentInfo类](#ComponentInfo类)

- [KeySelectBox类](#KeySelectBox类)

## 项目简介

<details>
<summary>功能</summary>

#### 这是一款WPF类库项目，旨在用一种更优雅的方式管理WPF全局热键
#### 特点
##### (1)功能函数简洁易用
##### (2)热键处理函数可高度自定义
##### (3)可自动监测热键处理函数的返回值
##### (4)提供预制的【组件】，可用于快速构建快捷键的设置界面

</details>

<details>
<summary>作者</summary>

##### 关于作者本人
###### 写这个项目的时候作者在上大二，一般都是兴致来了就维护一下项目，正常来说会泡在二游里
##### 联系方式
###### Bilibili: "真的不来一杯嘛"
###### QQ: 2789083329
###### WeChat: WeC_FZJSOP4996

</details>

<details>
<summary>获取此类库</summary>

#### NuGet
##### 解决方案资源管理器 => 右键项目 => 管理NuGet程序包 => 搜索FastHotKeyForWPF => 安装最新版 （需要修改项目的目标框架属性为.NET 8.0）
#### Github & Gitee
##### 下载Zip => 解压 => 将本项目与WPF项目放在同一个解决方案下 => 右键引用本项目

</details>

## 前置定义
##### Ⅰ 【enum ModelKeys : uint】 快捷键的修饰部分，目前支持以Ctrl\Alt作为修饰
##### Ⅱ 【enum NormalKeys : uint】 [Model+Normal]构成一个热键,目前支持【A-Z】【F1-F12】【0-9】【Up\Down\Left\Right】【Space】
##### Ⅲ 【enum FunctionTypes】 函数的返回值类型，Void\Return(有\无返回值)
##### Ⅳ 【delegate void KeyInvoke_Void()】 支持将无参、无返回值的函数的签名作为参数，注册为热键的处理函数
##### Ⅴ 【delegate object KeyInvoke_Return()】 支持将无参、返回一个object的函数的签名作为参数，注册为热键的处理函数

## GlobalHotKey类
#### 全局热键注册、修改、查询、销毁的主要实现
<details>
<summary>方法表</summary>

| 方法名             | 参数                                                          | 返回值                 | 权限            | 描述                                                       |
|--------------------|---------------------------------------------------------------|------------------------|-----------------|------------------------------------------------------------|
| Awake              |                                                               |                        | public static   | 激活                                                       |
| Destroy            |                                                               |                        | public static   | 销毁                                                       |
| Add                | ( ModelKeys , NormalKeys , KeyInvoke_Void / KeyInvoke_Return )| Tuple( bool , string ) | public static   | 注册热键，它的处理函数是无参、无返回值的                   |
| EditHotKey_Keys    | ( KeyInvoke_Void / KeyInvoke_Return , ModelKeys , NormalKeys )|                        | public static   | 依据组合键查找对应的处理函数，并替换为新的处理函数         |
| EditHotKey_Function| ( ModelKeys , NormalKeys , KeyInvoke_Void / KeyInvoke_Return )|                        | public static   | 依据现有处理函数，查找能触发它的组合键，并替换为新的组合键 |
| Clear              |                                                               |                        | public static   | 清空热键，但不解除钩子                                     |
| DeleteById         | int                                                           |                        | public static   | 依据注册编号来删除注册的热键                               |
| DeleteByFunction   | KeyInvoke_Void / KeyInvoke_Return                             |                        | public static   | 一个函数允许由多个热键触发，调用该函数可清除一个函数的所有触发组合键 |

</details>

<details>
<summary>可选项</summary>

| 属性名              | 类型                           | 默认                                 | 权限            | 描述                                                             |
|---------------------|--------------------------------|--------------------------------------|-----------------|------------------------------------------------------------------|
| IsDeBug             | bool                           | false                                | public static   | 是否进入调试模式（部分过程将使用MessageBox输出过程值）           |
| IsUpdate            | bool                           | true                                 | public static   | 是否实时监测返回值                                               |
| HOTKEY_ID           | int                            | 2004                                 | public static   | 第一个热键的注册编号，只建议在所有注册操作开始前修改一次         |
</details>

<details>
<summary>示例</summary>

#### Ⅰ 以下代码演示了如何使用Awake()与Destroy()管理GlobalHotKey的激活与销毁
##### 当然，你也可以在其它地方使用这两个方法，但是需要注意 : Awake()函数需要在MainWindow的句柄已经存在时，才去调用。这里重写OnSourceInitialized与OnClosed是比较推荐的方案
```csharp
using FastHotKeyForWPF;

namespace TestForHotKeyDll
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        protected override void OnSourceInitialized(EventArgs e)
        {
            GlobalHotKey.Awake();//激活
            base.OnSourceInitialized(e);
        }
        protected override void OnClosed(EventArgs e)
        {
            GlobalHotKey.Destroy();//销毁
            base.OnClosed(e);
        }
    }
}
```

#### Ⅱ 以下代码演示了如何使用Add()注册热键、如何使用EditHotKey_Keys()与EditHotKey_Function()修改热键
##### 注意:
##### (1)在你使用Add()函数注册热键时，若这个按键组合已经注册过了，则新的会覆盖旧的，且注册ID不同于旧的。
##### (2)一个处理函数可以注册多个热键，但一个按键组合只能注册一个热键，EditHotKey_Keys()本质上是先根据函数名找到第一个对应的按键组合，然后调用Add()去重新注册 
```csharp
using System.Windows;
using FastHotKeyForWPF;

namespace TestForHotKeyDll
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        protected override void OnSourceInitialized(EventArgs e)
        {
            GlobalHotKey.Awake();

            GlobalHotKey.Add(ModelKeys.CTRL, NormalKeys.F1, Test1);
            //注册 => CTRL+F1 => Test1()

            GlobalHotKey.EditHotKey_Keys(Test1, ModelKeys.ALT, NormalKeys.E);
            //保留Test1(),修改 => CTRL+F1 => ALT+E
            GlobalHotKey.EditHotKey_Function(ModelKeys.ALT, NormalKeys.E, Test2);
            //保留ALT+E,修改 => Test1() => Test2()

            //于是，最初注册的[CTRL+F1 => Test1]经过两次修改变成了[ALT+E => Test2]

            base.OnSourceInitialized(e);
        }
        protected override void OnClosed(EventArgs e)
        {
            GlobalHotKey.Destroy();
            base.OnClosed(e);
        }
        private void Test1()
        {
            MessageBox.Show("1");
        }
        private object Test2()
        {
            return "2";
        }

    }
}
```

#### Ⅲ 以下代码演示了如何使用DeleteByFunction()销毁有关于一个函数的所有热键
```csharp
using System.Windows;
using FastHotKeyForWPF;

namespace TestForHotKeyDll
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        protected override void OnSourceInitialized(EventArgs e)
        {
            GlobalHotKey.Awake();

            GlobalHotKey.Add(ModelKeys.CTRL, NormalKeys.F1, Test1);
            GlobalHotKey.Add(ModelKeys.CTRL, NormalKeys.F2, Test1);
            GlobalHotKey.Add(ModelKeys.CTRL, NormalKeys.F3, Test1);
            GlobalHotKey.Add(ModelKeys.CTRL, NormalKeys.F4, Test1);
            //允许多个热键触发同一函数

            GlobalHotKey.Add(ModelKeys.CTRL, NormalKeys.F5, Test2);
            //这是Test1的对照组

            BindingRef.BindingEvent(WhileUpdate);

            GlobalHotKey.DeleteByFunction(Test1);
            //删除指定函数名下所有的热键，这一步操作执行完，就只剩下CTRL+F5 => Test2() 了

            base.OnSourceInitialized(e);
        }
        protected override void OnClosed(EventArgs e)
        {
            GlobalHotKey.Destroy();
            base.OnClosed(e);
        }
        private object Test1()
        {
            return "测试1";
        }

        private object Test2()
        {
            return "测试2";
        }

        private void WhileUpdate()
        {
            foreach (var info in GlobalHotKey.HotKeyInfo())
            {
                MessageBox.Show(info.SuccessRegistration());
                //最后只会存在CTRL+F5 => Test2() ,Test1()相关的全部热键均被清除
            }
        }

    }
}
```

</details>

## BindingRef类
#### 实时更新机制
<details>
<summary>方法表</summary>

| 方法名       | 参数              | 返回值    | 权限            | 描述                                                                                                  |
|--------------|-------------------|-----------|-----------------|-------------------------------------------------------------------------------------------------------|
| Awake        |                   |           | public static   | 激活，当你使用GlobalHotKey.Awake()时，会自动激活一次                                                  |
| Destroy      |                   |           | public static   | 销毁                                                                                                  |
| BindingEvent | KeyInvoke_Void    |           | public static   | 将某个自定义的函数绑定至BindingRef,当BindingRef接收到热键处理函数的返回值时，自动调用这个绑定的函数   |
| Update       | object?           |           | private         | 更新数据，默认自动触发绑定给BindingRef的处理函数                                                      |
</details>

<details>
<summary>属性表</summary>

| 属性名              | 类型                                                                 | 权限            | 描述               |
|---------------------|----------------------------------------------------------------------|-----------------|--------------------|
| Value               | object?                                                              | public static   | 监测到的最新值     |
</details>

<details>
<summary>示例</summary>

#### 以下代码演示了实时更新机制的运用
##### 简单来说，当CTRL+F1被按下，会触发自定义函数Test(),而Test()具备返回值,你可能会希望拿到这个返回值并用它做些其它的事情，BindingRef支持自动监测并自动触发这个所谓的“其它的事情”
```csharp
using System.Windows;
using FastHotKeyForWPF;

namespace TestForHotKeyDll
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        protected override void OnSourceInitialized(EventArgs e)
        {
            GlobalHotKey.Awake();

            GlobalHotKey.IsUpdate = true;
            //这个开关决定了BindingRef是否处于实时监测状态,默认为true

            GlobalHotKey.Add(ModelKeys.CTRL, NormalKeys.F1, Test);
            //此处注册的Test()具备object返回值，而BindingRef会实时监测是否有object返回值的产生

            BindingRef.BindingEvent(WhileUpdate);
            //若BindingRef监测到一次返回值，则自动调用注册的WhileUpdate()函数来做进一步的处理

            base.OnSourceInitialized(e);
        }
        protected override void OnClosed(EventArgs e)
        {
            GlobalHotKey.Destroy();
            base.OnClosed(e);
        }
        private object Test()
        {
            return "测试";
        }

        private void WhileUpdate()
        {
            MessageBox.Show(BindingRef.Value.ToString());
            //这里可以获取监测到的最新返回值，并做进一步处理

            foreach (var info in GlobalHotKey.HotKeyInfo())
            {
                MessageBox.Show(info.SuccessRegistration());
                //从GlobalHotKey获取注册在列的所有热键信息并打印
            }
        }

    }
}
```

</details>

## PrefabComponent类
#### 如果你想迅速制作一个页面用于设置快捷方式，那么你可以使用此类型提供的预制组件

## RegisterInfo类
#### 用于表示注册信息
<details>
<summary>方法表</summary>

| 方法名              | 参数                                                                 | 返回值    | 权限            | 描述                       |
|---------------------|----------------------------------------------------------------------|-----------|-----------------|----------------------------|
|RegisterInfo         |( int , ModelKeys , NormalKeys , KeyInvoke_Void / KeyInvoke_Return )  |           | public          |初始化构造函数              |
|SuccessRegistration  |                                                                      | string    | public          |在注册成功时调用注册信息    |
|LoseRegistration     |( ModelKeys , NormalKeys , KeyInvoke_Void / KeyInvoke_Return )        | string    | public static   |在注册失败时调用注册信息    |
</details>

<details>
<summary>属性表</summary>

| 属性名              | 类型                                                                 | 权限            | 描述               |
|---------------------|----------------------------------------------------------------------|-----------------|--------------------|
| RegisterID          | int                                                                  | public readonly | 注册编号           |
| Model               | enum:uint ModelKeys                                                  | public readonly | 修饰键             |
| Normal              | enum:uint NormalKeys                                                 | public readonly | 一般键             |
| FunctionType        | enum:uint FunctionTypes                                              | public readonly | 处理函数类型       |
| Name                | string                                                               | public readonly | 处理函数的函数名   |
| FunctionVoid        | delegate KeyInvoke_Void                                              | public          | 处理函数           |
| FunctionReturn      | delegate KeyInvoke_Return                                            | public          | 处理函数           |
</details>

<details>
<summary>注意</summary>

##### 一般来说，不会直接使用RegisterInfo的方法(无论是否为public static)，它的存在是为了便于在 GlobalHotKey中管理热键信息的登记、查询、修改。具体而言,当GlobalHotKey激活后，可以使用GlobalHotKey的静态方法HotKeyInfo()获取当前注册在列的所有热键消息的集合List,然后去访问这个集合中的RegisterInfo对象的属性。
</details>

## ComponentInfo类
#### 一个预制组件的基本信息

## KeySelectBox类
#### 继承自WPF的TextBox类,是PrefabComponent支持的组件类型之一