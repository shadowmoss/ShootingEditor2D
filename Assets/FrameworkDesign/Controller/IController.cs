using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FrameworkDesign
{
    public interface IController : IBelongToArchitecture,ICanGetSystem,ICanGetModel,ICanSendCommand,ICanRegisterEvent
    { 
    }
}

