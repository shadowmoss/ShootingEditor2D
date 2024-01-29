using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootingEditor2D {
    public class Gun : MonoBehaviour
    {
        private Bullet mBullet;
        void Start()
        {
            mBullet = transform.Find("Bullet").GetComponent<Bullet>();
        }


        public void Shoot() {
            Transform bullet = Instantiate(mBullet.transform,mBullet.transform.position,mBullet.transform.rotation);
            // 统一缩放值
            bullet.transform.localScale = mBullet.transform.lossyScale;
            bullet.gameObject.SetActive(true);
        }
    }
}

