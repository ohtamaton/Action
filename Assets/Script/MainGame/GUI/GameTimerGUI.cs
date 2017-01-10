/**
 * GameTimerGUI.cs
 * 
 * ゲームタイマ用のGUI
 *
 * @author ys.ohta
 * @version 1.0
 * @date 2016/07/15
 */
using UnityEngine;
using System.Collections;

/**
 * GameTimerGUI
 */
public class GameTimerGUI : MonoBehaviour
{
//===========================================================
// 変数宣言
//===========================================================
    //---------------------------------------------------
    // public
    //---------------------------------------------------

	public Texture timerIcon;
	public GUIStyle timerLabelStyle;

    //---------------------------------------------------
    // private
    //---------------------------------------------------

    //None.

    //---------------------------------------------------
    // other
    //---------------------------------------------------

    GameRuleCtrl gameRuleCtrl;

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
     * 描画処理
     * </summary>
     * @param
     * @return
     **/
    void OnGUI()
	{
		// 解像度対応.
		GUI.matrix = Matrix4x4.TRS(
			Vector3.zero,
			Quaternion.identity,
			new Vector3(Screen.width / baseWidth, Screen.height / baseHeight, 1f));
		
		// タイマー.
		GUI.Label(
			new Rect(8f, 8f, 128f, 48f),
			new GUIContent(gameRuleCtrl.timeRemaining.ToString("0"), timerIcon),
			timerLabelStyle);
	}
}
