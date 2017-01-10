/**
 * EnemyGeneratorCtrl.cs
 * 
 * 敵ゲジェレータの制御
 *
 * @author ys.ohta
 * @version 1.0
 * @date 2016/07/15
 */
using UnityEngine;
using System.Collections;
/**
 * EnemyGeneratorCtrl
 */
public class EnemyGeneratorCtrl : MonoBehaviour {
//===========================================================
// 変数宣言
//===========================================================
    //---------------------------------------------------
    // public
    //---------------------------------------------------

    // 生まれてくる敵プレハブ
	public GameObject enemyPrefab;
    
    // アクティブの最大数
    public int maxEnemy = 2;

    //---------------------------------------------------
    // private
    //---------------------------------------------------

    //None.

    //---------------------------------------------------
    // other
    //---------------------------------------------------

    // 敵を格納
    GameObject[] existEnemys;
	
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
		// 配列確保
		existEnemys = new GameObject[maxEnemy];
		StartCoroutine(Exec());
	}

    /**
     * <summary>
     * 敵生成コルーチン
     * </summary>
     * @param
     * @return
     **/
    IEnumerator Exec()
	{
		while(true){ 
			Generate();
			yield return new WaitForSeconds( 3.0f );
		}
	}

    /**
     * <summary>
     * 敵生成処理
     * </summary>
     * @param
     * @return
     **/
    void Generate()
	{
		for(int enemyCount = 0; enemyCount < existEnemys.Length; ++ enemyCount)
		{
			if( existEnemys[enemyCount] == null ){
				// 敵作成
				existEnemys[enemyCount] = Instantiate(enemyPrefab,transform.position,transform.rotation) as GameObject;
				return;
			}
		}
	}
	
}
