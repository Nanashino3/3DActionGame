//*********************************************
// �T�@�v�F�����G���e�B�e�B
// �쐬�ҁFta.kusumoto
// �쐬���F2022/08/04
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="EquipEntity", menuName ="ScriptableObject/EquipInfoList")]
public class EquipEntity : ScriptableObject
{
    // ������񃊃X�g
    public List<EquipInfo> _equipList = new List<EquipInfo> ();
}

//****************************
// �������
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