using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FrameworkDesign {
    public class Registrations<T> : IRegistrations
    {
        // 接受待注册用于监听事件的委托
        public Action<T> OnEvent = obj =>{};
    }
}

