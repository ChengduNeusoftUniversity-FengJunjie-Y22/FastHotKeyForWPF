# FastHotKeyForWPF
## ☆ NuGet文档不再更新,可选择以下路径查看最新文档
- [github][1]
- [gitee][2]

[1]: https://github.com/ChengduNeusoftUniversity-FengJunjie-Y22/FastHotKeyForWPF
[2]: https://gitee.com/CNU-FJj-Y22/FastHotKeyForWPF

## 快速入手(按照使用场景划分,只介绍常用API+代码示例)
<details>
<summary>(1)管理类库功能的启用/关闭</summary>

|GlobalHotKey类     |功能               |
|-------------------|-------------------|
|Awake              |激活全局热键功能   |
|Destroy            |关闭全局热键功能   |

##### 启用
```csharp
//需要重写MainWindow的OnSourceInitialized函数,这句函数执行时,窗口句柄已存在,确保了激活函数能够正确执行
protected override void OnSourceInitialized(EventArgs e)
{
    GlobalHotKey.Awake();
    //激活类库功能

    base.OnSourceInitialized(e);
}
```

##### 关闭
```csharp
//需要重写MainWindow的OnClosed函数,程序退出时,执行类库功能的关闭函数
protected override void OnClosed(EventArgs e)
{
    GlobalHotKey.Destroy();
    //关闭类库功能

    base.OnClosed(e);
}
```
###### 你也可以在别处调用Awake()或Destroy(),但请注意句柄的存在问题,以及Destroy()会销毁已注册的热键
</details>

<details>
<summary>(2)受保护的热键名单</summary>

#### 名单获取


#### 名单增删
|GlobalHotKey类     |功能               |
|-------------------|-------------------|
|Awake              |激活全局热键功能   |
|Destroy            |关闭全局热键功能   |

</details>

<details>
<summary>(3)管理热键--不需要做设置界面</summary>

|GlobalHotKey类     |参数                                             |功能                              |
|-------------------|-------------------------------------------------|----------------------------------|
|Add                |( ModelKeys , NormalKeys , 处理函数 )            |注册热键【ModelKeys+ NormalKeys => 处理函数】|
|EditHotKey_Keys    |( 目标处理函数 , 新的ModelKeys ，新的NormalKeys )|修改一个函数的触发热键            |
|EditHotKey_Function|( 目标ModelKeys , 目标NormalKeys ，新的处理函数) |修改一个热键对应的处理函数        |
|Clear			    |                                                 |清空注册的热键                    |
|DeleteByFunction   |( 目标处理函数 )                                 |依据函数签名来清除注册的热键      |
|DeleteByKeys	    |( ModelKeys , NormalKeys )                       |依据热键组合来清除注册的热键      |
|DeleteById         |( int )                                          |依据热键注册时的id号(自动递增)清除注册的热键|

##### 注册热键
```csharp
//1.自定义一个热键加载函数(LoadHotKey)
//2.自定义一个函数作为热键触发的事件(假定你自定义了一个TestA函数)
private void LoadHotKey()
{
    GlobalHotKey.Add(ModelKeys.CTRL, NormalKeys.F1, TestA);
    //使用Add()注册热键
    //ModelKeys支持使用CTRL与ALT
    //NormalKeys支持使用数字、字母、Fx
    //TestA条件：必须是一个不包含任何参数的函数；允许为【void函数】或者【返回一个object对象的函数】
}
private void TestA()
{
    MessageBox.Show("测试A");
}
```
```csharp
//3.在Awake()成功执行后,调用加载函数
protected override void OnSourceInitialized(EventArgs e)
{
    GlobalHotKey.Awake();

    LoadHotKey();
    //到这里就完成注册了,不过需要注意的是,如果这个热键在系统中有对应功能,那可能会导致一些意料之外的情况

    base.OnSourceInitialized(e);
}
```

##### 编辑热键


##### 删除热键


</details>

<details>
<summary>(4)管理热键--运用组件自动管理热键、制作简易的热键设置界面</summary>

##### 支持的组件
|类型                   |实现                                     |功能                              |
|-----------------------|-----------------------------------------|----------------------------------|
|KeySelectBox           |KeyBox:TextBox,Component                 |接收单个用户按下的键              |
|KeysSelectBox          |KeyBox:TextBox,Component                 |接收两个用户按下的键              |

##### 组件信息的表示
|ComponentInfo对象字段  |类型                        |默认|
|-----------------------|----------------------------|----|
|FontSize               |double                      |1   |
|Width                  |double                      |400 |
|Height                 |double                      |100 |
|FontSizeRate           |double                      |0.8 |
|WidthRate              |double                      |1   |
|HeightRate             |double                      |1   |
|Background             |SolidColorBrush             |Transparent |
|Foreground             |SolidColorBrush             |Transparent |
|BorderBrush            |SolidColorBrush             |Transparent |
|BorderThickness        |Thickness                   |0    |
|CornerRadius           |CornerRadius                |0    |
|Margin                 |Thickness                   |0    |

##### 组件对象的通用方法
|方法                   |参数                                     |功能                              |
|-----------------------|-----------------------------------------|----------------------------------|
|UseFatherSize< T >     |T表示父级容器的类型                      |自适应父级容器的大小,并依据父级容器高度的70%自适应字体大小              |
|UseStyleProperty       |( string , string[] )                    |通过指定的样式名称string找到自定义的Style，再根据string[]中，希望应用的属性的名称，应用Style中的部分属性              |
|UseFocusTrigger        |( TextBoxFocusChange enter , TextBoxFocusChange leave)|填入两个自定义的函数,用于处理鼠标进入和离开时,要触发的事情,它们一定是void、具备一个TextBox参数的函数     |
|Protect                |                                         |锁定该组件               |
|UnProtect              |                                         |解锁该组件               |

##### 组件的统一管理
|PrefabComponent类      |参数                                     |功能                              |
|-----------------------|-----------------------------------------|----------------------------------|
|GetComponent< T >      |                                         |获取指定类型的组件(默认样式的)    |
|GetComponent< T >      |( ComponentInfo )                        |获取指定类型的组件(可指定部分样式的)|
|ProtectSelectBox< T >  |                                         |锁定所有T类型的组件               |
|UnProtectSelectBox< T >|                                         |解锁所有T类型的组件               |
|SetAsRoundBox< T >     |( Border )                               |令指定的Border控件变为圆角盒子(默认样式)|
|SetAsRoundBox< T >     |( Border ，ComponentInfo )               |令指定的Border控件变为圆角盒子(可指定部分样式的)|

##### 建立通信
|BindingRef类           |参数                                     |功能                              |
|-----------------------|-----------------------------------------|----------------------------------|
|Connect                |(KeysSelectBox , 处理函数 )              |建立预制组件与处理函数间的绑定，激活热键自动管理|
|Connect                |(KeySelectBox ，KeySelectBox , 处理函数 )|建立预制组件与处理函数间的绑定，激活热键自动管理|
|DisConnect             |( 预制组件 )                             |取消预制组件与处理函数间的绑定，不再自动管理热键|
|BindingAutoEvent       |( 处理函数 )                             |指定一个函数用于响应接收到object返回值时要做的事情|
|RemoveAutoEvent        |                                         |清除响应函数|

### 代码示例
##### XAML中,定义一个Border用于指定预制组件的位置
```xaml
<Window x:Class="TestDemo.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:TestDemo"
        mc:Ignorable="d"
        Title="MainWindow" Height="470" Width="800">
    <Viewbox>
        <Grid Height="450" Width="800">
            <!--Border用于给预制组件KeysSelectBox定位-->
            <Border x:Name="Box1" Margin="174,131,404,269" Height="50"/>
        </Grid>
    </Viewbox>
</Window>
```
##### C# 添加非圆角组件
```csharp
using FastHotKeyForWPF;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TestDemo
{
    public partial class MainWindow : Window
    {
        KeysSelectBox ksb = PrefabComponent.GetComponent<KeysSelectBox>(componentInfo);
        //使用PrefabComponent提供的方法获取组件

        private static ComponentInfo componentInfo = new ComponentInfo()
        //当ComponentInfo用于获取预制组件时(非圆角应用场景），需要为其设置的信息也不同，它只是个便于存储属性值的类型，并没有规定必须填写哪几个属性
        {
            Foreground = Brushes.Cyan,
            Background = Brushes.Black,
        };

        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            GlobalHotKey.Awake();
            //激活

            LoadHotKeys();
            //加载组件

            base.OnSourceInitialized(e);
        }

        protected override void OnClosed(EventArgs e)
        {
            GlobalHotKey.Destroy();
            //销毁

            base.OnClosed(e);
        }

        private void LoadHotKeys()
        {
            Box1.Child = ksb;
            //这种模式需要手动将预制组件放入容器内

            ksb.UseFatherSize<Border>(0.8);
            //自适应父级容器大小,0.8是字体应用的比率,可以不填写或自己指定比率

            ksb.UseStyleProperty("MyBoxStyle");
            //使用XAML中自定义的样式资源"MyBoxStyle"的所有属性,没找到的话,什么也不做

            ksb.UseStyleProperty("MyBoxStyle", new string[] { "Width", "Height" });
            //只应用"MyBoxStyle"中的"Width"和"Height"属性

            ksb.IsDefaultColorChange = false;
            //关闭默认的焦点进出事件(1.1.6开始，默认都是关闭的)

            ksb.UseFocusTrigger(Enter, Leave);
            //使用你自定义的焦点进出事件
        }

        private void Enter(TextBox e)
        {
            //当鼠标进入预制组件时
        }

        private void Leave(TextBox e)
        {
            //当鼠标离开预制组件时
        }
    }
}
```
##### C# 添加圆角组件
```csharp
using FastHotKeyForWPF;
using System.Windows;
using System.Windows.Media;

namespace TestDemo
{
    public partial class MainWindow : Window
    {
        KeysSelectBox? ksb;
        //预制组件不可在类库外部被直接实例化,需要通过PrefabComponent提供的相关方法来获取

        ComponentInfo componentInfo = new ComponentInfo()
        //预制组件的样式信息
        {
            BorderBrush = Brushes.Cyan,
            BorderThickness = new Thickness(1),
            Background = Brushes.Black,
            Foreground = Brushes.Cyan,

            CornerRadius = new CornerRadius(5),
            //设置圆角半径

            FontSizeRate = 0.8,
            //字体大小比率 * 父级容器的高度 = 预制组件的字体大小
            //注意父级Border容器的高度不可为double.NAN,需要显式地指定高度
        };

        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            GlobalHotKey.Awake();
            LoadHotKeys();
            base.OnSourceInitialized(e);
        }

        protected override void OnClosed(EventArgs e)
        {
            GlobalHotKey.Destroy();
            base.OnClosed(e);
        }

        private void LoadHotKeys()
        {
            ksb = PrefabComponent.SetAsRoundBox<KeysSelectBox>(Box1, componentInfo);
        }
    }
}
```
### ☆建立预制组件与处理函数的联系,激活热键的全自动管理！
```csharp
using FastHotKeyForWPF;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TestDemo
{
    public partial class MainWindow : Window
    {
        KeysSelectBox ksb = PrefabComponent.GetComponent<KeysSelectBox>(componentInfo);

        private static ComponentInfo componentInfo = new ComponentInfo()
        {
            Foreground = Brushes.Cyan,
            Background = Brushes.Black,
        };

        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            GlobalHotKey.Awake();
            //激活

            LoadHotKeys();
            //加载组件

            base.OnSourceInitialized(e);
        }

        protected override void OnClosed(EventArgs e)
        {
            GlobalHotKey.Destroy();
            //销毁

            base.OnClosed(e);
        }

        private void LoadHotKeys()
        {
            Box1.Child = ksb;

            BindingRef.BindingAutoEvent(WhileGetObject);
            //InvokeHotKey返回一个object,若你希望实时监测InvokeHotKey的触发情况,并拿到这个object做进一步的处理，那么可以通过这句函数激活实时监测返回值的功能

            GlobalHotKey.IsUpdate = false;
            //暂时关闭实时监测，但不清除响应函数

            BindingRef.RemoveAutoEvent();
            //清除实时响应

            BindingRef.Connect(ksb, InvokeHotKey);
            //ksb将依据用户输入的按键，自动管理热键，自动注册的热键将去触发自定义的InvokeHotKey函数
            //这里ksb是一种能同时接收两个按键的预制组件，如果你使用一次接收一个按键的盒子，Connect()需要同时填入两个盒子+一个处理函数
            //可以同时注册多个具备object返回值的处理函数，任何一个被触发，都可以自动响应绑定的WhileGetObject()

            BindingRef.DisConnect(ksb);
            //解除连接关系，关闭自动管理
            //如果是两个单键盒子被连接，只需要填入其中任何一个盒子，就能解除
        }

        private object InvokeHotKey()
        {
            return "热键被触发！";
        }

        private void WhileGetObject()
        {
            if (ksb != null) { return; }
            if (BindingRef.Value is string info)
            //这里拿到最新的返回值，并根据object的实际类型，做出不同的处理
            {
                MessageBox.Show($"接收到的返回值:{info}");
            }
        }
    }
}
```

</details>

## 若预制组件无法满足你的美工需求,则建议完全了解以下API

<details>
<summary>API</summary>

### Ⅰ GlobalHotKey
|方法               |参数                                             |功能                              |
|-------------------|-------------------------------------------------|----------------------------------|
|Awake              |                                                 |激活全局热键功能   |
|Destroy            |                                                 |关闭全局热键功能   |
|Add                |( ModelKeys , NormalKeys , 处理函数 )            |注册热键【ModelKeys+ NormalKeys => 处理函数】|
|EditHotKey_Keys    |( 目标处理函数 , 新的ModelKeys ，新的NormalKeys )|修改一个函数的触发热键            |
|EditHotKey_Function|( 目标ModelKeys , 目标NormalKeys ，新的处理函数) |修改一个热键对应的处理函数        |
|Clear			    |                                                 |清空注册的热键                    |
|DeleteByFunction   |( 目标处理函数 )                                 |清除注册的热键(依据函数签名)      |
|DeleteByKeys	    |( ModelKeys , NormalKeys )                       |清除注册的热键(依据热键组合)      |
|DeleteById         |( int )                                          |清除注册的热键(依据注册id号)|
|ProtectHotKeyByKeys|( ModelKeys , NormalKeys )                       |添加受保护的热键(依据组合键)|
|ProtectHotKeyById  |( int )                                          |添加受保护的热键键(依据注册id)|
|UnProtectHotKeyByKeys|( ModelKeys , NormalKeys )                     |删除受保护的热键(依据组合键)|
|UnProtectHotKeyById  |( int )                                        |删除受保护的热键(依据注册id)|

|属性               |类型       |默认       |含义                              |
|-------------------|-----------|-----------|----------------------------------|
|IsDeBug            |bool       |false      |若为true,部分过程将打印过程值     |
|IsUpdate           |bool       |true       |若为true,则启用BindingRef的实时监测返回值     |
|HOTKEY_ID          |int        |2004       |第一个热键的注册id（递增）     |
|ReturnValue        |object?    |null       |接收到的最新返回值     |
|Registers          |List< RegisterInfo >       |           |注册在列的所有热键的信息     |
|ProtectedHotKeys   |List< Tuple< ModelKeys , NormalKeys > >       |           |受保护的热键，不允许增删改     |

### Ⅱ BindingRef
|方法               |参数                                             |功能                              |
|-------------------|-------------------------------------------------|----------------------------------|
|Awake              |                                                 |激活监测功能，在GlobalHotKey苏醒时自动调用一次   |
|Destroy            |                                                 |关闭监测功能，在GlobalHotKey关闭时自动调用一次   |
|BindingAutoEvent   |( 一个处理函数 )                                 |接收到返回值时，自动触发此处绑定的处理函数   |
|RemoveAutoEvent    |                                                 |依然监测返回值，但是监测到返回值后，不再触发事件   |
|Connect            |( KeySelectBox , KeySelectBox , 处理函数 )       |连接两个预制组件+一个处理函数,激活热键的自动管理   |
|Connect            |( KeysSelectBox  , 处理函数 )                    |连接一个预制组件+一个处理函数，激活热键的自动管理   |
|DisConnect         |( KeysSelectBox )                                |取消连接   |
|DisConnect         |( KeySelectBox )                                 |取消连接   |

|属性               |类型       |默认       |含义                              |
|-------------------|-----------|-----------|----------------------------------|
|Value              |object?    |null       |最新接收到的返回值                |

### Ⅲ RegisterInfo
|方法               |参数                                  |返回      |功能                  |
|-------------------|--------------------------------------|----------|----------------------|
|SuccessRegistration|                                      |string    |手动获取成功的注册信息|
|LoseRegistration   |( ModelKeys , NormalKeys , 处理函数 ) |string    |手动获取失败的注册信息|

|属性                   |类型                        |含义        |
|-----------------------|----------------------------|------------|
|RegisterID             |int                         |注册id，起始为2004，自动递增        |
|Model                  |ModelKeys                   |热键左半 |
|Normal                 |NormalKeys                  |热键右半 |
|FunctionType           |FunctionTypes               |热键对应处理函数的函数类型 |
|Name                   |string                      |处理函数的函数名   |
|FunctionVoid           |KeyInvoke_Void              |可能的处理函数A   |
|FunctionReturn         |KeyInvoke_Return            |可能的处理函数B   |

</details>

<details>
<summary>美术需求高时,以下思路可以较好地将类库功能与你自定义的控件相结合</summary>


</details>

## 更新合集
[前往Bilibili查看各个版本的视频演示][1]

[1]: https://www.bilibili.com/video/BV1rr421L7qR

<details>
<summary>Version 1.1.5</summary>

#### (1)修复焦点离开盒子后，盒子仍然在接收用户按键的bug
#### (2)修复无法使用实例方法Protect()锁定盒子的问题
#### (3)新增组件之间的通信机制,自动清理重复的热键

</details>

<details>
<summary>Version 1.1.6</summary>

#### (1)提供开箱即用的圆角组件
#### (2)默认不使用变色效果,需要用户自定义对应函数
#### (3)非DeBug模式下再无注册成功与否的提示,需要用户自定义对应函数
#### (4)新增一个保护名单,名单中的任何热键不允许被增删改,即便这个热键没有被注册过
#### (5)新增静态属性,用于获取注册信息和保护名单

</details>

