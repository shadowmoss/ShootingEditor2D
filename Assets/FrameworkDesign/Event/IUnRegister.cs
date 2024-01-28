using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FrameworkDesign {
    // 监听注销类操作接口
    public interface IUnRegister
    {
        void UnRegister();
    }

    public static class UnRegisterExtension
    {
        public static void UnRegisterWhenGameObjectDestroyed(this IUnRegister unRegister, GameObject gameObject) {
            var trigger = gameObject.GetComponent<UnRegisterOnDestroyTrigger>();
            // 如果该GameObject不存在监听注销器组件。
            // 为其添加一个
            if (!trigger) { 
               trigger = gameObject.AddComponent<UnRegisterOnDestroyTrigger>();
            }
            // 将当前监听注销对象放入监听注销器组件的HashSet集合当中
            trigger.AddUnRegister(unRegister);
        }
    }

}

