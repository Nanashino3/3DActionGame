//*********************************************
// 概　要：装備切替管理者
// 作成者：ta.kusumoto
// 作成日：2022/08/06
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//************************************
// 戦闘画面でのコマンド選択ボタン処理
public class EquipChange : MonoBehaviour
{
    [SerializeField] private int _commandID;
    [SerializeField] private Image _icon;

    public int CommandID { get => _commandID; }
    public Image Icon { get => _icon; }

    private void Start()
    {
        var settingEquip = GameManager.Instance.GetEquipInfo(_commandID);
        _icon.sprite = settingEquip.icon;
    }
}
