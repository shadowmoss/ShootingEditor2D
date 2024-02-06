using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace ShootingEditor2D {
    public class UIGamePass : MonoBehaviour
    {
        private Lazy<GUIStyle> mLabelStyle = new Lazy<GUIStyle>(() => new GUIStyle(GUI.skin.label)
        {
            // 字体大小
            fontSize = 80,
            // 居中
            alignment = TextAnchor.MiddleCenter
        });

        private Lazy<GUIStyle> mButtonStyle = new Lazy<GUIStyle>(() => new GUIStyle(GUI.skin.button)
        {
            // 字体大小
            fontSize = 40,
            alignment = TextAnchor.MiddleCenter
        });

        private void OnGUI()
        {
            int labelWidth = 400;
            int labelHeight = 100;
            Vector2 labelPosition = new Vector2(Screen.width - labelWidth,Screen.height-labelHeight) * 0.5f;
            Vector2 labelSize = new Vector2(labelWidth, labelHeight);
            Rect labelRect = new Rect(labelPosition, labelSize);
            GUI.Label(labelRect, "游戏通关", mLabelStyle.Value);

            int buttonWidth = 200;
            int buttonHeight = 100;

            Vector2 buttonPosition = new Vector2(Screen.width - buttonWidth, Screen.height - buttonHeight + 300) * 0.5f;
            Vector2 buttonSize = new Vector2(buttonWidth,buttonHeight);
            Rect buttonRect = new Rect(buttonPosition, buttonSize);

            if (GUI.Button(buttonRect, "回到首页", mButtonStyle.Value)) {
                SceneManager.LoadScene("GameStart");
            }
        }
    }
}

