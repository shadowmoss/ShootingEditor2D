using FrameworkDesign;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ShootingEditor2D {
    public class HurtPlayerCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            var playerModel =  this.GetModel<IPlayerModel>();
            playerModel.HP.Value--;

            if (playerModel.HP.Value <= 0) {
                SceneManager.LoadScene("GameOver");
            }
        }
    }
}

