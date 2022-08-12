//*********************************************
// 概　要：装備エンティティ
// 作成者：ta.kusumoto
// 作成日：2022/08/04
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="EquipEntity", menuName ="ScriptableObject/EquipInfoList")]
public class EquipEntity : ScriptableObject
{
    // 装備情報リスト
    public List<EquipInfo> _equipList = new List<EquipInfo> ();
}

//****************************
// 装備情報
[Serializable]
public class EquipInfo
{
    [SerializeField] private int _id;
    [SerializeField] private Sprite _icon;
    [SerializeField] private string _name;

    public int EquipID { get => _id; }
    public Sprite EquipIcon { get => _icon; }
    public string EquipName { get => _name; }
}