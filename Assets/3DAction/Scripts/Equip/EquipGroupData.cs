//*********************************************
// 概　要：装備グループデータ
// 作成者：ta.kusumoto
// 作成日：2022/08/04

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

    // 装備データリスト
    [SerializeField] private List<EquipData> _equipDatas = new List<EquipData>();

    // シングルトン
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

    // JSON化してPlayerPrefsに保存
    public void Save()
    {
        var jsonString = JsonUtility.ToJson(this);
        PlayerPrefs.SetString(_playerPrefsKey, jsonString);
        PlayerPrefs.Save();
    }

    // 装備追加
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

    // 指定したIDで装備データを取得する
    public EquipData GetEqupData(int CommandID)
    {
        return _equipDatas.FirstOrDefault(x => x.CommandID == CommandID);
    }

    // 装備データを一括で取得
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
