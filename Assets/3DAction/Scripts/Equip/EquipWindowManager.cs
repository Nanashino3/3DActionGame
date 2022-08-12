//*********************************************
// 概　要：装備画面管理者
// 作成者：ta.kusumoto
// 作成日：2022/08/04

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EquipWindowManager : MonoBehaviour
{
    [SerializeField] private EquipSettingDialog _equipSettingDialog;        // 装備設定ウィンドウ
    [SerializeField] private EquipListDialog _equipListDialog;              // 装備一覧ウィンドウ
    [SerializeField] private List<Button> _buttons = new List<Button>();    // ボタンリスト(コマンド)

    private void Start()
    {
        // 各種ダイアログの初期化
        _equipSettingDialog.Initialize();
        _equipListDialog.Initialize();
        // 各種コマンドボタン押下時の処理
        foreach(var button in _buttons) {
            button.onClick.AddListener(ToggleEquipListDialog);
        }
        // リストアイテム押下時の処理
        foreach(var button in _equipListDialog.SelectedListItems) {
            button.onClick.AddListener(SetEquipInfo);
        }
    }

    // 装備一覧ウィンドウ開閉する
    private void ToggleEquipListDialog()
    {
        var commandObject = EventSystem.current.currentSelectedGameObject;
        _equipSettingDialog.SetCommandInfo(commandObject);
        _equipListDialog.Toggle();
    }

    // 装備情報を設定
    private void SetEquipInfo()
    {
        Debug.Log("装備が選択されました");
        var selectObject = EventSystem.current.currentSelectedGameObject;
        _equipSettingDialog.SetEquipInfo(selectObject);
        _equipListDialog.Toggle();
        _equipSettingDialog.EnableSelectedButton();
    }
}