/**
 * GameResultGUI.cs
 * 
 * ゲーム結果のGUI
 *
 * @author ys.ohta
 * @version 1.0
 * @date 2016/07/15
 */
using UnityEngine;
using System.Collections;

/**
 * GameResultGUI
 */
public class GameResultGUI : MonoBehaviour
{
//===========================================================
// 変数宣言
//===========================================================
    //---------------------------------------------------
    // public
    //---------------------------------------------------

    //ゲームオーバ用テクスチャ
	public Texture2D gameOverTexture;

    //ゲームクリア用テクスチャ
	public Texture2D gameClearTexture;

    //---------------------------------------------------
    // private
    //---------------------------------------------------

    //None.

    //---------------------------------------------------
    // other
    //---------------------------------------------------

    //ゲームルールコントローラ
    GameRuleCtrl gameRuleCtrl;

    //ベース横縦幅
    float baseWidth = 854f;
	float baseHeight = 480f;

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
    void Awake()
	{
		gameRuleCtrl = GameObject.FindObjectOfType(typeof(GameRuleCtrl)) as GameRuleCtrl;
	}

    /**
     * <summary>
     * GUI描画処理
     * </summary>
     * @param
     * @return
     **/
    void OnGUI()
	{
		Texture2D aTexture;
		if( gameRuleCtrl.gameClear )
		{
			aTexture = gameClearTexture;
		}
		else if( gameRuleCtrl.gameOver )
		{
			aTexture = gameOverTexture;
		}
		else
		{
			return;
		}

		// 解像度対応.
		GUI.matrix = Matrix4x4.TRS(
			Vector3.zero,
			Quaternion.identity,
			new Vector3(Screen.width / baseWidth, Screen.height / baseHeight, 1f));
		
		// リザルト.
		GUI.DrawTexture(new Rect(0.0f, 208.0f, 854.0f, 64.0f), aTexture);
	}
}
