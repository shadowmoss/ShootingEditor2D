using FrameworkDesign;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ShootingEditor2D {
    public class UIController : MonoBehaviour,IController
    {
        private IStatSystem mStatSystem;
        private IPlayerModel mPlayerModel;
        private IGunSystem mGunSystem;

        public IArchitecture GetArchitecture()
        {
            return ShootingEditor2D.Instance;
        }

        private void Awake()
        {
            mStatSystem = this.GetSystem<IStatSystem>();
            mPlayerModel = this.GetModel<IPlayerModel>();
            mGunSystem = this.GetSystem<IGunSystem>();
        }

        private readonly Lazy<GUIStyle> mLabelStyle = new Lazy<GUIStyle>(() => new GUIStyle(GUI.skin.label) {
            fontSize = 40,
        });

        private void OnGUI()
        {
            GUI.Label(new Rect(10, 10, 300, 100), $"����{mPlayerModel.HP.Value}/3", mLabelStyle.Value);

            GUI.Label(new Rect(10,60,300,300),$"�ӵ�:{mGunSystem.CurrentGun.BulletCount.Value}",mLabelStyle.Value);

            GUI.Label(new Rect(Screen.width - 10 - 300, 10, 300, 100), $"��ɱ����:{mStatSystem.KillCount.Value}", mLabelStyle.Value);
        }

        private void OnDestroy()
        {
            mStatSystem = null;
            mPlayerModel = null;
            mGunSystem = null;
        }


    }
}

