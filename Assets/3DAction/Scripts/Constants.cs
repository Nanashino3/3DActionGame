//*********************************************
// 概　要：定数管理クラス
// 作成者：ta.kusumoto
// 作成日：2022/08/04

static class Constants
{
    public static readonly int MAX_EQUIP_SLOT = 8;  // 最大装備スロット
    public static readonly int MAX_EQUIP_KIND = 2;  // 最大装備種
    
    public static readonly int EQUIP_GROUP_MAIN = 0;    // メイングループ
    public static readonly int EQUIP_GROUP_SUB = 1;     // サブグループ
    public static readonly int EQUIP_GROUP_MAX = 2;     // 最大グループ数

    public static readonly int EQUIP_SELECT_UP = 0;     // 装備選択：上
    public static readonly int EQUIP_SELECT_LEFT = 1;   // 装備選択：左
    public static readonly int EQUIP_SELECT_DOWN = 2;   // 装備選択：下
    public static readonly int EQUIP_SELECT_RIGHT = 3;  // 装備選択：右
    public static readonly int EQUIP_SELECT_MAX = 4;    // 最大装備選択数
}
