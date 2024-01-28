using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrameworkDesign {
    // ϵͳ����������һЩ������ʱ�洢״̬��صĲ�����
    // �����֮���ͨ�Ź�ϵ
    // ���ֲ�ͨ��Command��System���Model�����ͨ��
    // System����Model��ͨ��ί�л����¼�֪ͨ���ֲ�
    // ���ֲ㲻��Utility��ֱ��ͨ��
    // ���ֲ��ͨ��ֱ�ӻ�ȡSystem���Model����󣬻�ȡ��Ӧ��ֵ״̬
    // 
    public interface ISystem : IBelongToArchitecture, ICanSetArchitecture,ICanGetUtility,ICanGetModel,ICanSendEvent,ICanRegisterEvent,ICanGetSystem{ 
        void Init();
    }

    public abstract class AbstractSystem : ISystem {
        private IArchitecture mArchitecture = null;
        IArchitecture IBelongToArchitecture.GetArchitecture() {
            return mArchitecture;
        }
        void ICanSetArchitecture.SetArchitecture(IArchitecture architecture)
        {
            mArchitecture = architecture;
        }
        void ISystem.Init()
        {
            OnInit();
        }
        protected abstract void OnInit();
    }
}

