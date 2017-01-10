/**
 * CharacterStatusGUI.cs
 * 
 * Characterのステータス用のGUI
 *
 * @author ys.ohta
 * @version 1.0
 * @date 2016/07/15
 */
using UnityEngine;
using System.Collections;

/**
 * CharacterStatusGUI
 */
public class CharacterStatusGUI : MonoBehaviour
{
//===========================================================
// 変数宣言
//===========================================================
    //---------------------------------------------------
    // public
    //---------------------------------------------------

    // 名前ラベル用のGUIスタイル
    public GUIStyle nameLabelStyle;

    // ライフバーのテクスチャ.
    public Texture backLifeBarTexture;
    public Texture frontLifeBarTexture;

    //---------------------------------------------------
    // private
    //---------------------------------------------------
    
    //None.

    //---------------------------------------------------
    // other
    //---------------------------------------------------

    //ベース横幅
    float baseWidth = 854f;

    //ベース縦幅
    float baseHeight = 480f;

    // プレイヤのステータス.
    CharacterStatus playerStatus;
    // プレイヤのステータスの位置のオフセット
    Vector2 playerStatusOffset = new Vector2(8f, 80f);

    // 名前.
    Rect nameRect = new Rect(0f, 0f, 120f, 24f);

    //ライフバーのオフセット位置
    float frontLifeBarOffsetX = 2f;

    //ライフバーのテクスチャの幅
    float lifeBarTextureWidth = 128f;

    //プレイヤライフバーのRect
    Rect playerLifeBarRect = new Rect(0f, 0f, 128f, 16f);

    //プレイヤライフバーの色
    Color playerFrontLifeBarColor = Color.green;

    // 敵ライフバーのRect
    Rect enemyLifeBarRect = new Rect(0f, 0f, 128f, 24f);
    
    // 敵ライフバーの色
    Color enemyFrontLifeBarColor = Color.red;

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
     * プレイヤーステータスの描画.
     * </summary>
     * @param
     * @return
     **/
    void DrawPlayerStatus()
    {
        float x = baseWidth - playerLifeBarRect.width - playerStatusOffset.x;
        float y = playerStatusOffset.y;
        DrawCharacterStatus(
            x, y,
            playerStatus,
            playerLifeBarRect,
            playerFrontLifeBarColor);
    }

    /**
     * <summary>
     * 敵ステータスの描画.
     * </summary>
     * @param
     * @return
     **/
    void DrawEnemyStatus()
    {
		if (playerStatus.lastAttackTarget != null)
        {
			CharacterStatus target_status = playerStatus.lastAttackTarget.GetComponent<CharacterStatus>();
            DrawCharacterStatus(
                (baseWidth - enemyLifeBarRect.width) / 2.0f, 0f,
				target_status,
                enemyLifeBarRect,
                enemyFrontLifeBarColor);
        }
    }

    /**
     * <summary>
     * ステータスの描画.
     * </summary>
     * @param
     * @return
     **/
    void DrawCharacterStatus(float x, float y, CharacterStatus status, Rect bar_rect, Color front_color)
    {
        // 名前.
        GUI.Label(
            new Rect(x, y, nameRect.width, nameRect.height),
			status.characterName,
            nameLabelStyle);
		
		float life_value = (float)status.HP / status.MaxHP;
		if(backLifeBarTexture != null)
		{
			// 背面ライフバー.
			y += nameRect.height;
			GUI.DrawTexture(new Rect(x, y, bar_rect.width, bar_rect.height), backLifeBarTexture);
		}

        // 前面ライフバー.
		if(frontLifeBarTexture != null)
		{
			float resize_front_bar_offset_x = frontLifeBarOffsetX * bar_rect.width / lifeBarTextureWidth;
			float front_bar_width = bar_rect.width - resize_front_bar_offset_x * 2;
			var gui_color = GUI.color;
			GUI.color = front_color;
			GUI.DrawTexture(new Rect(x + resize_front_bar_offset_x, y, front_bar_width * life_value, bar_rect.height), frontLifeBarTexture);
			GUI.color = gui_color;
		}
    }

    /**
     * <summary>
     * 初期化処理
     * </summary>
     * @param
     * @return
     **/
    void Awake()
    {
        PlayerCtrl player_ctrl = GameObject.FindObjectOfType(typeof(PlayerCtrl)) as PlayerCtrl;
        playerStatus = player_ctrl.GetComponent<CharacterStatus>();
    }

    /**
     * <summary>
     * 描画処理本体
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

        // ステータス.
        DrawPlayerStatus();
        DrawEnemyStatus();
    }
}
 