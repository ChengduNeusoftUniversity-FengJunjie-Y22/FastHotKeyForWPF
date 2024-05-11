# FastHotKeyForWPF
#### NuGet�ĵ���ֹͣ���£���ǰ��github��gitee�鿴
- [github][1]
- [gitee][2]

[1]: https://github.com/ChengduNeusoftUniversity-FengJunjie-Y22/FastHotKeyForWPF
[2]: https://gitee.com/CNU-FJj-Y22/FastHotKeyForWPF
### Ŀ¼ ���� �� ������ͨ���ṩ�˴󲿷ֵĺ��Ĺ��ܣ���Ϊ���ã�
- [��Ŀ���](#��Ŀ���)

- [�� GlobalHotKey��](#GlobalHotKey��)

- [�� BindingRef��](#BindingRef��)

- [�� PrefabComponent��](#PrefabComponent��)

- [RegisterInfo��](#RegisterInfo��)

- [ComponentInfo��](#ComponentInfo��)

- [KeySelectBox��](#KeySelectBox��)

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
##### �� ��enum ModelKeys : uint�� ��ݼ������β��֣�Ŀǰ֧����Ctrl\Alt��Ϊ����
##### �� ��enum NormalKeys : uint�� [Model+Normal]����һ���ȼ�,Ŀǰ֧�֡�A-Z����F1-F12����0-9����Up\Down\Left\Right����Space��
##### �� ��enum FunctionTypes�� �����ķ���ֵ���ͣ�Void\Return(��\�޷���ֵ)
##### �� ��delegate void KeyInvoke_Void()�� ֧�ֽ��޲Ρ��޷���ֵ�ĺ�����ǩ����Ϊ������ע��Ϊ�ȼ��Ĵ�����
##### �� ��delegate object KeyInvoke_Return()�� ֧�ֽ��޲Ρ�����һ��object�ĺ�����ǩ����Ϊ������ע��Ϊ�ȼ��Ĵ�����

## GlobalHotKey��
#### ȫ���ȼ�ע�ᡢ�޸ġ���ѯ�����ٵ���Ҫʵ��
<details>
<summary>������</summary>

| ������             | ����                                                          | ����ֵ                 | Ȩ��            | ����                                                       |
|--------------------|---------------------------------------------------------------|------------------------|-----------------|------------------------------------------------------------|
| Awake              |                                                               |                        | public static   | ����                                                       |
| Destroy            |                                                               |                        | public static   | ����                                                       |
| Add                | ( ModelKeys , NormalKeys , KeyInvoke_Void / KeyInvoke_Return )| Tuple( bool , string ) | public static   | ע���ȼ������Ĵ��������޲Ρ��޷���ֵ��                   |
| EditHotKey_Keys    | ( KeyInvoke_Void / KeyInvoke_Return , ModelKeys , NormalKeys )|                        | public static   | ������ϼ����Ҷ�Ӧ�Ĵ����������滻Ϊ�µĴ�����         |
| EditHotKey_Function| ( ModelKeys , NormalKeys , KeyInvoke_Void / KeyInvoke_Return )|                        | public static   | �������д������������ܴ���������ϼ������滻Ϊ�µ���ϼ� |
| Clear              |                                                               |                        | public static   | ����ȼ��������������                                     |
| DeleteById         | int                                                           |                        | public static   | ����ע������ɾ��ע����ȼ�                               |
| DeleteByFunction   | KeyInvoke_Void / KeyInvoke_Return                             |                        | public static   | һ�����������ɶ���ȼ����������øú��������һ�����������д�����ϼ� |

</details>

<details>
<summary>��ѡ��</summary>

| ������              | ����                           | Ĭ��                                 | Ȩ��            | ����                                                             |
|---------------------|--------------------------------|--------------------------------------|-----------------|------------------------------------------------------------------|
| IsDeBug             | bool                           | false                                | public static   | �Ƿ�������ģʽ�����ֹ��̽�ʹ��MessageBox�������ֵ��           |
| IsUpdate            | bool                           | true                                 | public static   | �Ƿ�ʵʱ��ⷵ��ֵ                                               |
| HOTKEY_ID           | int                            | 2004                                 | public static   | ��һ���ȼ���ע���ţ�ֻ����������ע�������ʼǰ�޸�һ��         |
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

## BindingRef��
#### ʵʱ���»���
<details>
<summary>������</summary>

| ������       | ����              | ����ֵ    | Ȩ��            | ����                                                                                                  |
|--------------|-------------------|-----------|-----------------|-------------------------------------------------------------------------------------------------------|
| Awake        |                   |           | public static   | �������ʹ��GlobalHotKey.Awake()ʱ�����Զ�����һ��                                                  |
| Destroy      |                   |           | public static   | ����                                                                                                  |
| BindingEvent | KeyInvoke_Void    |           | public static   | ��ĳ���Զ���ĺ�������BindingRef,��BindingRef���յ��ȼ��������ķ���ֵʱ���Զ���������󶨵ĺ���   |
| Update       | object?           |           | private         | �������ݣ�Ĭ���Զ������󶨸�BindingRef�Ĵ�����                                                      |
</details>

<details>
<summary>���Ա�</summary>

| ������              | ����                                                                 | Ȩ��            | ����               |
|---------------------|----------------------------------------------------------------------|-----------------|--------------------|
| Value               | object?                                                              | public static   | ��⵽������ֵ     |
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

            BindingRef.BindingEvent(WhileUpdate);
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

## PrefabComponent��
#### �������Ѹ������һ��ҳ���������ÿ�ݷ�ʽ����ô�����ʹ�ô������ṩ��Ԥ�����

## RegisterInfo��
#### ���ڱ�ʾע����Ϣ
<details>
<summary>������</summary>

| ������              | ����                                                                 | ����ֵ    | Ȩ��            | ����                       |
|---------------------|----------------------------------------------------------------------|-----------|-----------------|----------------------------|
|RegisterInfo         |( int , ModelKeys , NormalKeys , KeyInvoke_Void / KeyInvoke_Return )  |           | public          |��ʼ�����캯��              |
|SuccessRegistration  |                                                                      | string    | public          |��ע��ɹ�ʱ����ע����Ϣ    |
|LoseRegistration     |( ModelKeys , NormalKeys , KeyInvoke_Void / KeyInvoke_Return )        | string    | public static   |��ע��ʧ��ʱ����ע����Ϣ    |
</details>

<details>
<summary>���Ա�</summary>

| ������              | ����                                                                 | Ȩ��            | ����               |
|---------------------|----------------------------------------------------------------------|-----------------|--------------------|
| RegisterID          | int                                                                  | public readonly | ע����           |
| Model               | enum:uint ModelKeys                                                  | public readonly | ���μ�             |
| Normal              | enum:uint NormalKeys                                                 | public readonly | һ���             |
| FunctionType        | enum:uint FunctionTypes                                              | public readonly | ����������       |
| Name                | string                                                               | public readonly | �������ĺ�����   |
| FunctionVoid        | delegate KeyInvoke_Void                                              | public          | ������           |
| FunctionReturn      | delegate KeyInvoke_Return                                            | public          | ������           |
</details>

<details>
<summary>ע��</summary>

##### һ����˵������ֱ��ʹ��RegisterInfo�ķ���(�����Ƿ�Ϊpublic static)�����Ĵ�����Ϊ�˱����� GlobalHotKey�й����ȼ���Ϣ�ĵǼǡ���ѯ���޸ġ��������,��GlobalHotKey����󣬿���ʹ��GlobalHotKey�ľ�̬����HotKeyInfo()��ȡ��ǰע�����е������ȼ���Ϣ�ļ���List,Ȼ��ȥ������������е�RegisterInfo��������ԡ�
</details>

## ComponentInfo��
#### һ��Ԥ������Ļ�����Ϣ

## KeySelectBox��
#### �̳���WPF��TextBox��,��PrefabComponent֧�ֵ��������֮һ