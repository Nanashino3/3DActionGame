//*********************************************
// 概　要：選択装備
// 作成者：ta.kusumoto
// 作成日：2022/08/04

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipSelectButton : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private Text _name;
    [SerializeField] private int _cmdID;

    public Image Icon { get => _icon; }
    public Text Name { get => _name; }
    public int CommandID { get => _cmdID; }
}
