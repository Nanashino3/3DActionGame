//*********************************************
// �T�@�v�F�����Ǘ���
// �쐬�ҁFta.kusumoto
// �쐬���F2022/08/04

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class EquipController : MonoBehaviour
{
    [SerializeField] private EquipView _equipView;  // ������(view)�Ɋւ��邱�Ƃ𑀍�
    EquipModel _equipModel;                         // �f�[�^(model)�Ɋւ��邱�Ƃ𑀍�

    //****************************
    // ������
    public void Initialize(EquipInfo info)
    {
        _equipModel = new EquipModel(info);
        _equipView.Draw(_equipModel);
    }

    //****************************
    // �i�r�Q�[�V�����̐ڑ�
    public void NavigationConnect(Button prev, Button next)
    {
        var navigation = gameObject.GetComponent<Button>().navigation;
        navigation.mode = Navigation.Mode.Explicit;

        navigation.selectOnUp = prev;
        navigation.selectOnDown = next;

        gameObject.GetComponent<Button>().navigation = navigation;
    }

    //****************************
    // �������(�ǂݎ��p)
    public EquipModel EquipModel { get => _equipModel; }
}
