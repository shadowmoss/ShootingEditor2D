using ShootingEditor2D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Trigger2DCheck mWallCheck;
    private Trigger2DCheck mFallCheck;
    private Trigger2DCheck mGroundCheck;

    private Rigidbody2D mRigidbody2D;

    private void Awake()
    {
        mWallCheck = transform.Find("WallCheck").GetComponent<Trigger2DCheck>();
        mFallCheck = transform.Find("FallCheck").GetComponent<Trigger2DCheck>();
        mGroundCheck = transform.Find("GroundCheck").GetComponent<Trigger2DCheck>();

        mRigidbody2D = GetComponent<Rigidbody2D>();
    }

    // �����ƶ��߼�
    private void FixedUpdate()
    {
        var scaleX = transform.localScale.x;
        // ��ǰ���˴���,���䴥��
        if (mGroundCheck.Triggerd && mFallCheck.Triggerd && !mWallCheck.Triggerd)
        {
            mRigidbody2D.velocity = new Vector2(scaleX * 10, mRigidbody2D.velocity.y);
        }
        else
        {
            // ��ת
            var localScale = transform.localScale;
            localScale.x = -scaleX;
            transform.localScale = localScale;
        }
    }
}
