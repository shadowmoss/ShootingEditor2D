using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootingEditor2D.Tests {
    public class TimeSystemTest : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            Debug.Log(Time.time);

            ShootingEditor2D.Instance.GetSystem<ITimeSystem>().AddDelayTask(3, () =>
            {
                Debug.Log(Time.time);
            });
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

