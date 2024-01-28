using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FrameworkDesign {
    // 监听注销实现类
    public class TypeEventSystemUnRegister<T> : IUnRegister
    {
        // 持有事件管理类
        public ITypeEventSystem TypeEventSystem { get; set; }
        // 监听操作委托对象,当事件触发了需要做什么操作
        public Action<T> OnEvent { get; set; }
        // 监听注销方法
        public void UnRegister()
        {
            TypeEventSystem.UnRegister(OnEvent);
            TypeEventSystem = null;
            OnEvent = null;
        }
    }

}
