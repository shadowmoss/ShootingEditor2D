using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrameworkDesign {
    // 系统层是用来做一些可能临时存储状态相关的操作。
    // 与各层之间的通信关系
    // 表现层通过Command与System层和Model层进行通信
    // System层与Model层通过委托或者事件通知表现层
    // 表现层不与Utility层直接通信
    // 表现层可通过直接获取System层和Model层对象，获取相应的值状态
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

