using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FrameworkDesign {
    // ����ע��������ӿ�
    public interface IUnRegister
    {
        void UnRegister();
    }

    public static class UnRegisterExtension
    {
        public static void UnRegisterWhenGameObjectDestroyed(this IUnRegister unRegister, GameObject gameObject) {
            var trigger = gameObject.GetComponent<UnRegisterOnDestroyTrigger>();
            // �����GameObject�����ڼ���ע���������
            // Ϊ�����һ��
            if (!trigger) { 
               trigger = gameObject.AddComponent<UnRegisterOnDestroyTrigger>();
            }
            // ����ǰ����ע������������ע���������HashSet���ϵ���
            trigger.AddUnRegister(unRegister);
        }
    }

}

