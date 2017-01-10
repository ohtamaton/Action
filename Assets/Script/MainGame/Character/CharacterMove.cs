/**
 * CharacterMove.cs
 * 
 * Characterの移動制御
 *
 * @author ys.ohta
 * @version 1.0
 * @date 2016/07/15
 */
using UnityEngine;
using System.Collections;

/**
 * CharacterMove
 */
public class CharacterMove : MonoBehaviour {
//===========================================================
// 変数宣言
//===========================================================
    //---------------------------------------------------
    // public
    //---------------------------------------------------

    // 目的地.
	public Vector3 destination; 
	
	// 移動速度.
	public float walkSpeed = 6.0f;
	
	// 回転速度.
	public float rotationSpeed = 360.0f;

    // 到着したか（到着した true/到着していない false)
    public bool arrived = false;

    //---------------------------------------------------
    // private
    //---------------------------------------------------

    //None.

    //---------------------------------------------------
    // other
    //---------------------------------------------------

    // 重力値.
    const float GravityPower = 9.8f; 

	//　目的地についたとみなす停止距離.
	const float StoppingDistance = 0.6f;
	
	// 現在の移動速度.
	Vector3 velocity = Vector3.zero; 

	// キャラクターコントローラー.
	CharacterController characterController; 
	
	// 向きを強制的に指示するか.
	bool forceRotate = false;
	
	// 強制的に向かせたい方向.
	Vector3 forceRotateDirection;

//===========================================================
// 関数宣言
//===========================================================
    //---------------------------------------------------
    // public
    //---------------------------------------------------

    /**
     * <summary>
     * 目的地を設定する.
     * </summary>
     * @param destination 目的地.
     * @return
     */
    public void SetDestination(Vector3 destination)
	{
		arrived = false;
		this.destination = destination;
	}

    /**
     * <summary>
     * 指定した向きを向かせる.
     * </summary>
     * @param direction 向かせたい方向.
     * @return
     */
    public void SetDirection(Vector3 direction)
	{
		forceRotateDirection = direction;
		forceRotateDirection.y = 0;
		forceRotateDirection.Normalize();
		forceRotate = true;
	}

    /**
     * <summary>
     * 移動を停止
     * </summary>
     * @param
     * @return
     */
    public void StopMove()
	{
        // 現在地点を目的地にしてしまう.
        destination = transform.position; 
	}


    /**
     * <summary>
     * 目的地に到着したかを調べる.
     * </summary>
     * @param
     * @return true　到着した/ false 到着していない.
     */
    public bool Arrived()
	{
		return arrived;
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
     * 初期化処理
     * </summary>
     * @param
     * @return
     */
	void Start () {
		characterController = GetComponent<CharacterController>();
		destination = transform.position;
	}

    /**
     * <summary>
     * Characterの移動処理
     * </summary>
     * @param
     * @return
     */
    void Update () {
		
		// 移動速度velocityを更新する
		if (characterController.isGrounded) {
			//　水平面での移動を考えるのでXZのみ扱う.
			Vector3 destinationXZ = destination; 
			destinationXZ.y = transform.position.y;// 高さを目的地と現在地を同じにしておく.
			
			//********* ここからXZのみで考える. ********
			// 目的地までの距離と方角を求める.
			Vector3 direction = (destinationXZ - transform.position).normalized;
			float distance = Vector3.Distance(transform.position,destinationXZ);
			
			// 現在の速度を退避.
			Vector3 currentVelocity = velocity;
			
			//　目的地にちかづいたら到着..
			if (arrived || distance < StoppingDistance)
				arrived = true;
			
			
			// 移動速度を求める.
			if (arrived)
				velocity = Vector3.zero;
			else 
				velocity = direction * walkSpeed;
			
			
			// スムーズに補間.
			velocity = Vector3.Lerp(currentVelocity, velocity,Mathf.Min (Time.deltaTime * 5.0f ,1.0f));
			velocity.y = 0;
			
			
			if (!forceRotate) {
				// 向きを行きたい方向に向ける.
				if (velocity.magnitude > 0.1f && !arrived) { // 移動してなかったら向きは更新しない.
					Quaternion characterTargetRotation = Quaternion.LookRotation(direction);
					transform.rotation = Quaternion.RotateTowards(transform.rotation,characterTargetRotation,rotationSpeed * Time.deltaTime);
				}
			} else {
				// 強制向き指定.
				Quaternion characterTargetRotation = Quaternion.LookRotation(forceRotateDirection);
				transform.rotation = Quaternion.RotateTowards(transform.rotation,characterTargetRotation,rotationSpeed * Time.deltaTime);
			}
		}
		
		// 重力.
		velocity += Vector3.down * GravityPower * Time.deltaTime;
		
		// 接地していたら思いっきり地面に押し付ける.
		// (UnityのCharactorControllerの特性のため）
		Vector3 snapGround = Vector3.zero;
		if (characterController.isGrounded)
			snapGround = Vector3.down;
		
		// CharacterControllerを使って動かす.
		characterController.Move(velocity * Time.deltaTime+snapGround);
		
		if (characterController.velocity.magnitude < 0.1f)
			arrived = true;
		
		// 強制的に向きを変えるを解除.
		if (forceRotate && Vector3.Dot(transform.forward,forceRotateDirection) > 0.99f)
			forceRotate = false;
		
		
	}

    /**
     * <summary>
     * 指定した位置に移動する
     * </summary>
     * @param pos 移動先の位置
     * @return 
     */
    void setPosition(Vector3 pos)
    {
        transform.Translate(new Vector3(pos.x-transform.position.x, 0 - transform.position.y, pos.z + 10 - transform.position.z));
        transform.GetComponent<CharacterMove>().arrived = true;
    }

}
