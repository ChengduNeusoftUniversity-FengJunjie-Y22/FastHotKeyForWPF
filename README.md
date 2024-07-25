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
<summary>Version 2.0.0 即将上线 ( Version 1.6.0是一个测试版本 ) </summary>

### ★本次更新将基于MVVM , 进行一次高度重构
### Ⅰ新控件是对XAML友好的
### Ⅱ新的委托用于热键处理函数,它更符合WPF的书写习惯
### Ⅲ新增了一些抽象基类 , 它们已实现了必要的接口 , 可快速构筑用于注册热键的用户控件 ， 即功能与库提供的控件相同但样式的自由度更高
### Ⅳ
</details>

---

## Ⅰ 引入命名空间
#### C#
```csharp
using FastHotKeyForWPF;
```
#### XAML
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

```
#### 示例. 使用 GlobalHotKey.Add 快速注册两个全局热键
```csharp

```
###### 恭喜，你已经掌握了该库最核心的功能！

---

## Ⅳ 使用 GlobalHotKey ，修改热键
#### 示例1. 已知触发Keys ,修改其对应的处理函数HotKeyEventHandler
```csharp

```
#### 示例2. 已知处理函数HotKeyEventHandler ，修改其对应的触发Keys
```csharp

```

---

## Ⅴ 使用 GlobalHotKey ，删除热键
#### 示例1. 根据热键的注册ID删除热键
```csharp

```
#### 示例2. 根据热键的触发Keys删除热键
```csharp

```
#### 示例3. 根据热键的处理函数HotKeyEventHandler删除热键
```csharp

```

---

## Ⅵ 使用 RegisterCollection ，查询注册在列的热键信息 （ RegisterInfo 对象 ）

#### 介绍. RegisterInfo 包含的信息
|属性                   |类型                        |含义        |
|-----------------------|----------------------------|------------|
|RegisterID             |int                         |注册id，-1表示无效的注册信息 |
|ModelKey               |ModelKeys                   |触发Key之一，支持 CTRL/ALT |
|NormalKey              |NormalKeys                  |触发Key之一，支持 数字/字母/Fx键/方向箭头|
|Handler                |HotKeyEventHandler?         |处理函数|

#### 示例1. 根据 ID 查询完整的注册信息 
```csharp
        RegisterInfo Info = GlobalHotKey.Registers[2004];
```
---