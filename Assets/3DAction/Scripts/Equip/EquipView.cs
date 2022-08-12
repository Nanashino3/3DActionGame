//*********************************************
// �T�@�v�F����������
// �쐬�ҁFta.kusumoto
// �쐬���F2022/08/04

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipView : MonoBehaviour
{
    [SerializeField] private Image _viewIcon;
    [SerializeField] private Text _viewName;
    
    //*******************************
    // �`�揈��
    public void Draw(EquipModel model)
    {
        _viewIcon.sprite = model.EquipIcon;
        _viewName.text = model.EquipName;
    }
}
