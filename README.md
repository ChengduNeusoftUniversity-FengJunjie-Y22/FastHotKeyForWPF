# FastHotKeyForWPF文档
## 目录
- [项目简介](#项目简介)
  - [功能](#功能)
  - [作者](#作者)
  - [视频教程](#获取此类库)

- [GlobalHotKey类](#GlobalHotKey类)
  - [方法表](#方法表GHK)
  - [参数表](#参数表GHK)
  - [使用示例](#使用示例GHK)

- [BindingRef类](#BindingRef类)
  - [方法表](#方法表BR)
  - [参数表](#参数表BR)
  - [使用示例](#使用示例BR)

- [RegisterInfo类](#RegisterInfo类)
  - [方法表](#方法表RI)
  - [参数表](#参数表RI)
  - [使用示例](#使用示例RI)

## 项目简介

<details id="功能">
<summary>功能</summary>

### 这是一款WPF类库项目，旨在提供更便捷的方式来管理全局热键
### 特点
#### 一句话式的功能实现
#### 可将函数签名直接注册进快捷键
#### 自带一套数据绑定，同样可以将一个函数签名绑定上去，以便在数据发生更新时自动调用绑定的函数

</details>

<details id="作者">
<summary>作者</summary>

### 关于作者本人
#### 写这个项目的时候作者在上大二，一般都是兴致来了就维护一下项目，正常来说会泡在二游里
### 联系方式
#### Bilibili:"真的不来一杯嘛"
#### QQ:2789083329
#### WeChat:WeC_FZJSOP4996

</details>

<details id="获取此类库">
<summary>获取此类库</summary>

### NuGet
#### 目前已经可以直接搜索到了，但此方法获取到的库无法查看XML注释文档，鼠标放在类名上是不会有提示词的，不过你可以照着这个文档用
### Github & Gitee
#### 下载完整项目，并将此类库项目添加到需要使用此类库的项目中，然后在解决方案资源管理器中引用你添加的类库项目

</details>

## GlobalHotKey类

<details id="方法表GHK">
<summary>方法表</summary>

| 方法名             | 参数                                                                             | 返回值            | 描述               |
|--------------------|----------------------------------------------------------------------------------|-------------------|--------------------|
| Awake              |                                                                                  |                   | 激活        |
| Destroy            |                                                                                  |                   | 销毁   |
| Add                | ( ModelKeys , NormalKeys , KeyInvoke_Void / KeyInvoke_Return )                   | ( bool , string ) | 注册热键，它的处理函数是无参、无返回值的         |
| EditHotKey_Keys    | ( KeyInvoke_Void / KeyInvoke_Return , ModelKeys , NormalKeys )                   |                   | 注册热键，它的处理函数是无参、有返回值的         |
| EditHotKey_Function| ( ModelKeys , NormalKeys , KeyInvoke_Void / KeyInvoke_Return )                   |                   | 注册热键，它的处理函数是无参、有返回值的         |

</details>

<details id="参数表GHK">
<summary>参数表</summary>

</details>

<details id="使用示例GHK">
<summary>使用示例</summary>

</details>

## BindingRef类

<details id="方法表BR">
<summary>方法表</summary>

| 方法名       | 参数                   | 返回值    | 描述               |
|--------------|------------------------|-----------|--------------------|
| Awake        |                        |           | 激活        |
| Destroy      |                        |           | 销毁   |
| Binding      | KeyInvoke_Void         |           | 要求传递一个无参、无返回值的函数签名，任何无参、无返回值的热键处理函数触发时，自动调用此函数           |
| Binding      | KeyInvoke_Return       |           | 要求传递一个无参、有返回值object的函数签名，接收到任何热键处理函数返回的object时，自动调用此函数             |

</details>

<details id="参数表BR">
<summary>参数表</summary>

</details>

<details id="使用示例BR">
<summary>使用示例</summary>

</details>

## RegisterInfo类

<details id="方法表RI">
<summary>方法表</summary>

| 方法名              | 参数                                                                 | 返回值    | 描述               |
|---------------------|----------------------------------------------------------------------|-----------|--------------------|
|RegisterInfo         |( int , ModelKeys , NormalKeys , KeyInvoke_Void / KeyInvoke_Return )  |           |初始化构造函数      |
|SuccessRegistration  |                                                                      | string    |在注册成功时调用，返回注册消息string      |
|LoseRegistration     |( ModelKeys , NormalKeys , KeyInvoke_Void / KeyInvoke_Return )        | string    |在注册失败时调用，返回注册消息string      |

</details>

<details id="参数表RI">
<summary>参数表</summary>

</details>

<details id="使用示例RI">
<summary>使用示例</summary>

</details>