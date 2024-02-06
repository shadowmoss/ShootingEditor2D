using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ShootingEditor2D {
    public class Player : MonoBehaviour
    {
        private Rigidbody2D mRigidbody2D;

        private Trigger2DCheck mGroundCheck;

        private Gun mGun;

        [SerializeField]
        public int speed = 5;

        private void Awake()
        {
            mRigidbody2D = GetComponent<Rigidbody2D>();

            mGroundCheck = transform.Find("GroundCheck").GetComponent<Trigger2DCheck>();

            mGun = transform.Find("Gun").GetComponent<Gun>();
        }
        private bool mJumpPressed;
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.K)) { 
                mJumpPressed = true;
            }
            if (Input.GetKeyDown(KeyCode.J)) {
                mGun.Shoot();
            }
        }
        private void FixedUpdate()
        {
            float horizontalMovement = Input.GetAxis("Horizontal");
            // 输入的horizontalMovement与transform.localScale.x,不同向时,就反转角色朝向
            if (horizontalMovement < 0 && transform.localScale.x > 0 || 
                horizontalMovement > 0 && transform.localScale.x < 0) {
                var localScale = transform.localScale;
                localScale.x = -localScale.x;
                transform.localScale = localScale;
            }
            // 移动代码
            mRigidbody2D.velocity = new Vector2(horizontalMovement * speed, mRigidbody2D.velocity.y);
            bool grounded = mGroundCheck.Triggerd;
            if(mJumpPressed && grounded)
            {
                mJumpPressed = false;
                mRigidbody2D.velocity = new Vector2(mRigidbody2D.velocity.x,5);
            }
            
        }
    }
}

