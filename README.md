# FastHotKeyForWPF
## ☆ NuGet文档不再更新,可选择以下路径查看最新文档
- [github][1]
- [gitee][2]

[1]: https://github.com/ChengduNeusoftUniversity-FengJunjie-Y22/FastHotKeyForWPF
[2]: https://gitee.com/CNU-FJj-Y22/FastHotKeyForWPF

## 使用指南
<details>
<summary>(1)管理类库功能的启用/关闭</summary>

|GlobalHotKey       |功能               |
|-------------------|-------------------|
|Awake              |激活全局热键功能   |
|Destroy            |关闭全局热键功能   |

##### 启用
```csharp
//需要重写MainWindow的OnSourceInitialized函数,这句函数执行时,窗口句柄已存在,确保了激活函数能够正确执行
protected override void OnSourceInitialized(EventArgs e)
{
    GlobalHotKey.Awake();
    //激活类库功能

    base.OnSourceInitialized(e);
}
```

##### 关闭
```csharp
//需要重写MainWindow的OnClosed函数,程序退出时,执行类库功能的关闭函数
protected override void OnClosed(EventArgs e)
{
    GlobalHotKey.Destroy();
    //关闭类库功能

    base.OnClosed(e);
}
```
###### 你也可以在别处调用Awake()或Destroy(),但请注意句柄的存在问题,以及Destroy()会销毁已注册的热键
</details>

<details>
<summary>(2)注册热键--不需要做设置界面</summary>

|GlobalHotKey       |参数                                             |功能                              |
|-------------------|-------------------------------------------------|----------------------------------|
|EditHotKey_Keys    |( 目标处理函数 , 新的ModelKeys ，新的NormalKeys )|修改一个函数的触发热键            |
|EditHotKey_Function|( 目标ModelKeys , 目标NormalKeys ，新的处理函数) |修改一个热键对应的处理函数        |
|Clear			    |                                                 |清空注册的热键                    |
|DeleteByFunction   |目标处理函数                                     |依据函数签名来清除注册的热键      |
|DeleteByKeys	    |( ModelKeys , NormalKeys )                       |依据热键组合来清除注册的热键      |

##### 注册热键
```csharp
//1.自定义一个热键加载函数(LoadHotKey)
//2.自定义一个函数作为热键触发的事件(假定你自定义了一个TestA函数)
private void LoadHotKey()
{
    GlobalHotKey.Add(ModelKeys.CTRL, NormalKeys.F1, TestA);
    //使用Add()注册热键
    //ModelKeys支持使用CTRL与ALT
    //NormalKeys支持使用数字、字母、Fx
    //TestA条件：必须是一个不包含任何参数的函数；允许为【void函数】或者【返回一个object对象的函数】
}
private void TestA()
{
    MessageBox.Show("测试A");
}
```
```csharp
//3.在Awake()成功执行后,调用加载函数
protected override void OnSourceInitialized(EventArgs e)
{
    GlobalHotKey.Awake();

    LoadHotKey();
    //到这里就完成注册了,不过需要注意的是,如果这个热键在系统中有对应功能,那可能会导致一些意料之外的情况

    base.OnSourceInitialized(e);
}
```

##### 编辑热键


##### 删除热键


</details>

<details>
<summary>(3)注册热键--包含简易的热键设置界面</summary>

##### 组件管理
|PrefabComponent        |参数                                     |功能                              |
|-----------------------|-----------------------------------------|----------------------------------|
|GetComponent< T >      |                                         |获取指定类型的组件(默认样式的)    |
|GetComponent< T >      |( ComponentInfo )                        |获取指定类型的组件(指定部分样式的)|
|ProtectSelectBox< T >  |                                         |锁定所有T类型的组件               |
|UnProtectSelectBox< T >|                                         |解锁所有T类型的组件               |

##### 支持的组件类型
|支持的组件类型         |实现                                     |功能                              |
|-----------------------|-----------------------------------------|----------------------------------|
|KeySelectBox           |KeyBox:TextBox,Component                 |接收单个用户按下的键              |
|KeysSelectBox          |KeyBox:TextBox,Component                 |接收两个用户按下的键              |

##### 组件对象的方法
|通用实例方法           |参数                                     |功能                              |
|-----------------------|-----------------------------------------|----------------------------------|
|UseFatherSize< T >     |T表示父级容器的类型                      |自适应父级容器的大小,并依据父级容器高度的70%自适应字体大小              |
|UseStyleProperty       |( string , string[] )                    |通过指定的样式名称string找到自定义的Style，再根据string[]中，希望应用的属性的名称，应用Style中的部分属性              |
|UseFocusTrigger        |( TextBoxFocusChange enter , TextBoxFocusChange leave)|填入两个自定义的函数,用于处理鼠标进入和离开时,要触发的事情,它们一定是void、具备一个TextBox参数的函数     |
|Protect                |                                         |锁定该组件               |
|UnProtect              |                                         |解锁该组件               |

</details>

<details>
<summary>(4)注册热键--需要做设置界面,并且界面美术要求更高,不想使用预制组件--你需要额外了解以下内容</summary>

#### Ⅰ GlobalHotKey的热键管理机制



#### Ⅱ BindingRef的传递机制



#### Ⅲ RegisterInfo包含了哪些信息

</details>

## 更新合集
[前往Bilibili查看各个版本的视频演示][1]

[1]: https://www.bilibili.com/video/BV1rr421L7qR

<details>
<summary>Version 1.1.5</summary>

#### (1)修复焦点离开盒子后，盒子仍然在接收用户按键的bug
#### (2)修复无法使用实例方法Protect()锁定盒子的问题
#### (3)新增组件之间的通信机制,自动清理重复的热键

</details>

<details>
<summary>Version 1.1.6</summary>

#### (1)提供圆角盒子来接收用户输入
#### (2)不再使用默认的焦点进出事件
#### (3)允许自定义函数,用于实现注册成功or失败的提示功能
#### (4)新增一个保护名单,名单中的任何热键不允许被修改、删除
#### (5)新增一个属性用于访问当前所有注册在列的热键信息

</details>

