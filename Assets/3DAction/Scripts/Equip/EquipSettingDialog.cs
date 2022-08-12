//*********************************************
// 概　要：装備設定画面管理者
// 作成者：ta.kusumoto
// 作成日：2022/08/04

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipSettingDialog : MonoBehaviour
{
    [SerializeField] private Sprite _initIcon;          // 初期アイコン
    [SerializeField] private GameObject _initSelect;    // 初期選択
    private EquipSelectButton _selectedButton;          // 選択コマンド

    //*********************************************
    // 初期化
    public void Initialize()
    {
        // 初期ボタン選択
        EventSystem.current.SetSelectedGameObject(_initSelect);

        // 一旦全スロットを初期化
        for (int i = 0; i < Constants.MAX_EQUIP_SLOT; ++i) {
            SetEquipInfoImpl(i, -1, _initIcon, "");
        }

        // 情報があるスロットのみ更新
        var datas = EquipGroupData.Instance.EquipDatas;
        if(datas.Length == 0) { return; }

        var selectButtons = GetComponentsInChildren<EquipSelectButton>();
        var entity = Resources.Load<EquipEntity>("ScriptableObject/EquipList");
        foreach (var data in datas) {
            foreach (var equip in entity._equipList) {
                if (equip.EquipID != data.EquipID) { continue; }

                selectButtons[data.CommandID].Icon.sprite = equip.EquipIcon;
                selectButtons[data.CommandID].Name.text = equip.EquipName;
                SetEquipInfoImpl(data.CommandID, equip.EquipID, equip.EquipIcon, equip.EquipName);
            }
        }
    }

    //*********************************************
    // 選択したボタンを表示する
    public void EnableSelectedButton()
    {
        EventSystem.current.SetSelectedGameObject(_selectedButton.gameObject);
    }

    //*********************************************
    // コマンド情報を設定
    public void SetCommandInfo(GameObject command)
    {
        _selectedButton = command.GetComponent<EquipSelectButton>();
    }

    //*********************************************
    // 装備情報を設定
    public void SetEquipInfo(GameObject equip)
    {
        // 選択したオブジェクトからモデルを取得する
        var conroller = equip.GetComponent<EquipController>();
        var model = conroller.EquipModel;
        _selectedButton.Icon.sprite = model.EquipIcon;
        _selectedButton.Name.text = model.EquipName;

        // 装備情報記憶
        MemoryEquipInfo(_selectedButton.CommandID, model);

        // 情報設定(別シーン共有用)
        SetEquipInfoImpl(_selectedButton.CommandID, model.EquipID, model.EquipIcon, model.EquipName);
    }

    //*********************************************
    // 装備情報記憶
    private void MemoryEquipInfo(int commandID, EquipModel model)
    {
        // データをJSONに覚えさせる
        EquipGroupData.Instance.AddEquip(commandID, model.EquipID);
        EquipGroupData.Instance.Save();
    }

    //*********************************************
    // 装備情報設定
    private void SetEquipInfoImpl(int cmdID, int equipID, Sprite equipIcon, string equipName) {
        Debug.Log($"設定されたコマンドID：{cmdID}");
        GameManager.S_EQUIP_INFO equipInfo = new GameManager.S_EQUIP_INFO { 
            id = equipID, icon = equipIcon, name = equipName 
        };
        GameManager.Instance.SetEquipInfo(cmdID, equipInfo);
    }
}
