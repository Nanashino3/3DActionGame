//*********************************************
// �T�@�v�F�����ؑ֊Ǘ���
// �쐬�ҁFta.kusumoto
// �쐬���F2022/08/06
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipChangeManager : MonoBehaviour
{
    [Header("�I�����������A�C�R��")]
    [SerializeField] private Image[] _selectedIcon;
    [Header("�I�𒆃O���[�v�A�C�R��")]
    [SerializeField] private Image[] _selectingGroup;
    [Header("�����O���[�v�I�u�W�F�N�g(Main or Sub")]
    [SerializeField] private GameObject[] _equipGroup;
    [Header("�I���\�����ꗗ")]
    [SerializeField] private EquipChange[] _selectableEquipButton;

    private int[] _prevCommandID = new int[Constants.MAX_EQUIP_KIND];    // �O��̃R�}���hID

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
            // RB�{�^��
            Debug.Log("RB�{�^��");
            group = Constants.EQUIP_GROUP_MAIN;
        }else if((int)Input.GetAxis("Trigger") > 0) {
            // RT�{�^��
            Debug.Log("RT�{�^��");
            group = Constants.EQUIP_GROUP_SUB;
        }
        if(group == Constants.EQUIP_GROUP_MAX) { return; }

        // �I���O���[�v�̐؂�ւ�
        EqupGroupChange(group);

        // ����I��
        int selectButton = Constants.EQUIP_SELECT_MAX;
        if(Input.GetKeyDown("joystick button 0")) {
            Debug.Log("A�{�^��");
            selectButton = Constants.EQUIP_SELECT_DOWN;
        }else if(Input.GetKeyDown("joystick button 1")) {
            Debug.Log("B�{�^��");
            selectButton = Constants.EQUIP_SELECT_RIGHT;
        }else if(Input.GetKeyDown("joystick button 3")) {
            Debug.Log("Y�{�^��");
            selectButton = Constants.EQUIP_SELECT_UP;
        }else if(Input.GetKeyDown("joystick button 2")) {
            Debug.Log("X�{�^��");
            selectButton = Constants.EQUIP_SELECT_LEFT;
        }

        // �I�������{�^���̏������Ăяo��
        if(selectButton != Constants.EQUIP_SELECT_MAX) {
            var commandID = selectButton + group * Constants.EQUIP_SELECT_MAX;
            var button = _selectableEquipButton[commandID].GetComponent<Button>();
            button.onClick.Invoke();
        }
    }

    //******************************
    // �I�����������ݒ�
    private void SetSelectedEquip(int commandID)
    {
        Debug.Log($"�I�������R�}���hID�F{commandID}");
        var kind = (int)(IsMainGroup ? Constants.EQUIP_GROUP_MAIN : Constants.EQUIP_GROUP_SUB);
        // �I�����������A�C�R����ݒ�
        var equipInfo = GameManager.Instance.GetEquipInfo(commandID);
        _selectedIcon[kind].sprite = equipInfo.icon;

        if (_prevCommandID[kind] == commandID) { return; }

        // �I�𒆂̑����𖾎��I�ɕ�����悤�ɐF��ύX
        _selectableEquipButton[commandID].gameObject.GetComponent<Image>().color = Color.cyan;
        if (_prevCommandID[kind] >= 0) { _selectableEquipButton[_prevCommandID[kind]].gameObject.GetComponent<Image>().color = Color.white; }

        _prevCommandID[kind] = commandID;
    }

    //******************************
    // �I�𒆂̑����O���[�v
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
            Debug.LogError("�ϊ��Ɏ��s���܂���");
        }
    }

    //******************************
    // �����O���[�v�̐؂�ւ�
    private void EqupGroupChange(int group)
    {
        if (group == Constants.EQUIP_GROUP_MAIN) {
            _equipGroup[Constants.EQUIP_GROUP_MAIN].SetActive(true);
            _equipGroup[Constants.EQUIP_GROUP_SUB].SetActive(false);
        } else {
            _equipGroup[Constants.EQUIP_GROUP_MAIN].SetActive(false);
            _equipGroup[Constants.EQUIP_GROUP_SUB].SetActive(true);
        }

        // �I�𑕔��O���[�v
        SelectingEquipGroup();
    }

    //******************************
    // ���C���O���[�v���L����
    private bool IsMainGroup { get => _equipGroup[Constants.EQUIP_GROUP_MAIN].gameObject.activeSelf; }
}
