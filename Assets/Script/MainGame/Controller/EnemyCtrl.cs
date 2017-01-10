/**
 * EnemyCtrl.cs
 * 
 * 敵制御
 *
 * @author ys.ohta
 * @version 1.0
 * @date 2016/07/15
 */
using UnityEngine;
using System.Collections;
/**
 * EnemyCtrl.cs
 */
public class EnemyCtrl : MonoBehaviour
{
//===========================================================
// 変数宣言
//===========================================================
    //---------------------------------------------------
    // public
    //---------------------------------------------------

    //待機時間は２秒とする
    public float waitBaseTime = 2.0f;
    
    //移動範囲５メートル
    public float walkRange = 5.0f;
    
    //初期位置を保存しておく変数
    public Vector3 basePosition;

    //ドロップアイテムのプレハブ
    public GameObject[] dropItemPrefab;

    //ゲームルールコントローラ
    GameRuleCtrl gameRuleCtrl;

    //攻撃ヒットエフェクト
    public GameObject hitEffect;

    //---------------------------------------------------
    // private
    //---------------------------------------------------

    //None.

    //---------------------------------------------------
    // other
    //---------------------------------------------------

    // ステートの種類.
    enum State
    {
        Walking,	// 探索
        Chasing,	// 追跡
        Attacking,	// 攻撃
        Died,       // 死亡
    };
    
    //ステータス
    CharacterStatus status;

    //アニメーション
    CharaAnimation charaAnimation;

    //攻撃ターゲット
    Transform attackTarget;

    //移動制御
    CharacterMove characterMove;

    //現在の状態
    State state = State.Walking;

    //次の状態
    State nextState = State.Walking;

    //残り待機時間
    float waitTime;

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
        status = GetComponent<CharacterStatus>();
        charaAnimation = GetComponent<CharaAnimation>();
        characterMove = GetComponent<CharacterMove>();
        // 初期位置を保持
        basePosition = transform.position;
        // 待機時間
        waitTime = waitBaseTime;
        gameRuleCtrl = FindObjectOfType<GameRuleCtrl>();
    }

    /**
     * <summary>
     * 敵の状態ごとの処理
     * </summary>
     * @param
     * @return
     **/
    void Update()
    {
        switch (state)
        {
            case State.Walking:
                Walking();
                break;
            case State.Chasing:
                Chasing();
                break;
            case State.Attacking:
                Attacking();
                break;
        }

        if (state != nextState)
        {
            state = nextState;
            switch (state)
            {
                case State.Walking:
                    WalkStart();
                    break;
                case State.Chasing:
                    ChaseStart();
                    break;
                case State.Attacking:
                    AttackStart();
                    break;
                case State.Died:
                    Died();
                    break;
            }
        }
    }


    /**
     * <summary>
     * ステートを変更する.
     * </summary>
     * @param 次のステート
     * @return
     **/
    void ChangeState(State nextState)
    {
        this.nextState = nextState;
    }

    /**
     * <summary>
     * Walk処理のスタート処理
     * </summary>
     * @param 
     * @return
     **/
    void WalkStart()
    {
        StateStartCommon();
    }

    /**
     * <summary>
     * Walking処理
     * </summary>
     * @param 
     * @return
     **/
    void Walking()
    {
        // 待機時間がまだあったら
        if (waitTime > 0.0f)
        {
            // 待機時間を減らす
            waitTime -= Time.deltaTime;
            // 待機時間が無くなったら
            if (waitTime <= 0.0f)
            {
                // 範囲内の何処か
                Vector2 randomValue = Random.insideUnitCircle * walkRange;
                // 移動先の設定
                Vector3 destinationPosition = basePosition + new Vector3(randomValue.x, 0.0f, randomValue.y);
                //　目的地の指定.
                SendMessage("SetDestination", destinationPosition);
            }
        }
        else
        {
            // 目的地へ到着
            if (characterMove.Arrived())
            {
                // 待機状態へ
                waitTime = Random.Range(waitBaseTime, waitBaseTime * 2.0f);
            }
            // ターゲットを発見したら追跡
            if (attackTarget)
            {
                ChangeState(State.Chasing);
            }
        }
    }

    /**
     * <summary>
     * 追跡処理のスタート処理
     * </summary>
     * @param 
     * @return
     **/
    void ChaseStart()
    {
        StateStartCommon();
    }

    /**
     * <summary>
     * 追跡処理
     * </summary>
     * @param 
     * @return
     **/
    void Chasing()
    {
        // 移動先をプレイヤーに設定
        SendMessage("SetDestination", attackTarget.position);
        // 2m以内に近づいたら攻撃
        if (Vector3.Distance(attackTarget.position, transform.position) <= 2.0f)
        {
            ChangeState(State.Attacking);
        }
    }

    /**
     * <summary>
     * 攻撃処理のスタート処理
     * </summary>
     * @param 
     * @return
     **/
    void AttackStart()
    {
        StateStartCommon();
        status.attacking = true;

        // 敵の方向に振り向かせる.
        Vector3 targetDirection = (attackTarget.position - transform.position).normalized;
        SendMessage("SetDirection", targetDirection);

        // 移動を止める.
        SendMessage("StopMove");
    }

    /**
     * <summary>
     * 攻撃処理
     * </summary>
     * @param 
     * @return
     **/
    void Attacking()
    {
        if (charaAnimation.IsAttacked())
            ChangeState(State.Walking);
        // 待機時間を再設定
        waitTime = Random.Range(waitBaseTime, waitBaseTime * 2.0f);
        // ターゲットをリセットする
        attackTarget = null;
    }

    /**
     * <summary>
     * アイテムドロップ処理
     * </summary>
     * @param 
     * @return
     **/
    void dropItem()
    {
        if (dropItemPrefab.Length == 0) { return; }
        GameObject dropItem = dropItemPrefab[Random.Range(0, dropItemPrefab.Length)];
        Instantiate(dropItem, transform.position, Quaternion.identity);
    }

    /**
     * <summary>
     * 死亡処理
     * </summary>
     * @param 
     * @return
     **/
    void Died()
    {
        status.died = true;
        if(gameObject.tag == "Boss")
        {
            gameRuleCtrl.GameClear();
        }
        dropItem();
        Destroy(gameObject);
    }

    /**
     * <summary>
     * ダメージ処理
     * </summary>
     * @param 
     * @return
     **/
    void Damage(AttackArea.AttackInfo attackInfo)
    {
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity) as GameObject;
        effect.transform.localPosition = transform.position + new Vector3(0.0f, 0.5f, 0.0f);
        Destroy(effect, 0.3f);

        status.HP -= attackInfo.attackPower;
        if (status.HP <= 0)
        {
            status.HP = 0;
            // 体力０なので死亡
            ChangeState(State.Died);
        }
    }

    /**
     * <summary>
     * 各ステートのスタート共通処理
     * </summary>
     * @param 
     * @return
     **/
    void StateStartCommon()
    {
        status.attacking = false;
        status.died = false;
    }

    /**
     * <summary>
     * 攻撃対象を設定する
     * </summary>
     * @param 攻撃対象
     * @return
     **/
    public void SetAttackTarget(Transform target)
    {
        attackTarget = target;
    }
}
