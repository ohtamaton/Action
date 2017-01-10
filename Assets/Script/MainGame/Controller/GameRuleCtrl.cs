/**
 * GameRuleCtrl.cs
 * 
 * ゲームルール制御
 *
 * @author ys.ohta
 * @version 1.0
 * @date 2016/07/15
 */
using UnityEngine;
using System.Collections;
/**
 * GameRuleCtrl
 */
public class GameRuleCtrl : MonoBehaviour {
//===========================================================
// 変数宣言
//===========================================================
    //---------------------------------------------------
    // public
    //---------------------------------------------------

    // 残り時間
	public float timeRemaining = 5.0f * 60.0f;
    
    // ゲームオーバーフラグ
    public bool gameOver = false;
    
    // ゲームクリア
    public bool gameClear = false;
    
    // シーン移行時間
    public float sceneChangeTime = 3.0f;

    //---------------------------------------------------
    // private
    //---------------------------------------------------

    //None.

    //===========================================================
    // 関数宣言
    //===========================================================
    //---------------------------------------------------
    // public
    //---------------------------------------------------

    /**
     * <summary>
     * ゲームオーバフラグ設定
     * </summary>
     * @param
     * @return
     **/
    public void GameOver()
	{
		gameOver = true;
        Debug.Log("GameOver");
	}

    /**
     * <summary>
     * ゲームクリアフラグ設定
     * </summary>
     * @param
     * @return
     **/
    public void GameClear()
	{
		gameClear = true;
        Debug.Log("GameClear");
    }

    //---------------------------------------------------
    // private
    //---------------------------------------------------

    //None.

    //---------------------------------------------------
    // other
    //---------------------------------------------------

    /**
     * <summary>
     * ゲームルール制御
     * </summary>
     * @param
     * @return
     **/
    void Update()
	{
        // ゲーム終了条件成立後、シーン遷移
        if (gameOver || gameClear)
        {
            sceneChangeTime -= Time.deltaTime;
            if (sceneChangeTime <= 0.0f)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("TitleScene");
            }
            return;
        }

        //タイマ処理
        timeRemaining -= Time.deltaTime;
		
        // 残り時間が無くなったらゲームオーバー
		if(timeRemaining<= 0.0f ){
			GameOver();
		}
	}
	
	
}
