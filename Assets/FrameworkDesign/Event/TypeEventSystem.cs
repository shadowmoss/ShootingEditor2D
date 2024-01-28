using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FrameworkDesign {
    // 事件管理类
    public class TypeEventSystem : ITypeEventSystem
    {
        private Dictionary<Type,IRegistrations> mEventRegistrations = new Dictionary<Type,IRegistrations>();
        // 事件监听注册方法API
        // T类型为监听的对应的事件类型,监听方法接受事件对象进行处理
        public IUnRegister Register<T>(Action<T> onEvent)
        {
            Type type = typeof(T);
            IRegistrations eventRegistrations;
            // 查找当前字典集合当中是否存在,目标监听事件

            if (mEventRegistrations.TryGetValue(type, out eventRegistrations))
            {
                // 获取到了
                
            }   
            else {
                // 获取不到 新建一个监听接受对象
                eventRegistrations = new Registrations<T>();
                // 将目标事件类型和监听接收对象连接起来
                mEventRegistrations.Add(type, eventRegistrations);
            }
            // 将监听方法引用传给监听接收对象
            (eventRegistrations as Registrations<T>).OnEvent += onEvent;

            // 对应返回一个用于注销监听的对象,IUnRegister类型的扩展方法中已包含对应的注销操作UnRegisterWhenGameObjectDestroyed()方法
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
                // 获取到了待注销监听方法的目标方法，并取出了对应接受监听委托的对象。
                (eventRegistrations as Registrations<T>).OnEvent -= onEvent;
            }
        }
        // 直接通过类型发送事件API
        public void Send<T>() where T : new()
        {
            T triggeredEvent = new T();
            Send<T>(triggeredEvent);
        }
        // 触发事件的方法T类型标识对应的事件类型，事件类型对象当中存放需要传递给监听方法的处理参数
        // 用户自建事件对象进行事件触发。
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
