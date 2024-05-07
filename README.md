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

<details id="功能" open>
<summary>功能</summary>

### 这是一款WPF类库项目，旨在提供更便捷的方式来管理全局热键
### 特点
#### 一句话式的功能实现
#### 可将函数签名直接注册进快捷键
#### 自带一套数据绑定，同样可以将一个函数签名绑定上去，以便在数据发生更新时自动调用绑定的函数

</details>

<details id="作者" open>
<summary>作者</summary>

### 关于作者本人
#### 写这个项目的时候作者在上大二，一般都是兴致来了就维护一下项目，正常来说会泡在二游里
### 联系方式
#### Bilibili:"真的不来一杯嘛"
#### QQ:2789083329
#### WeChat:WeC_FZJSOP4996

</details>

<details id="获取此类库" open>
<summary>获取此类库</summary>

### NuGet
#### 目前已经可以直接搜索到了，但此方法获取到的库无法查看XML注释文档，鼠标放在类名上是不会有提示词的，不过你可以照着这个文档用
### Github & Gitee
#### 下载完整项目，并将此类库项目添加到需要使用此类库的项目中，然后在解决方案资源管理器中引用你添加的类库项目

</details>

## GlobalHotKey类

<details id="方法表GHK" open>
<summary>方法表</summary>

| 类名         | 方法名       | 参数          | 描述          |
|--------------|--------------|---------------|---------------|
| GlobalHotKey | Register     | int id        | 注册全局热键   |
| GlobalHotKey | Unregister   | int id        | 取消注册全局热键 |
| BindingRef   | Bind         | Key key       | 绑定按键       |
| BindingRef   | Unbind       | Key key       | 解绑按键       |
| RegisterInfo | GetModifiers |               | 获取修改键列表  |
| RegisterInfo | GetKeys      |               | 获取绑定的按键列表 |

</details>

<details id="参数表GHK" open>
<summary>参数表</summary>

</details>

<details id="使用示例GHK" open>
<summary>使用示例</summary>

</details>

## BindingRef类

<details id="方法表BR" open>
<summary>方法表</summary>

</details>

<details id="参数表BR" open>
<summary>参数表</summary>

</details>

<details id="使用示例BR" open>
<summary>使用示例</summary>

</details>

## RegisterInfo类

<details id="方法表RI" open>
<summary>方法表</summary>

</details>

<details id="参数表RI" open>
<summary>参数表</summary>

</details>

<details id="使用示例RI" open>
<summary>使用示例</summary>

</details>