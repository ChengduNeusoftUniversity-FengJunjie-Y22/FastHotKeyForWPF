# FastHotKeyForWPF
## Quickly build global hotkeys in WPF programs
- [github][1]
- [gitee][2]

[1]: https://github.com/ChengduNeusoftUniversity-FengJunjie-Y22/FastHotKeyForWPF
[2]: https://gitee.com/CNU-FJj-Y22/FastHotKeyForWPF

---

## 更新进度
[Bilibili合集][3]

[3]: https://www.bilibili.com/video/BV1WTbReZEZU

<details>
<summary>Version 1.1.6 已上线 ( 使用PrefabComponent的最后一个版本 ) </summary>

#### (1)提供开箱即用的圆角组件
#### (2)默认不使用变色效果,需要用户自定义对应函数
#### (3)非DeBug模式下再无注册成功与否的提示,需要用户自定义对应函数
#### (4)新增一个保护名单,名单中的任何热键不允许被增删改,即便这个热键没有被注册过
#### (5)新增静态属性,用于获取注册信息和保护名单

</details>

<details>
<summary>Version 1.2.3 已上线 </summary>

### 修复 HotKeysBox 在 手动设置热键 时，部分情况下文本显示异常的问题 (即手动设置初始热键后，文本显示None+None而不是初始设置的热键,但鼠标进入一下框体就恢复了正常)
### 优化了用户控件的圆角效果，新增ActualBackground可选项
</details>

---

## Ⅰ 引入命名空间
##### 后端
```csharp
using FastHotKeyForWPF;
```
##### 前端
```xaml
xmlns:fh="clr-namespace:FastHotKeyForWPF;assembly=FastHotKeyForWPF"
```

---

## Ⅱ 激活与销毁
#### 示例1. GlobalHotKey - 热键相关的核心功能
```csharp
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            GlobalHotKey.Awake();
        }

        protected override void OnClosed(EventArgs e)
        {
            GlobalHotKey.Destroy();

            base.OnClosed(e);
        }
```
#### 示例2. ReturnValueMonitor - 拓展功能（非必要）
```csharp
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            ReturnValueMonitor.Awake();
        }

        protected override void OnClosed(EventArgs e)
        {
            ReturnValueMonitor.Destroy();

            base.OnClosed(e);
        }
```
###### 重写MainWindow的OnSourceInitialized与OnClosed是推荐的做法，当然，你可以选择其它时刻激活，只要你能确保Awake()时窗口句柄已存在

---

## Ⅲ 使用 GlobalHotKey ，注册热键
#### 情景. 假定你自定义了以下函数并希望用户按下 [ Ctrl + F1 ] 与 [ Ctrl + F2 ] 时，分别执行 TestA 与 TestB
```csharp
        private void TestA()//无参数、无返回值
        {
            MessageBox.Show("热键A被触发了！");
        }

        private object TestB()//无参数、返回一个object
        {
            return "热键B被触发了！";
        }
```
#### 示例. 使用 GlobalHotKey.Add 快速注册两个全局热键
```csharp
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            GlobalHotKey.Awake();

            GlobalHotKey.Add(ModelKeys.CTRL, NormalKeys.F1, TestA);
            GlobalHotKey.Add(ModelKeys.CTRL, NormalKeys.F2, TestB);
        }
```
###### 恭喜，你已经掌握了该库最核心的功能！

---

## Ⅳ 使用 GlobalHotKey ，修改热键
#### 示例1. 已知 Keys ,修改其对应的处理事件（函数）
```csharp
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            GlobalHotKey.Awake();
            GlobalHotKey.Add(ModelKeys.CTRL, NormalKeys.F1, TestA);

            GlobalHotKey.EditHotKey_Function(ModelKeys.CTRL, NormalKeys.F1, TestB);
            //原本 [ Ctrl + F1 ] 应该触发 TestA
            //经修改后 , 应该被触发的变为 TestB
        }
```
#### 示例2. 已知处理事件，修改其对应的 Keys
```csharp
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            GlobalHotKey.Awake();
            GlobalHotKey.Add(ModelKeys.CTRL, NormalKeys.F1, TestA);

            GlobalHotKey.EditHotKey_Keys(TestA, ModelKeys.CTRL, NormalKeys.F2);
            //原本 TestA 应由 [ Ctrl + F1 ] 触发 
            //经修改后 , 应由 [ Ctrl + F2 ] 触发
        }
```

---

## Ⅴ 使用 GlobalHotKey ，删除热键
#### 示例1. 根据 注册ID 删除热键（默认第一个ID是2004，之后逐个累加）
```csharp
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            GlobalHotKey.Awake();
            GlobalHotKey.Add(ModelKeys.CTRL, NormalKeys.F1, TestA);

            GlobalHotKey.DeleteById(2004);
        }
```
#### 示例2. 根据 Keys 删除热键
```csharp
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            GlobalHotKey.Awake();
            GlobalHotKey.Add(ModelKeys.CTRL, NormalKeys.F1, TestA);

            GlobalHotKey.DeleteByKeys(ModelKeys.CTRL, NormalKeys.F1);
        }
```
#### 示例3. 根据 处理函数 删除热键
```csharp
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            GlobalHotKey.Awake();
            GlobalHotKey.Add(ModelKeys.CTRL, NormalKeys.F1, TestA);

            GlobalHotKey.DeleteByFunction(TestA);
        }
```

---

## Ⅵ 使用 RegisterCollection ，查询注册在列的热键信息
#### 示例1. 根据 ID 查询完整的注册信息 （ RegisterInfo 对象 ）
```csharp
        RegisterInfo Info = GlobalHotKey.Registers[2004];
```
#### 示例2. 从 RegisterInfo 中 ，取得热键的细节信息
|属性                   |类型                        |含义        |
|-----------------------|----------------------------|------------|
|RegisterID             |int                         |注册id，起始为2004，自动递增 |
|Model                  |ModelKeys                   |热键 - 系统按键 |
|Normal                 |NormalKeys                  |热键 - 文本按键 |
|FunctionType           |FunctionTypes               |处理函数所属分类 |
|Name                   |string                      |处理函数的函数名 |
|FunctionVoid           |KeyInvoke_Void              |处理函数 - void 型   |
|FunctionReturn         |KeyInvoke_Return            |处理函数B - return object 型   |

---

## Ⅶ 使用 ReturnValueMonitor ，在热键事件处理完毕后，对其返回值进一步处理（不常用）
#### 示例. 使用 BindingAutoEvent 处理监测到的返回值
```csharp
        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            GlobalHotKey.Awake();

            ReturnValueMonitor.Awake();
            ReturnValueMonitor.BindingAutoEvent(WhileObjectReturned);
            //WhileObjectReturned将对TestA与TestB返回的object做处理

            GlobalHotKey.Add(ModelKeys.CTRL, NormalKeys.F1, TestA);
            GlobalHotKey.Add(ModelKeys.CTRL, NormalKeys.F2, TestB);
            //TestA与TestB只负责返回object,并不对其做任何处理
        }

        protected override void OnClosed(EventArgs e)
        {
            GlobalHotKey.Destroy();
            ReturnValueMonitor.Destroy();

            base.OnClosed(e);
        }

        private object TestA()
        {
            return "热键A被触发了！";
        }

        private object TestB()
        {
            return 66868;
        }

        private void WhileObjectReturned()
        {
            if (ReturnValueMonitor.Value == null) { return; }

            if (ReturnValueMonitor.Value is string text)
            {
                //……
                //string 处理逻辑，例如打印值

                return;
            }

            if (ReturnValueMonitor.Value is int number)
            {
                //……
                //int 处理逻辑，例如打印值

                return;
            }
        }
```

---

## Ⅷ [ HotKeyBox ] 控件 & [ HotKeysBox ] 控件
#### 情景. 假定你希望制作一个设置界面，允许用户自己设置热键
#### 示例1. 接入控件
##### 引入库
```xaml          
            xmlns:ff="clr-namespace:FastHotKeyForWPF;assembly=FastHotKeyForWPF"
```
##### 定义控件
```xaml
            <!--每个控件只接收一个Key-->
            <ff:HotKeyBox x:Name="Box1"/>
            <ff:HotKeyBox x:Name="Box2"/>

            <!--每个控件接收两个Key-->
            <ff:HotKeysBox x:Name="Box3"/>
```
##### 建立连接
```csharp
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            GlobalHotKey.Awake();

            Box1.ConnectWith(Box2, TestA);
            Box3.ConnectWith(TestA);
            //无论哪种 Box ，连接操作只需要执行一次
        }

        private object TestA()
        {
            return "热键A被触发了！";
        }
```
#### 示例2. 为控件设置初始热键
```csharp
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            GlobalHotKey.Awake();

            Box1.ConnectWith(Box2, TestA);
            Box3.ConnectWith(TestB);
            //注意先建立连接再设置初始热键

            Box1.SetHotKey(ModelKeys.CTRL,NormalKeys.F1,TestA);           
            Box3.SetHotKey(ModelKeys.CTRL, NormalKeys.F2, TestB);
        }
```
#### [ HotKeyBox ] 可选项
|属性                   |类型                        |含义        |
|-----------------------|----------------------------|------------|
|CurrentKey             |Key                         |当前值 |
|WhileInput             |event Action?               |用户发生输入行为时，触发此事件 |
|ErrorText              |string                      |若按键不受库支持，则控件显示该文本 |
|IsHotKeyRegistered     |bool                        |目前是否成功注册 |
|LastHotKeyID           |int                         |最近一次注册成功的ID |
|CornerRadius           |CornerRadius                |圆滑度   |
|DefaultTextColor       |SolidColorBrush             |默认文本色|
|DefaultBorderBrush     |SolidColorBrush             |默认外边框色|
|HoverTextColor         |SolidColorBrush             |悬停文本色|
|HoverBorderBrush       |SolidColorBrush             |悬停外边框色|
|ActualBackground       |SolidColorBrush             |背景色,注意不是 Background|
#### [ HotKeysBox ] 可选项
|属性                   |类型                        |含义        |
|-----------------------|----------------------------|------------|
|CurrentKeyA            |Key                         |左键值 |
|CurrentKeyB            |Key                         |右键值 |
|WhileInput             |event Action?               |用户发生输入行为时，触发此事件 |
|ErrorText              |string                      |若按键不受库支持，则控件显示该文本 |
|IsHotKeyRegistered     |bool                        |目前是否成功注册 |
|LastHotKeyID           |int                         |最近一次注册成功的ID |
|CornerRadius           |CornerRadius                |圆滑度   |
|DefaultTextColor       |SolidColorBrush             |默认文本色|
|DefaultBorderBrush     |SolidColorBrush             |默认外边框色|
|HoverTextColor         |SolidColorBrush             |悬停文本色|
|HoverBorderBrush       |SolidColorBrush             |悬停外边框色|
|ActualBackground       |SolidColorBrush             |背景色,注意不是 Background|

#### [ HotKeyBox ] & [ HotKeysBox ] 在Xaml构成上几乎一模一样，你可以通过 x:Name 访问内部元素并修改它们
##### 内部元素如下
```xaml
<UserControl x:Class="FastHotKeyForWPF.HotKeysBox"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006" 
             xmlns:d="http://schemas.microsoft.com/expression/blend/2008" 
             xmlns:local="clr-namespace:FastHotKeyForWPF"
             mc:Ignorable="d" 
             Height="50"
             Width="320"
             Background="Transparent"
             x:Name="Total"  MouseLeave="TextBox_MouseLeave" MouseEnter="TextBox_MouseEnter">
    <UserControl.Resources>
        <local:DoubleConvertor ConvertRate="0.7" x:Key="HeightToFontSize"/>
    </UserControl.Resources>
    <Grid x:Name="BackGrid" Background="{Binding ElementName=Total,Path=Background}">
        <Border x:Name="FixedBorder" BorderBrush="White" Background="#1e1e1e" BorderThickness="1" CornerRadius="5" ClipToBounds="True"/>
        <TextBox x:Name="FocusGet" Background="Transparent" IsReadOnly="True" PreviewKeyDown="UserInput" BorderBrush="Transparent" BorderThickness="0"/>
        <TextBox x:Name="EmptyOne" Width="0" Height="0"/>
        <TextBlock x:Name="ActualText" Foreground="White" VerticalAlignment="Center" HorizontalAlignment="Center" FontSize="{Binding ElementName=Total,Path=Height,Converter={StaticResource HeightToFontSize}}"/>
    </Grid>
</UserControl>
```

---