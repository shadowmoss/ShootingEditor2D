using FrameworkDesign;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootingEditor2D {
    public interface ITimeSystem : ISystem
    {
        float CurrentSeconds { get; }

        void AddDelayTask(float seconds, Action onFinish);
    }

    public enum DelayTaskState { 
        NotStart,
        Started,
        Finish
    }
    public class  DelayTask
    {
        public float Seconds { get; set; }
        public Action OnFinish { get; set; }
        public float StartSeconds { get; set; }
        public float FinishSeconds { get; set; }
        public DelayTaskState State { get; set; }

    }

    public class TimeSystem : AbstractSystem, ITimeSystem
    {

        public class TimeSystemUpdateBehaviour : MonoBehaviour {
            public event Action OnUpdate;
            private void Update()
            {
                OnUpdate?.Invoke();
            }
        }
        public float CurrentSeconds { get; set; } = 0.0f;

        private LinkedList<DelayTask> mDelayTasks = new LinkedList<DelayTask>();

        private void OnUpdate() {
            CurrentSeconds += Time.deltaTime;

            if (mDelayTasks.Count > 0) {
                LinkedListNode<DelayTask> currentNode = mDelayTasks.First;
                while (currentNode != null)
                {
                    DelayTask delayTask = currentNode.Value;
                    // 缓存一下下一个节点
                    LinkedListNode<DelayTask> nextNode = currentNode.Next;
                    // 如果当前节点的延时任务状态未开始，将其改为开始状态
                    if (delayTask.State == DelayTaskState.NotStart)
                    {
                        delayTask.State = DelayTaskState.Started;

                        delayTask.StartSeconds = CurrentSeconds;

                        delayTask.FinishSeconds = CurrentSeconds + delayTask.Seconds;
                    }
                    // 如果当前节点任务状态已开始,比较时间判断是否结束,结束则执行任务,移除当前节点。
                    else if (delayTask.State == DelayTaskState.Started) { 
                        if(CurrentSeconds > delayTask.FinishSeconds)
                        {
                            delayTask.State = DelayTaskState.Finish;
                            delayTask.OnFinish();
                            delayTask.OnFinish = null;
                            mDelayTasks.Remove(currentNode);
                        }
                    }
                    currentNode = nextNode;
                }

            }
        }

        public void AddDelayTask(float seconds, Action onFinish)
        {
            DelayTask delayTask = new DelayTask() {
                Seconds = seconds,
                OnFinish = onFinish,
                State = DelayTaskState.NotStart,
            };
            mDelayTasks.AddLast(new LinkedListNode<DelayTask>(delayTask));
        }

        protected override void OnInit()
        {
            var updateBehaviourGameObj = new GameObject(nameof(TimeSystemUpdateBehaviour));

            UnityEngine.Object.DontDestroyOnLoad(updateBehaviourGameObj);

            // 如果需要销毁，可以缓存为成员变量
            var updateBehaviour = updateBehaviourGameObj.AddComponent<TimeSystemUpdateBehaviour>();

            updateBehaviour.OnUpdate += OnUpdate;
        }
    }
}

