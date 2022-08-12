//*********************************************
// �T�@�v�F�����ꗗ�Ǘ���
// �쐬�ҁFta.kusumoto
// �쐬���F2022/08/04

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

    // �����A�C�e�����ꊇ�Ŏ擾
    public Button[] SelectedListItems { get => _selectedListItems; }

    // ������
    public void Initialize()
    {
        // �������X�g���C���X�^���X����
        EquipEntity entity = Resources.Load<EquipEntity>("ScriptableObject/EquipList");
        foreach (var equip in entity._equipList) {
            var controller = Instantiate(_equipPrefab, _content, false);
            controller.Initialize(equip);
        }

        // �{�^���̌o�H�ݒ�
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

    // �����ꗗ�̕\��/��\����؂�ւ���
    public void Toggle()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    private void OnEnable()
    {
        // �����I���{�^���̍Ďw��
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(_selectedListItems[0].gameObject);
    }
}