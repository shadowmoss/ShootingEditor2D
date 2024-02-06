using FrameworkDesign;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ShootingEditor2D {
    public class KillEnemyCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            this.GetSystem<IStatSystem>().KillCount.Value++;

            var randomIndex = Random.Range(0, 100);
            if (randomIndex < 80) {
                this.GetSystem<IGunSystem>().CurrentGun.BulletCount.Value += Random.Range(1, 4);
            }
        }
    }
}

