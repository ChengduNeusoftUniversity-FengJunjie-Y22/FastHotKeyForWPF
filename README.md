# FastHotKeyForWPF
#### Select a link to view the full repository and documentation
- [github][1]
- [gitee][2]

[1]: https://github.com/ChengduNeusoftUniversity-FengJunjie-Y22/FastHotKeyForWPF
[2]: https://gitee.com/CNU-FJj-Y22/FastHotKeyForWPF
### Ŀ¼
- [��Ŀ���](#��Ŀ���)
  - [����](#����)
  - [����](#����)
  - [��ȡ�����](#��ȡ�����)

- [GlobalHotKey��](#GlobalHotKey��)
  - [������](#������GHK)
  - [��ѡ��](#������GHK)
  - [ʾ��](#ʹ��ʾ��GHK)

- [BindingRef��](#BindingRef��)
  - [������](#������BR)
  - [���Ա�](#������BR)
  - [ʾ��](#ʹ��ʾ��BR)

- [RegisterInfo��](#RegisterInfo��)
  - [������](#������RI)
  - [���Ա�](#������RI)
  - [ע��](#ʹ��ʾ��RI)

## ��Ŀ���

<details id="����">
<summary>����</summary>

#### ����һ��WPF�����Ŀ��ּ���ṩ����ݵķ�ʽ������ȫ���ȼ�
#### �ص�
##### һ�仰ʽ�Ĺ���ʵ��
##### �ɽ�����ǩ��ֱ��ע�����ݼ�
##### �Դ�һ�����ݰ󶨣�ͬ�����Խ�һ������ǩ������ȥ���Ա������ݷ�������ʱ�Զ����ð󶨵ĺ���

</details>

<details id="����">
<summary>����</summary>

#### �������߱���
##### д�����Ŀ��ʱ���������ϴ����һ�㶼���������˾�ά��һ����Ŀ��������˵�����ڶ�����
#### ��ϵ��ʽ
##### Bilibili:"��Ĳ���һ����"
##### QQ:2789083329
##### WeChat:WeC_FZJSOP4996

</details>

<details id="��ȡ�����">
<summary>��ȡ�����</summary>

#### NuGet
##### Ŀǰ�Ѿ�����ֱ���������ˣ����˷�����ȡ���Ŀ��޷��鿴XMLע���ĵ����������������ǲ�������ʾ�ʵģ������������������ĵ���
#### Github & Gitee
##### ����������Ŀ�������������Ŀ��ӵ���Ҫʹ�ô�������Ŀ�У�Ȼ���ڽ��������Դ����������������ӵ������Ŀ

</details>

## ǰ�ö���
#### ��ǰ�˽���Щ������԰������������
##### �� ��enum ModelKeys : uint�� ��ݼ������β��֣�Ŀǰ֧����Ctrl\Alt��Ϊ����
##### �� ��enum NormalKeys : uint�� [Model+Normal]����һ���ȼ�,Ŀǰ֧�֡�A-Z����F1-F12����0-9����Up\Down\Left\Right����Space��
##### �� ��enum FunctionTypes�� �����ķ���ֵ���ͣ�Void\Return(��\�޷���ֵ)
##### �� ��delegate void KeyInvoke_Void()�� ֧�ֽ��޲Ρ��޷���ֵ�ĺ�����ǩ����Ϊ������ע��Ϊ�ȼ��Ĵ�����
##### �� ��delegate object KeyInvoke_Return()�� ֧�ֽ��޲Ρ�����һ��object�ĺ�����ǩ����Ϊ������ע��Ϊ�ȼ��Ĵ�����

## GlobalHotKey��
#### ȫ���ȼ�ע�ᡢ�޸ġ���ѯ�����ٵ���Ҫʵ��
<details id="������GHK">
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

<details id="������GHK">
<summary>��ѡ��</summary>

| ������              | ����                           | Ĭ��                                 | Ȩ��            | ����                                                             |
|---------------------|--------------------------------|--------------------------------------|-----------------|------------------------------------------------------------------|
| IsDeBug             | bool                           | false                                | public static   | �Ƿ�������ģʽ�����ֹ��̽�ʹ��MessageBox�������ֵ��           |
| IsUpdate            | bool                           | true                                 | public static   | �Ƿ�ʵʱ��ⷵ��ֵ                                               |
| HOTKEY_ID           | int                            | 2004                                 | public static   | ��һ���ȼ���ע���ţ�ֻ����������ע�������ʼǰ�޸�һ��         |
</details>

<details id="ʹ��ʾ��GHK">
<summary>ʾ��</summary>

#### �� ���´�����ʾ�����ʹ��Awake()��Destroy()����GlobalHotKey�ļ���������
##### ��Ȼ����Ҳ�����������ط�ʹ��������������������Ҫע�� : Awake()������Ҫ��MainWindow�ľ���Ѿ�����ʱ����ȥ���á�������дOnSourceInitialized��OnClosed�����߱Ƚ��Ƽ��ķ���
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
##### ���գ�����Alt+E���Դ����Զ����Test2()����
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

#### �� ���´�����ʾ����θ��ݺ���ǩ����ɾ��������֮��Ӧ���ȼ�
##### Ҳ�Ǹպ�˵һ�£�ʵ�ʿ����У���������һ���ȼ������������󶨣������������ȼ�ָ��ͬһ�������������Բ���Ҫ����ɾ�����ơ�����ʹ��Add()����ע���ȼ�ʱ���������������Ѿ�ע����ˣ����µĻḲ�Ǿɵģ���ע��ID��ͬ�ھɵġ�
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
<details id="������BR">
<summary>������</summary>

| ������       | ����              | ����ֵ    | Ȩ��            | ����                                                                                                  |
|--------------|-------------------|-----------|-----------------|-------------------------------------------------------------------------------------------------------|
| Awake        |                   |           | public static   | �������ʹ��GlobalHotKey.Awake()ʱ�����Զ�����һ��                                                  |
| Destroy      |                   |           | public static   | ����                                                                                                  |
| BindingEvent | KeyInvoke_Void    |           | public static   | ��ĳ���Զ���ĺ�������BindingRef,��BindingRef���յ��ȼ��������ķ���ֵʱ���Զ���������󶨵ĺ���   |
| Update       | object?           |           | private         | �������ݣ�Ĭ���Զ������󶨸�BindingRef�Ĵ�����                                                      |
</details>

<details id="������BR">
<summary>���Ա�</summary>

| ������              | ����                                                                 | Ȩ��            | ����               |
|---------------------|----------------------------------------------------------------------|-----------------|--------------------|
| Value               | object?                                                              | public          | ��⵽������ֵ     |
</details>

<details id="ʹ��ʾ��BR">
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

## RegisterInfo��
#### ���ڱ�ʾע����Ϣ
<details id="������RI">
<summary>������</summary>

| ������              | ����                                                                 | ����ֵ    | Ȩ��            | ����                       |
|---------------------|----------------------------------------------------------------------|-----------|-----------------|----------------------------|
|RegisterInfo         |( int , ModelKeys , NormalKeys , KeyInvoke_Void / KeyInvoke_Return )  |           | public          |��ʼ�����캯��              |
|SuccessRegistration  |                                                                      | string    | public          |��ע��ɹ�ʱ����ע����Ϣ    |
|LoseRegistration     |( ModelKeys , NormalKeys , KeyInvoke_Void / KeyInvoke_Return )        | string    | public static   |��ע��ʧ��ʱ����ע����Ϣ    |
</details>

<details id="������RI">
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

<details id="ʹ��ʾ��RI">
<summary>ע��</summary>

##### һ����˵������ֱ��ʹ��RegisterInfo����ķ��������Ĵ�����Ϊ�˱����� GlobalHotKey�й����ȼ���Ϣ�ĵǼǡ���ѯ���޸ġ��������,��GlobalHotKey����󣬿���ʹ�����ľ�̬����HotKeyInfo��ȡ��ǰע�����е������ȼ���Ϣ�ļ���List,Ȼ��ȥ������������е�RegisterInfo��������ԡ�
</details>