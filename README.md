# FastHotKeyForWPF
## Quickly build global hotkeys in WPF programs
- [github][1]
- [gitee][2]

[1]: https://github.com/ChengduNeusoftUniversity-FengJunjie-Y22/FastHotKeyForWPF
[2]: https://gitee.com/CNU-FJj-Y22/FastHotKeyForWPF

---

## ���½���
[Bilibili�ϼ�][3]

[3]: https://www.bilibili.com/video/BV1WTbReZEZU

<details>
<summary>Version 1.1.6 ������ ( ʹ��PrefabComponent�����һ���汾 ) </summary>

#### (1)�ṩ���伴�õ�Բ�����
#### (2)Ĭ�ϲ�ʹ�ñ�ɫЧ��,��Ҫ�û��Զ����Ӧ����
#### (3)��DeBugģʽ������ע��ɹ�������ʾ,��Ҫ�û��Զ����Ӧ����
#### (4)����һ����������,�����е��κ��ȼ���������ɾ��,��������ȼ�û�б�ע���
#### (5)������̬����,���ڻ�ȡע����Ϣ�ͱ�������

</details>

<details>
<summary>Version 1.2.3 ������ </summary>

### �޸� HotKeysBox �� �ֶ������ȼ� ʱ������������ı���ʾ�쳣������ (���ֶ����ó�ʼ�ȼ����ı���ʾNone+None�����ǳ�ʼ���õ��ȼ�,��������һ�¿���ͻָ�������)
### �Ż����û��ؼ���Բ��Ч��������ActualBackground��ѡ��
</details>

---

## �� ���������ռ�
##### ���
```csharp
using FastHotKeyForWPF;
```
##### ǰ��
```xaml
xmlns:fh="clr-namespace:FastHotKeyForWPF;assembly=FastHotKeyForWPF"
```

---

## �� ����������
#### ʾ��1. GlobalHotKey - �ȼ���صĺ��Ĺ���
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
#### ʾ��2. ReturnValueMonitor - ��չ���ܣ��Ǳ�Ҫ��
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
###### ��дMainWindow��OnSourceInitialized��OnClosed���Ƽ�����������Ȼ�������ѡ������ʱ�̼��ֻҪ����ȷ��Awake()ʱ���ھ���Ѵ���

---

## �� ʹ�� GlobalHotKey ��ע���ȼ�
#### �龰. �ٶ����Զ��������º�����ϣ���û����� [ Ctrl + F1 ] �� [ Ctrl + F2 ] ʱ���ֱ�ִ�� TestA �� TestB
```csharp
        private void TestA()//�޲������޷���ֵ
        {
            MessageBox.Show("�ȼ�A�������ˣ�");
        }

        private object TestB()//�޲���������һ��object
        {
            return "�ȼ�B�������ˣ�";
        }
```
#### ʾ��. ʹ�� GlobalHotKey.Add ����ע������ȫ���ȼ�
```csharp
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            GlobalHotKey.Awake();

            GlobalHotKey.Add(ModelKeys.CTRL, NormalKeys.F1, TestA);
            GlobalHotKey.Add(ModelKeys.CTRL, NormalKeys.F2, TestB);
        }
```
###### ��ϲ�����Ѿ������˸ÿ�����ĵĹ��ܣ�

---

## �� ʹ�� GlobalHotKey ���޸��ȼ�
#### ʾ��1. ��֪ Keys ,�޸����Ӧ�Ĵ����¼���������
```csharp
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            GlobalHotKey.Awake();
            GlobalHotKey.Add(ModelKeys.CTRL, NormalKeys.F1, TestA);

            GlobalHotKey.EditHotKey_Function(ModelKeys.CTRL, NormalKeys.F1, TestB);
            //ԭ�� [ Ctrl + F1 ] Ӧ�ô��� TestA
            //���޸ĺ� , Ӧ�ñ������ı�Ϊ TestB
        }
```
#### ʾ��2. ��֪�����¼����޸����Ӧ�� Keys
```csharp
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            GlobalHotKey.Awake();
            GlobalHotKey.Add(ModelKeys.CTRL, NormalKeys.F1, TestA);

            GlobalHotKey.EditHotKey_Keys(TestA, ModelKeys.CTRL, NormalKeys.F2);
            //ԭ�� TestA Ӧ�� [ Ctrl + F1 ] ���� 
            //���޸ĺ� , Ӧ�� [ Ctrl + F2 ] ����
        }
```

---

## �� ʹ�� GlobalHotKey ��ɾ���ȼ�
#### ʾ��1. ���� ע��ID ɾ���ȼ���Ĭ�ϵ�һ��ID��2004��֮������ۼӣ�
```csharp
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            GlobalHotKey.Awake();
            GlobalHotKey.Add(ModelKeys.CTRL, NormalKeys.F1, TestA);

            GlobalHotKey.DeleteById(2004);
        }
```
#### ʾ��2. ���� Keys ɾ���ȼ�
```csharp
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            GlobalHotKey.Awake();
            GlobalHotKey.Add(ModelKeys.CTRL, NormalKeys.F1, TestA);

            GlobalHotKey.DeleteByKeys(ModelKeys.CTRL, NormalKeys.F1);
        }
```
#### ʾ��3. ���� ������ ɾ���ȼ�
```csharp
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            GlobalHotKey.Awake();
            GlobalHotKey.Add(ModelKeys.CTRL, NormalKeys.F1, TestA);

            GlobalHotKey.DeleteByFunction(TestA);
        }
```

---

## �� ʹ�� RegisterCollection ����ѯע�����е��ȼ���Ϣ
#### ʾ��1. ���� ID ��ѯ������ע����Ϣ �� RegisterInfo ���� ��
```csharp
        RegisterInfo Info = GlobalHotKey.Registers[2004];
```
#### ʾ��2. �� RegisterInfo �� ��ȡ���ȼ���ϸ����Ϣ
|����                   |����                        |����        |
|-----------------------|----------------------------|------------|
|RegisterID             |int                         |ע��id����ʼΪ2004���Զ����� |
|Model                  |ModelKeys                   |�ȼ� - ϵͳ���� |
|Normal                 |NormalKeys                  |�ȼ� - �ı����� |
|FunctionType           |FunctionTypes               |�������������� |
|Name                   |string                      |�������ĺ����� |
|FunctionVoid           |KeyInvoke_Void              |������ - void ��   |
|FunctionReturn         |KeyInvoke_Return            |������B - return object ��   |

---

## �� ʹ�� ReturnValueMonitor �����ȼ��¼�������Ϻ󣬶��䷵��ֵ��һ�����������ã�
#### ʾ��. ʹ�� BindingAutoEvent �����⵽�ķ���ֵ
```csharp
        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            GlobalHotKey.Awake();

            ReturnValueMonitor.Awake();
            ReturnValueMonitor.BindingAutoEvent(WhileObjectReturned);
            //WhileObjectReturned����TestA��TestB���ص�object������

            GlobalHotKey.Add(ModelKeys.CTRL, NormalKeys.F1, TestA);
            GlobalHotKey.Add(ModelKeys.CTRL, NormalKeys.F2, TestB);
            //TestA��TestBֻ���𷵻�object,�����������κδ���
        }

        protected override void OnClosed(EventArgs e)
        {
            GlobalHotKey.Destroy();
            ReturnValueMonitor.Destroy();

            base.OnClosed(e);
        }

        private object TestA()
        {
            return "�ȼ�A�������ˣ�";
        }

        private object TestB()
        {
            return 66868;
        }

        private void WhileObjectReturned()
        {
            if (ReturnValueMonitor.Value == null) { return; }

            if (ReturnValueMonitor.Value is string text)
            {
                //����
                //string �����߼��������ӡֵ

                return;
            }

            if (ReturnValueMonitor.Value is int number)
            {
                //����
                //int �����߼��������ӡֵ

                return;
            }
        }
```

---

## �� [ HotKeyBox ] �ؼ� & [ HotKeysBox ] �ؼ�
#### �龰. �ٶ���ϣ������һ�����ý��棬�����û��Լ������ȼ�
#### ʾ��1. ����ؼ�
##### �����
```xaml          
            xmlns:ff="clr-namespace:FastHotKeyForWPF;assembly=FastHotKeyForWPF"
```
##### ����ؼ�
```xaml
            <!--ÿ���ؼ�ֻ����һ��Key-->
            <ff:HotKeyBox x:Name="Box1"/>
            <ff:HotKeyBox x:Name="Box2"/>

            <!--ÿ���ؼ���������Key-->
            <ff:HotKeysBox x:Name="Box3"/>
```
##### ��������
```csharp
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            GlobalHotKey.Awake();

            Box1.ConnectWith(Box2, TestA);
            Box3.ConnectWith(TestA);
            //�������� Box �����Ӳ���ֻ��Ҫִ��һ��
        }

        private object TestA()
        {
            return "�ȼ�A�������ˣ�";
        }
```
#### ʾ��2. Ϊ�ؼ����ó�ʼ�ȼ�
```csharp
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            GlobalHotKey.Awake();

            Box1.ConnectWith(Box2, TestA);
            Box3.ConnectWith(TestB);
            //ע���Ƚ������������ó�ʼ�ȼ�

            Box1.SetHotKey(ModelKeys.CTRL,NormalKeys.F1,TestA);           
            Box3.SetHotKey(ModelKeys.CTRL, NormalKeys.F2, TestB);
        }
```
#### [ HotKeyBox ] ��ѡ��
|����                   |����                        |����        |
|-----------------------|----------------------------|------------|
|CurrentKey             |Key                         |��ǰֵ |
|WhileInput             |event Action?               |�û�����������Ϊʱ���������¼� |
|ErrorText              |string                      |���������ܿ�֧�֣���ؼ���ʾ���ı� |
|IsHotKeyRegistered     |bool                        |Ŀǰ�Ƿ�ɹ�ע�� |
|LastHotKeyID           |int                         |���һ��ע��ɹ���ID |
|CornerRadius           |CornerRadius                |Բ����   |
|DefaultTextColor       |SolidColorBrush             |Ĭ���ı�ɫ|
|DefaultBorderBrush     |SolidColorBrush             |Ĭ����߿�ɫ|
|HoverTextColor         |SolidColorBrush             |��ͣ�ı�ɫ|
|HoverBorderBrush       |SolidColorBrush             |��ͣ��߿�ɫ|
|ActualBackground       |SolidColorBrush             |����ɫ,ע�ⲻ�� Background|
#### [ HotKeysBox ] ��ѡ��
|����                   |����                        |����        |
|-----------------------|----------------------------|------------|
|CurrentKeyA            |Key                         |���ֵ |
|CurrentKeyB            |Key                         |�Ҽ�ֵ |
|WhileInput             |event Action?               |�û�����������Ϊʱ���������¼� |
|ErrorText              |string                      |���������ܿ�֧�֣���ؼ���ʾ���ı� |
|IsHotKeyRegistered     |bool                        |Ŀǰ�Ƿ�ɹ�ע�� |
|LastHotKeyID           |int                         |���һ��ע��ɹ���ID |
|CornerRadius           |CornerRadius                |Բ����   |
|DefaultTextColor       |SolidColorBrush             |Ĭ���ı�ɫ|
|DefaultBorderBrush     |SolidColorBrush             |Ĭ����߿�ɫ|
|HoverTextColor         |SolidColorBrush             |��ͣ�ı�ɫ|
|HoverBorderBrush       |SolidColorBrush             |��ͣ��߿�ɫ|
|ActualBackground       |SolidColorBrush             |����ɫ,ע�ⲻ�� Background|

#### [ HotKeyBox ] & [ HotKeysBox ] ��Xaml�����ϼ���һģһ���������ͨ�� x:Name �����ڲ�Ԫ�ز��޸�����
##### �ڲ�Ԫ������
```xaml
<UserControl x:Class="FastHotKeyForWPF.HotKeysBox"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006" 
             xmlns:d="http://schemas.microsoft.com/expression/blend/2008" 
             xmlns:local="clr-namespace:FastHotKeyForWPF"
             mc:Ignorable="d" 
             Height="50"
             Width="320"
             Background="Transparent"
             x:Name="Total"  MouseLeave="TextBox_MouseLeave" MouseEnter="TextBox_MouseEnter">
    <UserControl.Resources>
        <local:DoubleConvertor ConvertRate="0.7" x:Key="HeightToFontSize"/>
    </UserControl.Resources>
    <Grid x:Name="BackGrid" Background="{Binding ElementName=Total,Path=Background}">
        <Border x:Name="FixedBorder" BorderBrush="White" Background="#1e1e1e" BorderThickness="1" CornerRadius="5" ClipToBounds="True"/>
        <TextBox x:Name="FocusGet" Background="Transparent" IsReadOnly="True" PreviewKeyDown="UserInput" BorderBrush="Transparent" BorderThickness="0"/>
        <TextBox x:Name="EmptyOne" Width="0" Height="0"/>
        <TextBlock x:Name="ActualText" Foreground="White" VerticalAlignment="Center" HorizontalAlignment="Center" FontSize="{Binding ElementName=Total,Path=Height,Converter={StaticResource HeightToFontSize}}"/>
    </Grid>
</UserControl>
```

---