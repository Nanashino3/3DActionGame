//*********************************************
// �T�@�v�F�������f��
// �쐬�ҁFta.kusumoto
// �쐬���F2022/08/04

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipModel : MonoBehaviour
{
    private int _equipID;
    private Sprite _equipIcon;
    private string _equipName;

    public EquipModel(EquipInfo info)
    {
        _equipID = info.EquipID;
        _equipIcon = info.EquipIcon;
        _equipName = info.EquipName;
    }

    public int EquipID { get => _equipID; }
    public Sprite EquipIcon { get => _equipIcon; }
    public string EquipName { get => _equipName; }
}
