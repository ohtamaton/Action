/**
 * AttackArea.cs
 * 
 * 攻撃エリア 
 *
 * @author ys.ohta
 * @version 1.0
 * @date 2016/07/15
 */
using UnityEngine;
using System.Collections;

/**
 * AttackArea
 */
public class AttackArea : MonoBehaviour
{
//===========================================================
// 変数宣言
//===========================================================
    //---------------------------------------------------
    // public
    //---------------------------------------------------

    //攻撃者情報
    public class AttackInfo
    {
        //攻撃力.
        public int attackPower;

        //攻撃者.
        public Transform attacker; 
    }

    //---------------------------------------------------
    // private
    //---------------------------------------------------

    //攻撃側のキャラクタステータス
    [SerializeField] private CharacterStatus status;

    //攻撃側のCollider
    private Collider collider;

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
        collider = GetComponent<Collider>();
    }

    /**
     * <summary>
     * 攻撃情報を取得する.
     * </summary>
     * @param
     * @return 攻撃情報
     **/
    AttackInfo GetAttackInfo()
    {
        AttackInfo attackInfo = new AttackInfo();
        // 攻撃力の計算.
        attackInfo.attackPower = status.Power;
        // 攻撃強化中
        if (status.powerBoost)
            attackInfo.attackPower += attackInfo.attackPower;

        attackInfo.attacker = transform.root;

        return attackInfo;
    }

    /**
     * <summary>
     * 攻撃がColliderに衝突したときの処理
     * </summary>
     * @param other 衝突したCollider
     * @return
     **/
    void OnTriggerEnter(Collider other)
    {
        // 攻撃が当たった相手にDamage Messageをおくる.
        other.SendMessage("Damage", GetAttackInfo());
        // 攻撃した対象を保存.
        status.lastAttackTarget = other.transform.root.gameObject;
    }


    /**
     * <summary>
     * 攻撃判定を有効にする.
     * </summary>
     * @param
     * @return
     **/
    void OnAttack()
    {
        collider.enabled = true;
    }


    /**
     * <summary>
     * 攻撃判定を無効にする.
     * </summary>
     * @param
     * @return
     **/
    void OnAttackTermination()
    {
        collider.enabled = false;
    }
}
