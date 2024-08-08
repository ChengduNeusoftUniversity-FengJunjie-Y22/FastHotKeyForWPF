﻿# FastHotKeyForWPF
## Quickly build global hotkeys in WPF programs
## Supported [ .NET6.0 ] [ .NET8.0 ]
- [github][1]
- [nuget][2]
- [Bilibili][3]
- [gitee][4]


[1]: https://github.com/ChengduNeusoftUniversity-FengJunjie-Y22/FastHotKeyForWPF
[2]: https://www.nuget.org/packages/FastHotKeyForWPF/
[3]: https://www.bilibili.com/video/BV1rRi3eHEes
[4]: https://gitee.com/CNU-FJj-Y22/FastHotKeyForWPF

---

<details>
<summary>Documentation => Chinese</summary>

## 功能概述
- [ GlobalHotKey ] 允许你 注册/修改/删除/锁定 全局热键
- [ RegisterCollection ] 允许你使用索引查找注册信息 [ RegisterInfo ] 
- [ KeyHelper ] 为您提供强大的Key值转换工具
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
- 补充. RegisterInfo 包含的具体信息
|属性                   |类型                        |含义        |
|-----------------------|----------------------------|------------|
|RegisterID             |int                         |注册ID |
|ModelKey               |uint                        |支持 [ CTRL/ALT/SHIFT ] 中的[ 若干 ]|
|NormalKey              |NormalKeys                  |支持 [ 数字/字母/Fx键/方向箭头 ] 中的[ 一个 ]|
|Handler                |delegate HotKeyEventHandler?|处理事件|

---

## Ⅵ KeyHelper提供的 [ Key值转换 ] 工具
- 方法表
|方法                   |返回                        |作用        |
|-----------------------|----------------------------|------------|
|UintParse(uint key)    |List< ModelKeys >           |解算一个uint由哪些ModelKeys构成 |
|UintCalculate(ICollection< ModelKeys > keys) |uint|将ICollection中的ModelKeys合并成一个uint|
|KeyParse(IAutoHotKeyProperty item, KeyEventArgs e)||在View层处理接收到的用户输入Key|

---

## Ⅶ HotKeyBox基于控件提供的 [ 热键自动管理 ] 功能 
- 数字以D开头 , 范围 D0~D9
- ModelKey以 uint 书写 , 可以直接书写位或运算的结果 , 例如 0x0006 表示 [ CTRL + SHIFT ]
|ModelKey   |uint        |
|-----------|------------|
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
- 规范
|接口                       |在哪些层实现它           |
|---------------------------|-------------------------|
|IAutoHotKeyProperty        |Model & ViewModel & View |
|IAutoHotKeyUpdate          |ViewModel                |

|抽象基类                   |说明/注意                    |
|---------------------------|-----------------------------|
|ViewModelBase              |实现ViewModel层的简单基类    |
|HotKeyViewModelBase        |使用此基类将采用固定的Model  |
|HotKeyModelBase            |实现Model层的简单基类        |

- 效果. 自动注册/修改热键并自动清除其它控件中,与自身Keys重复的内容,同时控件的外观将完全由您定制
- 注意. 在控件初始化时,必须调用BoxPool.Add方法并依次传入控件自身引用与ViewModel引用


</details>

<details>
<summary>Documentation => English</summary>

## Feature Overview
- [ GlobalHotKey ] Allows you to register, modify, and delete global hotkeys
- [  ]

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
#### 情景. 假设你定义了以下HandlerA , 并希望用户触发热键时执行它
```csharp
        private void HandlerA(object sender, HotKeyEventArgs e)
        {
            int ID = e.RegisterInfo.RegisterID;

            MessageBox.Show($"A HotKey Has Been Invoked Whose ID is {ID}");
        }
```
#### 示例1. 注册热键 [ Ctrl + F1 ] => [ HandlerA ]
```csharp
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            GlobalHotKey.Awake();

            GlobalHotKey.Add(ModelKeys.CTRL, TestA);
        }
```
#### 示例2. 注册热键 [ Alt + Ctrl + F1 ] => [ HandlerA ]
```csharp
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            GlobalHotKey.Awake();

            GlobalHotKey.Add(ModelKeys.CTRL | ModelKeys.ALT, TestA);
        }
```
#### 示例3. 注册热键 [ Alt + Ctrl + Shift + F1 ] => [ HandlerA ]
```csharp
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            GlobalHotKey.Awake();

            GlobalHotKey.Add(ModelKeys.CTRL | ModelKeys.ALT | ModelKeys.SHIFT, NormalKeys.F1, TestA);
        }
```
#### 拓展1. 使用集合表示 ModelKeys , 注册热键 [ Alt + Ctrl + Shift + F1 ] => [ HandlerA ]
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
#### 拓展2. 使用uint表示 ModelKeys , 注册热键 [ Alt + Ctrl + Shift + F1 ] => [ HandlerA ]
```csharp
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            GlobalHotKey.Awake();

            uint target = (uint)(ModelKeys.CTRL | ModelKeys.ALT | ModelKeys.SHIFT);
            GlobalHotKey.Add(target, NormalKeys.F1, TestA);
        }
```
#### 注意
- 若使用 ICollection 表示多个 ModelKeys , 该集合的元素个数应该 >0
- GlobalHotKey 对 uint ICollection 实现了重载 , 接下来只以使用 ModelKeys 为例
- RegisterInfoCollection 对 uint ICollection 实现了重载 , 接下来只以使用 ModelKeys 为例

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
#### 示例1. 删除所有
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
#### 示例2. 条件删除
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
|ModelKey               |uint                        |触发Key之一，支持 CTRL/ALT/SHIFT 中的若干|
|NormalKey              |NormalKeys                  |触发Key之一，支持 数字/字母/Fx键/方向箭头 中的一个|
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

## Ⅶ 使用 KeyHelper 提供的静态工具
|方法                   |返回                        |作用        |
|-----------------------|----------------------------|------------|
|UintParse(uint key)    |List< ModelKeys >           |解算一个uint由哪些ModelKeys构成 |
|UintCalculate(ICollection< ModelKeys > keys) |uint|将ICollection中的ModelKeys合并成一个uint|
|KeyParse(IAutoHotKeyProperty item, KeyEventArgs e)||在View层处理接收到的用户输入Key|

---

## Ⅷ 使用库提供的UserControl搭建您的热键设置界面
#### 引入库
```xaml
xmlns:hk="clr-namespace:FastHotKeyForWPF;assembly=FastHotKeyForWPF"
```
#### XAML使用库中控件
- 数字以D开头 , 范围 D0~D9
- ModelKey以 uint 书写

|ModelKey   |uint        |
|-----------|------------|
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
```csharp
        private void HandlerA(object sender, HotKeyEventArgs e)
        {
            int ID = e.RegisterInfo.RegisterID;
            //此处可获取热键的具体信息

            MessageBox.Show($"A HotKey Has Been Invoked Whose ID is {ID}");
        }
```

---

## Ⅸ 使用库提供的抽象基类或接口,在MVVM下实现属于您自己的UserControl
#### 引导. 接口与抽象类
|接口                       |在哪些层实现它           |
|---------------------------|-------------------------|
|IAutoHotKeyProperty        |Model & ViewModel & View |
|IAutoHotKeyUpdate          |ViewModel                |

|抽象基类                   |说明/注意                    |
|---------------------------|-----------------------------|
|ViewModelBase              |实现ViewModel层的简单基类    |
|HotKeyViewModelBase        |使用此基类将采用固定的Model  |
|HotKeyModelBase            |实现Model层的简单基类        |

#### 示例. 一个注册成功会播放动画的UserControl ( 非常用功能，示例会延后在github补全 )

</details>

---
