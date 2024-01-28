using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrameworkDesign {
    // 为了解决Model模块与Architecture之间的初始化无限递归问题，
    // 决定将Model和Architecture之间互相持有各自的对象。
    // Architecture通过注册方法持有Model对象
    // Model通过IBelongToArchitecture的IArchitecture属性持有Architecture对象
    // 但是Model的构造器方法获取Arhictecture对象的时间点早于，将Model注册进Architecture，此时Model构造其中获取Architecture获取不到，
    // 因为将Arhictecture对象交给Model持有是在Model对象的构造器方法调用之后。
    // 解决方法:
    // 将Model对象的完全初始化延后，先将Model对象注册进Architecture当中,并让它持有Architecture对象。
    public interface IModel : IBelongToArchitecture,ICanSetArchitecture,ICanGetUtility,ICanSendEvent
    {
        // Model类型的初始化生命周期方法，在这里进行完全的初始化。
        void Init();
    }
    public abstract class AbstractModel : IModel
    {
        private IArchitecture mArchitecture = null;
        IArchitecture IBelongToArchitecture.GetArchitecture()
        {
            return mArchitecture;
        }
        void ICanSetArchitecture.SetArchitecture(IArchitecture architecture)
        {
            mArchitecture = architecture;
        }
        void IModel.Init()
        {
            OnInit();
        }
        protected abstract void OnInit();
    }
}