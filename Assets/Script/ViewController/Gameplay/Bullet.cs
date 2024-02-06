using FrameworkDesign;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootingEditor2D {
    public class Bullet : MonoBehaviour,IController
    {
        private Rigidbody2D mRigidbody2D;

        private void Awake()
        {
            mRigidbody2D = GetComponent<Rigidbody2D>();

            Destroy(gameObject,5);
        }

        private void Start()
        {
            mRigidbody2D.velocity = Vector2.right * 10 * Mathf.Sign(transform.localScale.x);
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.CompareTag("Enemy")){
                // 发送一个击杀指令
                this.SendCommand<KillEnemyCommand>();
                Destroy(collision.gameObject);

                Destroy(gameObject);
            }
        }

        public IArchitecture GetArchitecture()
        {
            return ShootingEditor2D.Instance;
        }
    }
}

