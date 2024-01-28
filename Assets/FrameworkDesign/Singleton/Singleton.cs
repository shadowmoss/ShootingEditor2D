using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace FrameworkDesign {
    public class Singleton<T> where T : class 
    {
        public static T Instance
        {
            get
            {
                if (mInstance == null) { 
                    // ͨ�������ȡ���͹�����
                    var ctors = typeof(T).GetConstructors(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);

                    // ��ȡ�޲η�public�Ĺ���
                    var ctor = Array.Find(ctors, c => c.GetParameters().Length == 0);
                    if (ctor == null) {
                        throw new Exception("Non-Public Constructor() not found in " + typeof(T));
                    }

                    mInstance = ctor.Invoke(null) as T; 
                }
                return mInstance;
            }
        }
        private static T mInstance;
    }
}

