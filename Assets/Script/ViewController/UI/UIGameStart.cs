using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace ShootingEditor2D {
    public class UIGameStart : MonoBehaviour
    {
        private readonly Lazy<GUIStyle> mLabelStyle = new Lazy<GUIStyle>(() => new GUIStyle(GUI.skin.label) {
            fontSize = 60,
            alignment = TextAnchor.MiddleCenter
        });
        private readonly Lazy<GUIStyle> mButtonStyle = new Lazy<GUIStyle>(() => new GUIStyle(GUI.skin.button) { 
            fontSize = 40,
            alignment = TextAnchor.MiddleCenter
        });
        // IMGUI以屏幕左上角为原点,横向为x轴,纵向为y轴
        private void OnGUI()
        {
            //var labelWidth = 600;
            //var labelHeight = 100;
            //var labelPosition = new Vector2(Screen.width-labelWidth,Screen.height - labelHeight) * 0.5f;

            //var labelSize = new Vector2(labelWidth, labelHeight);
            //var labelRect = new Rect(labelPosition,labelSize);

            var labelRect = RectHelper.RectForAnchorCenter(Screen.width * 0.5f,Screen.height * 0.5f,600,100);

            GUI.Label(labelRect, "ShootingEditor2D", mLabelStyle.Value);

            //var buttonWidth = 300;
            //var buttonHeight = 100;
            //var buttonPosition = new Vector2(Screen.width - buttonWidth, Screen.height - buttonHeight + 300) * 0.5f;

            //var buttonSize = new Vector2(buttonWidth, buttonHeight);
            //var buttonRect = new Rect(buttonPosition, buttonSize);
            var buttonRect = RectHelper.RectForAnchorCenter(Screen.width * 0.5f,Screen.height * 0.5f + 150,300,100);

            if (GUI.Button(buttonRect, "开始游戏", mButtonStyle.Value)) {
                SceneManager.LoadScene("SampleScene");
            }
        }
    }

}
