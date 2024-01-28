using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrameworkDesign {
    public class IOCContainer
    {
        // IOC容器，一个以Type为键，对应的object实例为值的字典集合
        public Dictionary<Type,object> mInstances = new Dictionary<Type, object> ();

        // 将对象注册到IOC容器
        public void Register<T>(T instance) {
            var key = typeof(T);

            if (mInstances.ContainsKey(key))
            {
                mInstances[key] = instance;
            }
            else {
                mInstances.Add(key,instance);
            }
        }

        // 从IOC容器中获取对象。
        public T Get<T>() where T : class { 
            var key = typeof(T);
            object retObj;
            if (mInstances.TryGetValue(key, out retObj)) {
                return retObj as T;
            }
            return null;
        }
    }

}
