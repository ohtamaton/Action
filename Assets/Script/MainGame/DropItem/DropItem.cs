/**
 * DropItem.cs
 * 
 * ドロップアイテム処理
 *
 * @author ys.ohta
 * @version 1.0
 * @date 2016/07/15
 */
using UnityEngine;
using System.Collections;

/**
 * DropItem
 */
public class DropItem : MonoBehaviour {
//===========================================================
// 変数宣言
//===========================================================
    //---------------------------------------------------
    // public
    //---------------------------------------------------

    //アイテム種別
    public enum ItemKind
	{
		Attack,
		Heal,
	};

    //アイテム種別
	public ItemKind kind;

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
     * 衝突時の処理
     * </summary>
     * @param other 衝突対象
     * @return
     */
    void OnTriggerEnter(Collider other)
	{	
		// Playerか判定
		if( other.tag == "Player" ){
			// アイテム取得
			CharacterStatus aStatus = other.GetComponent<CharacterStatus>();
			aStatus.GetItem(kind);
			// 取得したらアイテムを消す
			Destroy(gameObject);
		}
	}

	/**
     * <summary>
     * アイテム生成処理. 
     * 上に速度を与える.
     * </summary>
     * @param
     * @return
     */
	void Start () {
		Vector3 velocity = Random.insideUnitSphere * 2.0f + Vector3.up * 8.0f;
		GetComponent<Rigidbody>().velocity = velocity;
	}
	
}
