/**
 * CharacterStatus.cs
 * 
 * Characterのステータス
 *
 * @author ys.ohta
 * @version 1.0
 * @date 2016/07/15
 */
using UnityEngine;
using System.Collections;

/**
 * CharacterStatus
 */
public class CharacterStatus : MonoBehaviour
{
//===========================================================
// 変数宣言
//===========================================================
    //---------------------------------------------------
    // public
    //---------------------------------------------------

    // HP.
    public int HP = 100;

    // 最大HP
    public int MaxHP = 100;

    // 攻撃力.
    public int Power = 10;

    // 最後に攻撃した対象.
    public GameObject lastAttackTarget = null;

    // 攻撃状態.
    public bool attacking = false;

    // 死亡状態.
    public bool died = false;

    // 攻撃力強化
    public bool powerBoost = false;

    // キャラクタ名
    public string characterName = "Player";

    //---------------------------------------------------
    // private
    //---------------------------------------------------

    //None.

    //---------------------------------------------------
    // other
    //---------------------------------------------------

    // 攻撃強化時間
    float powerBoostTime = 0.0f;

//===========================================================
// 関数宣言
//===========================================================
    //---------------------------------------------------
    // public
    //---------------------------------------------------
       
    /**
     * <summery>
     * アイテム取得時のイベント処理
     * </summery>
     * @param 取得アイテム種別
     * @return
     */
    public void GetItem(DropItem.ItemKind itemKind)
    {
        switch (itemKind)
        {
            case DropItem.ItemKind.Attack:
                powerBoostTime = 5.0f;
                break;
            case DropItem.ItemKind.Heal:
                // MaxHPの半分回復
                HP = Mathf.Min(HP + MaxHP / 2, MaxHP);
                break;
        }
    }

    //---------------------------------------------------
    // private
    //---------------------------------------------------

    //None.

    //---------------------------------------------------
    // other
    //---------------------------------------------------

    /**
     * <summery>
     * アイテムイベントの処理. 
     * ブースト時間の処理
     * </summery>
     * @param 
     * @return
     */
    void Update()
    {
        powerBoost = false;
        if (powerBoostTime > 0.0f)
        {
            powerBoost = true;
            powerBoostTime = Mathf.Max(powerBoostTime - Time.deltaTime, 0.0f);
        }
    }

}
