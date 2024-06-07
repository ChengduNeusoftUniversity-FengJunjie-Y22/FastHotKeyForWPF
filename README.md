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
|GlobalHotKey属性   |类型                                |
|-------------------|------------------------------------|
|ProtectedHotKeys   |List<Tuple<ModelKeys, NormalKeys>>? |

#### 名单增删
|GlobalHotKey方法   |参数                                 |功能                              |
|-------------------|-------------------------------------|----------------------------------|
|ProtectHotKeyByKeys|( ModelKeys , NormalKeys )           |在受保护名单中新增热键组合（直接添加）        |
|ProtectHotKeyById  |int                                  |在受保护名单中新增热键组合（先依据id找到热键，再添加）       |
|UnProtectHotKeyByKeys|( ModelKeys , NormalKeys )         |解除指定热键的保护（直接解除）               |
|UnProtectHotKeyById  |int                                |解除指定热键的保护（先依据id找到热键，再解除）  |

###### 受保护的热键，无论注册与否，都不可对其执行注册、修改、删除操作（例如有些热键属于系统热键的范畴，不适合被修改，可以放到这个名单下）
</details>

<details>
<summary>(3)注册热键--不需要做设置界面</summary>

|GlobalHotKey类     |参数                                             |功能                              |
|-------------------|-------------------------------------------------|----------------------------------|
|Add                |( ModelKeys , NormalKeys , 处理函数 )            |注册热键【ModelKeys+ NormalKeys => 处理函数】|

#### 代码示例
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
#### XAML中,定义Border用于指定预制组件的位置
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
            <!--Border用于给预制组件定位-->
            <Border x:Name="Box1" Margin="109,39,341,361" Height="50"/>
            <Border x:Name="Box2" Margin="109,162,537,238" Height="50"/>
            <Border x:Name="Box3" Margin="305,162,341,238" Height="50"/>
        </Grid>
    </Viewbox>
</Window>
```
#### C#中,分别使用KeySelectBox与KeysSelectBox组件,自动地管理两个热键
```csharp
using FastHotKeyForWPF;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TestDemo
{
    public partial class MainWindow : Window
    {
        KeySelectBox? k1;
        KeySelectBox? k2;
        //k1与k2分别都只能接收一个按键,它们需要（k1,k2,处理函数）的连接才能自动管理热键
        //这里因为想直接设置成圆角组件,所以需要写成可空类型且不进行赋值或初始化操作

        KeysSelectBox k3 = PrefabComponent.GetComponent<KeysSelectBox>(RectComponentInfo);
        //k3可以同时接收两个按键,它需要（k3,处理函数）的连接就可以自动管理热键

        private static ComponentInfo RectComponentInfo = new ComponentInfo()
        //ComponentInfo只是一种属性值的存储媒介,它其实包含很多可填字段,但不同组件的不同功能,其实只会用到一部分存储的属性值,因此没有必要为所有属性赋值
        //例如，这条组件信息只是用于获取简单矩形组件，定义两个属性就够用了
        {
            Foreground = Brushes.Cyan,
            Background = Brushes.Black,
        };
        private static ComponentInfo RoundComponentInfo = new ComponentInfo()
        //例如,这条组件信息用于获取圆角组件,那你便需要设置以下属性以获取更好的效果
        {
            BorderBrush = Brushes.Red,
            BorderThickness = new Thickness(1),
            CornerRadius = new CornerRadius(10),
            Background = Brushes.LightGray,
            Foreground = Brushes.Cyan,
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
            Box1.Child = k3;
            k3.UseFatherSize<Border>();
            //非圆角组件的添加

            k1 = PrefabComponent.SetAsRoundBox<KeySelectBox>(Box2, RoundComponentInfo);
            k2 = PrefabComponent.SetAsRoundBox<KeySelectBox>(Box3, RoundComponentInfo);
            //向指定的Border对象嵌入预制组件，形成圆角

            k1.UseFocusTrigger(EnterColor, LeaveColor);
            k2.UseFocusTrigger(EnterColor, LeaveColor);
            k3.UseFocusTrigger(EnterColor, LeaveColor);
            //鼠标进出控件时，拥有变色效果

            k3.UseSuccessTrigger(WhileSuccessRegister);
            k1.UseSuccessTrigger(WhileSuccessRegister);
            k2.UseSuccessTrigger(WhileSuccessRegister);
            //成功注册热键时，给予用户提示

            k3.UseFailureTrigger(WhileFailRegister);
            k1.UseFailureTrigger(WhileFailRegister);
            k2.UseFailureTrigger(WhileFailRegister);
            //注册失败时，给予用户提示

            BindingRef.Connect(k1, k2, TestA);
            BindingRef.Connect(k3, InvokeHotKey);
            //创建连接关系，启用自动管理

            BindingRef.BindingAutoEvent(WhileGetObject);
            //InvokeHotKey()返回的object对象将被捕获，并在WhileGetObject()函数中，对这个object做进一步解析
        }

        private void TestA()//被k1和k2管理
        {
            MessageBox.Show("触发了TestA！");
        }

        private object InvokeHotKey()//被k3管理
        {
            return "热键被触发！";
        }

        private void WhileGetObject()//对监测到的object返回值做进一步操作
        {
            if (k3 == null) { return; }
            if (BindingRef.Value is string info)
            {
                MessageBox.Show($"接收到的返回值:{info}");
            }
        }

        //注意
        //新版本中，无论是鼠标进出事件还是注册结果提示事件，参数都是（object sender）
        //sender表示触发了这个事件的对象，你可以依据sender的具体类型，对组件做出修改

        private void WhileSuccessRegister(object sender)//若注册成功
        {
            MessageBox.Show("注册成功!");
        }

        private void WhileFailRegister(object sender)//若注册失败
        {
            if (sender is KeysSelectBox e)
            {
                e.Text = e.DefaultErrorText;
                //DefaultErrorText表示默认注册失败时，组件文本显示为什么内容，默认"Error"
            }
        }

        private void EnterColor(object sender)//鼠标进入组件时
        {
            if (sender is KeySelectBox e1)
            {
                e1.Foreground = Brushes.Red;

                Border? border = e1.Parent as Border;              
                if (border != null) { border.Background = Brushes.Wheat; }
                //注意这里如果要修改圆角组件的背景色，本质是需要修改父级容器的背景色，而非组件自身的背景色
            }
            else if (sender is KeysSelectBox e2)
            {
                Border? border = e2.Parent as Border;
                e2.Foreground = Brushes.Cyan;
                if (border != null) { border.Background = Brushes.Black; }
            }
        }

        private void LeaveColor(object sender)//鼠标离开组件时
        {
            if (sender is KeySelectBox e1)
            {
                Border? border = e1.Parent as Border;
                e1.Foreground = Brushes.Cyan;
                if (border != null) { border.Background = Brushes.LightGray; }
            }
            else if (sender is KeysSelectBox e2)
            {
                Border? border = e2.Parent as Border;
                e2.Foreground = Brushes.Red;
                if (border != null) { border.Background = Brushes.Wheat; }
            }
        }
    }
}
```
</details>

## 若预制组件无法满足你的美工需求,则需要完全了解以下API

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
<summary>若要与你自定义的控件结合使用,需要了解以下流程</summary>

### 1.热键的增删改查
#### GlobalHotKey注册热键 => Registers集合内的注册信息会跟随变动 => 通过访问Registers内的RegisterInfo对象，查询指定热键的全部信息 => 依据id号、热键、处理函数等信息，调用GlobalHotKey提供的系列方法，增删改全局热键 => Registers集合内的注册信息会跟随变动 => ……

### 2.实时监测机制
#### 例如你注册了【CTRL+F1】=>【TestA】这样一个热键，而TestA()具备一个object返回值，此时，只要按下【CTRL+F1】，就会触发TestA()并产生一个object对象，而BindingRef会监测到这个值，并自动触发BindingAutoEvent()绑定的处理函数。

### 3.一些特性
##### 【注册】操作会先执行一次清理，前置的Delete()函数是没有必要的
##### 【GlobalHotKey】提供的所有有关热键的变动操作，都会自动更新Registers集合，该属性只读

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

