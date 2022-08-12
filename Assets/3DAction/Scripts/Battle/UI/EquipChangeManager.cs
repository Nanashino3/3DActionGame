//*********************************************
// 概　要：装備切替管理者
// 作成者：ta.kusumoto
// 作成日：2022/08/06
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipChangeManager : MonoBehaviour
{
    [Header("選択した装備アイコン")]
    [SerializeField] private Image[] _selectedIcon;
    [Header("選択中グループアイコン")]
    [SerializeField] private Image[] _selectingGroup;
    [Header("装備グループオブジェクト(Main or Sub")]
    [SerializeField] private GameObject[] _equipGroup;
    [Header("選択可能装備一覧")]
    [SerializeField] private EquipChange[] _selectableEquipButton;

    private int[] _prevCommandID = new int[Constants.MAX_EQUIP_KIND];    // 前回のコマンドID

    private void Start()
    {
        for (int i = 0; i < Constants.MAX_EQUIP_KIND; ++i) {
            _prevCommandID[i] = -1;
        }
        foreach (var equip in _selectableEquipButton) {
            var button = equip.GetComponent<Button>();
            button.onClick.AddListener(() => SetSelectedEquip(equip.CommandID));
        }
    }

    private void Update()
    {
        int group = Constants.EQUIP_GROUP_MAX;
        if(Input.GetKey("joystick button 5")) {
            // RBボタン
            Debug.Log("RBボタン");
            group = Constants.EQUIP_GROUP_MAIN;
        }else if((int)Input.GetAxis("Trigger") > 0) {
            // RTボタン
            Debug.Log("RTボタン");
            group = Constants.EQUIP_GROUP_SUB;
        }
        if(group == Constants.EQUIP_GROUP_MAX) { return; }

        // 選択グループの切り替え
        EqupGroupChange(group);

        // 武器選択
        int selectButton = Constants.EQUIP_SELECT_MAX;
        if(Input.GetKeyDown("joystick button 0")) {
            Debug.Log("Aボタン");
            selectButton = Constants.EQUIP_SELECT_DOWN;
        }else if(Input.GetKeyDown("joystick button 1")) {
            Debug.Log("Bボタン");
            selectButton = Constants.EQUIP_SELECT_RIGHT;
        }else if(Input.GetKeyDown("joystick button 3")) {
            Debug.Log("Yボタン");
            selectButton = Constants.EQUIP_SELECT_UP;
        }else if(Input.GetKeyDown("joystick button 2")) {
            Debug.Log("Xボタン");
            selectButton = Constants.EQUIP_SELECT_LEFT;
        }

        // 選択したボタンの処理を呼び出す
        if(selectButton != Constants.EQUIP_SELECT_MAX) {
            var commandID = selectButton + group * Constants.EQUIP_SELECT_MAX;
            var button = _selectableEquipButton[commandID].GetComponent<Button>();
            button.onClick.Invoke();
        }
    }

    //******************************
    // 選択した装備設定
    private void SetSelectedEquip(int commandID)
    {
        Debug.Log($"選択したコマンドID：{commandID}");
        var kind = (int)(IsMainGroup ? Constants.EQUIP_GROUP_MAIN : Constants.EQUIP_GROUP_SUB);
        // 選択した装備アイコンを設定
        var equipInfo = GameManager.Instance.GetEquipInfo(commandID);
        _selectedIcon[kind].sprite = equipInfo.icon;

        if (_prevCommandID[kind] == commandID) { return; }

        // 選択中の装備を明示的に分かるように色を変更
        _selectableEquipButton[commandID].gameObject.GetComponent<Image>().color = Color.cyan;
        if (_prevCommandID[kind] >= 0) { _selectableEquipButton[_prevCommandID[kind]].gameObject.GetComponent<Image>().color = Color.white; }

        _prevCommandID[kind] = commandID;
    }

    //******************************
    // 選択中の装備グループ
    private void SelectingEquipGroup()
    {
        string colorCode = "#FF00FF";
        Color color = default(Color);
        if (ColorUtility.TryParseHtmlString(colorCode, out color)) {
            var currentKind = IsMainGroup ? Constants.EQUIP_GROUP_MAIN : Constants.EQUIP_GROUP_SUB;
            var prevKind = IsMainGroup ? Constants.EQUIP_GROUP_SUB : Constants.EQUIP_GROUP_MAIN;
            _selectingGroup[currentKind].color = color;
            _selectingGroup[prevKind].color = Color.white;
        } else {
            Debug.LogError("変換に失敗しました");
        }
    }

    //******************************
    // 装備グループの切り替え
    private void EqupGroupChange(int group)
    {
        if (group == Constants.EQUIP_GROUP_MAIN) {
            _equipGroup[Constants.EQUIP_GROUP_MAIN].SetActive(true);
            _equipGroup[Constants.EQUIP_GROUP_SUB].SetActive(false);
        } else {
            _equipGroup[Constants.EQUIP_GROUP_MAIN].SetActive(false);
            _equipGroup[Constants.EQUIP_GROUP_SUB].SetActive(true);
        }

        // 選択装備グループ
        SelectingEquipGroup();
    }

    //******************************
    // メイングループが有効化
    private bool IsMainGroup { get => _equipGroup[Constants.EQUIP_GROUP_MAIN].gameObject.activeSelf; }
}
