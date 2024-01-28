using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ShootingEditor2D {
    public class Trigger2DCheck : MonoBehaviour
    {
        // ����һ��LayerMask���ã��������õ�ǰ��������Ҫ����Щ�㼶��������ײ���Ӵ���
        public LayerMask targetLayers;
        // �Ӵ�����
        public int EnterCount = 0;
        public bool Triggerd
        {
            get {
                return EnterCount > 0;
            }
        }
        /// <summary>
        /// ��д����ײ���¼�����
        /// </summary>
        /// <param name="collision">�Ӵ������������ײ��</param>
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (IsInLayerMask(collision.gameObject, targetLayers)) {
                EnterCount++;
            }
            
        }
        /// <summary>
        /// ��ײ���뿪�¼�����
        /// </summary>
        /// <param name="collision">�뿪���������ײ������</param>
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (IsInLayerMask(collision.gameObject, targetLayers)) {
                EnterCount--;
            }
        }

        // �ж�һ�������Ƿ�����������õ�LayerMask��
        bool IsInLayerMask(GameObject obj,LayerMask layerMask) {
            // ����Layer��ֵ����λ�ƻ�����������Maskֵ
            LayerMask objLayerMask = 1 << obj.layer;
            Debug.Log("���в㼶�жϵ�Layerֵ:"+obj.layer);
            Debug.Log("���õĲ㼶Layerֵ:" + layerMask.value);
            // ��������õ���Ҫ����Layer�㼶��ֵ��Ӧ���ˣ�˵������˵��ȷʵ��ײ���ˡ�
            return (layerMask.value & objLayerMask) > 0;
        }
    }
}

