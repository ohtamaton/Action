/**
 * AttackAreaActivater.cs
 * 
 * AttackAreaに対するアクティベーション処理を行う
 *
 * @author ys.ohta
 * @version 1.0
 * @date 2016/07/15
 */
using UnityEngine;
using System.Collections;

/**
 * AttackAreaActivater
 */
public class AttackAreaActivator : MonoBehaviour {
//===========================================================
// 変数宣言
//===========================================================
    //---------------------------------------------------
    // public
    //---------------------------------------------------

    //None.

    //---------------------------------------------------
    // private
    //---------------------------------------------------

    //攻撃判定Colliders
    private Collider[] attackAreaColliders;

//===========================================================
// 関数宣言
//===========================================================
    //---------------------------------------------------
    // public
    //---------------------------------------------------

    //None.

    //---------------------------------------------------
    // private
    //---------------------------------------------------

    //None.

    //---------------------------------------------------
    // other
    //---------------------------------------------------

    /**
     * <summary>
     * 初期化処理
     * </summary>
     * @param
     * @return
     **/
    void Start () {
		//子供のGameObjectからAttackAreaスクリプトがついているGameObjectを探す。
		AttackArea[] attackAreas = GetComponentsInChildren<AttackArea>();
		attackAreaColliders = new Collider[attackAreas.Length];

        for (int attackAreaCnt = 0; attackAreaCnt < attackAreas.Length; attackAreaCnt++)
        {
            //AttackAreaスクリプトがついているGameObjectのコライダを配列に格納する.
            attackAreaColliders[attackAreaCnt] = attackAreas[attackAreaCnt].GetComponent<Collider>();
            //初期はfalseにしておく.
            attackAreaColliders[attackAreaCnt].enabled = false;
        }  
	}

    /**
     * <summary>
     * アニメーションイベントのStartAttackHitを受け取ってコライダを有効にする
     * </summary>
     * @param
     * @return
     **/
    void StartAttackHit()
	{
		foreach (Collider attackAreaCollider in attackAreaColliders)
			attackAreaCollider.enabled = true;
	}

    /**
     * <summary>
     * アニメーションイベントのStartAttackHitを受け取ってコライダを無効にする
     * </summary>
     * @param
     * @return
     **/
    void EndAttackHit()
	{
		foreach (Collider attackAreaCollider in attackAreaColliders)
			attackAreaCollider.enabled = false;
	}
}
