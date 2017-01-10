/**
 * PlayerCtrl.cs
 * 
 * プレイヤ制御
 *
 * @author ys.ohta
 * @version 1.0
 * @date 2016/07/15
 */
using UnityEngine;
using System.Collections;
/**
 * PlayerCtrl.cs
 */
public class PlayerCtrl : MonoBehaviour
{
//===========================================================
// 変数宣言
//===========================================================
    //---------------------------------------------------
    // public
    //---------------------------------------------------
     
    public float attackRange = 1.5f;

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
        Walking,
        Attacking,
        Died,
    };

    //RayCastの最大距離
    const float RayCastMaxDistance = 100.0f;

    //ステータス
    CharacterStatus status;

    //アニメーション
    CharaAnimation charaAnimation;

    //攻撃ターゲット
    Transform attackTarget;

    //マウスマネージャ
    MouseManager inputManager;

    //現在の状態
    State state = State.Walking;        

    //次の状態
    State nextState = State.Walking;

    //ゲームルールコントローラ
    GameRuleCtrl gameRuleCtrl;

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
        inputManager = FindObjectOfType<MouseManager>();
        gameRuleCtrl = FindObjectOfType<GameRuleCtrl>();
    }

    /**
     * <summary>
     * プレイヤ状態ごとの処理
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
        if (inputManager.Clicked())
        {
            // RayCastで対象物を調べる.
            Ray ray = Camera.main.ScreenPointToRay(inputManager.GetCursorPosition());
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, RayCastMaxDistance, (1 << LayerMask.NameToLayer("Ground")) | (1 << LayerMask.NameToLayer("EnemyHit"))))
            {
                // 地面がクリックされた.
                if (hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
                    SendMessage("SetDestination", hitInfo.point);
                // 敵がクリックされた.
                if (hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("EnemyHit"))
                {
                    // 水平距離をチェックして攻撃するか決める.
                    Vector3 hitPoint = hitInfo.point;
                    hitPoint.y = transform.position.y;
                    float distance = Vector3.Distance(hitPoint, transform.position);
                    if (distance < attackRange)
                    {
                        // 攻撃.
                        attackTarget = hitInfo.collider.transform;
                        ChangeState(State.Attacking);
                    }
                    else
                    {
                        SendMessage("SetDestination", hitInfo.point);
                    }
                }
            }
        }
    }


    /**
     * <summary>
     * 攻撃のスタート処理
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
        gameRuleCtrl.GameOver();
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
        status.HP -= attackInfo.attackPower;
        if (status.HP <= 0)
        {
            status.HP = 0;
            // 体力０なので死亡ステートへ.
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
}
