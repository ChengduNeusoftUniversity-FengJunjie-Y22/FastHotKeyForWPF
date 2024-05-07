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

| 方法名             | 参数                                                                             | 返回值                 | 描述               |
|--------------------|----------------------------------------------------------------------------------|------------------------|--------------------|
| Awake              |                                                                                  |                        | 激活               |
| Destroy            |                                                                                  |                        | 销毁               |
| Add                | ( ModelKeys , NormalKeys , KeyInvoke_Void / KeyInvoke_Return )                   | Tuple( bool , string ) | 注册热键，它的处理函数是无参、无返回值的         |
| EditHotKey_Keys    | ( KeyInvoke_Void / KeyInvoke_Return , ModelKeys , NormalKeys )                   |                        | 依据组合键查找对应的处理函数，并替换为新的处理函数         |
| EditHotKey_Function| ( ModelKeys , NormalKeys , KeyInvoke_Void / KeyInvoke_Return )                   |                        | 依据现有处理函数，查找能触发它的组合键，并替换为新的组合键 |

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

| 方法名       | 参数                                 | 返回值    | 描述               |
|--------------|--------------------------------------|-----------|--------------------|
| Awake        |                                      |           | 激活   |
| Destroy      |                                      |           | 销毁   |
| Binding      | KeyInvoke_Void / KeyInvoke_Return    |           | 将某个自定义的函数绑定至BindingRef,当BindingRef接收到热键处理函数的返回值时，自动调用这个绑定的函数   |

</details>

<details id="参数表BR">
<summary>参数表</summary>

</details>

<details id="使用示例BR">
<summary>使用示例</summary>

</details>

## RegisterInfo类
### 用于存储注册信息的类型
<details id="方法表RI">
<summary>方法表</summary>

| 方法名              | 参数                                                                 | 返回值    | 描述                       |
|---------------------|----------------------------------------------------------------------|-----------|----------------------------|
|RegisterInfo         |( int , ModelKeys , NormalKeys , KeyInvoke_Void / KeyInvoke_Return )  |           |初始化构造函数              |
|SuccessRegistration  |                                                                      | string    |在注册成功时调用注册信息    |
|LoseRegistration     |( ModelKeys , NormalKeys , KeyInvoke_Void / KeyInvoke_Return )        | string    |在注册失败时调用注册信息    |
</details>

<details id="参数表RI">
<summary>属性表</summary>

| 属性名              | 类型                                                                 | 权限            | 描述               |
|---------------------|----------------------------------------------------------------------|-----------------|--------------------|
| RegisterID          | int                                                                  | public readonly | 注册编号           |
| Model               | enum:uint ModelKeys                                                  | public readonly | 修饰键             |
| Normal              | enum:uint NormalKeys                                                 | public readonly | 一般键             |
| FunctionType        | enum:uint FunctionTypes                                              | public readonly | 处理函数类型       |
| Name                | string                                                               | public readonly | 处理函数的函数名   |
| FunctionVoid        | delegate KeyInvoke_Void                                              | public          | 存放处理函数       |
| FunctionReturn      | delegate KeyInvoke_Return                                            | public          | 存放处理函数       |
</details>

<details id="使用示例RI">
<summary>注意</summary>

### 一般来说，不会直接使用RegisterInfo对象的方法，它的存在是为了便于在 GlobalHotKey中管理热键信息的登记、查询、修改。具体而言,当GlobalHotKey激活后，可以使用它的静态方法HotKeyInfo获取当前注册在列的所有热键消息的集合List,然后去访问这个集合中的RegisterInfo对象的属性。
</details>