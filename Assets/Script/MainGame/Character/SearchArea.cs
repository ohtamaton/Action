﻿/**
 * SearchArea.cs
 * 
 * サーチエリアとの衝突処理
 *
 * @author ys.ohta
 * @version 1.0
 * @date 2016/07/15
 */
using UnityEngine;
using System.Collections;

/**
 * SearchArea
 */
public class SearchArea : MonoBehaviour {
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

    //None.

    //---------------------------------------------------
    // other
    //---------------------------------------------------

    //敵コントローラ
	EnemyCtrl enemyCtrl;

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
    void Start()
	{
		// EnemyCtrlをキャッシュする
		enemyCtrl = transform.root.GetComponent<EnemyCtrl>();
	}

    /**
     * <summary>
     * 衝突処理
     * </summary>
     * @param other 衝突対象
     * @return
     **/
    void OnTriggerStay( Collider other )
	{
        // Playerタグをターゲットにする
		if( other.tag == "Player" )
			enemyCtrl.SetAttackTarget( other.transform );
	}
}