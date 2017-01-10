/**
 * FollowCamera.cs
 * 
 * 注視対象にカメラの焦点を合わせる. 
 *
 * @author ys.ohta
 * @version 1.0
 * @date 2016/08/XX
 */
using UnityEngine;
using System.Collections;

/**
 * FollowCamera
 */
public class FollowCamera : MonoBehaviour {
//===========================================================
// 変数宣言
//===========================================================
    //---------------------------------------------------
    // public
    //---------------------------------------------------

    //カメラと注視対象の距離
	public float distance = 5.0f;

    //注視対象の位置のオフセット
	public Vector3 offset = Vector3.zero;

    //---------------------------------------------------
    // private
    //---------------------------------------------------

    //回転後の垂直方向の角度
    private float horizontalAngle;

    //画面の横幅分スライドさせたときの回転幅
    private float rotAngle;

    //回転後の水平方向の角度
    private float verticalAngle;

    //マウス操作によるカメラコントローラ
    [SerializeField] private MouseCameraController controller;

    //マウスマネージャ
    [SerializeField] private MouseManager inputManager;

    //注視対象
    [SerializeField] private Transform lookTarget;

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
     * マウスのスライドでlookTargetにカメラの焦点を合わせた状態でカメラの角度を変更する.
     * </summary>
     * @param
     * @return 
     **/
    void LateUpdate () {
        //ドラッグ入力でカメラのアングルを更新する.
        horizontalAngle = controller.horizontalAngle;
        rotAngle = controller.rotAngle;
        verticalAngle = controller.verticalAngle;

		//カメラを位置と回転を更新する.
		if (lookTarget != null) {
			Vector3 lookPosition = lookTarget.position + offset;
			//注視対象からの相対位置を求める.
			Vector3 relativePos = Quaternion.Euler(verticalAngle,horizontalAngle,0) *  new Vector3(0,0,-distance);
			
			//注視対象の位置にオフセット加算した位置に移動させる.
			transform.position = lookPosition + relativePos ;
			
			//注視対象を注視させる.
			transform.LookAt(lookPosition);
			
			//障害物を避ける.
			RaycastHit hitInfo;
			if (Physics.Linecast(lookPosition,transform.position,out hitInfo,1<<LayerMask.NameToLayer("Ground")))
				transform.position = hitInfo.point;
		}
	}
}
