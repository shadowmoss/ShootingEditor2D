using FrameworkDesign;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ShootingEditor2D {
    public class ShootCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            IGunSystem gunSystem = this.GetSystem<IGunSystem>();
            gunSystem.CurrentGun.BulletCountInGun.Value--;
            gunSystem.CurrentGun.State.Value = GunState.Shooting;

            var timeSystem = this.GetSystem<ITimeSystem>();
            timeSystem.AddDelayTask(0.33f, () =>
            {
                gunSystem.CurrentGun.State.Value = GunState.Idle;
            });
        }
    }

}
