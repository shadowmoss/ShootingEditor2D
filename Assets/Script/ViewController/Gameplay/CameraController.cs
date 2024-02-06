using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ShootingEditor2D {
    public class CameraController : MonoBehaviour
    {
        private Transform mPlayerTrans;

        private Vector3 TargetPos;

        private float mMinX = -5;
        private float mMaxX = 5;
        private float mMinY = -5;
        private float mMaxY = 5;

        private void Update()
        {

            if (!mPlayerTrans)
            {
                GameObject playerGameObj = GameObject.FindWithTag("Player");
                if (playerGameObj)
                {
                    mPlayerTrans = playerGameObj.transform;
                }
                else
                {
                    return;
                }
            }

            var isRight = Mathf.Sign(mPlayerTrans.localScale.x);

            Vector3 playerPos = mPlayerTrans.position;


            TargetPos.x = playerPos.x + 3 * Mathf.Sign(mPlayerTrans.localScale.x);
            TargetPos.y = playerPos.y + 2;
            TargetPos.z = -10;

            var smoothSpeed = 5;
            // ����һ��ƽ������
            var position = transform.position;
            position = Vector3.Lerp(position, new Vector3(TargetPos.x, TargetPos.y, position.z), smoothSpeed * Time.deltaTime);


            // ��������ƶ�λ�ù̶���һ��������
            transform.position = new Vector3(Mathf.Clamp(position.x, mMinX, mMaxX), 
                                            Mathf.Clamp(position.y, mMinY, mMaxY),position.z);
        }
    }
}

