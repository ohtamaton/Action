using UnityEngine;
using System.Collections;

public class TitleScenenCtrl : MonoBehaviour {
    //タイトル画面テクスチャ
    public Texture2D bgTexture;
    //タイトル画面サイズ
    public int Width = 1920;
    public int Height = 1080;
    //FPS
    public int FPS = 30;

    void OnGUI()
    {
        // スタイルを準備.
        GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);

        // 解像度対応
        GUI.matrix = Matrix4x4.TRS(
            Vector3.zero,
            Quaternion.identity,
            new Vector3(Screen.width / 854.0f, Screen.height / 480.0f, 1.0f));
        // タイトル画面テクスチャ表示
        GUI.DrawTexture(new Rect(0.0f, 0.0f, 854.0f, 480.0f), bgTexture);
        // スタートボタンを作成します。
        if (GUI.Button(new Rect(327, 290, 200, 54), "Start", buttonStyle))
        {
            // シーンの切り替え
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        }
    }
}
