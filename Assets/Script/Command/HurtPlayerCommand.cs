using FrameworkDesign;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootingEditor2D {
    public class HurtPlayerCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            this.GetModel<IPlayerModel>().HP.Value--;
        }
    }
}

