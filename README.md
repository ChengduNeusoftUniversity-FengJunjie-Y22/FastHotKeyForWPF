# FastHotKeyForWPF
## �� NuGet�ĵ����ٸ���,��ѡ������·���鿴�����ĵ�
- [github][1]
- [gitee][2]

[1]: https://github.com/ChengduNeusoftUniversity-FengJunjie-Y22/FastHotKeyForWPF
[2]: https://gitee.com/CNU-FJj-Y22/FastHotKeyForWPF

## ��������
<details>
<summary>(1)��ι�����⹦�ܵ�����/�ر�</summary>

```csharp
//��Ҫ��дMainWindow��OnSourceInitialized����,��亯��ִ��ʱ,���ھ���Ѵ���,ȷ���˼�����ܹ���ȷִ��
protected override void OnSourceInitialized(EventArgs e)
{
    GlobalHotKey.Awake();
    //������⹦��

    base.OnSourceInitialized(e);
}

//��Ҫ��дMainWindow��OnClosed����,�����˳�ʱ,ִ����⹦�ܵĹرպ���
protected override void OnClosed(EventArgs e)
{
    GlobalHotKey.Destroy();
    //�ر���⹦��

    base.OnClosed(e);
}

//��Ҳ�����ڱ𴦵���Awake()��Destroy(),����ע�����Ĵ�������,�Լ�Destroy()��������ע����ȼ�
```

</details>

<details>
<summary>(2)����Ҫ�����ý���,ֻ���õ����º���</summary>

###### ע���������в���Ӧ������Awake()�ɹ�ִ�к�,Destroy()ִ��ǰ

##### ע���ȼ�
```csharp
//1.�Զ���һ���ȼ����غ���(LoadHotKey)
//2.�Զ���һ��������Ϊ�ȼ��������¼�(�ٶ����Զ�����һ��TestA����)
private void LoadHotKey()
{
    GlobalHotKey.Add(ModelKeys.CTRL, NormalKeys.F1, TestA);
    //ʹ��Add()ע���ȼ�
    //ModelKeys֧��ʹ��CTRL��ALT
    //NormalKeys֧��ʹ�����֡���ĸ��Fx
    //TestA������������һ���������κβ����ĺ���������Ϊ��void���������ߡ�����һ��object����ĺ�����
}
private void TestA()
{
    MessageBox.Show("����A");
}
```
```csharp
//3.��Awake()�ɹ�ִ�к�,���ü��غ���
protected override void OnSourceInitialized(EventArgs e)
{
    GlobalHotKey.Awake();

    LoadHotKey();
    //����������ע����,������Ҫע�����,�������ȼ���ϵͳ���ж�Ӧ����,�ǿ��ܻᵼ��һЩ����֮������

    base.OnSourceInitialized(e);
}
```

##### �༭�ȼ�
|GlobalHotKey       |����                                             |����                              |
|-------------------|-------------------------------------------------|----------------------------------|
|EditHotKey_Keys    |( Ŀ�괦���� , �µ�ModelKeys ���µ�NormalKeys )|�޸�һ�������Ĵ����ȼ�            |
|EditHotKey_Function|( Ŀ��ModelKeys , Ŀ��NormalKeys ���µĴ�����) |�޸�һ���ȼ���Ӧ�Ĵ�����        |

##### ɾ���ȼ�
|GlobalHotKey    |����                                     |����                              |
|----------------|-----------------------------------------|----------------------------------|
|Clear			 |                                         |���ע����ȼ�                    |
|DeleteByFunction|Ŀ�괦����                             |���ݺ���ǩ�������ע����ȼ�      |
|DeleteByKeys	 |( ModelKeys , NormalKeys )               |�����ȼ���������ע����ȼ�      |

</details>

<details>
<summary>(3)��Ҫ�����׵����ý���,����ʹ������ṩ��Ԥ�����</summary>

##### PrefabComponentԤ�����
|PrefabComponent        |����                                     |����                              |
|-----------------------|-----------------------------------------|----------------------------------|
|GetComponent< T >      |                                         |��ȡָ�����͵����(Ĭ����ʽ��)    |
|GetComponent< T >      |( ComponentInfo )                        |��ȡָ�����͵����(ָ��������ʽ��)|
|ProtectSelectBox< T >  |                                         |��������T���͵����               |
|UnProtectSelectBox< T >|                                         |��������T���͵����               |

##### ���
|֧�ֵ��������         |ʵ��                                     |����                              |
|-----------------------|-----------------------------------------|----------------------------------|
|KeySelectBox           |KeyBox:TextBox,Component                 |���յ����û����µļ�              |
|KeysSelectBox          |KeyBox:TextBox,Component                 |���������û����µļ�              |

##### ����������ط���
|ͨ��ʵ������           |����                                     |����                              |
|-----------------------|-----------------------------------------|----------------------------------|
|UseFatherSize< T >     |T��ʾ��������������                      |����Ӧ���������Ĵ�С,�����ݸ��������߶ȵ�70%����Ӧ�����С              |
|UseStyleProperty       |( string , string[] )                    |ͨ��ָ������ʽ����string�ҵ��Զ����Style���ٸ���string[]�У�ϣ��Ӧ�õ����Ե����ƣ�Ӧ��Style�еĲ�������              |
|UseFocusTrigger        |( TextBoxFocusChange enter , TextBoxFocusChange leave)|���������Զ���ĺ���,���ڴ�����������뿪ʱ,Ҫ����������,����һ����void���߱�һ��TextBox�����ĺ���     |
|Protect                |                                         |���������               |
|UnProtect              |                                         |���������               |

</details>

<details>
<summary>(4)���������ý��������Ҫ��ܸ�,����ʹ��Ԥ�����,��Ҫ�����˽���������</summary>



</details>

