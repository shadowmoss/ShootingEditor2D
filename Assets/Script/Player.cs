using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ShootingEditor2D {
    public class Player : MonoBehaviour
    {
        private Rigidbody2D mRigidbody2D;

        private Trigger2DCheck mGroundCheck;

        [SerializeField]
        public int speed = 5;

        private void Awake()
        {
            mRigidbody2D = GetComponent<Rigidbody2D>();

            mGroundCheck = transform.Find("GroundCheck").GetComponent<Trigger2DCheck>();
        }
        private bool mJumpPressed;
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.K)) { 
                mJumpPressed = true;
            }
        }
        private void FixedUpdate()
        {
            float horizontalMovement = Input.GetAxis("Horizontal");
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

