# FastHotKeyForWPF
## �� NuGet documentation is no longer updated, please check out github or gitee
- [github][1]
- [gitee][2]

[1]: https://github.com/ChengduNeusoftUniversity-FengJunjie-Y22/FastHotKeyForWPF
[2]: https://gitee.com/CNU-FJj-Y22/FastHotKeyForWPF
## Ŀ¼
###### Ŀǰֻ�к��Ĺ��ܺ����Ƚ�ȫ�����ڲ��ĵ���һ����д��ĺ��ѣ����£�
- [��Ŀ���](#��Ŀ���)

<details>
<summary>�� ���Ĺ��ܺ����������￪ʼ�����Կ����˽����ʹ������</summary>

- [�� GlobalHotKey��](#GlobalHotKey��)

- [�� BindingRef��](#BindingRef��)

- [�� PrefabComponent��](#PrefabComponent��)

</details>

<details>
<summary>�� �������ݣ��������˽������ʵ��˼·��</summary>

- [RegisterInfo��](#RegisterInfo��)

- [ComponentInfo��](#ComponentInfo��)

- [KeySelectBox�� / KeysSelectBox��](#KeySelectBox��/KeysSelectBox��)

</details>

## ��Ŀ���

<details>
<summary>����</summary>

#### ����һ��WPF�����Ŀ��ּ����һ�ָ����ŵķ�ʽ����WPFȫ���ȼ�
#### �ص�
##### (1)���ܺ����������
##### (2)�ȼ��������ɸ߶��Զ���
##### (3)���Զ�����ȼ��������ķ���ֵ
##### (4)�ṩԤ�Ƶġ�������������ڿ��ٹ�����ݼ������ý���

</details>

<details>
<summary>����</summary>

##### �������߱���
###### д�����Ŀ��ʱ���������ϴ����һ�㶼���������˾�ά��һ����Ŀ��������˵�����ڶ�����
##### ��ϵ��ʽ
###### Bilibili: "��Ĳ���һ����"
###### QQ: 2789083329
###### WeChat: WeC_FZJSOP4996

</details>

<details>
<summary>��ȡ�����</summary>

#### NuGet
##### ���������Դ������ => �Ҽ���Ŀ => ����NuGet����� => ����FastHotKeyForWPF => ��װ���°� ����Ҫ�޸���Ŀ��Ŀ��������Ϊ.NET 8.0��
#### Github & Gitee
##### ����Zip => ��ѹ => ������Ŀ��WPF��Ŀ����ͬһ����������� => �Ҽ����ñ���Ŀ

</details>

## ǰ�ö���

<details>
<summary>ö��</summary>

##### �� ��enum ModelKeys : uint�� ��ݼ������β��֣�Ŀǰ֧����Ctrl\Alt��Ϊ����
##### �� ��enum NormalKeys : uint�� [Model+Normal]����һ���ȼ�,Ŀǰ֧�֡�A-Z����F1-F12����0-9����Up\Down\Left\Right����Space��
##### �� ��enum FunctionTypes�� �����ķ���ֵ���ͣ�Void\Return(��\�з���ֵ)
</details>

<details>
<summary>ί��</summary>

##### �� ��delegate void KeyInvoke_Void()�� ֧�ֽ��޲Ρ��޷���ֵ�ĺ�����ǩ����Ϊ������ע��Ϊ�ȼ��Ĵ�����
##### �� ��delegate object KeyInvoke_Return()�� ֧�ֽ��޲Ρ�����һ��object�ĺ�����ǩ����Ϊ������ע��Ϊ�ȼ��Ĵ�����
</details>

## �� GlobalHotKey��
#### ȫ���ȼ�ע�ᡢ�޸ġ���ѯ�����ٵ���Ҫʵ��
<details>
<summary>����</summary>

| ������             | ����                                                          | ����ֵ                 | ����                                                       |
|--------------------|---------------------------------------------------------------|------------------------|------------------------------------------------------------|
| Awake              |                                                               |                        | ����                                                       |
| Destroy            |                                                               |                        | ����                                                       |
| Add                | ( ModelKeys , NormalKeys , KeyInvoke_Void / KeyInvoke_Return )| Tuple( bool , string ) | ע���ȼ������Ĵ��������޲Ρ��޷���ֵ��                   |
| EditHotKey_Keys    | ( KeyInvoke_Void / KeyInvoke_Return , ModelKeys , NormalKeys )|                        | ������ϼ����Ҷ�Ӧ�Ĵ����������滻Ϊ�µĴ�����         |
| EditHotKey_Function| ( ModelKeys , NormalKeys , KeyInvoke_Void / KeyInvoke_Return )|                        | �������д������������ܴ���������ϼ������滻Ϊ�µ���ϼ� |
| Clear              |                                                               |                        | ����ȼ��������������                                     |
| DeleteById         | int                                                           |                        | ����ע���������ע����ȼ�                               |
| DeleteByFunction   | KeyInvoke_Void / KeyInvoke_Return                             |                        | ���ݺ���ǩ�������ע����ȼ� |
| DeleteByKeys       | ( enum ModelKeys , enum NormalKeys )                          |                        | �����ȼ���������ע����ȼ� |

</details>

<details>
<summary>��ѡ��</summary>

| ������              | ����                           | Ĭ��                                 | ����                                                             |
|---------------------|--------------------------------|--------------------------------------|------------------------------------------------------------------|
| IsDeBug             | bool                           | false                                | �Ƿ�������ģʽ�����ֹ��̽�ʹ��MessageBox�������ֵ��           |
| IsUpdate            | bool                           | true                                 | �Ƿ�ʵʱ��ⷵ��ֵ                                               |
| HOTKEY_ID           | int                            | 2004                                 | ��һ���ȼ���ע���ţ�ֻ����������ע�������ʼǰ�޸�һ��         |
</details>

<details>
<summary>ʾ��</summary>

#### �� ���´�����ʾ�����ʹ��Awake()��Destroy()����GlobalHotKey�ļ���������
##### ��Ȼ����Ҳ�����������ط�ʹ��������������������Ҫע�� : Awake()������Ҫ��MainWindow�ľ���Ѿ�����ʱ����ȥ���á�������дOnSourceInitialized��OnClosed�ǱȽ��Ƽ��ķ���
```csharp
using FastHotKeyForWPF;

namespace TestForHotKeyDll
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        protected override void OnSourceInitialized(EventArgs e)
        {
            GlobalHotKey.Awake();//����
            base.OnSourceInitialized(e);
        }
        protected override void OnClosed(EventArgs e)
        {
            GlobalHotKey.Destroy();//����
            base.OnClosed(e);
        }
    }
}
```

#### �� ���´�����ʾ�����ʹ��Add()ע���ȼ������ʹ��EditHotKey_Keys()��EditHotKey_Function()�޸��ȼ�
##### ע��:
##### (1)����ʹ��Add()����ע���ȼ�ʱ���������������Ѿ�ע����ˣ����µĻḲ�Ǿɵģ���ע��ID��ͬ�ھɵġ�
##### (2)һ������������ע�����ȼ�����һ���������ֻ��ע��һ���ȼ���EditHotKey_Keys()���������ȸ��ݺ������ҵ���һ����Ӧ�İ�����ϣ�Ȼ�����Add()ȥ����ע�� 
```csharp
using System.Windows;
using FastHotKeyForWPF;

namespace TestForHotKeyDll
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        protected override void OnSourceInitialized(EventArgs e)
        {
            GlobalHotKey.Awake();

            GlobalHotKey.Add(ModelKeys.CTRL, NormalKeys.F1, Test1);
            //ע�� => CTRL+F1 => Test1()

            GlobalHotKey.EditHotKey_Keys(Test1, ModelKeys.ALT, NormalKeys.E);
            //����Test1(),�޸� => CTRL+F1 => ALT+E
            GlobalHotKey.EditHotKey_Function(ModelKeys.ALT, NormalKeys.E, Test2);
            //����ALT+E,�޸� => Test1() => Test2()

            //���ǣ����ע���[CTRL+F1 => Test1]���������޸ı����[ALT+E => Test2]

            base.OnSourceInitialized(e);
        }
        protected override void OnClosed(EventArgs e)
        {
            GlobalHotKey.Destroy();
            base.OnClosed(e);
        }
        private void Test1()
        {
            MessageBox.Show("1");
        }
        private object Test2()
        {
            return "2";
        }

    }
}
```

#### �� ���´�����ʾ�����ʹ��DeleteByFunction()�����й���һ�������������ȼ�
```csharp
using System.Windows;
using FastHotKeyForWPF;

namespace TestForHotKeyDll
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        protected override void OnSourceInitialized(EventArgs e)
        {
            GlobalHotKey.Awake();

            GlobalHotKey.Add(ModelKeys.CTRL, NormalKeys.F1, Test1);
            GlobalHotKey.Add(ModelKeys.CTRL, NormalKeys.F2, Test1);
            GlobalHotKey.Add(ModelKeys.CTRL, NormalKeys.F3, Test1);
            GlobalHotKey.Add(ModelKeys.CTRL, NormalKeys.F4, Test1);
            //�������ȼ�����ͬһ����

            GlobalHotKey.Add(ModelKeys.CTRL, NormalKeys.F5, Test2);
            //����Test1�Ķ�����

            BindingRef.BindingEvent(WhileUpdate);

            GlobalHotKey.DeleteByFunction(Test1);
            //ɾ��ָ�������������е��ȼ�����һ������ִ���꣬��ֻʣ��CTRL+F5 => Test2() ��

            base.OnSourceInitialized(e);
        }
        protected override void OnClosed(EventArgs e)
        {
            GlobalHotKey.Destroy();
            base.OnClosed(e);
        }
        private object Test1()
        {
            return "����1";
        }

        private object Test2()
        {
            return "����2";
        }

        private void WhileUpdate()
        {
            foreach (var info in GlobalHotKey.HotKeyInfo())
            {
                MessageBox.Show(info.SuccessRegistration());
                //���ֻ�����CTRL+F5 => Test2() ,Test1()��ص�ȫ���ȼ��������
            }
        }

    }
}
```

</details>

## �� BindingRef��
#### ʵʱ���»���
<details>
<summary>����</summary>

| ������       | ����              | ����ֵ    | ����                                                                                                  |
|--------------|-------------------|-----------|-------------------------------------------------------------------------------------------------------|
| Awake        |                   |           | �������ʹ��GlobalHotKey.Awake()ʱ�����Զ�����һ��                                                  |
| Destroy      |                   |           | ����                                                                                                  |
| BindingAutoEvent | KeyInvoke_Void    |           | ��ĳ���Զ���ĺ�������BindingRef,��BindingRef���յ��ȼ��������ķ���ֵʱ���Զ���������󶨵ĺ���   |
| Update       | object?           |           | �������ݣ�Ĭ���Զ������󶨸�BindingRef�Ĵ�����                                                      |
| Connect      | ( KeySelectBox , KeySelectBox , KeyInvoke_Void / KeyInvoke_Return  )           |           | ��������������Ǹ������Ĵ������໥���ӣ����ӹ� |
| Connect      | ( KeysSelectBox , KeyInvoke_Void / KeyInvoke_Return )           |           | ΪKeysSelectBoxָ��һ�������������ӹ� |
| DisConnect   | KeySelectBox      |           | ȡ�����֮��������Լ����ǽӹܵĺ���                                                  |
| DisConnect   | KeysSelectBox      |           | ȡ��KeysSelectBox�ӹܵĴ�����                                                  |
| GetKeysFromConnection| KeySelectBox | Tuple( enum ModelKeys? , enum NormalKeys? )|��һ����������״̬��KeySelectBox��ȡ�������|
</details>

<details>
<summary>����</summary>

| ������              | ����                                                                 | ����               |
|---------------------|----------------------------------------------------------------------|--------------------|
| Value               | object?                                                              | ��⵽������ֵ     |
</details>

<details>
<summary>ʾ��</summary>

#### ���´�����ʾ��ʵʱ���»��Ƶ�����
##### ����˵����CTRL+F1�����£��ᴥ���Զ��庯��Test(),��Test()�߱�����ֵ,����ܻ�ϣ���õ��������ֵ��������Щ���������飬BindingRef֧���Զ���Ⲣ�Զ����������ν�ġ����������顱
```csharp
using System.Windows;
using FastHotKeyForWPF;

namespace TestForHotKeyDll
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        protected override void OnSourceInitialized(EventArgs e)
        {
            GlobalHotKey.Awake();

            GlobalHotKey.IsUpdate = true;
            //������ؾ�����BindingRef�Ƿ���ʵʱ���״̬,Ĭ��Ϊtrue

            GlobalHotKey.Add(ModelKeys.CTRL, NormalKeys.F1, Test);
            //�˴�ע���Test()�߱�object����ֵ����BindingRef��ʵʱ����Ƿ���object����ֵ�Ĳ���

            BindingRef.BindingAutoEvent(WhileUpdate);
            //��BindingRef��⵽һ�η���ֵ�����Զ�����ע���WhileUpdate()����������һ���Ĵ���

            base.OnSourceInitialized(e);
        }
        protected override void OnClosed(EventArgs e)
        {
            GlobalHotKey.Destroy();
            base.OnClosed(e);
        }
        private object Test()
        {
            return "����";
        }

        private void WhileUpdate()
        {
            MessageBox.Show(BindingRef.Value.ToString());
            //������Ի�ȡ��⵽�����·���ֵ��������һ������

            foreach (var info in GlobalHotKey.HotKeyInfo())
            {
                MessageBox.Show(info.SuccessRegistration());
                //��GlobalHotKey��ȡע�����е������ȼ���Ϣ����ӡ
            }
        }

    }
}
```

</details>

## �� PrefabComponent��
#### �������Ѹ������һ��ҳ���������ÿ�ݷ�ʽ����ô�����ʹ�ô������ṩ��Ԥ�����
<details>
<summary>������</summary>

###### Ŀǰֻ�ṩ��KeySelectBox���

| ������              | ����                                        | ����ֵ    | Լ��            | ����                                               |
|---------------------|---------------------------------------------|-----------|-----------------|----------------------------------------------------|
|GetComponent         | < T >( )                                    | T         | Component�ӿ�   |��ȡ���--Ĭ�ϵ������Ϣ                            |
|GetComponent         | < T >( ComponentInfo )                      | T         | Component�ӿ�   |��ȡ���--ָ�������С��ɫ��ָ������ɫ��ָ��Margin  |
|ProtectSelectBox     | < T >( )                                    |           | KeyBox������    |��ָ�����͵ļ��̺��ӱ����������ٽ����û����µļ�    |
|UnProtectSelectBox   | < T >( )                                    |           | KeyBox������    |�������״̬                                        |
</details>

<details>
<summary>ʾ��</summary>

#### ���´�����ʾ�����ʹ��Ԥ��������ٹ��ɿ�ݼ����ý���,�������ӱ�ΪԲ�ǵ�
###### C#����:

```csharp
using FastHotKeyForWPF;
using System.CodeDom;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TestDemo
{
    public partial class MainWindow : Window
    {
        private static ComponentInfo info = new ComponentInfo(20, Brushes.Black, Brushes.Wheat, new Thickness());
        //�������ǵ������С��ɫ������ɫ��Margin����ͬ��

        KeySelectBox k1 = PrefabComponent.GetComponent<KeySelectBox>(info);
        KeySelectBox k2 = PrefabComponent.GetComponent<KeySelectBox>(info);
        //�������������ӹܺ���TestA

        KeySelectBox k3 = PrefabComponent.GetComponent<KeySelectBox>(info);
        KeySelectBox k4 = PrefabComponent.GetComponent<KeySelectBox>(info);
        //�������������ӹܺ���TestB

        KeysSelectBox k5 = PrefabComponent.GetComponent<KeysSelectBox>(info);
        //����������ӹܺ���TestC

        KeysSelectBox k6 = PrefabComponent.GetComponent<KeysSelectBox>(info);
        //����������ӹܺ���TestD

        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            GlobalHotKey.Awake();
            //����

            LoadingHotKeyPage();
            //�������

            base.OnSourceInitialized(e);
        }

        protected override void OnClosed(EventArgs e)
        {
            GlobalHotKey.Destroy();
            //����

            base.OnClosed(e);
        }

        private void LoadingHotKeyPage()
        {
            GlobalHotKey.IsDeBug = true;
            //����ģʽ���ӡ���ֹ���ֵ��Ĭ�Ϲر�

            Box1.Child = k1;
            Box2.Child = k2;
            Box3.Child = k3;
            Box4.Child = k4;
            Box5.Child = k5;
            Box6.Child = k6;
            //���������Ϊ��������Ԫ��

            k1.UseFatherSize<Border>();
            k2.UseFatherSize<Border>();
            k3.UseFatherSize<Border>();
            k4.UseFatherSize<Border>();
            k5.UseFatherSize<Border>();
            k6.UseFatherSize<Border>();
            //������Ŀ�ߡ������С�븸��������Ӧ

            k1.UseStyleProperty("MyBox");
            k2.UseStyleProperty("MyBox");
            k3.UseStyleProperty("MyBox");
            k4.UseStyleProperty("MyBox");
            k5.UseStyleProperty("MyBox");
            k6.UseStyleProperty("MyBox");
            //Ϊ���Ӧ��ָ����ʽ�е�ָ������

            k1.IsDefaultColorChange = false;
            k2.IsDefaultColorChange = false;
            k3.IsDefaultColorChange = false;
            k4.IsDefaultColorChange = false;
            k5.IsDefaultColorChange = false;
            k6.IsDefaultColorChange = false;
            //�ر�Ĭ�ϵĽ����ɫ�¼�

            BindingRef.Connect(k1, k2, TestA);
            BindingRef.Connect(k3, k4, TestB);
            BindingRef.Connect(k5, TestC);
            BindingRef.Connect(k6, TestD);
            //�ӹ�ָ������

            GlobalHotKey.IsUpdate = true;
            //��Ϊfalse��رռ�ⷵ��ֵ���ܣ�Ĭ�ϴ�
            BindingRef.BindingAutoEvent(WhileReceiveValue);
            //��⵽����ֵ�Զ�����ָ������

            k1.Protect();
            k1.UnProtect();
            //���������һ��KeySelectBox����İ������չ���,KeysSelectBox���Ҳ��������������

            PrefabComponent.ProtectSelectBox<KeySelectBox>();
            PrefabComponent.UnProtectSelectBox<KeySelectBox>();
            //����ֱ�����������ָ�����͵�������������ȼ�����Protect()��UnProtect()
        }

        //k1��k2�ӹܴ˺���
        private void TestA()
        {
            MessageBox.Show("����A");
        }

        //k3��k4�ӹܴ˺���
        private object TestB()
        {
            return "����BB";
        }

        //k5�ӹܴ˺���
        private void TestC()
        {
            MessageBox.Show("����CCC");
        }

        //k6�ӹܴ˺���
        private object TestD()
        {
            int a = 1;
            return a;
        }

        //��⵽����ֵʱ�������¼� 
        private void WhileReceiveValue()
        {
            if (BindingRef.Value == null)
            {
                //ֵΪ��ʱ�Ĵ���취

                return;
            }
            if (BindingRef.Value is string)
            {
                MessageBox.Show("B�¼�����!");
                return;
            }
            if (BindingRef.Value is int)
            {
                MessageBox.Show("D�¼�����!");
                return;
            }
        }

    }
}

```

###### XAML����:
```xaml
<Window x:Class="TestDemo.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:TestDemo"
        mc:Ignorable="d"
        Title="MainWindow" Height="470" Width="800">
    <Window.Resources>
        <!--�������Զ������Դ��ʽ����������Ϊ���ʹ����ʽ�ж��������-->
        <Style x:Key="MyBox" TargetType="TextBox">
            <Setter Property="FontSize" Value="36"/>
            <Setter Property="Foreground" Value="Cyan"/>
            <Setter Property="Background" Value="Transparent"/>
            <Setter Property="BorderThickness" Value="0"/>
            <Setter Property="BorderBrush" Value="Red"/>
        </Style>
        <Style x:Key="MyBorderA" TargetType="Border">
            <Setter Property="Background" Value="#29353d"/>
            <Setter Property="BorderThickness" Value="1"/>
            <Setter Property="Height" Value="50"/>
            <Setter Property="CornerRadius" Value="10"/>
        </Style>
        <Style x:Key="MyBorderB" TargetType="Border">
            <Setter Property="Background" Value="#29353d"/>
            <Setter Property="BorderThickness" Value="1"/>
            <Setter Property="Height" Value="50"/>
            <Setter Property="CornerRadius" Value="10"/>
        </Style>
    </Window.Resources>
    <Viewbox>
        <Grid Height="450" Width="800">
            <!--������Border��������Ԥ�����KeySelectBox��λ��-->
            <Border x:Name="Box1" Margin="174,131,404,269" Style="{StaticResource MyBorderA}"/>
            <Border x:Name="Box2" Margin="434,131,144,269" Style="{StaticResource MyBorderA}"/>

            <!--������Border��������Ԥ�����KeySelectBox��λ��-->
            <Border x:Name="Box3" Margin="174,202,404,198" Style="{StaticResource MyBorderA}"/>
            <Border x:Name="Box4" Margin="434,202,144,198" Style="{StaticResource MyBorderA}"/>

            <!--���Border��������Ԥ�����KeysSelectBox��λ��-->
            <Border x:Name="Box5" Margin="174,277,144,123" Style="{StaticResource MyBorderB}"/>
            <!--���Border��������Ԥ�����KeysSelectBox��λ��-->
            <Border x:Name="Box6" Margin="174,349,144,51" Style="{StaticResource MyBorderB}"/>

            
            
            <TextBlock Margin="28,130,569,270" Text="����A" FontSize="40"/>
            <TextBlock Margin="28,200,569,200" Text="����B" FontSize="40"/>
            <TextBlock Margin="28,276,569,124" Text="����C" FontSize="40"/>
            <TextBlock Margin="28,348,569,52" Text="����D" FontSize="40"/>
            <TextBlock Margin="28,30,404,370" Text="���� => ��ݷ�ʽ" FontSize="40"/>
        </Grid>
    </Viewbox>

</Window>

```

</details>

## RegisterInfo��
#### ���ڱ�ʾע����Ϣ
<details>
<summary>����</summary>

| ������              | ����                                                                 | ����ֵ    | ����                        |
|---------------------|----------------------------------------------------------------------|-----------|-----------------------------|
|RegisterInfo         |( int , ModelKeys , NormalKeys , KeyInvoke_Void / KeyInvoke_Return )  |           | ��ʼ�����캯��              |
|SuccessRegistration  |                                                                      | string    | ��ע��ɹ�ʱ����ע����Ϣ    |
|LoseRegistration     |( ModelKeys , NormalKeys , KeyInvoke_Void / KeyInvoke_Return )        | string    | ��ע��ʧ��ʱ����ע����Ϣ    |
</details>

<details>
<summary>����</summary>

| ������              | ����                                                                 | ����               |
|---------------------|----------------------------------------------------------------------|--------------------|
| RegisterID          | int                                                                  | ע����           |
| Model               | enum:uint ModelKeys                                                  | ���μ�             |
| Normal              | enum:uint NormalKeys                                                 | һ���             |
| FunctionType        | enum:uint FunctionTypes                                              | ����������       |
| Name                | string                                                               | �������ĺ�����   |
| FunctionVoid        | delegate KeyInvoke_Void                                              | ������           |
| FunctionReturn      | delegate KeyInvoke_Return                                            | ������           |
</details>

<details>
<summary>ע��</summary>

##### һ����˵������ֱ��ʹ��RegisterInfo�ķ���(�����Ƿ�Ϊpublic static)�����Ĵ�����Ϊ�˱����� GlobalHotKey�й����ȼ���Ϣ�ĵǼǡ���ѯ���޸ġ��������,��GlobalHotKey����󣬿���ʹ��GlobalHotKey�ľ�̬����HotKeyInfo()��ȡ��ǰע�����е������ȼ���Ϣ�ļ���List,Ȼ��ȥ������������е�RegisterInfo��������ԡ�
</details>

## ComponentInfo��
#### һ��Ԥ������Ļ�����Ϣ
<details>
<summary>����</summary>

| ������              | ����                                                                 | ����               |
|---------------------|----------------------------------------------------------------------|--------------------|
| ComponentInfo       |                                                                      | ʵ����             |
| ComponentInfo       | ( double , SolidColorBrush , SolidColorBrush , Thickness )           | ʵ�����������С��������ɫ������ɫ��Margin��             |
</details>

<details>
<summary>����</summary>

| ������              | ����                                                                 | ����               |
|---------------------|----------------------------------------------------------------------|--------------------|
| Foreground          | SolidColorBrush                                                      | �����С           |
| Background          | SolidColorBrush                                                      | ������ɫ           |
| Margin              | Thickness                                                            | ���λ��           |
</details>

## KeySelectBox��/KeysSelectBox��
#### ����Ԥ�������ͬ����KeyBox,���ڽ����û����µ�Key�����ڵ���BindingRef�ṩ�Ĺ��ܺ����󣬽ӹ��ȼ���ע�ᡢ�޸ġ�����