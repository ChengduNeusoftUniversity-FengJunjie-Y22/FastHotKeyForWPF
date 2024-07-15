using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Input;

namespace FastHotKeyForWPF.DIYControls
{
    internal static class BoxPool
    {
        internal static List<HotKeyBox> hotKeyBoxes = new List<HotKeyBox>();
        internal static List<HotKeysBox> hotKeysBoxes = new List<HotKeysBox>();
        internal static void RemoveSameInKey(Key key1, Key key2, HotKeyBox sender)
        {
            List<HotKeyBox> listA = new List<HotKeyBox>();
            foreach (var boxA in hotKeyBoxes)
            {
                if (boxA != sender && boxA != sender.Link)
                {
                    listA.Add(boxA);
                }
            }
            List<HotKeyBox> listB = new List<HotKeyBox>();
            foreach (var boxB in listA)
            {
                if ((boxB.CurrentKey == key1 && boxB.Link.CurrentKey == key2) ||
                    (boxB.CurrentKey == key2 && boxB.Link.CurrentKey == key1))
                {
                    listB.Add(boxB);
                }
            }
            for (int i = 0; i < listB.Count; i++)
            {
                listB[i].CurrentKey = Key.None;
                listB[i].Link.CurrentKey = Key.None;
                listB[i].UpdateText();
                listB[i].Link.UpdateText();
            }
        }
        internal static void RemoveSameInKey(Key key1, Key key2)
        {
            List<HotKeyBox> listA = new List<HotKeyBox>();
            foreach (var boxA in hotKeyBoxes)
            {
                if (boxA.IsMainBox)
                {
                    listA.Add(boxA);
                }
            }
            List<HotKeyBox> listB = new List<HotKeyBox>();
            foreach (var boxB in listA)
            {
                if ((boxB.CurrentKey == key1 && boxB.Link.CurrentKey == key2) ||
                    (boxB.CurrentKey == key2 && boxB.Link.CurrentKey == key1))
                {
                    listB.Add(boxB);
                }
            }
            for (int i = 0; i < listB.Count; i++)
            {
                listB[i].CurrentKey = Key.None;
                listB[i].Link.CurrentKey = Key.None;
                listB[i].ActualText.Text = listB[i].CurrentKey.ToString();
                listB[i].Link.ActualText.Text = listB[i].Link.CurrentKey.ToString();
            }
        }
        internal static void RemoveSameInKeys(Key key1, Key key2, HotKeysBox sender)
        {
            List<HotKeysBox> listA = new List<HotKeysBox>();
            foreach (var boxA in hotKeysBoxes)
            {
                if (boxA.CurrentKeyA == key1 && boxA.CurrentKeyB == key2 && boxA != sender)
                {
                    listA.Add(boxA);
                }
            }
            List<HotKeysBox> listB = new List<HotKeysBox>();
            foreach (var boxB in listA)
            {
                if (boxB.CurrentKeyA == key1 && boxB.CurrentKeyB == key2)
                {
                    listB.Add(boxB);
                }
            }
            for (int i = 0; i < listB.Count; i++)
            {
                listB[i].CurrentKeyA = Key.None;
                listB[i].CurrentKeyB = Key.None;
                listB[i].UpdateText();
            }
        }
        internal static void RemoveSameInKeys(Key key1, Key key2)
        {
            List<HotKeysBox> listA = new List<HotKeysBox>();
            foreach (var boxA in hotKeysBoxes)
            {
                if ((boxA.CurrentKeyA == key1 && boxA.CurrentKeyB == key2) ||
                    (boxA.CurrentKeyA == key2 && boxA.CurrentKeyB == key1))
                {
                    listA.Add(boxA);
                }
            }
            for (int i = 0; i < listA.Count; i++)
            {
                listA[i].CurrentKeyA = Key.None;
                listA[i].CurrentKeyB = Key.None;
                listA[i].UpdateText();
            }
        }
    }
}
