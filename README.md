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
|GlobalHotKey����   |����                                |
|-------------------|------------------------------------|
|ProtectedHotKeys   |List<Tuple<ModelKeys, NormalKeys>>? |

#### ������ɾ
|GlobalHotKey����   |����                                 |����                              |
|-------------------|-------------------------------------|----------------------------------|
|ProtectHotKeyByKeys|( ModelKeys , NormalKeys )           |���ܱ��������������ȼ���ϣ�ֱ����ӣ�        |
|ProtectHotKeyById  |int                                  |���ܱ��������������ȼ���ϣ�������id�ҵ��ȼ�������ӣ�       |
|UnProtectHotKeyByKeys|( ModelKeys , NormalKeys )         |���ָ���ȼ��ı�����ֱ�ӽ����               |
|UnProtectHotKeyById  |int                                |���ָ���ȼ��ı�����������id�ҵ��ȼ����ٽ����  |

###### �ܱ������ȼ�������ע����񣬶����ɶ���ִ��ע�ᡢ�޸ġ�ɾ��������������Щ�ȼ�����ϵͳ�ȼ��ķ��룬���ʺϱ��޸ģ����Էŵ���������£�
</details>

<details>
<summary>(3)ע���ȼ�--����Ҫ�����ý���</summary>

|GlobalHotKey��     |����                                             |����                              |
|-------------------|-------------------------------------------------|----------------------------------|
|Add                |( ModelKeys , NormalKeys , ������ )            |ע���ȼ���ModelKeys+ NormalKeys => ��������|

#### ����ʾ��
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
#### XAML��,����Border����ָ��Ԥ�������λ��
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
            <!--Border���ڸ�Ԥ�������λ-->
            <Border x:Name="Box1" Margin="109,39,341,361" Height="50"/>
            <Border x:Name="Box2" Margin="109,162,537,238" Height="50"/>
            <Border x:Name="Box3" Margin="305,162,341,238" Height="50"/>
        </Grid>
    </Viewbox>
</Window>
```
#### C#��,�ֱ�ʹ��KeySelectBox��KeysSelectBox���,�Զ��ع��������ȼ�
```csharp
using FastHotKeyForWPF;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TestDemo
{
    public partial class MainWindow : Window
    {
        KeySelectBox? k1;
        KeySelectBox? k2;
        //k1��k2�ֱ�ֻ�ܽ���һ������,������Ҫ��k1,k2,�������������Ӳ����Զ������ȼ�
        //������Ϊ��ֱ�����ó�Բ�����,������Ҫд�ɿɿ������Ҳ����и�ֵ���ʼ������

        KeysSelectBox k3 = PrefabComponent.GetComponent<KeysSelectBox>(RectComponentInfo);
        //k3����ͬʱ������������,����Ҫ��k3,�������������ӾͿ����Զ������ȼ�

        private static ComponentInfo RectComponentInfo = new ComponentInfo()
        //ComponentInfoֻ��һ������ֵ�Ĵ洢ý��,����ʵ�����ܶ�����ֶ�,����ͬ����Ĳ�ͬ����,��ʵֻ���õ�һ���ִ洢������ֵ,���û�б�ҪΪ�������Ը�ֵ
        //���磬���������Ϣֻ�����ڻ�ȡ�򵥾�������������������Ծ͹�����
        {
            Foreground = Brushes.Cyan,
            Background = Brushes.Black,
        };
        private static ComponentInfo RoundComponentInfo = new ComponentInfo()
        //����,���������Ϣ���ڻ�ȡԲ�����,�������Ҫ�������������Ի�ȡ���õ�Ч��
        {
            BorderBrush = Brushes.Red,
            BorderThickness = new Thickness(1),
            CornerRadius = new CornerRadius(10),
            Background = Brushes.LightGray,
            Foreground = Brushes.Cyan,
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
            Box1.Child = k3;
            k3.UseFatherSize<Border>();
            //��Բ����������

            k1 = PrefabComponent.SetAsRoundBox<KeySelectBox>(Box2, RoundComponentInfo);
            k2 = PrefabComponent.SetAsRoundBox<KeySelectBox>(Box3, RoundComponentInfo);
            //��ָ����Border����Ƕ��Ԥ��������γ�Բ��

            k1.UseFocusTrigger(EnterColor, LeaveColor);
            k2.UseFocusTrigger(EnterColor, LeaveColor);
            k3.UseFocusTrigger(EnterColor, LeaveColor);
            //�������ؼ�ʱ��ӵ�б�ɫЧ��

            k3.UseSuccessTrigger(WhileSuccessRegister);
            k1.UseSuccessTrigger(WhileSuccessRegister);
            k2.UseSuccessTrigger(WhileSuccessRegister);
            //�ɹ�ע���ȼ�ʱ�������û���ʾ

            k3.UseFailureTrigger(WhileFailRegister);
            k1.UseFailureTrigger(WhileFailRegister);
            k2.UseFailureTrigger(WhileFailRegister);
            //ע��ʧ��ʱ�������û���ʾ

            BindingRef.Connect(k1, k2, TestA);
            BindingRef.Connect(k3, InvokeHotKey);
            //�������ӹ�ϵ�������Զ�����

            BindingRef.BindingAutoEvent(WhileGetObject);
            //InvokeHotKey()���ص�object���󽫱����񣬲���WhileGetObject()�����У������object����һ������
        }

        private void TestA()//��k1��k2����
        {
            MessageBox.Show("������TestA��");
        }

        private object InvokeHotKey()//��k3����
        {
            return "�ȼ���������";
        }

        private void WhileGetObject()//�Լ�⵽��object����ֵ����һ������
        {
            if (k3 == null) { return; }
            if (BindingRef.Value is string info)
            {
                MessageBox.Show($"���յ��ķ���ֵ:{info}");
            }
        }

        //ע��
        //�°汾�У��������������¼�����ע������ʾ�¼����������ǣ�object sender��
        //sender��ʾ����������¼��Ķ������������sender�ľ������ͣ�����������޸�

        private void WhileSuccessRegister(object sender)//��ע��ɹ�
        {
            MessageBox.Show("ע��ɹ�!");
        }

        private void WhileFailRegister(object sender)//��ע��ʧ��
        {
            if (sender is KeysSelectBox e)
            {
                e.Text = e.DefaultErrorText;
                //DefaultErrorText��ʾĬ��ע��ʧ��ʱ������ı���ʾΪʲô���ݣ�Ĭ��"Error"
            }
        }

        private void EnterColor(object sender)//���������ʱ
        {
            if (sender is KeySelectBox e1)
            {
                e1.Foreground = Brushes.Red;

                Border? border = e1.Parent as Border;              
                if (border != null) { border.Background = Brushes.Wheat; }
                //ע���������Ҫ�޸�Բ������ı���ɫ����������Ҫ�޸ĸ��������ı���ɫ�������������ı���ɫ
            }
            else if (sender is KeysSelectBox e2)
            {
                Border? border = e2.Parent as Border;
                e2.Foreground = Brushes.Cyan;
                if (border != null) { border.Background = Brushes.Black; }
            }
        }

        private void LeaveColor(object sender)//����뿪���ʱ
        {
            if (sender is KeySelectBox e1)
            {
                Border? border = e1.Parent as Border;
                e1.Foreground = Brushes.Cyan;
                if (border != null) { border.Background = Brushes.LightGray; }
            }
            else if (sender is KeysSelectBox e2)
            {
                Border? border = e2.Parent as Border;
                e2.Foreground = Brushes.Red;
                if (border != null) { border.Background = Brushes.Wheat; }
            }
        }
    }
}
```
</details>

## ��Ԥ������޷����������������,����Ҫ��ȫ�˽�����API

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
<summary>��Ҫ�����Զ���Ŀؼ����ʹ��,��Ҫ�˽���������</summary>

### 1.�ȼ�����ɾ�Ĳ�
#### GlobalHotKeyע���ȼ� => Registers�����ڵ�ע����Ϣ�����䶯 => ͨ������Registers�ڵ�RegisterInfo���󣬲�ѯָ���ȼ���ȫ����Ϣ => ����id�š��ȼ�������������Ϣ������GlobalHotKey�ṩ��ϵ�з�������ɾ��ȫ���ȼ� => Registers�����ڵ�ע����Ϣ�����䶯 => ����

### 2.ʵʱ������
#### ������ע���ˡ�CTRL+F1��=>��TestA������һ���ȼ�����TestA()�߱�һ��object����ֵ����ʱ��ֻҪ���¡�CTRL+F1�����ͻᴥ��TestA()������һ��object���󣬶�BindingRef���⵽���ֵ�����Զ�����BindingAutoEvent()�󶨵Ĵ�������

### 3.һЩ����
##### ��ע�᡿��������ִ��һ������ǰ�õ�Delete()������û�б�Ҫ��
##### ��GlobalHotKey���ṩ�������й��ȼ��ı䶯�����������Զ�����Registers���ϣ�������ֻ��

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

