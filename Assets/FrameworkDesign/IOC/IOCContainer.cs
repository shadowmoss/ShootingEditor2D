using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrameworkDesign {
    public class IOCContainer
    {
        // IOC������һ����TypeΪ������Ӧ��objectʵ��Ϊֵ���ֵ伯��
        public Dictionary<Type,object> mInstances = new Dictionary<Type, object> ();

        // ������ע�ᵽIOC����
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

        // ��IOC�����л�ȡ����
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
