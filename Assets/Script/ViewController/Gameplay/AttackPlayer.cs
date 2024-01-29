using FrameworkDesign;
using UnityEngine;
namespace ShootingEditor2D {
    public class AttackPlayer : MonoBehaviour,IController
    {
        public int Hurt = 1;

        public IArchitecture GetArchitecture()
        {
            return ShootingEditor2D.Instance;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player")) {
                this.SendCommand<HurtPlayerCommand>();
            }
        }
    }
}
