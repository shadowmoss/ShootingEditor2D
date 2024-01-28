using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FrameworkDesign {
    // �¼�������
    public class TypeEventSystem : ITypeEventSystem
    {
        private Dictionary<Type,IRegistrations> mEventRegistrations = new Dictionary<Type,IRegistrations>();
        // �¼�����ע�᷽��API
        // T����Ϊ�����Ķ�Ӧ���¼�����,�������������¼�������д���
        public IUnRegister Register<T>(Action<T> onEvent)
        {
            Type type = typeof(T);
            IRegistrations eventRegistrations;
            // ���ҵ�ǰ�ֵ伯�ϵ����Ƿ����,Ŀ������¼�

            if (mEventRegistrations.TryGetValue(type, out eventRegistrations))
            {
                // ��ȡ����
                
            }   
            else {
                // ��ȡ���� �½�һ���������ܶ���
                eventRegistrations = new Registrations<T>();
                // ��Ŀ���¼����ͺͼ������ն�����������
                mEventRegistrations.Add(type, eventRegistrations);
            }
            // �������������ô����������ն���
            (eventRegistrations as Registrations<T>).OnEvent += onEvent;

            // ��Ӧ����һ������ע�������Ķ���,IUnRegister���͵���չ�������Ѱ�����Ӧ��ע������UnRegisterWhenGameObjectDestroyed()����
            return new TypeEventSystemUnRegister<T>()
            {
                OnEvent = onEvent,
                TypeEventSystem = this
            };
        }
        public void UnRegister<T>(Action<T> onEvent)
        {
            Type type = typeof(T);
            IRegistrations eventRegistrations;
            if (mEventRegistrations.TryGetValue(type, out eventRegistrations))
            {
                // ��ȡ���˴�ע������������Ŀ�귽������ȡ���˶�Ӧ���ܼ���ί�еĶ���
                (eventRegistrations as Registrations<T>).OnEvent -= onEvent;
            }
        }
        // ֱ��ͨ�����ͷ����¼�API
        public void Send<T>() where T : new()
        {
            T triggeredEvent = new T();
            Send<T>(triggeredEvent);
        }
        // �����¼��ķ���T���ͱ�ʶ��Ӧ���¼����ͣ��¼����Ͷ����д����Ҫ���ݸ����������Ĵ������
        // �û��Խ��¼���������¼�������
        public void Send<T>(T e)
        {
            Type type = typeof (T);
            IRegistrations eventRegistrations;

            if (mEventRegistrations.TryGetValue(type,out eventRegistrations)) {
                (eventRegistrations as Registrations<T>)?.OnEvent.Invoke(e);
            }
        }


    }

}
