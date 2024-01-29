using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ShootingEditor2D {
    public class CameraController : MonoBehaviour
    {
        private Transform mPlayerTrans;
        // Update is called once per frame

        private void Start()
        {
           
        }
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

            Vector3 cameraPos = transform.position;
            Vector3 playerPos = mPlayerTrans.position;

            cameraPos.x = playerPos.x + 2;
            cameraPos.y = playerPos.y + 2;

            transform.position = cameraPos;
        }
    }
}

