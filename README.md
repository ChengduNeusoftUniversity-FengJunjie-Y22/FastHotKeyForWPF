# FastHotKeyForWPF
## ☆ NuGet documentation is no longer updated, please check out github or gitee
- [github][1]
- [gitee][2]

[1]: https://github.com/ChengduNeusoftUniversity-FengJunjie-Y22/FastHotKeyForWPF
[2]: https://gitee.com/CNU-FJj-Y22/FastHotKeyForWPF
## 目录
###### 目前只有核心功能函数比较全，还在补文档（一个人写真的很难，见谅）
- [项目简介](#项目简介)

<details>
<summary>★ 核心功能函数（从这里开始，可以快速了解如何使用它）</summary>

- [★ GlobalHotKey类](#GlobalHotKey类)

- [★ BindingRef类](#BindingRef类)

- [★ PrefabComponent类](#PrefabComponent类)

</details>

<details>
<summary>■ 其它内容（帮助你了解此类库的实现思路）</summary>

- [RegisterInfo类](#RegisterInfo类)

- [ComponentInfo类](#ComponentInfo类)

- [KeySelectBox类 / KeysSelectBox类](#KeySelectBox类/KeysSelectBox类)

</details>

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

<details>
<summary>枚举</summary>

##### Ⅰ 【enum ModelKeys : uint】 快捷键的修饰部分，目前支持以Ctrl\Alt作为修饰
##### Ⅱ 【enum NormalKeys : uint】 [Model+Normal]构成一个热键,目前支持【A-Z】【F1-F12】【0-9】【Up\Down\Left\Right】【Space】
##### Ⅲ 【enum FunctionTypes】 函数的返回值类型，Void\Return(无\有返回值)
</details>

<details>
<summary>委托</summary>

##### Ⅰ 【delegate void KeyInvoke_Void()】 支持将无参、无返回值的函数的签名作为参数，注册为热键的处理函数
##### Ⅱ 【delegate object KeyInvoke_Return()】 支持将无参、返回一个object的函数的签名作为参数，注册为热键的处理函数
</details>

## ★ GlobalHotKey类
#### 全局热键注册、修改、查询、销毁的主要实现
<details>
<summary>方法</summary>

| 方法名             | 参数                                                          | 返回值                 | 描述                                                       |
|--------------------|---------------------------------------------------------------|------------------------|------------------------------------------------------------|
| Awake              |                                                               |                        | 激活                                                       |
| Destroy            |                                                               |                        | 销毁                                                       |
| Add                | ( ModelKeys , NormalKeys , KeyInvoke_Void / KeyInvoke_Return )| Tuple( bool , string ) | 注册热键，它的处理函数是无参、无返回值的                   |
| EditHotKey_Keys    | ( KeyInvoke_Void / KeyInvoke_Return , ModelKeys , NormalKeys )|                        | 依据组合键查找对应的处理函数，并替换为新的处理函数         |
| EditHotKey_Function| ( ModelKeys , NormalKeys , KeyInvoke_Void / KeyInvoke_Return )|                        | 依据现有处理函数，查找能触发它的组合键，并替换为新的组合键 |
| Clear              |                                                               |                        | 清空热键，但不解除钩子                                     |
| DeleteById         | int                                                           |                        | 依据注册编号来清除注册的热键                               |
| DeleteByFunction   | KeyInvoke_Void / KeyInvoke_Return                             |                        | 依据函数签名来清除注册的热键 |
| DeleteByKeys       | ( enum ModelKeys , enum NormalKeys )                          |                        | 依据热键组合来清除注册的热键 |

</details>

<details>
<summary>可选项</summary>

| 属性名              | 类型                           | 默认                                 | 描述                                                             |
|---------------------|--------------------------------|--------------------------------------|------------------------------------------------------------------|
| IsDeBug             | bool                           | false                                | 是否进入调试模式（部分过程将使用MessageBox输出过程值）           |
| IsUpdate            | bool                           | true                                 | 是否实时监测返回值                                               |
| HOTKEY_ID           | int                            | 2004                                 | 第一个热键的注册编号，只建议在所有注册操作开始前修改一次         |
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

## ★ BindingRef类
#### 实时更新机制
<details>
<summary>方法</summary>

| 方法名       | 参数              | 返回值    | 描述                                                                                                  |
|--------------|-------------------|-----------|-------------------------------------------------------------------------------------------------------|
| Awake        |                   |           | 激活，当你使用GlobalHotKey.Awake()时，会自动激活一次                                                  |
| Destroy      |                   |           | 销毁                                                                                                  |
| BindingAutoEvent | KeyInvoke_Void    |           | 将某个自定义的函数绑定至BindingRef,当BindingRef接收到热键处理函数的返回值时，自动调用这个绑定的函数   |
| Update       | object?           |           | 更新数据，默认自动触发绑定给BindingRef的处理函数                                                      |
| Connect      | ( KeySelectBox , KeySelectBox , KeyInvoke_Void / KeyInvoke_Return  )           |           | 将两个组件、它们负责管理的处理函数相互连接，并接管 |
| Connect      | ( KeysSelectBox , KeyInvoke_Void / KeyInvoke_Return )           |           | 为KeysSelectBox指定一个处理函数，并接管 |
| DisConnect   | KeySelectBox      |           | 取消组件之间的连接以及它们接管的函数                                                  |
| DisConnect   | KeysSelectBox      |           | 取消KeysSelectBox接管的处理函数                                                  |
| GetKeysFromConnection| KeySelectBox | Tuple( enum ModelKeys? , enum NormalKeys? )|从一个处于连接状态的KeySelectBox获取键盘组合|
</details>

<details>
<summary>属性</summary>

| 属性名              | 类型                                                                 | 描述               |
|---------------------|----------------------------------------------------------------------|--------------------|
| Value               | object?                                                              | 监测到的最新值     |
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

            BindingRef.BindingAutoEvent(WhileUpdate);
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

## ★ PrefabComponent类
#### 如果你想迅速制作一个页面用于设置快捷方式，那么你可以使用此类型提供的预制组件
<details>
<summary>方法表</summary>

###### 目前只提供了KeySelectBox组件

| 方法名              | 参数                                        | 返回值    | 约束            | 描述                                               |
|---------------------|---------------------------------------------|-----------|-----------------|----------------------------------------------------|
|GetComponent         | < T >( )                                    | T         | Component接口   |获取组件--默认的组件信息                            |
|GetComponent         | < T >( ComponentInfo )                      | T         | Component接口   |获取组件--指定字体大小颜色；指定背景色；指定Margin  |
|ProtectSelectBox     | < T >( )                                    |           | KeyBox抽象类    |令指定类型的键盘盒子被锁定，不再接收用户按下的键    |
|UnProtectSelectBox   | < T >( )                                    |           | KeyBox抽象类    |解除保护状态                                        |
</details>

<details>
<summary>示例</summary>

#### 以下代码演示了如何使用预制组件快速构成快捷键设置界面,并将盒子变为圆角的
###### C#部分:

```csharp
using FastHotKeyForWPF;
using System.CodeDom;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TestDemo
{
    public partial class MainWindow : Window
    {
        private static ComponentInfo info = new ComponentInfo(20, Brushes.Black, Brushes.Wheat, new Thickness());
        //假设它们的字体大小颜色、背景色、Margin是相同的

        KeySelectBox k1 = PrefabComponent.GetComponent<KeySelectBox>(info);
        KeySelectBox k2 = PrefabComponent.GetComponent<KeySelectBox>(info);
        //这两个组件负责接管函数TestA

        KeySelectBox k3 = PrefabComponent.GetComponent<KeySelectBox>(info);
        KeySelectBox k4 = PrefabComponent.GetComponent<KeySelectBox>(info);
        //这两个组件负责接管函数TestB

        KeysSelectBox k5 = PrefabComponent.GetComponent<KeysSelectBox>(info);
        //这个组件负责接管函数TestC

        KeysSelectBox k6 = PrefabComponent.GetComponent<KeysSelectBox>(info);
        //这个组件负责接管函数TestD

        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            GlobalHotKey.Awake();
            //激活

            LoadingHotKeyPage();
            //加载组件

            base.OnSourceInitialized(e);
        }

        protected override void OnClosed(EventArgs e)
        {
            GlobalHotKey.Destroy();
            //销毁

            base.OnClosed(e);
        }

        private void LoadingHotKeyPage()
        {
            GlobalHotKey.IsDeBug = true;
            //调试模式会打印部分过程值，默认关闭

            Box1.Child = k1;
            Box2.Child = k2;
            Box3.Child = k3;
            Box4.Child = k4;
            Box5.Child = k5;
            Box6.Child = k6;
            //将组件设置为容器的子元素

            k1.UseFatherSize<Border>();
            k2.UseFatherSize<Border>();
            k3.UseFatherSize<Border>();
            k4.UseFatherSize<Border>();
            k5.UseFatherSize<Border>();
            k6.UseFatherSize<Border>();
            //令组件的宽高、字体大小与父容器相适应

            k1.UseStyleProperty("MyBox");
            k2.UseStyleProperty("MyBox");
            k3.UseStyleProperty("MyBox");
            k4.UseStyleProperty("MyBox");
            k5.UseStyleProperty("MyBox");
            k6.UseStyleProperty("MyBox");
            //为组件应用指定样式中的指定属性

            k1.IsDefaultColorChange = false;
            k2.IsDefaultColorChange = false;
            k3.IsDefaultColorChange = false;
            k4.IsDefaultColorChange = false;
            k5.IsDefaultColorChange = false;
            k6.IsDefaultColorChange = false;
            //关闭默认的焦点变色事件

            BindingRef.Connect(k1, k2, TestA);
            BindingRef.Connect(k3, k4, TestB);
            BindingRef.Connect(k5, TestC);
            BindingRef.Connect(k6, TestD);
            //接管指定函数

            GlobalHotKey.IsUpdate = true;
            //设为false会关闭监测返回值功能，默认打开
            BindingRef.BindingAutoEvent(WhileReceiveValue);
            //监测到返回值自动触发指定函数

            k1.Protect();
            k1.UnProtect();
            //锁定与解锁一个KeySelectBox组件的按键接收功能,KeysSelectBox组件也可以像这样锁定

            PrefabComponent.ProtectSelectBox<KeySelectBox>();
            PrefabComponent.UnProtectSelectBox<KeySelectBox>();
            //这是直接锁定与解锁指定类型的所有组件，优先级高于Protect()与UnProtect()
        }

        //k1与k2接管此函数
        private void TestA()
        {
            MessageBox.Show("测试A");
        }

        //k3与k4接管此函数
        private object TestB()
        {
            return "测试BB";
        }

        //k5接管此函数
        private void TestC()
        {
            MessageBox.Show("测试CCC");
        }

        //k6接管此函数
        private object TestD()
        {
            int a = 1;
            return a;
        }

        //监测到返回值时发生的事件 
        private void WhileReceiveValue()
        {
            if (BindingRef.Value == null)
            {
                //值为空时的处理办法

                return;
            }
            if (BindingRef.Value is string)
            {
                MessageBox.Show("B事件触发!");
                return;
            }
            if (BindingRef.Value is int)
            {
                MessageBox.Show("D事件触发!");
                return;
            }
        }

    }
}

```

###### XAML部分:
```xaml
<Window x:Class="TestDemo.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:TestDemo"
        mc:Ignorable="d"
        Title="MainWindow" Height="470" Width="800">
    <Window.Resources>
        <!--这是你自定义的资源样式，后续可以为组件使用样式中定义的属性-->
        <Style x:Key="MyBox" TargetType="TextBox">
            <Setter Property="FontSize" Value="36"/>
            <Setter Property="Foreground" Value="Cyan"/>
            <Setter Property="Background" Value="Transparent"/>
            <Setter Property="BorderThickness" Value="0"/>
            <Setter Property="BorderBrush" Value="Red"/>
        </Style>
        <Style x:Key="MyBorderA" TargetType="Border">
            <Setter Property="Background" Value="#29353d"/>
            <Setter Property="BorderThickness" Value="1"/>
            <Setter Property="Height" Value="50"/>
            <Setter Property="CornerRadius" Value="10"/>
        </Style>
        <Style x:Key="MyBorderB" TargetType="Border">
            <Setter Property="Background" Value="#29353d"/>
            <Setter Property="BorderThickness" Value="1"/>
            <Setter Property="Height" Value="50"/>
            <Setter Property="CornerRadius" Value="10"/>
        </Style>
    </Window.Resources>
    <Viewbox>
        <Grid Height="450" Width="800">
            <!--这两个Border是用来给预制组件KeySelectBox定位的-->
            <Border x:Name="Box1" Margin="174,131,404,269" Style="{StaticResource MyBorderA}"/>
            <Border x:Name="Box2" Margin="434,131,144,269" Style="{StaticResource MyBorderA}"/>

            <!--这两个Border是用来给预制组件KeySelectBox定位的-->
            <Border x:Name="Box3" Margin="174,202,404,198" Style="{StaticResource MyBorderA}"/>
            <Border x:Name="Box4" Margin="434,202,144,198" Style="{StaticResource MyBorderA}"/>

            <!--这个Border是用来给预制组件KeysSelectBox定位的-->
            <Border x:Name="Box5" Margin="174,277,144,123" Style="{StaticResource MyBorderB}"/>
            <!--这个Border是用来给预制组件KeysSelectBox定位的-->
            <Border x:Name="Box6" Margin="174,349,144,51" Style="{StaticResource MyBorderB}"/>

            
            
            <TextBlock Margin="28,130,569,270" Text="测试A" FontSize="40"/>
            <TextBlock Margin="28,200,569,200" Text="测试B" FontSize="40"/>
            <TextBlock Margin="28,276,569,124" Text="测试C" FontSize="40"/>
            <TextBlock Margin="28,348,569,52" Text="测试D" FontSize="40"/>
            <TextBlock Margin="28,30,404,370" Text="设置 => 快捷方式" FontSize="40"/>
        </Grid>
    </Viewbox>

</Window>

```

</details>

## RegisterInfo类
#### 用于表示注册信息
<details>
<summary>方法</summary>

| 方法名              | 参数                                                                 | 返回值    | 描述                        |
|---------------------|----------------------------------------------------------------------|-----------|-----------------------------|
|RegisterInfo         |( int , ModelKeys , NormalKeys , KeyInvoke_Void / KeyInvoke_Return )  |           | 初始化构造函数              |
|SuccessRegistration  |                                                                      | string    | 在注册成功时调用注册信息    |
|LoseRegistration     |( ModelKeys , NormalKeys , KeyInvoke_Void / KeyInvoke_Return )        | string    | 在注册失败时调用注册信息    |
</details>

<details>
<summary>属性</summary>

| 属性名              | 类型                                                                 | 描述               |
|---------------------|----------------------------------------------------------------------|--------------------|
| RegisterID          | int                                                                  | 注册编号           |
| Model               | enum:uint ModelKeys                                                  | 修饰键             |
| Normal              | enum:uint NormalKeys                                                 | 一般键             |
| FunctionType        | enum:uint FunctionTypes                                              | 处理函数类型       |
| Name                | string                                                               | 处理函数的函数名   |
| FunctionVoid        | delegate KeyInvoke_Void                                              | 处理函数           |
| FunctionReturn      | delegate KeyInvoke_Return                                            | 处理函数           |
</details>

<details>
<summary>注意</summary>

##### 一般来说，不会直接使用RegisterInfo的方法(无论是否为public static)，它的存在是为了便于在 GlobalHotKey中管理热键信息的登记、查询、修改。具体而言,当GlobalHotKey激活后，可以使用GlobalHotKey的静态方法HotKeyInfo()获取当前注册在列的所有热键消息的集合List,然后去访问这个集合中的RegisterInfo对象的属性。
</details>

## ComponentInfo类
#### 一个预制组件的基本信息
<details>
<summary>方法</summary>

| 方法名              | 类型                                                                 | 描述               |
|---------------------|----------------------------------------------------------------------|--------------------|
| ComponentInfo       |                                                                      | 实例化             |
| ComponentInfo       | ( double , SolidColorBrush , SolidColorBrush , Thickness )           | 实例化（字体大小、字体颜色、背景色、Margin）             |
</details>

<details>
<summary>属性</summary>

| 属性名              | 类型                                                                 | 描述               |
|---------------------|----------------------------------------------------------------------|--------------------|
| Foreground          | SolidColorBrush                                                      | 字体大小           |
| Background          | SolidColorBrush                                                      | 字体颜色           |
| Margin              | Thickness                                                            | 相对位置           |
</details>

## KeySelectBox类/KeysSelectBox类
#### 两个预制组件，同属于KeyBox,用于接收用户按下的Key，并在调用BindingRef提供的功能函数后，接管热键的注册、修改、销毁