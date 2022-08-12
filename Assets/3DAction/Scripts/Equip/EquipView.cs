//*********************************************
// 概　要：装備見かけ
// 作成者：ta.kusumoto
// 作成日：2022/08/04

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipView : MonoBehaviour
{
    [SerializeField] private Image _viewIcon;
    [SerializeField] private Text _viewName;
    
    //*******************************
    // 描画処理
    public void Draw(EquipModel model)
    {
        _viewIcon.sprite = model.EquipIcon;
        _viewName.text = model.EquipName;
    }
}
