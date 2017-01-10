/**
 * CharaAnimation.cs
 * 
 * Characterのアニメーション制御
 *
 * @author ys.ohta
 * @version 1.0
 * @date 2016/07/15
 */
using UnityEngine;

/**
 * CharaAnimation
 */
public class CharaAnimation : MonoBehaviour
{
    //Animator
    private Animator animator;

    //Character Status
	private CharacterStatus status;

    //移動前の位置
	private Vector3 prePosition;

    //CharacterがDown状態かどうか
	private bool isDown = false;

    //Characterが攻撃状態かどうか
	private bool attacked = false;

    /**
     * <summary>
     * 攻撃状態かどうかを返す
     * </summary>
     * @param
     * @return attacked 攻撃状態かどうか
     **/
    public bool IsAttacked()
	{
		return attacked;
	}

    /**
     * <summary>
     * 攻撃が完了したときにattackedをtrueに設定
     * </summary>
     * @param
     * @return
     **/
    void EndAttack()
	{
		attacked = true;
	}

    /**
     * <summary>
     * 初期化処理
     * </summary>
     * @param
     * @return
     **/
    void Start ()
	{
		animator = GetComponent<Animator>();
		status = GetComponent<CharacterStatus>();
		
		prePosition = transform.position;
	}

    /**
     * <summary>
     * Animatorの各種情報更新
     * </summary>
     * @param
     * @return
     **/
    void Update ()
	{
        //移動距離を計算
        Vector3 delta_position = transform.position - prePosition;

        //移動速度を計算し、Speedに設定
		animator.SetFloat("Speed", delta_position.magnitude / Time.deltaTime);
		
        //Character Statusが攻撃状態ではなく、攻撃完了時、attackedをfalseに設定
		if(attacked && !status.attacking)
		{
			attacked = false;
		}
        //攻撃中の場合にAttackingをtrueに設定
		animator.SetBool("Attacking", (!attacked && status.attacking));
		
        //CharacterがDownしていなくて、死亡状態の場合
		if(!isDown && status.died)
		{
			isDown = true;
			animator.SetTrigger("Down");
		}		
		prePosition = transform.position;
	}
}