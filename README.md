# FastHotKeyForWPF
## Quickly build global hotkeys in WPF programs
## Supported [ .NET6.0 ] [ .NET8.0 ]
- [☆ github - The best document reading experience][1]
- [nuget][2]

[1]: https://github.com/ChengduNeusoftUniversity-FengJunjie-Y22/FastHotKeyForWPF
[2]: https://www.nuget.org/packages/FastHotKeyForWPF/
[4]: https://gitee.com/CNU-FJj-Y22/FastHotKeyForWPF

---

<details>
<summary>V2.3.0 变更</summary>

### Ⅰ 修改 [ IAutoHotKeyProperty ] 接口
- HandlerData 已修改为 Hander 并添加了 event 修饰

### Ⅱ 新增 [ HotKeyControlBase ] , 仅一次继承 , 即可实现与类库控件 [ HotKeyBox ] 同款的热键自动管理功能
- 示例:
- 假设已引入命名空间
  ```xml
  xmlns:hk="clr-namespace:FastHotKeyForWPF;assembly=FastHotKeyForWPF"
  ```
  ```csharp
  using FastHotKeyForWPF;
  ```
- 1.XAML
  ```xml
  <hk:HotKeyControlBase x:Class="WpfApp1.MyHotKeyBox"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006" 
             xmlns:d="http://schemas.microsoft.com/expression/blend/2008" 
             xmlns:local="clr-namespace:WpfApp1"
             mc:Ignorable="d" 
             Height="50" Width="500"
             xmlns:hk="clr-namespace:FastHotKeyForWPF;assembly=FastHotKeyForWPF"
             MouseEnter="UserControl_MouseEnter" 
             MouseLeave="UserControl_MouseLeave">
    <Grid>
        <TextBox x:Name="FocusGet" PreviewKeyDown="FocusGet_PreviewKeyDown" IsReadOnly="True" Width="500" Height="50"/>
        <TextBlock Text="{Binding RelativeSource={RelativeSource Mode=FindAncestor,AncestorType=local:MyHotKeyBox},Path=HotKeyText}" FontSize="30" Foreground="Violet" Panel.ZIndex="2"/>
    </Grid>
  </hk:HotKeyControlBase>
  ```
- 2.C#
  ```csharp
    public partial class MyHotKeyBox : HotKeyControlBase
    {
        public MyHotKeyBox()
        {
            InitializeComponent();
            BoxPool.Add(this);
        }
        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            FocusGet.Focus();
        }
        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            Keyboard.ClearFocus();
        }
        private void FocusGet_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            UpdateText();
            KeyHelper.KeyParse(this, e);
            e.Handled = true;
        }
    }
  ```
- 3.要点解释
  - (1) XAML基底元素不再为UserControl , 而是自类库导入的 hk:HotKeyControlBase
  - (2) XAML需要一个控件接收用户的KeyDown , 这里是用了一个TextBox , 接着在【KeyDown事件】中可使用【KeyHelper.KeyParse(this,e)】完成输入处理
  - (3) XAML需要一个控件表示当前热键信息 ，这里是用了一个TextBlock , 接着将其【Text属性】与基类提供的【HotKeyText依赖属性】作绑定完成显示效果
  - (4) BoxPool用于确保控件之间不出现重复热键 , 这里在初始化时调用【BoxPool.Add(this)】即可
  - (5) HotKeyControlBase内置的属性和方法大多是可以重写的 ，例如从Keys变为string的逻辑

</details>

---

<details>
<summary>文档</summary>

## 功能概述
- [ GlobalHotKey ] 允许你 注册/修改/删除/锁定 全局热键
- [ RegisterCollection ] 允许你使用索引查找注册信息 [ RegisterInfo ] 
- [ KeyHelper ] Key值转换工具,例如将一个uint拆解为多个key
- [ HotKeyBox ] 是类库为您提供的控件,可自动化热键的管理工作
- 此外,类库还针对控件的热键相关功能提供了 [ 接口 ] [ 抽象类 ] ，您可在此基础上定制外观更丰富的控件

---

## Ⅰ 引入命名空间
- 文档示例均已按照下述方法引入
- C#
```csharp
using FastHotKeyForWPF;
```
- XAML
```xaml
xmlns:hk="clr-namespace:FastHotKeyForWPF;assembly=FastHotKeyForWPF"
```

---

## Ⅱ GlobalHotKey提供的 [ 注册 ] 功能
- 示例1. ☆ 激活/销毁 [ 推荐在MainWindow执行下述操作 ]
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
- 示例2. 定义热键的处理事件 [ e.RegisterInfo可获取详细的热键信息 ]
```csharp
        private void HandlerA(object sender, HotKeyEventArgs e)
        {
            int ID = e.RegisterInfo.RegisterID;

            MessageBox.Show($"A HotKey Has Been Invoked Whose ID is {ID}");
        }
```
- 示例3. 注册热键 [ Ctrl + F1 ] => [ HandlerA ]
```csharp
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            GlobalHotKey.Awake();

            GlobalHotKey.Add(ModelKeys.CTRL, TestA);
        }
```
- 示例4. 注册热键 [ Alt + Ctrl + F1 ] => [ HandlerA ]
```csharp
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            GlobalHotKey.Awake();

            GlobalHotKey.Add(ModelKeys.CTRL | ModelKeys.ALT, TestA);
        }
```
- 示例5. 使用集合表示 ModelKeys
```csharp
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            GlobalHotKey.Awake();

            List<ModelKeys> list = new List<ModelKeys>()
            {
                ModelKeys.CTRL,
                ModelKeys.ALT,
                ModelKeys.SHIFT
            };
            GlobalHotKey.Add(list, NormalKeys.F1, TestA);
        }
```
- 示例6. 使用uint表示 ModelKeys
```csharp
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            GlobalHotKey.Awake();

            uint target = (uint)(ModelKeys.CTRL | ModelKeys.ALT | ModelKeys.SHIFT);
            GlobalHotKey.Add(target, NormalKeys.F1, TestA);
        }
```
- 注意. Add具备int返回值,表示注册时的ID号,默认从2004开始,-1表示失败的注册操作
- 注意. 使用集合/uint表达Keys时,[ ModelKeys ]可以是多个,[ Normalkeys ]只能是一个

---

## Ⅲ GlobalHotKey提供的 [ 修改 ] 功能
- 示例1. 已知热键 [ CTRL + F1 ] => [ HandlerA ] , 执行 [ HandlerA ] => [ HandlerB ] 修改
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
- 示例2. 已知热键 [ CTRL + F1 ] => [ HandlerA ] , 执行 [ CTRL + F1 ] => [ CTRL + Q ] 修改
```csharp
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            GlobalHotKey.Awake();
            GlobalHotKey.Add(ModelKeys.CTRL, NormalKeys.F1, HandlerA);
            //初始热键为 [ CTRL + F1 => HandlerA ]

            GlobalHotKey.EditKeys(HandlerA, ModelKeys.CTRL, NormalKeys.Q);
            //由 [ CTRL + F1 => HandlerA ] 变为 [ CTRL + Q => HandlerA ];
        }
```
- 注意. 通常情况下,即便允许多个组合键指向同一Handler,也不建议您这么做,类库默认只修改第一个找到的Handler,意外的情况需要您手动查询并修改热键

---

## Ⅳ GlobalHotKey提供的 [ 删除 ] 功能
- 示例1. 删除所有
```csharp
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            GlobalHotKey.Awake();
            int ID = GlobalHotKey.Add(ModelKeys.CTRL, NormalKeys.F1, HandlerA);
            //初始热键为 [ CTRL + F1 => HandlerA ]

            GlobalHotKey.Clear();
            //删除所有热键
        }
```
- 示例2. 条件删除
```csharp
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            GlobalHotKey.Awake();
            int ID = GlobalHotKey.Add(ModelKeys.CTRL, NormalKeys.F1, HandlerA);
            //初始热键为 [ CTRL + F1 => HandlerA ]

            GlobalHotKey.DeleteById(ID);
            GlobalHotKey.DeleteByKeys(ModelKeys.CTRL, NormalKeys.F1);
            GlobalHotKey.DeleteByHandler(HandlerA);
            //删除指定热键(依据已知注册信息,从三种方案选一个即可)
        }
```
- 注意. DeleteByHandler与EditKeys特性不同,它会删除所有注册了指定Handler的热键,而不是只针对第一个

---

## Ⅴ RegisterCollection提供的 [ 索引式查询 ] 功能（ RegisterInfo 对象 ）
- 示例1. 根据 ID 查询注册信息 
```csharp
        RegisterInfo Info = GlobalHotKey.Registers[2004];
```
- 示例2. 根据 Keys 查询注册信息 
```csharp
        RegisterInfo Info = GlobalHotKey.Registers[ModelKeys.CTRL,NormalKeys.F1];
```
- 示例3. 根据 Handler 查询注册信息 
```csharp
        List<RegisterInfo> Infos = GlobalHotKey.Registers[HandlerA];
```
#### RegisterInfo 包含的具体信息
|属性                   |类型                        |含义        |
|-----------------------|----------------------------|------------|
|RegisterID             |int                         |注册ID |
|ModelKey               |uint                        |支持 [ CTRL/ALT/SHIFT ] 中的[ 若干 ]|
|NormalKey              |NormalKeys                  |支持 [ 数字/字母/Fx键/方向箭头 ] 中的[ 一个 ]|
|Handler                |delegate HotKeyEventHandler?|处理事件|

---

## Ⅵ KeyHelper提供的 [ Key值转换 ] 功能
- 示例1. 将多个类型不同但受GlobalHotKey支持的Keys合并为统一的uint值
```csharp
            ModelKeys[] modelKeys = new ModelKeys[] { ModelKeys.SHIFT };
            uint result = KeyHelper.UintSum(0x0001, ModelKeys.CTRL, modelKeys));
```
- 示例2. 将一个object转为可能受支持的uint值
```csharp
            KeyHelper.ValueToUint(ModelKeys.SHIFT)
```
- 示例3. 将一个uint值转为[一个]可能的枚举值
```csharp
            bool result1 = KeyHelper.UintToEnum<ModelKeys>(0x0002) == ModelKeys.CTRL ? true : false;
            bool result2 = KeyHelper.UintToEnum<Key>(0x0002) == Key.LeftCtrl ? true : false;
```
- 示例4. 将一个uint值转为[若干]可能的ModelKeys枚举值
```csharp
            List<ModelKeys> result1 = KeyHelper.UintSplit<List<ModelKeys>>(0x0006);
```
- 示例5. 检测一个[ System.Window.Input.Key ]是否受到GlobalHotKey支持
```csharp
            var result = KeyHelper.IsKeyValid(key);
            if (result.Item1)
            {
                MessageBox.Show($"合法,类型为{result.Item2}");
            }
            else
            {
                MessageBox.Show($"非法");
            }
```
- 示例6. 制作用户控件时,快速处理用户按下的Key
```csharp
        private void FocusGet_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            ViewModel.UpdateText();

            KeyHelper.KeyParse(this, e);

            e.Handled = true;
        }
```
- 注意. 若控件需要使用KeyHelper提供的输入处理,必须实现IAutoHotKey接口,然后在控件内的KeyDown事件中使用 KeyHelper.KeyParse(this, e)完成输入处理;

---

## Ⅶ HotKeyBox基于控件提供的 [ 热键自动管理 ] 功能
#### 数字以D开头 , 范围 D0~D9
#### ModelKey以 uint 书写 , 可以直接书写位或运算的结果 , 例如 0x0006 表示 [ CTRL + SHIFT ]

|ModelKey   |uint        |
|-----------|------------|
|无         |0x0000|
|ALT        |0x0001|
|CTRL       |0x0002|
|SHIFT      |0x0004|

```xaml
            <!--类库控件,初始注册 [ CTRL + 1 ] => [ HandlerA ]-->
            <hk:HotKeyBox x:Name="KeyBoxA"
                          CurrentKeyA="0x0002"
                          CurrentKeyB="D1"
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

---

## Ⅷ 使用库提供的 [ 抽象基类 ] 或 [ 接口 ],在MVVM下实现属于您自己的UserControl
- 效果. 自动注册/修改热键并自动清除其它控件中,与自身Keys重复的内容,同时控件的外观将完全由您定制
- 注意. 在控件初始化时,必须调用BoxPool.Add方法并依次传入控件自身引用与ViewModel引用

#### 规范
|接口                       |在哪些层实现它           |
|---------------------------|-------------------------|
|IAutoHotKeyProperty        |Model & ViewModel & View |
|IAutoHotKeyUpdate          |ViewModel                |

|抽象基类                   |说明/注意                    |
|---------------------------|-----------------------------|
|ViewModelBase              |实现ViewModel层的简单基类    |
|HotKeyViewModelBase        |使用此基类将采用固定的Model  |
|HotKeyModelBase            |实现Model层的简单基类        |

</details>

<details>
<summary>Documentation</summary>

## Feature Overview
- [ GlobalHotKey ] Allows you to register/modify/delete/LOCK global hotkeys
- [ RegisterCollection ] Allows you to find registration information using an index [RegisterInfo]
- [ KeyHelper ] Provides you with a powerful Key value conversion tool
- [ HotKeyBox ] Is a control provided by the class library to automate the management of hotkeys
- In addition, the library provides an [interface] [abstract class] to the hotkey-related functionality of the control, which you can use to customize the control with a richer appearance

---

## Ⅰ Introducing namespaces
- The document examples have been included as follows
- C#
```csharp
using FastHotKeyForWPF;
```
- XAML
```xaml
xmlns:hk="clr-namespace:FastHotKeyForWPF;assembly=FastHotKeyForWPF"
```

---

## Ⅱ [Register] feature provided by GlobalHotKey
- Example 1. ☆ Activate/Destroy [It is recommended to do the following on MainWindow]
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
- Example 2. Hotkey handler event definition [e.RegisterInfo for detailed hotkey information]
```csharp
        private void HandlerA(object sender, HotKeyEventArgs e)
        {
            int ID = e.RegisterInfo.RegisterID;

            MessageBox.Show($"A HotKey Has Been Invoked Whose ID is {ID}");
        }
```
- Example 3. Registering hotkeys [Ctrl + F1] => [HandlerA]
```csharp
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            GlobalHotKey.Awake();

            GlobalHotKey.Add(ModelKeys.CTRL, TestA);
        }
```
- Example 4. Registering hotkeys [Alt + Ctrl + F1] => [HandlerA]
```csharp
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            GlobalHotKey.Awake();

            GlobalHotKey.Add(ModelKeys.CTRL | ModelKeys.ALT, TestA);
        }
```
- Example 5. Using collections to represent ModelKeys
```csharp
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            GlobalHotKey.Awake();

            List<ModelKeys> list = new List<ModelKeys>()
            {
                ModelKeys.CTRL,
                ModelKeys.ALT,
                ModelKeys.SHIFT
            };
            GlobalHotKey.Add(list, NormalKeys.F1, TestA);
        }
```
- Example 6. ModelKeys using uint
```csharp
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            GlobalHotKey.Awake();

            uint target = (uint)(ModelKeys.CTRL | ModelKeys.ALT | ModelKeys.SHIFT);
            GlobalHotKey.Add(target, NormalKeys.F1, TestA);
        }
```
- Note that.add has an int return value that indicates the ID number at registration, which defaults to 2004 and -1 indicates a failed registration operation
- Note that when using /uint to represent Keys,[ModelKeys] can be multiple and [Normalkeys] can only be one

---

## Ⅲ [Modify] feature provided by GlobalHotKey
- Example 1. Given the hotkey [CTRL + F1] => [HandlerA], perform the [HandlerA] => [HandlerB] modification
```csharp
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            GlobalHotKey.Awake();
            GlobalHotKey.Add(ModelKeys.CTRL, NormalKeys.F1, HandlerA);

            GlobalHotKey.EditHandler(ModelKeys.CTRL,NormalKeys.F1, HandlerB);
        }
```
- Example 2. Given the hotkey [CTRL + F1] => [HandlerA], perform [CTRL + F1] => [CTRL + Q] modification
```csharp
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            GlobalHotKey.Awake();
            GlobalHotKey.Add(ModelKeys.CTRL, NormalKeys.F1, HandlerA);

            GlobalHotKey.EditKeys(HandlerA, ModelKeys.CTRL, NormalKeys.Q);
        }
```
- Note: In general, even if you allow multiple keys to point to the same Handler, this is not recommended; by default, the library only modifies the first Handler it finds, requiring you to manually look up and change the hotkey

---

## Ⅳ [Delete] feature provided by GlobalHotKey
- Example 1. Delete all
```csharp
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            GlobalHotKey.Awake();
            int ID = GlobalHotKey.Add(ModelKeys.CTRL, NormalKeys.F1, HandlerA);

            GlobalHotKey.Clear();
        }
```
- Example 2. Conditional deletion
```csharp
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            GlobalHotKey.Awake();
            int ID = GlobalHotKey.Add(ModelKeys.CTRL, NormalKeys.F1, HandlerA);

            GlobalHotKey.DeleteById(ID);
            GlobalHotKey.DeleteByKeys(ModelKeys.CTRL, NormalKeys.F1);
            GlobalHotKey.DeleteByHandler(HandlerA);
            //Delete the specified hotkey (one of three options based on known registration information)
        }
```
- Note that the.deleteByHandler feature is different from the EditKeys feature in that it deletes all hotkeys registered with a given Handler, not just the first one

---

## Ⅴ [Indexed query] functionality provided by RegisterCollection (RegisterInfo object)
- Example 1. Querying for registration information based on ID
```csharp
        RegisterInfo Info = GlobalHotKey.Registers[2004];
```
- Example 2. Searching for registration information based on Keys
```csharp
        RegisterInfo Info = GlobalHotKey.Registers[ModelKeys.CTRL,NormalKeys.F1];
```
- Example 3. Looking up registration information from the Handler
```csharp
        List<RegisterInfo> Infos = GlobalHotKey.Registers[HandlerA];
```
#### RegisterInfo contains specific information
|Attribute              |Type                        |Meaning     |
|-----------------------|----------------------------|------------|
|RegisterID             |int                         |Registration ID|
|ModelKey               |uint                        |Support [several] of [CTRL/ALT/SHIFT]|
|NormalKey              |NormalKeys                  |Support [one] of [numbers/letters /Fx keys/directional arrows]|
|Handler                |delegate HotKeyEventHandler?|Handling events|

---

## Ⅵ [Key value conversion] functionality provided by KeyHelper
- Example 1. Combining multiple Keys of different types but supported by GlobalHotKey into a unified uint value
```csharp
            ModelKeys[] modelKeys = new ModelKeys[] { ModelKeys.SHIFT };
            uint result = KeyHelper.UintSum(0x0001, ModelKeys.CTRL, modelKeys));
```
- Example 2. Converting an object to a potentially supported uint value
```csharp
            KeyHelper.ValueToUint(ModelKeys.SHIFT)
```
- Example 3. Converting a uint value to [one] possible enumeration value
```csharp
            bool result1 = KeyHelper.UintToEnum<ModelKeys>(0x0002) == ModelKeys.CTRL ? true : false;
            bool result2 = KeyHelper.UintToEnum<Key>(0x0002) == Key.LeftCtrl ? true : false;
```
- Example 4. Converting a uint value to [several] possible ModelKeys enum values
```csharp
            List<ModelKeys> result1 = KeyHelper.UintSplit<List<ModelKeys>>(0x0006);
```
- Example 5. Checking if a [System.Window.Input.Key] is supported by GlobalHotKey
```csharp
            var result = KeyHelper.IsKeyValid(key);
            if (result.Item1)
            {
                MessageBox.Show($"Legal, of type{result.Item2}");
            }
            else
            {
                MessageBox.Show($"illegal");
            }
```
- Example 6. When making a user control, quickly process a Key pressed by the user
```csharp
        private void FocusGet_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            ViewModel.UpdateText();

            KeyHelper.KeyParse(this, e);

            e.Handled = true;
        }
```
- Note that if the control wants to use the input processing provided by KeyHelper, it must implement the IAutoHotKey interface and then use keyHelp.keyParse (this, e) in the control's KeyDown event to complete the input processing

---

## Ⅶ HotKeyBox is based on the hotkey automatic management function provided by the control
#### Numbers start with D and range from D0 to D9
#### ModelKey is written in uint and can directly write the bit or the result of the operation, such as 0x0006 for [CTRL + SHIFT].

|ModelKey   |uint        |
|-----------|------------|
|Null       |0x0000|
|ALT        |0x0001|
|CTRL       |0x0002|
|SHIFT      |0x0004|

```xaml
            <hk:HotKeyBox x:Name="KeyBoxA"
                          CurrentKeyA="0x0002"
                          CurrentKeyB="D1"
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

---

## Ⅷ Implement your own UserControl under MVVM using the [abstract base class] or [interface] provided by the library
- Effect: Automatically register/modify hotKEYS and automatically remove the duplicate contents of your own Keys in other controls, while the appearance of the control will be completely customized by you
- Note that when the control is initialized, you must call the BoxPool.Add method and pass in a reference to the control itself and a reference to the ViewModel

#### Specification
|Interface                  |Which layers to implement it at  |
|---------------------------|-------------------------|
|IAutoHotKeyProperty        |Model & ViewModel & View |
|IAutoHotKeyUpdate          |ViewModel                |

|Abstract base class        |Notes/Notes                  |
|---------------------------|-----------------------------|
|ViewModelBase              |A simple base class that implements the ViewModel layer    |
|HotKeyViewModelBase        |Using this base class will take a fixed Model  |
|HotKeyModelBase            |A simple base class that implements the Model layer        |

</details>

---
