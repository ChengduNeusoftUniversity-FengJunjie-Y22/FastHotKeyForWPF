# FastHotKeyForWPF
## Quickly build global hotkeys in WPF programs
- [github][1]
- [gitee][2]

[1]: https://github.com/ChengduNeusoftUniversity-FengJunjie-Y22/FastHotKeyForWPF
[2]: https://gitee.com/CNU-FJj-Y22/FastHotKeyForWPF

---

## 更新进度
[Bilibili][1]

[1]: https://www.bilibili.com/video/BV1rr421L7qR

<details>
<summary>Version 1.1.5 已上线</summary>

#### (1)修复焦点离开盒子后，盒子仍然在接收用户按键的bug
#### (2)修复无法使用实例方法Protect()锁定盒子的问题
#### (3)新增组件之间的通信机制,自动清理重复的热键

</details>

<details>
<summary>Version 1.1.6 已上线</summary>

#### (1)提供开箱即用的圆角组件
#### (2)默认不使用变色效果,需要用户自定义对应函数
#### (3)非DeBug模式下再无注册成功与否的提示,需要用户自定义对应函数
#### (4)新增一个保护名单,名单中的任何热键不允许被增删改,即便这个热键没有被注册过
#### (5)新增静态属性,用于获取注册信息和保护名单

</details>

<details>
<summary>Version 1.2.0 即将上线</summary>

### 在保留 99% GlobalHotKey 的前提下，进行了大量重构操作
##### （1）弃用 BindingRef ，改为 ReturnValueMonitor ，不再自动 Awake()
##### （2）弃用 PrefabComponent , 改为 UserControl , 这次是真的开箱即用了
##### （3）新增 RegisterCollection ，可将ID作为索引，便捷地查询注册信息
##### （4）全新的文档，以代码示例为主
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
                //stringg 处理逻辑，例如打印值

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

## Ⅷ [ HotKeyBox ] 控件
#### 情景. 假定你希望制作一个设置界面，允许用户自己设置热键
#### 示例. 接入控件后，自动接收用户输入并据此自动注册、修改、删除热键
###### 引入
```xaml
            xmlns:ff="clr-namespace:FastHotKeyForWPF;assembly=FastHotKeyForWPF"
```
###### 接入控件
```xaml
            <ff:HotKeyBox x:Name="Box1"/>
            <ff:HotKeyBox x:Name="Box2"/>
```
###### 后端逻辑
```csharp
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            GlobalHotKey.Awake();

            Box1.ConnectWith(Box2, TestA);
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
            Box1.SetHotKey(ModelKeys.CTRL,NormalKeys.F1,TestA);
        }
```
#### 可选项
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

#### UserControl 的 Xaml构成 - 你可用 x:Name 更好地改变外观效果
```xaml
    <Grid Background="#1e1e1e">
        <!--外边框-->
         <Border x:Name="FixedBorder" BorderBrush="White" BorderThickness="1" CornerRadius="5" ClipToBounds="True"/>
        <!--用于获取焦点的Box-->
        <TextBox x:Name="FocusGet" Background="Transparent" IsReadOnly="True" PreviewKeyDown="UserInput" BorderBrush="Transparent" BorderThickness="0"/>
        <!--用于移除焦点的Box-->
        <TextBox x:Name="EmptyOne" Width="0" Height="0"/>
        <!--文本显示Box-->
        <TextBlock x:Name="ActualText" Foreground="White" VerticalAlignment="Center" HorizontalAlignment="Center" FontSize="{Binding ElementName=Total,Path=Height,Converter={StaticResource HeightToFontSize}}"/>
    </Grid>
```

---

## Ⅸ [ HotKeysBox ] 控件
#### 情景. 假定你希望制作一个设置界面，允许用户自己设置热键
#### 示例1. 接入控件后，自动接收用户输入并据此自动注册、修改、删除热键
###### 引入
```xaml
            xmlns:ff="clr-namespace:FastHotKeyForWPF;assembly=FastHotKeyForWPF"
```
###### 接入控件
```xaml
            <ff:HotKeysBox x:Name="Box3"/>
```
###### 后端逻辑
```csharp
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            GlobalHotKey.Awake();

            Box3.ConnectWith(TestA);
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

            Box3.ConnectWith(TestB);
            Box3.SetHotKey(ModelKeys.CTRL, NormalKeys.F2, TestB);
        }
```
#### 可选项
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

#### UserControl 的 Xaml构成 - 你可用 x:Name 更好地改变外观效果
```xaml
    <Grid Background="#1e1e1e">
        <!--外边框-->
         <Border x:Name="FixedBorder" BorderBrush="White" BorderThickness="1" CornerRadius="5" ClipToBounds="True"/>
        <!--用于获取焦点的Box-->
        <TextBox x:Name="FocusGet" Background="Transparent" IsReadOnly="True" PreviewKeyDown="UserInput" BorderBrush="Transparent" BorderThickness="0"/>
        <!--用于移除焦点的Box-->
        <TextBox x:Name="EmptyOne" Width="0" Height="0"/>
        <!--文本显示Box-->
        <TextBlock x:Name="ActualText" Foreground="White" VerticalAlignment="Center" HorizontalAlignment="Center" FontSize="{Binding ElementName=Total,Path=Height,Converter={StaticResource HeightToFontSize}}"/>
    </Grid>
```