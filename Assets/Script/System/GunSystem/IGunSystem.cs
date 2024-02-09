using FrameworkDesign;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ShootingEditor2D {
    public interface IGunSystem : ISystem
    {
        GunInfo CurrentGun { get; }
    }
    public class GunSystem : AbstractSystem, IGunSystem
    {
        public GunSystem() {
            Console.WriteLine("初始化枪械系统");
        }
        public GunInfo CurrentGun { get; } = new GunInfo()
        {
            BulletCountInGun = new BindableProperty<int>()
            {
                Value = 3
            },
            BulletCountOutGun = new BindableProperty<int>()
            {
                Value = 1
            },
            State = new BindableProperty<GunState>
            {
                Value = GunState.Idle
            },
            Name = new BindableProperty<string>
            {
                Value = "pistol"
            }
        };

        protected override void OnInit()
        {
            
        }
    }
}

