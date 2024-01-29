using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ShootingEditor2D {
    public class Trigger2DCheck : MonoBehaviour
    {
        // 设置一个LayerMask引用，用于配置当前触发器想要和哪些层级的物体碰撞器接触。
        public LayerMask targetLayers;
        // 接触次数
        public int EnterCount = 0;
        public bool Triggerd
        {
            get {
                return EnterCount > 0;
            }
        }
        /// <summary>
        /// 重写的碰撞器事件方法
        /// </summary>
        /// <param name="collision">接触到的物体的碰撞器</param>
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (IsInLayerMask(collision.gameObject, targetLayers)) {
                EnterCount++;
            }
            
        }
        /// <summary>
        /// 碰撞器离开事件方法
        /// </summary>
        /// <param name="collision">离开的物体的碰撞器对象</param>
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (IsInLayerMask(collision.gameObject, targetLayers)) {
                EnterCount--;
            }
        }

        // 判断一个物体是否存在于我配置的LayerMask里
        bool IsInLayerMask(GameObject obj,LayerMask layerMask) {
            // 根据Layer数值进行位移获得用于运算的Mask值
            LayerMask objLayerMask = 1 << obj.layer;
            Debug.Log("进行层级判断的Layer值:"+obj.layer);
            Debug.Log("配置的层级Layer值:" + layerMask.value);
            // 如果与配置的需要检测的Layer层级的值对应上了，说明可以说明确实碰撞到了。
            return (layerMask.value & objLayerMask) > 0;
        }
    }
}

