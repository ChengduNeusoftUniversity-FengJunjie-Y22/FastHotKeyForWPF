# FastHotKeyForWPF
## �� NuGet�ĵ����ٸ���,��ѡ������·���鿴�����ĵ�
- [github][1]
- [gitee][2]

[1]: https://github.com/ChengduNeusoftUniversity-FengJunjie-Y22/FastHotKeyForWPF
[2]: https://gitee.com/CNU-FJj-Y22/FastHotKeyForWPF

## ��������(����ʹ�ó�������,ֻ���ܳ���API+����ʾ��)
<details>
<summary>(1)������⹦�ܵ�����/�ر�</summary>

|GlobalHotKey��     |����               |
|-------------------|-------------------|
|Awake              |����ȫ���ȼ�����   |
|Destroy            |�ر�ȫ���ȼ�����   |

##### ����
```csharp
//��Ҫ��дMainWindow��OnSourceInitialized����,��亯��ִ��ʱ,���ھ���Ѵ���,ȷ���˼�����ܹ���ȷִ��
protected override void OnSourceInitialized(EventArgs e)
{
    GlobalHotKey.Awake();
    //������⹦��

    base.OnSourceInitialized(e);
}
```

##### �ر�
```csharp
//��Ҫ��дMainWindow��OnClosed����,�����˳�ʱ,ִ����⹦�ܵĹرպ���
protected override void OnClosed(EventArgs e)
{
    GlobalHotKey.Destroy();
    //�ر���⹦��

    base.OnClosed(e);
}
```
###### ��Ҳ�����ڱ𴦵���Awake()��Destroy(),����ע�����Ĵ�������,�Լ�Destroy()��������ע����ȼ�
</details>

<details>
<summary>(2)�ܱ������ȼ�����</summary>

#### ������ȡ


#### ������ɾ
|GlobalHotKey��     |����               |
|-------------------|-------------------|
|Awake              |����ȫ���ȼ�����   |
|Destroy            |�ر�ȫ���ȼ�����   |

</details>

<details>
<summary>(3)�����ȼ�--����Ҫ�����ý���</summary>

|GlobalHotKey��     |����                                             |����                              |
|-------------------|-------------------------------------------------|----------------------------------|
|Add                |( ModelKeys , NormalKeys , ������ )            |ע���ȼ���ModelKeys+ NormalKeys => ��������|
|EditHotKey_Keys    |( Ŀ�괦���� , �µ�ModelKeys ���µ�NormalKeys )|�޸�һ�������Ĵ����ȼ�            |
|EditHotKey_Function|( Ŀ��ModelKeys , Ŀ��NormalKeys ���µĴ�����) |�޸�һ���ȼ���Ӧ�Ĵ�����        |
|Clear			    |                                                 |���ע����ȼ�                    |
|DeleteByFunction   |( Ŀ�괦���� )                                 |���ݺ���ǩ�������ע����ȼ�      |
|DeleteByKeys	    |( ModelKeys , NormalKeys )                       |�����ȼ���������ע����ȼ�      |
|DeleteById         |( int )                                          |�����ȼ�ע��ʱ��id��(�Զ�����)���ע����ȼ�|

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


##### ɾ���ȼ�


</details>

<details>
<summary>(4)�����ȼ�--��������Զ������ȼ����������׵��ȼ����ý���</summary>

##### ֧�ֵ����
|����                   |ʵ��                                     |����                              |
|-----------------------|-----------------------------------------|----------------------------------|
|KeySelectBox           |KeyBox:TextBox,Component                 |���յ����û����µļ�              |
|KeysSelectBox          |KeyBox:TextBox,Component                 |���������û����µļ�              |

##### �����Ϣ�ı�ʾ
|ComponentInfo�����ֶ�  |����                        |Ĭ��|
|-----------------------|----------------------------|----|
|FontSize               |double                      |1   |
|Width                  |double                      |400 |
|Height                 |double                      |100 |
|FontSizeRate           |double                      |0.8 |
|WidthRate              |double                      |1   |
|HeightRate             |double                      |1   |
|Background             |SolidColorBrush             |Transparent |
|Foreground             |SolidColorBrush             |Transparent |
|BorderBrush            |SolidColorBrush             |Transparent |
|BorderThickness        |Thickness                   |0    |
|CornerRadius           |CornerRadius                |0    |
|Margin                 |Thickness                   |0    |

##### ��������ͨ�÷���
|����                   |����                                     |����                              |
|-----------------------|-----------------------------------------|----------------------------------|
|UseFatherSize< T >     |T��ʾ��������������                      |����Ӧ���������Ĵ�С,�����ݸ��������߶ȵ�70%����Ӧ�����С              |
|UseStyleProperty       |( string , string[] )                    |ͨ��ָ������ʽ����string�ҵ��Զ����Style���ٸ���string[]�У�ϣ��Ӧ�õ����Ե����ƣ�Ӧ��Style�еĲ�������              |
|UseFocusTrigger        |( TextBoxFocusChange enter , TextBoxFocusChange leave)|���������Զ���ĺ���,���ڴ�����������뿪ʱ,Ҫ����������,����һ����void���߱�һ��TextBox�����ĺ���     |
|Protect                |                                         |���������               |
|UnProtect              |                                         |���������               |

##### �����ͳһ����
|PrefabComponent��      |����                                     |����                              |
|-----------------------|-----------------------------------------|----------------------------------|
|GetComponent< T >      |                                         |��ȡָ�����͵����(Ĭ����ʽ��)    |
|GetComponent< T >      |( ComponentInfo )                        |��ȡָ�����͵����(��ָ��������ʽ��)|
|ProtectSelectBox< T >  |                                         |��������T���͵����               |
|UnProtectSelectBox< T >|                                         |��������T���͵����               |
|SetAsRoundBox< T >     |( Border )                               |��ָ����Border�ؼ���ΪԲ�Ǻ���(Ĭ����ʽ)|
|SetAsRoundBox< T >     |( Border ��ComponentInfo )               |��ָ����Border�ؼ���ΪԲ�Ǻ���(��ָ��������ʽ��)|

##### ����ͨ��
|BindingRef��           |����                                     |����                              |
|-----------------------|-----------------------------------------|----------------------------------|
|Connect                |(KeysSelectBox , ������ )              |����Ԥ������봦������İ󶨣������ȼ��Զ�����|
|Connect                |(KeySelectBox ��KeySelectBox , ������ )|����Ԥ������봦������İ󶨣������ȼ��Զ�����|
|DisConnect             |( Ԥ����� )                             |ȡ��Ԥ������봦������İ󶨣������Զ������ȼ�|
|BindingAutoEvent       |( ������ )                             |ָ��һ������������Ӧ���յ�object����ֵʱҪ��������|
|RemoveAutoEvent        |                                         |�����Ӧ����|

### ����ʾ��
##### XAML��,����һ��Border����ָ��Ԥ�������λ��
```xaml
<Window x:Class="TestDemo.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:TestDemo"
        mc:Ignorable="d"
        Title="MainWindow" Height="470" Width="800">
    <Viewbox>
        <Grid Height="450" Width="800">
            <!--Border���ڸ�Ԥ�����KeysSelectBox��λ-->
            <Border x:Name="Box1" Margin="174,131,404,269" Height="50"/>
        </Grid>
    </Viewbox>
</Window>
```
##### C# ��ӷ�Բ�����
```csharp
using FastHotKeyForWPF;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TestDemo
{
    public partial class MainWindow : Window
    {
        KeysSelectBox ksb = PrefabComponent.GetComponent<KeysSelectBox>(componentInfo);
        //ʹ��PrefabComponent�ṩ�ķ�����ȡ���

        private static ComponentInfo componentInfo = new ComponentInfo()
        //��ComponentInfo���ڻ�ȡԤ�����ʱ(��Բ��Ӧ�ó���������ҪΪ�����õ���ϢҲ��ͬ����ֻ�Ǹ����ڴ洢����ֵ�����ͣ���û�й涨������д�ļ�������
        {
            Foreground = Brushes.Cyan,
            Background = Brushes.Black,
        };

        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            GlobalHotKey.Awake();
            //����

            LoadHotKeys();
            //�������

            base.OnSourceInitialized(e);
        }

        protected override void OnClosed(EventArgs e)
        {
            GlobalHotKey.Destroy();
            //����

            base.OnClosed(e);
        }

        private void LoadHotKeys()
        {
            Box1.Child = ksb;
            //����ģʽ��Ҫ�ֶ���Ԥ���������������

            ksb.UseFatherSize<Border>(0.8);
            //����Ӧ����������С,0.8������Ӧ�õı���,���Բ���д���Լ�ָ������

            ksb.UseStyleProperty("MyBoxStyle");
            //ʹ��XAML���Զ������ʽ��Դ"MyBoxStyle"����������,û�ҵ��Ļ�,ʲôҲ����

            ksb.UseStyleProperty("MyBoxStyle", new string[] { "Width", "Height" });
            //ֻӦ��"MyBoxStyle"�е�"Width"��"Height"����

            ksb.IsDefaultColorChange = false;
            //�ر�Ĭ�ϵĽ�������¼�(1.1.6��ʼ��Ĭ�϶��ǹرյ�)

            ksb.UseFocusTrigger(Enter, Leave);
            //ʹ�����Զ���Ľ�������¼�
        }

        private void Enter(TextBox e)
        {
            //��������Ԥ�����ʱ
        }

        private void Leave(TextBox e)
        {
            //������뿪Ԥ�����ʱ
        }
    }
}
```
##### C# ���Բ�����
```csharp
using FastHotKeyForWPF;
using System.Windows;
using System.Windows.Media;

namespace TestDemo
{
    public partial class MainWindow : Window
    {
        KeysSelectBox? ksb;
        //Ԥ���������������ⲿ��ֱ��ʵ����,��Ҫͨ��PrefabComponent�ṩ����ط�������ȡ

        ComponentInfo componentInfo = new ComponentInfo()
        //Ԥ���������ʽ��Ϣ
        {
            BorderBrush = Brushes.Cyan,
            BorderThickness = new Thickness(1),
            Background = Brushes.Black,
            Foreground = Brushes.Cyan,

            CornerRadius = new CornerRadius(5),
            //����Բ�ǰ뾶

            FontSizeRate = 0.8,
            //�����С���� * ���������ĸ߶� = Ԥ������������С
            //ע�⸸��Border�����ĸ߶Ȳ���Ϊdouble.NAN,��Ҫ��ʽ��ָ���߶�
        };

        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            GlobalHotKey.Awake();
            LoadHotKeys();
            base.OnSourceInitialized(e);
        }

        protected override void OnClosed(EventArgs e)
        {
            GlobalHotKey.Destroy();
            base.OnClosed(e);
        }

        private void LoadHotKeys()
        {
            ksb = PrefabComponent.SetAsRoundBox<KeysSelectBox>(Box1, componentInfo);
        }
    }
}
```
### ���Ԥ������봦��������ϵ,�����ȼ���ȫ�Զ�����
```csharp
using FastHotKeyForWPF;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TestDemo
{
    public partial class MainWindow : Window
    {
        KeysSelectBox ksb = PrefabComponent.GetComponent<KeysSelectBox>(componentInfo);

        private static ComponentInfo componentInfo = new ComponentInfo()
        {
            Foreground = Brushes.Cyan,
            Background = Brushes.Black,
        };

        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            GlobalHotKey.Awake();
            //����

            LoadHotKeys();
            //�������

            base.OnSourceInitialized(e);
        }

        protected override void OnClosed(EventArgs e)
        {
            GlobalHotKey.Destroy();
            //����

            base.OnClosed(e);
        }

        private void LoadHotKeys()
        {
            Box1.Child = ksb;

            BindingRef.BindingAutoEvent(WhileGetObject);
            //InvokeHotKey����һ��object,����ϣ��ʵʱ���InvokeHotKey�Ĵ������,���õ����object����һ���Ĵ�����ô����ͨ����亯������ʵʱ��ⷵ��ֵ�Ĺ���

            GlobalHotKey.IsUpdate = false;
            //��ʱ�ر�ʵʱ��⣬���������Ӧ����

            BindingRef.RemoveAutoEvent();
            //���ʵʱ��Ӧ

            BindingRef.Connect(ksb, InvokeHotKey);
            //ksb�������û�����İ������Զ������ȼ����Զ�ע����ȼ���ȥ�����Զ����InvokeHotKey����
            //����ksb��һ����ͬʱ��������������Ԥ������������ʹ��һ�ν���һ�������ĺ��ӣ�Connect()��Ҫͬʱ������������+һ��������
            //����ͬʱע�����߱�object����ֵ�Ĵ��������κ�һ�����������������Զ���Ӧ�󶨵�WhileGetObject()

            BindingRef.DisConnect(ksb);
            //������ӹ�ϵ���ر��Զ�����
            //����������������ӱ����ӣ�ֻ��Ҫ���������κ�һ�����ӣ����ܽ��
        }

        private object InvokeHotKey()
        {
            return "�ȼ���������";
        }

        private void WhileGetObject()
        {
            if (ksb != null) { return; }
            if (BindingRef.Value is string info)
            //�����õ����µķ���ֵ��������object��ʵ�����ͣ�������ͬ�Ĵ���
            {
                MessageBox.Show($"���յ��ķ���ֵ:{info}");
            }
        }
    }
}
```

</details>

## ��Ԥ������޷����������������,������ȫ�˽�����API

<details>
<summary>API</summary>

### �� GlobalHotKey
|����               |����                                             |����                              |
|-------------------|-------------------------------------------------|----------------------------------|
|Awake              |                                                 |����ȫ���ȼ�����   |
|Destroy            |                                                 |�ر�ȫ���ȼ�����   |
|Add                |( ModelKeys , NormalKeys , ������ )            |ע���ȼ���ModelKeys+ NormalKeys => ��������|
|EditHotKey_Keys    |( Ŀ�괦���� , �µ�ModelKeys ���µ�NormalKeys )|�޸�һ�������Ĵ����ȼ�            |
|EditHotKey_Function|( Ŀ��ModelKeys , Ŀ��NormalKeys ���µĴ�����) |�޸�һ���ȼ���Ӧ�Ĵ�����        |
|Clear			    |                                                 |���ע����ȼ�                    |
|DeleteByFunction   |( Ŀ�괦���� )                                 |���ע����ȼ�(���ݺ���ǩ��)      |
|DeleteByKeys	    |( ModelKeys , NormalKeys )                       |���ע����ȼ�(�����ȼ����)      |
|DeleteById         |( int )                                          |���ע����ȼ�(����ע��id��)|
|ProtectHotKeyByKeys|( ModelKeys , NormalKeys )                       |����ܱ������ȼ�(������ϼ�)|
|ProtectHotKeyById  |( int )                                          |����ܱ������ȼ���(����ע��id)|
|UnProtectHotKeyByKeys|( ModelKeys , NormalKeys )                     |ɾ���ܱ������ȼ�(������ϼ�)|
|UnProtectHotKeyById  |( int )                                        |ɾ���ܱ������ȼ�(����ע��id)|

|����               |����       |Ĭ��       |����                              |
|-------------------|-----------|-----------|----------------------------------|
|IsDeBug            |bool       |false      |��Ϊtrue,���ֹ��̽���ӡ����ֵ     |
|IsUpdate           |bool       |true       |��Ϊtrue,������BindingRef��ʵʱ��ⷵ��ֵ     |
|HOTKEY_ID          |int        |2004       |��һ���ȼ���ע��id��������     |
|ReturnValue        |object?    |null       |���յ������·���ֵ     |
|Registers          |List< RegisterInfo >       |           |ע�����е������ȼ�����Ϣ     |
|ProtectedHotKeys   |List< Tuple< ModelKeys , NormalKeys > >       |           |�ܱ������ȼ�����������ɾ��     |

### �� BindingRef
|����               |����                                             |����                              |
|-------------------|-------------------------------------------------|----------------------------------|
|Awake              |                                                 |�����⹦�ܣ���GlobalHotKey����ʱ�Զ�����һ��   |
|Destroy            |                                                 |�رռ�⹦�ܣ���GlobalHotKey�ر�ʱ�Զ�����һ��   |
|BindingAutoEvent   |( һ�������� )                                 |���յ�����ֵʱ���Զ������˴��󶨵Ĵ�����   |
|RemoveAutoEvent    |                                                 |��Ȼ��ⷵ��ֵ�����Ǽ�⵽����ֵ�󣬲��ٴ����¼�   |
|Connect            |( KeySelectBox , KeySelectBox , ������ )       |��������Ԥ�����+һ��������,�����ȼ����Զ�����   |
|Connect            |( KeysSelectBox  , ������ )                    |����һ��Ԥ�����+һ���������������ȼ����Զ�����   |
|DisConnect         |( KeysSelectBox )                                |ȡ������   |
|DisConnect         |( KeySelectBox )                                 |ȡ������   |

|����               |����       |Ĭ��       |����                              |
|-------------------|-----------|-----------|----------------------------------|
|Value              |object?    |null       |���½��յ��ķ���ֵ                |

### �� RegisterInfo
|����               |����                                  |����      |����                  |
|-------------------|--------------------------------------|----------|----------------------|
|SuccessRegistration|                                      |string    |�ֶ���ȡ�ɹ���ע����Ϣ|
|LoseRegistration   |( ModelKeys , NormalKeys , ������ ) |string    |�ֶ���ȡʧ�ܵ�ע����Ϣ|

|����                   |����                        |����        |
|-----------------------|----------------------------|------------|
|RegisterID             |int                         |ע��id����ʼΪ2004���Զ�����        |
|Model                  |ModelKeys                   |�ȼ���� |
|Normal                 |NormalKeys                  |�ȼ��Ұ� |
|FunctionType           |FunctionTypes               |�ȼ���Ӧ�������ĺ������� |
|Name                   |string                      |�������ĺ�����   |
|FunctionVoid           |KeyInvoke_Void              |���ܵĴ�����A   |
|FunctionReturn         |KeyInvoke_Return            |���ܵĴ�����B   |

</details>

<details>
<summary>���������ʱ,����˼·���ԽϺõؽ���⹦�������Զ���Ŀؼ�����</summary>


</details>

## ���ºϼ�
[ǰ��Bilibili�鿴�����汾����Ƶ��ʾ][1]

[1]: https://www.bilibili.com/video/BV1rr421L7qR

<details>
<summary>Version 1.1.5</summary>

#### (1)�޸������뿪���Ӻ󣬺�����Ȼ�ڽ����û�������bug
#### (2)�޸��޷�ʹ��ʵ������Protect()�������ӵ�����
#### (3)�������֮���ͨ�Ż���,�Զ������ظ����ȼ�

</details>

<details>
<summary>Version 1.1.6</summary>

#### (1)�ṩ���伴�õ�Բ�����
#### (2)Ĭ�ϲ�ʹ�ñ�ɫЧ��,��Ҫ�û��Զ����Ӧ����
#### (3)��DeBugģʽ������ע��ɹ�������ʾ,��Ҫ�û��Զ����Ӧ����
#### (4)����һ����������,�����е��κ��ȼ���������ɾ��,��������ȼ�û�б�ע���
#### (5)������̬����,���ڻ�ȡע����Ϣ�ͱ�������

</details>

