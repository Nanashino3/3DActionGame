//*********************************************
// 概　要：装備一覧管理者
// 作成者：ta.kusumoto
// 作成日：2022/08/04

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class EquipListDialog : MonoBehaviour
{
    [SerializeField] private EquipController _equipPrefab;
    [SerializeField] private Transform _content;

    private Button[] _selectedListItems;

    // 装備アイテムを一括で取得
    public Button[] SelectedListItems { get => _selectedListItems; }

    // 初期化
    public void Initialize()
    {
        // 装備リスト分インスタンス生成
        EquipEntity entity = Resources.Load<EquipEntity>("ScriptableObject/EquipList");
        foreach (var equip in entity._equipList) {
            var controller = Instantiate(_equipPrefab, _content, false);
            controller.Initialize(equip);
        }

        // ボタンの経路設定
        _selectedListItems = GetComponentsInChildren<Button>();
        for (int i = 0; i < _selectedListItems.Length; ++i) {
            Button prev = null, next = null;
            var controller = _selectedListItems[i].GetComponent<EquipController>();
            if(i == 0) {
                next = _selectedListItems[i + 1];
            }else if(i == _selectedListItems.Length - 1) {
                prev = _selectedListItems[i - 1];
            }else{
                prev = _selectedListItems[i - 1];
                next = _selectedListItems[i + 1];
            }
            controller.NavigationConnect(prev, next);
        }
    }

    // 装備一覧の表示/非表示を切り替える
    public void Toggle()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    private void OnEnable()
    {
        // 初期選択ボタンの再指定
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(_selectedListItems[0].gameObject);
    }
}