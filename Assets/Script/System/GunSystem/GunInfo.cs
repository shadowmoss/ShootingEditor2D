using FrameworkDesign;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ShootingEditor2D {
    public enum GunState { 
        Idle,
        Shooting,
        Reload,
        EmptyBullet,
        CoolDown
    }
    public class GunInfo
    {
        public BindableProperty<int> BulletCountInGun;

        public BindableProperty<string> Name;

        public BindableProperty<GunState> State;

        public BindableProperty<int> BulletCountOutGun;

    }
}

