using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FrameworkDesign {
    public class UnRegisterOnDestroyTrigger : MonoBehaviour
    {
        private HashSet<IUnRegister> mUnRegister = new HashSet<IUnRegister>();

        public void AddUnRegister(IUnRegister unRegister) {
            mUnRegister.Add(unRegister);
        }

        private void OnDestroy()
        {
            foreach (var unRegister in mUnRegister)
            {
                unRegister.UnRegister();
            }
            mUnRegister.Clear();
        }
    }

}
