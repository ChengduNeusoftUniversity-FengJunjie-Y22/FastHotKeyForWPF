# FastHotKeyForWPF
## Quickly build global hotkeys in WPF programs
- [github][1]
- [gitee][2]

[1]: https://github.com/ChengduNeusoftUniversity-FengJunjie-Y22/FastHotKeyForWPF
[2]: https://gitee.com/CNU-FJj-Y22/FastHotKeyForWPF

---

## ¸EÂ½ø¶È
[BilibiliºÏ¼¯][3]

[3]: https://www.bilibili.com/video/BV1WTbReZEZU

<details>
<summary>Version 1.1.6 ÒÑÉÏÏß ( Ê¹ÓÃPrefabComponentµÄ×ûÖóÒ»¸ö°æ±¾ ) </summary>

#### (1)Ìá¹©¿ªÏä¼´ÓÃµÄÔ²½Ç×é¼ş
#### (2)Ä¬ÈÏ²»Ê¹ÓÃ±äÉ«Ğ§¹EĞèÒªÓÃ»§×Ô¶¨Òå¶ÔÓ¦º¯Êı
#### (3)·ÇDeBugÄ£Ê½ÏÂÔÙÎŞ×¢²á³É¹¦ÓEñµÄÌáÊ¾,ĞèÒªÓÃ»§×Ô¶¨Òå¶ÔÓ¦º¯Êı
#### (4)ĞÂÔöÒ»¸ö±£»¤Ãûµ¥,Ãûµ¥ÖĞµÄÈÎºÎÈÈ¼E»ÔÊĞúÍ»ÔöÉ¾¸Ä,¼´±ãÕâ¸öÈÈ¼E»ÓĞ±»×¢²á¹ı
#### (5)ĞÂÔö¾²Ì¬ÊôĞÔ,ÓÃÓÚ»ñÈ¡×¢²áĞÅÏ¢ºÍ±£»¤Ãûµ¥

</details>

<details>
<summary>Version 1.2.3 ÒÑÉÏÏß </summary>

### ĞŞ¸´ HotKeysBox ÔÚ ÊÖ¶¯ÉèÖÃÈÈ¼EÊ±£¬²¿·ÖÇé¿öÏÂÎÄ±¾ÏÔÊ¾ÒE£µÄÎÊÌE(¼´ÊÖ¶¯ÉèÖÃ³õÊ¼ÈÈ¼Eó£¬ÎÄ±¾ÏÔÊ¾None+None¶ø²»ÊÇ³õÊ¼ÉèÖÃµÄÈÈ¼Eµ«Êó±EøÈE»ÏÂ¿òÌå¾Í»Ö¸´ÁËÕı³£)
### ÓÅ»¯ÁËÓÃ»§¿Ø¼şµÄÔ²½ÇĞ§¹û£¬ĞÂÔöActualBackground¿ÉÑ¡ÏE
</details>

---

## ¢EÒıÈEEû¿Õ¼E
##### ºó¶Ë
```csharp
using FastHotKeyForWPF;
```
##### Ç°¶Ë
```xaml
xmlns:fh="clr-namespace:FastHotKeyForWPF;assembly=FastHotKeyForWPF"
```

---

## ¢E¼¤»ûïEú»Ù
#### Ê¾Àı1. GlobalHotKey - ÈÈ¼Eà¹ØµÄºËĞÄ¹¦ÄÜ
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
#### Ê¾Àı2. ReturnValueMonitor - ÍØÕ¹¹¦ÄÜ£¨·Ç±ØÒª£©
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
###### ÖØĞ´MainWindowµÄOnSourceInitializedÓEnClosedÊÇÍÆ¼öµÄ×ö·¨£¬µ±È»£¬Äã¿ÉÒÔÑ¡ÔñÆäËE±¿Ì¼¤»û¿¬Ö»ÒªÄãÄÜÈ·±£Awake()Ê±´°¿Ú¾ä±úÒÑ´æÔÚ

---

## ¢EÊ¹ÓÃ GlobalHotKey £¬×¢²áÈÈ¼E
#### Çé¾°. ¼Ù¶¨Äã×Ô¶¨ÒåÁËÒÔÏÂº¯Êı²¢Ï£ÍûÓÃ»§°´ÏÂ [ Ctrl + F1 ] ÓE[ Ctrl + F2 ] Ê±£¬·Ö±ğÖ´ĞĞ TestA ÓETestB
```csharp
        private void TestA()//ÎŞ²ÎÊı¡¢ÎŞ·µ»ØÖµ
        {
            MessageBox.Show("ÈÈ¼üA±»´¥·¢ÁË£¡");
        }

        private object TestB()//ÎŞ²ÎÊı¡¢·µ»ØÒ»¸öobject
        {
            return "ÈÈ¼üB±»´¥·¢ÁË£¡";
        }
```
#### Ê¾Àı. Ê¹ÓÃ GlobalHotKey.Add ¿EÙ×¢²áÁ½¸öÈ«¾ÖÈÈ¼E
```csharp
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            GlobalHotKey.Awake();

            GlobalHotKey.Add(ModelKeys.CTRL, NormalKeys.F1, TestA);
            GlobalHotKey.Add(ModelKeys.CTRL, NormalKeys.F2, TestB);
        }
```
###### ¹§Ï²£¬ÄãÒÑ¾­ÕÆÎÕÁË¸Ã¿â×ûÖËĞÄµÄ¹¦ÄÜ£¡

---

## ¢EÊ¹ÓÃ GlobalHotKey £¬ĞŞ¸ÄÈÈ¼E
#### Ê¾Àı1. ÒÑÖª Keys ,ĞŞ¸ÄÆä¶ÔÓ¦µÄ´¦ÀúæÂ¼ş£¨º¯Êı£©
```csharp
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            GlobalHotKey.Awake();
            GlobalHotKey.Add(ModelKeys.CTRL, NormalKeys.F1, TestA);

            GlobalHotKey.EditHotKey_Function(ModelKeys.CTRL, NormalKeys.F1, TestB);
            //Ô­±¾ [ Ctrl + F1 ] Ó¦¸Ã´¥·¢ TestA
            //¾­ĞŞ¸ÄºE, Ó¦¸Ã±»´¥·¢µÄ±äÎª TestB
        }
```
#### Ê¾Àı2. ÒÑÖª´¦ÀúæÂ¼ş£¬ĞŞ¸ÄÆä¶ÔÓ¦µÄ Keys
```csharp
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            GlobalHotKey.Awake();
            GlobalHotKey.Add(ModelKeys.CTRL, NormalKeys.F1, TestA);

            GlobalHotKey.EditHotKey_Keys(TestA, ModelKeys.CTRL, NormalKeys.F2);
            //Ô­±¾ TestA Ó¦ÓÉ [ Ctrl + F1 ] ´¥·¢ 
            //¾­ĞŞ¸ÄºE, Ó¦ÓÉ [ Ctrl + F2 ] ´¥·¢
        }
```

---

## ¢EÊ¹ÓÃ GlobalHotKey £¬É¾³ıÈÈ¼E
#### Ê¾Àı1. ¸ù¾İ ×¢²áID É¾³ıÈÈ¼E¨Ä¬ÈÏµÚÒ»¸öIDÊÇ2004£¬Ö®ºóÖğ¸öÀÛ¼Ó£©
```csharp
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            GlobalHotKey.Awake();
            GlobalHotKey.Add(ModelKeys.CTRL, NormalKeys.F1, TestA);

            GlobalHotKey.DeleteById(2004);
        }
```
#### Ê¾Àı2. ¸ù¾İ Keys É¾³ıÈÈ¼E
```csharp
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            GlobalHotKey.Awake();
            GlobalHotKey.Add(ModelKeys.CTRL, NormalKeys.F1, TestA);

            GlobalHotKey.DeleteByKeys(ModelKeys.CTRL, NormalKeys.F1);
        }
```
#### Ê¾Àı3. ¸ù¾İ ´¦ÀúÖ¯Êı É¾³ıÈÈ¼E
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

## ¢EÊ¹ÓÃ RegisterCollection £¬²éÑ¯×¢²áÔÚÁĞµÄÈÈ¼EÅÏ¢
#### Ê¾Àı1. ¸ù¾İ ID ²éÑ¯ÍEûµÄ×¢²áĞÅÏ¢ £¨ RegisterInfo ¶ÔÏE£©
```csharp
        RegisterInfo Info = GlobalHotKey.Registers[2004];
```
#### Ê¾Àı2. ´Ó RegisterInfo ÖĞ £¬È¡µÃÈÈ¼EÄÏ¸½ÚĞÅÏ¢
|ÊôĞÔ                   |ÀàĞÍ                        |º¬ÒE       |
|-----------------------|----------------------------|------------|
|RegisterID             |int                         |×¢²áid£¬ÆğÊ¼Îª2004£¬×Ô¶¯µİÔE|
|Model                  |ModelKeys                   |ÈÈ¼E- ÏµÍ³°´¼E|
|Normal                 |NormalKeys                  |ÈÈ¼E- ÎÄ±¾°´¼E|
|FunctionType           |FunctionTypes               |´¦ÀúÖ¯ÊıËùÊô·ÖÀE|
|Name                   |string                      |´¦ÀúÖ¯ÊıµÄº¯ÊıÃE|
|FunctionVoid           |Action              |´¦ÀúÖ¯Êı - void ĞÍ   |
|FunctionReturn         |Func<object>            |´¦ÀúÖ¯ÊıB - return object ĞÍ   |

---

## ¢EÊ¹ÓÃ ReturnValueMonitor £¬ÔÚÈÈ¼EÂ¼ş´¦ÀúéEÏºó£¬¶ÔÆä·µ»ØÖµ½øÒ»²½´¦Àú¿¨²»³£ÓÃ£©
#### Ê¾Àı. Ê¹ÓÃ BindingAutoEvent ´¦ÀúØà²âµ½µÄ·µ»ØÖµ
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
            //WhileObjectReturned½«¶ÔTestAÓEestB·µ»ØµÄobject×ö´¦ÀE

            GlobalHotKey.Add(ModelKeys.CTRL, NormalKeys.F1, TestA);
            GlobalHotKey.Add(ModelKeys.CTRL, NormalKeys.F2, TestB);
            //TestAÓEestBÖ»¸ºÔğ·µ»Øobject,²¢²»¶ÔÆä×öÈÎºÎ´¦ÀE
        }

        protected override void OnClosed(EventArgs e)
        {
            GlobalHotKey.Destroy();
            ReturnValueMonitor.Destroy();

            base.OnClosed(e);
        }

        private object TestA()
        {
            return "ÈÈ¼üA±»´¥·¢ÁË£¡";
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
                //¡­¡­
                //string ´¦ÀúŞß¼­£¬ÀıÈç´òÓ¡Öµ

                return;
            }

            if (ReturnValueMonitor.Value is int number)
            {
                //¡­¡­
                //int ´¦ÀúŞß¼­£¬ÀıÈç´òÓ¡Öµ

                return;
            }
        }
```

---

## ¢E[ HotKeyBox ] ¿Ø¼ş & [ HotKeysBox ] ¿Ø¼ş
#### Çé¾°. ¼Ù¶¨ÄãÏ£ÍûÖÆ×÷Ò»¸öÉèÖÃ½çÃæ£¬ÔÊĞúïÃ»§×Ô¼ºÉèÖÃÈÈ¼E
#### Ê¾Àı1. ½ÓÈEØ¼ş
##### ÒıÈEE
```xaml          
            xmlns:ff="clr-namespace:FastHotKeyForWPF;assembly=FastHotKeyForWPF"
```
##### ¶¨Òå¿Ø¼ş
```xaml
            <!--Ã¿¸ö¿Ø¼şÖ»½ÓÊÕÒ»¸öKey-->
            <ff:HotKeyBox x:Name="Box1"/>
            <ff:HotKeyBox x:Name="Box2"/>

            <!--Ã¿¸ö¿Ø¼ş½ÓÊÕÁ½¸öKey-->
            <ff:HotKeysBox x:Name="Box3"/>
```
##### ½¨Á¢Á¬½Ó
```csharp
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            GlobalHotKey.Awake();

            Box1.ConnectWith(Box2, TestA);
            Box3.ConnectWith(TestA);
            //ÎŞÂÛÄÄÖÖ Box £¬Á¬½Ó²Ù×÷Ö»ĞèÒªÖ´ĞĞÒ»´Î
        }

        private object TestA()
        {
            return "ÈÈ¼üA±»´¥·¢ÁË£¡";
        }
```
#### Ê¾Àı2. Îª¿Ø¼şÉèÖÃ³õÊ¼ÈÈ¼E
```csharp
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            GlobalHotKey.Awake();

            Box1.ConnectWith(Box2, TestA);
            Box3.ConnectWith(TestB);
            //×¢ÒâÏÈ½¨Á¢Á¬½ÓÔÙÉèÖÃ³õÊ¼ÈÈ¼E

            Box1.SetHotKey(ModelKeys.CTRL,NormalKeys.F1,TestA);           
            Box3.SetHotKey(ModelKeys.CTRL, NormalKeys.F2, TestB);
        }
```
#### [ HotKeyBox ] ¿ÉÑ¡ÏE
|ÊôĞÔ                   |ÀàĞÍ                        |º¬ÒE       |
|-----------------------|----------------------------|------------|
|CurrentKey             |Key                         |µ±Ç°Öµ |
|WhileInput             |event Action?               |ÓÃ»§·¢ÉúÊäÈEĞÎªÊ±£¬´¥·¢´ËÊÂ¼ş |
|ErrorText              |string                      |Èô°´¼E»ÊÜ¿âÖ§³Ö£¬Ôò¿Ø¼şÏÔÊ¾¸ÃÎÄ±¾ |
|IsHotKeyRegistered     |bool                        |Ä¿Ç°ÊÇ·ñ³É¹¦×¢²E|
|LastHotKeyID           |int                         |×ûÙE»´Î×¢²á³É¹¦µÄID |
|CornerRadius           |CornerRadius                |Ô²»¬¶È   |
|DefaultTextColor       |SolidColorBrush             |Ä¬ÈÏÎÄ±¾É«|
|DefaultBorderBrush     |SolidColorBrush             |Ä¬ÈÏÍâ±ß¿òÉ«|
|HoverTextColor         |SolidColorBrush             |ĞE£ÎÄ±¾É«|
|HoverBorderBrush       |SolidColorBrush             |ĞE£Íâ±ß¿òÉ«|
|ActualBackground       |SolidColorBrush             |±³¾°É«,×¢Òâ²»ÊÇ Background|
#### [ HotKeysBox ] ¿ÉÑ¡ÏE
|ÊôĞÔ                   |ÀàĞÍ                        |º¬ÒE       |
|-----------------------|----------------------------|------------|
|CurrentKeyA            |Key                         |×ó¼Eµ |
|CurrentKeyB            |Key                         |ÓÒ¼Eµ |
|WhileInput             |event Action?               |ÓÃ»§·¢ÉúÊäÈEĞÎªÊ±£¬´¥·¢´ËÊÂ¼ş |
|ErrorText              |string                      |Èô°´¼E»ÊÜ¿âÖ§³Ö£¬Ôò¿Ø¼şÏÔÊ¾¸ÃÎÄ±¾ |
|IsHotKeyRegistered     |bool                        |Ä¿Ç°ÊÇ·ñ³É¹¦×¢²E|
|LastHotKeyID           |int                         |×ûÙE»´Î×¢²á³É¹¦µÄID |
|CornerRadius           |CornerRadius                |Ô²»¬¶È   |
|DefaultTextColor       |SolidColorBrush             |Ä¬ÈÏÎÄ±¾É«|
|DefaultBorderBrush     |SolidColorBrush             |Ä¬ÈÏÍâ±ß¿òÉ«|
|HoverTextColor         |SolidColorBrush             |ĞE£ÎÄ±¾É«|
|HoverBorderBrush       |SolidColorBrush             |ĞE£Íâ±ß¿òÉ«|
|ActualBackground       |SolidColorBrush             |±³¾°É«,×¢Òâ²»ÊÇ Background|

#### [ HotKeyBox ] & [ HotKeysBox ] ÔÚXaml¹¹³ÉÉÏ¼¸ºõÒ»Ä£Ò»Ñù£¬Äã¿ÉÒÔÍ¨¹ı x:Name ·ÃÎÊÄÚ²¿ÔªËØ²¢ĞŞ¸ÄËEÇ
##### ÄÚ²¿ÔªËØÈçÏÂ
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