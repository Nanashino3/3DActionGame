//*********************************************
// �T�@�v�F�����O���[�v�f�[�^
// �쐬�ҁFta.kusumoto
// �쐬���F2022/08/04

using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class EquipGroupData
{
    private const string _playerPrefsKey = "EQUIP_DATA";
    private static EquipGroupData _instance;

    // �����f�[�^���X�g
    [SerializeField] private List<EquipData> _equipDatas = new List<EquipData>();

    // �V���O���g��
    public static EquipGroupData Instance
    {
        get
        {
            if(_instance == null) {
                _instance = PlayerPrefs.HasKey(_playerPrefsKey) ? JsonUtility.FromJson<EquipGroupData>(PlayerPrefs.GetString(_playerPrefsKey)) : new EquipGroupData();
            }
            return _instance;
        }
    }
    private EquipGroupData(){}

    // JSON������PlayerPrefs�ɕۑ�
    public void Save()
    {
        var jsonString = JsonUtility.ToJson(this);
        PlayerPrefs.SetString(_playerPrefsKey, jsonString);
        PlayerPrefs.Save();
    }

    // �����ǉ�
    public void AddEquip(int commandID, int equipID)
    {
        var equipData = GetEqupData(commandID);
        if (equipData == null) {
            equipData = new EquipData(commandID, equipID);
            _equipDatas.Add(equipData);
            return;
        }
        equipData.CommandID = commandID;
        equipData.EquipID = equipID;
    }

    // �w�肵��ID�ő����f�[�^���擾����
    public EquipData GetEqupData(int CommandID)
    {
        return _equipDatas.FirstOrDefault(x => x.CommandID == CommandID);
    }

    // �����f�[�^���ꊇ�Ŏ擾
    public EquipData[] EquipDatas
    {
        get { return _equipDatas.ToArray(); }
    }

    [Serializable]
    public class EquipData
    {
        [SerializeField] private int _cmdID;
        [SerializeField] private int _equipID;

        public EquipData(int cmdID, int equipID)
        {
            _cmdID = cmdID;
            _equipID = equipID;
        }

        public int CommandID { get => _cmdID; set => _cmdID = value; }
        public int EquipID { get => _equipID; set => _equipID = value; }
    }
}
