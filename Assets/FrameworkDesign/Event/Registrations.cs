using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FrameworkDesign {
    public class Registrations<T> : IRegistrations
    {
        // ���ܴ�ע�����ڼ����¼���ί��
        public Action<T> OnEvent = obj =>{};
    }
}

