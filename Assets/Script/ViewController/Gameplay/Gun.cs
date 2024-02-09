using FrameworkDesign;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootingEditor2D {
    public class Gun : MonoBehaviour,IController
    {
        private Bullet mBullet;
        private GunInfo mGunInfo;
        void Start()
        {
            mBullet = transform.Find("Bullet").GetComponent<Bullet>();
            mGunInfo = this.GetSystem<IGunSystem>().CurrentGun;
        }


        public void Shoot() {
            if (mGunInfo.BulletCountInGun.Value > 0 && mGunInfo.State.Value == GunState.Idle) {
                Transform bullet = Instantiate(mBullet.transform, mBullet.transform.position, mBullet.transform.rotation);
                // 统一缩放值
                bullet.transform.localScale = mBullet.transform.lossyScale;
                bullet.gameObject.SetActive(true);
                this.SendCommand<ShootCommand>();
            }
        }

        public IArchitecture GetArchitecture()
        {
            return ShootingEditor2D.Instance;
        }

        private void OnDestroy()
        {
            mGunInfo = null;
        }
    }
}

