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
<summary>Version 1.1.6 已上线 ( 使用 PrefabComponent 的最后一个版本 ) </summary>

#### (1)提供开箱即用的圆角组件
#### (2)默认不使用变色效果,需要用户自定义对应函数
#### (3)非DeBug模式下再无注册成功与否的提示,需要用户自定义对应函数
#### (4)新增一个保护名单,名单中的任何热键不允许被增删改,即便这个热键没有被注册过
#### (5)新增静态属性,用于获取注册信息和保护名单

</details>

<details>
<summary>Version 1.2.3 已上线（ 使用 非MVVM 的最后一个版本 ） </summary>

### 修复 HotKeysBox 在 手动设置热键 时，部分情况下文本显示异常的问题 (即手动设置初始热键后，文本显示None+None而不是初始设置的热键,但鼠标进入一下框体就恢复了正常)
### 优化了用户控件的圆角效果，新增ActualBackground可选项
</details>

<details>
<summary>Version 2.0.0 已上线</summary>

## 变更
#### 1.GlobalHotKey.Add() 返回( bool,int ) => 返回注册编号 int , -1表示失败的操作
#### 2.GlobalHotKey.EditHotKey_Keys() => GlobalHotKey.EditKeys()
#### 3.GlobalHotKey.EditHotKey_Function() => GlobalHotKey.EditHandler()
#### 4.GlobalHotKey.DeleteByFunction() => GlobalHotKey.DeleteByHandler()
#### 5.[delegate KeyInvoke_Void & delegate KeyInvoke_Return] => [ delegate HotKeyEventHandler & HotKeyEventArgs ]
#### 6.[HotKeyBox & HotKeysBox] => [HotKeyBox]
## 新增
#### 1.RegisterCollection[ModelKey,NormalKey] => RegisterInfo
#### 2.RegisterCollection[HotKeyEventHandler] => List< RegisterInfo >
#### 3.[接口]IAutoHotKeyProperty 约束 Model、View、ViewModel
#### 4.[接口]IAutoHotKeyUpdate   约束 ViewModel
#### 5.[抽象基类]HotKeyModelBase     快速实现用于热键注册的UserControl的Model
#### 6.[抽象基类]HotKeyViewModelBase 快速实现用于热键注册的UserControl的ViewModel
## 相比于旧版本
#### 1.对XAML更友好
#### 2.事件书写更符合WPF的习惯
#### 3.更好的可拓展性

</details>

---

## Ⅰ 引入命名空间
#### C#
```csharp
using FastHotKeyForWPF;
```
#### XAML
```xaml
xmlns:hk="clr-namespace:FastHotKeyForWPF;assembly=FastHotKeyForWPF"
```

---

## Ⅱ 激活与销毁
#### 示例. GlobalHotKey
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
###### 重写MainWindow的OnSourceInitialized与OnClosed是推荐的做法，当然，你可以选择其它时刻激活，只要你能确保Awake()时窗口句柄已存在

---

## Ⅲ 使用 GlobalHotKey ，注册热键
#### 情景. 假设你定义了以下HandlerA , 并希望用户按下 [ Ctrl + F1 ] 时执行它
```csharp
        private void HandlerA(object sender, HotKeyEventArgs e)
        {
            int ID = e.RegisterInfo.RegisterID;

            MessageBox.Show($"A HotKey Has Been Invoked Whose ID is {ID}");
        }
```
#### 示例. 使用 GlobalHotKey.Add 注册热键 [ Ctrl + F1 ] => [ HandlerA ]
```csharp
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            GlobalHotKey.Awake();
            GlobalHotKey.Add(ModelKeys.CTRL, NormalKeys.F1, HandlerA);
        }
```
###### 恭喜，你已经掌握了该库最核心的功能！

---

## Ⅳ 使用 GlobalHotKey ，修改热键
#### 示例1. 已知触发Keys ,修改其对应的处理函数HotKeyEventHandler
```csharp
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            GlobalHotKey.Awake();
            GlobalHotKey.Add(ModelKeys.CTRL, NormalKeys.F1, HandlerA);
            //初始热键为 [ CTRL + F1 => HandlerA ]

            GlobalHotKey.EditHandler(ModelKeys.CTRL,NormalKeys.F1, HandlerB);
            //由 [ CTRL + F1 => HandlerA ] 变为 [ CTRL + F1 => HandlerB ];
        }
```
#### 示例2. 已知处理函数HotKeyEventHandler ，修改其对应的触发Keys
```csharp
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            GlobalHotKey.Awake();
            GlobalHotKey.Add(ModelKeys.CTRL, NormalKeys.F1, HandlerA);
            //初始热键为 [ CTRL + F1 => HandlerA ]

            GlobalHotKey.EditKeys(HandlerA, ModelKeys.CTRL, NormalKeys.Q);
            //由 [ CTRL + F1 => HandlerA ] 变为 [ CTRL + Q => HandlerA ];
            //注意:通常情况下,即便允许多个组合键指向同一Handler,也不建议您这么做,类库默认只修改第一个找到的Handler,意外的情况需要您手动查询并修改热键
        }
```

---

## Ⅴ 使用 GlobalHotKey ，删除热键
#### 示例1.
```csharp
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            GlobalHotKey.Awake();
            int ID = GlobalHotKey.Add(ModelKeys.CTRL, NormalKeys.F1, HandlerA);
            //初始热键为 [ CTRL + F1 => HandlerA ]
            //注册成功将返回注册ID，否则返回-1

            GlobalHotKey.Clear();
            //删除所有热键
        }
```
#### 示例2.
```csharp
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            GlobalHotKey.Awake();
            int ID = GlobalHotKey.Add(ModelKeys.CTRL, NormalKeys.F1, HandlerA);
            //初始热键为 [ CTRL + F1 => HandlerA ]
            //注册成功将返回注册ID，否则返回-1

            GlobalHotKey.DeleteById(ID);
            GlobalHotKey.DeleteByKeys(ModelKeys.CTRL, NormalKeys.F1);
            GlobalHotKey.DeleteByHandler(HandlerA);
            //删除指定热键(三种方案选一个即可)
            //注意:DeleteByHandler与EditKeys特性不同,它会删除所有注册了指定Handler的热键,而不是只针对第一个
        }
```

---

## Ⅵ 使用 RegisterCollection ，索引式地查询注册在列的热键信息 （ RegisterInfo 对象 ）

#### 介绍. RegisterInfo 包含的信息
|属性                   |类型                        |含义        |
|-----------------------|----------------------------|------------|
|RegisterID             |int                         |注册id，-1表示无效的注册信息 |
|ModelKey               |ModelKeys                   |触发Key之一，支持 CTRL/ALT |
|NormalKey              |NormalKeys                  |触发Key之一，支持 数字/字母/Fx键/方向箭头|
|Handler                |delegate HotKeyEventHandler?|处理函数|

#### 示例1. 根据 ID 查询注册信息 
```csharp
        RegisterInfo Info = GlobalHotKey.Registers[2004];
```
#### 示例2. 根据 Keys 查询注册信息 
```csharp
        RegisterInfo Info = GlobalHotKey.Registers[ModelKeys.CTRL,NormalKeys.F1];
```
#### 示例3. 根据 Handler 查询注册信息 
```csharp
        List<RegisterInfo> Infos = GlobalHotKey.Registers[HandlerA];
```
---

## Ⅶ 使用库提供的UserControl搭建您的热键设置界面
#### 引入库
```xaml
xmlns:hk="clr-namespace:FastHotKeyForWPF;assembly=FastHotKeyForWPF"
```
#### 使用库中控件
```xaml
            <!--类库控件,注意ErrorText与ConnectText不是依赖属性-->
            <hk:HotKeyBox x:Name="KeyBoxA"
                          CurrentKeyA="LeftCtrl"
                          CurrentKeyB="Q"
                          Handler="HandlerA"
                          CornerRadius="15"
                          ActualBackground="#1e1e1e"
                          FixedBorderBrush="White"
                          FixedBorderThickness="2"
                          TextColor="White"
                          HoverTextColor="Violet"
                          HoverBorderBrush="Cyan"
                          ConnectText=" + "
                          ErrorText="Failed"/>
```
```csharp
        private void HandlerA(object sender, HotKeyEventArgs e)
        {
            int ID = e.RegisterInfo.RegisterID;
            //此处可获取热键的具体信息

            MessageBox.Show($"A HotKey Has Been Invoked Whose ID is {ID}");
        }
```

---

## Ⅷ 使用库提供的抽象基类或接口,实现属于您自己的UserControl,详情可在 SampleDemo查看
### 介绍1.接口的使用规范
#### IAutoHotKeyProperty接口必须在View与ViewModel实现
#### IAutoHotKeyUpdate接口必须在ViewModel实现

### 示例1.您对于Model层没有定制需求 ( 1次接口的手动实现 )
##### ViewModel
```csharp
    /// <summary>
    /// 基于抽象基类设计 ViewModel 层 , 要求使用统一的 HotKeyBoxModel 作为 Model ( 已在基类中定义_model )
    /// </summary>
    public class MyHotKeyBoxViewModelA : HotKeyViewModelBase
    {
        public MyHotKeyBoxViewModelA()
        {
            _model = new HotKeyBoxModel();
        }

        private SolidColorBrush _fixedtransparent = Brushes.Transparent;
        public SolidColorBrush FixedTransparent
        {
            get => _fixedtransparent;
            set
            {
                if (_fixedtransparent != value)
                {
                    OnPropertyChanged(nameof(FixedTransparent));
                }
            }
        }
        //…
        //拓展属性,它们不存在于Model,只负责逻辑数据的处理
        //例如,将【UserControl的Background】与【FixedTransparent】做【TwoWay绑定】,即可永远保持为透明


        //…
        //重写属性,多数情况并不需要这一步


        public override void UpdateText()
        {
            string? PossibilityA = (CurrentKeyA == Key.LeftCtrl || CurrentKeyA == Key.RightCtrl) ? "CTRL" : null;
            string? PossibilityB = (CurrentKeyA == Key.LeftAlt || CurrentKeyA == Key.RightAlt) ? "ALT" : null;

            string Left = (PossibilityA == null ? string.Empty : PossibilityA) + (PossibilityB == null ? string.Empty : PossibilityB);

            Text = Left + " + " + CurrentKeyB.ToString();
        }
        //…
        //重写方法
        //例如,您希望不再区分 CTRL/ALT的左右,那么您可以重写UpdateText()
    }
```
##### View
```xaml
<UserControl x:Class="Sample.MyHotKeyBoxA"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006" 
             xmlns:d="http://schemas.microsoft.com/expression/blend/2008" 
             xmlns:local="clr-namespace:Sample"
             mc:Ignorable="d" 
             Height="50" 
             Width="300"
             Background="{Binding FixedTransparent,Mode=TwoWay}"
             MouseEnter="UserControl_MouseEnter"
             MouseLeave="UserControl_MouseLeave">
    <!--经过拓展的ViewModel-->
    <UserControl.DataContext>
        <local:MyHotKeyBoxViewModelA x:Name="ViewModel"/>
    </UserControl.DataContext>
    
    <Grid>
        <TextBox x:Name="KeyGet"
                 Background="{Binding FixedTransparent}"
                 IsReadOnly="True"
                 PreviewKeyDown="KeyGet_PreviewKeyDown"/>
        <TextBox x:Name="ActualText"
                 Text="{Binding Text}"
                 Background="{Binding FixedTransparent}"
                 FontSize="30"
                 Foreground="White"
                 HorizontalAlignment="Center"
                 VerticalAlignment="Center"
                 BorderBrush="Transparent"/>
    </Grid>
</UserControl>
```
```csharp
    public partial class MyHotKeyBoxA : UserControl, IAutoHotKeyProperty
    {
        public MyHotKeyBoxA()
        {
            InitializeComponent();

            BoxPool.Add(this, ViewModel);
            //必须执行这句话才可以参与库提供的唯一热键的实现流程
        }


        #region 接口实现

        public int PoolID { get; set; } = 0;

        public Key CurrentKeyA
        {
            get { return (Key)GetValue(CurrentKeyAProperty); }
            set { SetValue(CurrentKeyAProperty, value); }
        }
        public Key CurrentKeyB
        {
            get { return (Key)GetValue(CurrentKeyBProperty); }
            set { SetValue(CurrentKeyBProperty, value); }
        }

        public HotKeyEventHandler? HandlerData { get; set; }
        public event HotKeyEventHandler? Handler
        {
            add { SetValue(HandlerProperty, value); }
            remove { SetValue(HandlerProperty, null); }
        }

        #endregion


        #region 依赖属性定义

        public static readonly DependencyProperty CurrentKeyAProperty =
            DependencyProperty.Register(nameof(CurrentKeyA), typeof(Key), typeof(MyHotKeyBoxA), new PropertyMetadata(Key.None, OnKeyAChanged));
        public static readonly DependencyProperty CurrentKeyBProperty =
            DependencyProperty.Register(nameof(CurrentKeyB), typeof(Key), typeof(MyHotKeyBoxA), new PropertyMetadata(Key.None, OnKeyBChanged));
        public static readonly DependencyProperty HandlerProperty =
            DependencyProperty.Register(nameof(Handler), typeof(HotKeyEventHandler), typeof(MyHotKeyBoxA), new PropertyMetadata(null, OnHandlerChanged));

        private static void OnKeyAChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var target = (MyHotKeyBoxA)d;
            target.ViewModel.CurrentKeyA = (Key)e.NewValue;
        }
        private static void OnKeyBChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var target = (MyHotKeyBoxA)d;
            target.ViewModel.CurrentKeyB = (Key)e.NewValue;
        }
        private static void OnHandlerChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var target = (MyHotKeyBoxA)d;
            target.HandlerData = (HotKeyEventHandler)e.NewValue;
            target.ViewModel.HandlerData = (HotKeyEventHandler)e.NewValue;
        }

        #endregion


        #region 事件

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            KeyGet.Focus();
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            Keyboard.ClearFocus();
        }

        private void KeyGet_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            ViewModel.UpdateText();

            Key key = (e.Key == Key.System ? e.SystemKey : e.Key);

            var result = KeyHelper.IsKeyValid(key);
            //KeyHelper提供Key合法性检查,以确保是一个受库支持的Key
            if (result.Item1)
            {
                if (result.Item2 == KeyTypes.ModelKey)
                {
                    CurrentKeyA = key;
                    //注意这里应该向依赖属性通知更改,直接通知ViewModel会导致BoxPool功能异常
                }
                else if (result.Item2 == KeyTypes.NormalKey)
                {
                    CurrentKeyB = key;
                }
            }

            e.Handled = true;
        }

        #endregion
    }
```

### 示例2.您需要定制Model层 ( 3~4次接口的手动实现 )
#### 需求场景过少,示例代码将延后提交
---