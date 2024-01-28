using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FrameworkDesign {
    // ����ע��ʵ����
    public class TypeEventSystemUnRegister<T> : IUnRegister
    {
        // �����¼�������
        public ITypeEventSystem TypeEventSystem { get; set; }
        // ��������ί�ж���,���¼���������Ҫ��ʲô����
        public Action<T> OnEvent { get; set; }
        // ����ע������
        public void UnRegister()
        {
            TypeEventSystem.UnRegister(OnEvent);
            TypeEventSystem = null;
            OnEvent = null;
        }
    }

}
