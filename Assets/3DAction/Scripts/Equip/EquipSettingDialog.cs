//*********************************************
// �T�@�v�F�����ݒ��ʊǗ���
// �쐬�ҁFta.kusumoto
// �쐬���F2022/08/04

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipSettingDialog : MonoBehaviour
{
    [SerializeField] private Sprite _initIcon;          // �����A�C�R��
    [SerializeField] private GameObject _initSelect;    // �����I��
    private EquipSelectButton _selectedButton;          // �I���R�}���h

    //*********************************************
    // ������
    public void Initialize()
    {
        // �����{�^���I��
        EventSystem.current.SetSelectedGameObject(_initSelect);

        // ��U�S�X���b�g��������
        for (int i = 0; i < Constants.MAX_EQUIP_SLOT; ++i) {
            SetEquipInfoImpl(i, -1, _initIcon, "");
        }

        // ��񂪂���X���b�g�̂ݍX�V
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
    // �I�������{�^����\������
    public void EnableSelectedButton()
    {
        EventSystem.current.SetSelectedGameObject(_selectedButton.gameObject);
    }

    //*********************************************
    // �R�}���h����ݒ�
    public void SetCommandInfo(GameObject command)
    {
        _selectedButton = command.GetComponent<EquipSelectButton>();
    }

    //*********************************************
    // ��������ݒ�
    public void SetEquipInfo(GameObject equip)
    {
        // �I�������I�u�W�F�N�g���烂�f�����擾����
        var conroller = equip.GetComponent<EquipController>();
        var model = conroller.EquipModel;
        _selectedButton.Icon.sprite = model.EquipIcon;
        _selectedButton.Name.text = model.EquipName;

        // �������L��
        MemoryEquipInfo(_selectedButton.CommandID, model);

        // ���ݒ�(�ʃV�[�����L�p)
        SetEquipInfoImpl(_selectedButton.CommandID, model.EquipID, model.EquipIcon, model.EquipName);
    }

    //*********************************************
    // �������L��
    private void MemoryEquipInfo(int commandID, EquipModel model)
    {
        // �f�[�^��JSON�Ɋo��������
        EquipGroupData.Instance.AddEquip(commandID, model.EquipID);
        EquipGroupData.Instance.Save();
    }

    //*********************************************
    // �������ݒ�
    private void SetEquipInfoImpl(int cmdID, int equipID, Sprite equipIcon, string equipName) {
        Debug.Log($"�ݒ肳�ꂽ�R�}���hID�F{cmdID}");
        GameManager.S_EQUIP_INFO equipInfo = new GameManager.S_EQUIP_INFO { 
            id = equipID, icon = equipIcon, name = equipName 
        };
        GameManager.Instance.SetEquipInfo(cmdID, equipInfo);
    }
}
