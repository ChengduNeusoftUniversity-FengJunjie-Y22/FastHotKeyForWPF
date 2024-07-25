# FastHotKeyForWPF ( ⚠ 文档维护中 ，将于 V2.0.0 完成 )
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
<summary>Version 2.0.0 即将上线 ( Version 1.6.x 都是测试版本 ) </summary>

### ★本次更新将基于MVVM , 进行一次高度重构
### Ⅰ新控件是对XAML友好的
### Ⅱ新的委托用于热键处理函数,它更符合WPF的书写习惯
### Ⅲ新增了一些抽象基类 , 它们已实现了必要的接口 , 可快速构筑用于注册热键的用户控件 ， 即功能与库提供的控件相同但样式的自由度更高
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

## Ⅷ 使用库提供的抽象基类或接口,实现属于您自己的UserControl
### 情景1.您对于Model层没有定制需求,这种情况只需要借助抽象基类即可实现
### 情景2.您需要定制Model层,这种情况需要借助接口实现

---