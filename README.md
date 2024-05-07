# FastHotKeyForWPF�ĵ�
## Ŀ¼
- [��Ŀ���](#��Ŀ���)
  - [����](#����)
  - [����](#����)
  - [��Ƶ�̳�](#��ȡ�����)

- [GlobalHotKey��](#GlobalHotKey��)
  - [������](#������GHK)
  - [������](#������GHK)
  - [ʹ��ʾ��](#ʹ��ʾ��GHK)

- [BindingRef��](#BindingRef��)
  - [������](#������BR)
  - [������](#������BR)
  - [ʹ��ʾ��](#ʹ��ʾ��BR)

- [RegisterInfo��](#RegisterInfo��)
  - [������](#������RI)
  - [������](#������RI)
  - [ʹ��ʾ��](#ʹ��ʾ��RI)

## ��Ŀ���

<details id="����">
<summary>����</summary>

### ����һ��WPF�����Ŀ��ּ���ṩ����ݵķ�ʽ������ȫ���ȼ�
### �ص�
#### һ�仰ʽ�Ĺ���ʵ��
#### �ɽ�����ǩ��ֱ��ע�����ݼ�
#### �Դ�һ�����ݰ󶨣�ͬ�����Խ�һ������ǩ������ȥ���Ա������ݷ�������ʱ�Զ����ð󶨵ĺ���

</details>

<details id="����">
<summary>����</summary>

### �������߱���
#### д�����Ŀ��ʱ���������ϴ����һ�㶼���������˾�ά��һ����Ŀ��������˵�����ڶ�����
### ��ϵ��ʽ
#### Bilibili:"��Ĳ���һ����"
#### QQ:2789083329
#### WeChat:WeC_FZJSOP4996

</details>

<details id="��ȡ�����">
<summary>��ȡ�����</summary>

### NuGet
#### Ŀǰ�Ѿ�����ֱ���������ˣ����˷�����ȡ���Ŀ��޷��鿴XMLע���ĵ����������������ǲ�������ʾ�ʵģ������������������ĵ���
### Github & Gitee
#### ����������Ŀ�������������Ŀ��ӵ���Ҫʹ�ô�������Ŀ�У�Ȼ���ڽ��������Դ����������������ӵ������Ŀ

</details>

## GlobalHotKey��

<details id="������GHK">
<summary>������</summary>

| ������             | ����                                                                             | ����ֵ            | ����               |
|--------------------|----------------------------------------------------------------------------------|-------------------|--------------------|
| Awake              |                                                                                  |                   | ����        |
| Destroy            |                                                                                  |                   | ����   |
| Add                | ( ModelKeys , NormalKeys , KeyInvoke_Void / KeyInvoke_Return )                   | ( bool , string ) | ע���ȼ������Ĵ��������޲Ρ��޷���ֵ��         |
| EditHotKey_Keys    | ( KeyInvoke_Void / KeyInvoke_Return , ModelKeys , NormalKeys )                   |                   | ע���ȼ������Ĵ��������޲Ρ��з���ֵ��         |
| EditHotKey_Function| ( ModelKeys , NormalKeys , KeyInvoke_Void / KeyInvoke_Return )                   |                   | ע���ȼ������Ĵ��������޲Ρ��з���ֵ��         |

</details>

<details id="������GHK">
<summary>������</summary>

</details>

<details id="ʹ��ʾ��GHK">
<summary>ʹ��ʾ��</summary>

</details>

## BindingRef��

<details id="������BR">
<summary>������</summary>

| ������       | ����                   | ����ֵ    | ����               |
|--------------|------------------------|-----------|--------------------|
| Awake        |                        |           | ����        |
| Destroy      |                        |           | ����   |
| Binding      | KeyInvoke_Void         |           | Ҫ�󴫵�һ���޲Ρ��޷���ֵ�ĺ���ǩ�����κ��޲Ρ��޷���ֵ���ȼ�����������ʱ���Զ����ô˺���           |
| Binding      | KeyInvoke_Return       |           | Ҫ�󴫵�һ���޲Ρ��з���ֵobject�ĺ���ǩ�������յ��κ��ȼ����������ص�objectʱ���Զ����ô˺���             |

</details>

<details id="������BR">
<summary>������</summary>

</details>

<details id="ʹ��ʾ��BR">
<summary>ʹ��ʾ��</summary>

</details>

## RegisterInfo��

<details id="������RI">
<summary>������</summary>

| ������              | ����                                                                 | ����ֵ    | ����               |
|---------------------|----------------------------------------------------------------------|-----------|--------------------|
|RegisterInfo         |( int , ModelKeys , NormalKeys , KeyInvoke_Void / KeyInvoke_Return )  |           |��ʼ�����캯��      |
|SuccessRegistration  |                                                                      | string    |��ע��ɹ�ʱ���ã�����ע����Ϣstring      |
|LoseRegistration     |( ModelKeys , NormalKeys , KeyInvoke_Void / KeyInvoke_Return )        | string    |��ע��ʧ��ʱ���ã�����ע����Ϣstring      |

</details>

<details id="������RI">
<summary>������</summary>

</details>

<details id="ʹ��ʾ��RI">
<summary>ʹ��ʾ��</summary>

</details>