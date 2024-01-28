using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
namespace FrameworkDesign {

    public interface IArchitecture {
        #region 系统模块相关接口
        // 注册系统层API
        void RegisterSystem<T>(T instance) where T : ISystem;
        // 提供一个获取ISystem的API
        T GetSystem<T>() where T : class, ISystem;
        #endregion
        #region 模型对象相关接口
        // 提供一个注册IModel的API
        void RegisterModel<T>(T instance) where T : IModel;
        // 提供一个可以获取IModel的APi
        T GetModel<T>() where T : class, IModel;
        #endregion
        #region 工具相关接口
        // 提供一个注册Utility的API
        void RegisterUtility<T>(T instance);

        // 提供一个获取Utility的API
        T GetUtility<T>() where T :class;
        #endregion
        #region 命令相关接口
        // 发送指令
        void SendCommand<T>() where T:ICommand,new();

        // 发送指令
        void SendCommand<T>(T command) where T : ICommand;
        #endregion
        #region 事件接口
        // 有关事件的接口
        // 注册
        IUnRegister RegisterEvent<T>(Action<T> onEvent);
        // 注销
        void UnRegisterEvent<T>(Action<T> onEvent);
        // 根据事件类型发送事件
        void SendEvent<T>() where T : new();
        // 用户自定义事件类型发送事件
        void SendEvent<T>(T e);
        #endregion
    }
    public abstract class Architecture<T> : IArchitecture where T : Architecture<T>,new()
    {
        // 用于切换注册的模块的委托,这里的T代表框架对象本身,也就是一个需要传入Architecutre类型返回值为void的委托
        public static Action<T> OnRegisterPatch = architecture => {};
        // 用于确认当前框架是否初始化完成
        private bool mInited = false;

        // 用于初始化IModel的缓存队列
        List<IModel> mModels = new List<IModel>();

        // 用于初始化ISystem的缓存队列
        List<ISystem> mSystems = new List<ISystem>();

        #region 类似单例模式 但是仅在内部访问使用
        private static T mArchitecture = null;

        // 确保Container是有实例的
        static void MakeSureArchitecture() {
            if (mArchitecture == null) {
                mArchitecture = new T();
                // 这里只执行注册，不执行各个模块的完全初始化。
                mArchitecture.Init();

                OnRegisterPatch?.Invoke(mArchitecture);

                // 初始化IModel
                foreach (IModel architectureModel in mArchitecture.mModels) { 
                    architectureModel.Init();
                }

                // 清空缓存的IModel队列
                mArchitecture.mModels.Clear();

                // 初始化ISystem
                foreach (ISystem architectureSystem in mArchitecture.mSystems) {
                    architectureSystem.Init();
                }
                mArchitecture.mSystems.Clear();
                // 修改标志位表示初始化完成
                mArchitecture.mInited = true;
            }
        }
        #endregion

        public static IArchitecture Instance
        {
            get {
                if (mArchitecture == null) { 
                    MakeSureArchitecture();
                }
                return mArchitecture;
            }
        }

        private IOCContainer mContainer = new IOCContainer();

        // 留给子类注册模块
        protected abstract void Init();
        #region 静态的注册获取API
        // 提供一个注册模块的API,现在是静态的
        public static void Register<T>(T instance) { 
            MakeSureArchitecture();
            mArchitecture.mContainer.Register<T>(instance);
        }
        #endregion

        #region 非静态的API
        #region Model模块相关接口
        // 提供一个注册Model的API
        public void RegisterModel<T>(T instance) where T : IModel { 
            // 给每个注册进框架的Model持有当前框架对象
            instance.SetArchitecture(mArchitecture);
            // 将Model注册进框架
            mContainer.Register<T>(instance);

            // 如果初始化过了
            if (mInited)
            {
                instance.Init();
            }
            else {
                // 添加到Model缓存中,用于初始化
                mModels.Add(instance);
            }
        }
        // 提供一个获取IModel层的方法
        public T GetModel<T>() where T : class ,IModel
        {
            return mContainer.Get<T>();
        }
        #endregion
        #region 工具模块相关接口
        // 注册工具模块的API
        public void RegisterUtility<T>(T instance)
        {
            mContainer.Register<T>(instance);
        }
        // 获取工具模块的API
        public T GetUtility<T>() where T : class
        {
            return mContainer.Get<T>();
        }
        #endregion
        #region 系统模块相关接口
        // 注册系统层API
        public void RegisterSystem<T>(T instance) where T : ISystem
        {
            // System层和Model层差不多，有可能会获取Model或者Utility层.所以需要持有architecture对象防止递归调用
            instance.SetArchitecture(this);

            mContainer.Register<T>(instance);

            // 如果当前框架初始化过了
            if (mInited)
            {
                instance.Init();
            }
            else { 
                // 将当前System添加到缓存列表当中
                mSystems.Add(instance);
            }
        }
        // 提供一个获取ISystem的API
        public T GetSystem<T>() where T :class,ISystem
        {
            return mContainer.Get<T>();
        }
        #endregion
        #region 命令模块相关接口
        public void SendCommand<T>() where T : ICommand, new()
        {
            T command = new T();
            command.SetArchitecture(this);
            command.Execute();
        }

        public void SendCommand<T>(T command) where T : ICommand
        {
            command.SetArchitecture(this);
            command.Execute();
        }
        #endregion
        #region 事件模块相关接口

        private ITypeEventSystem mTypeEventSystem = new TypeEventSystem();
        public IUnRegister RegisterEvent<T>(Action<T> onEvent)
        {
            return mTypeEventSystem.Register<T>(onEvent);
        }

        public void UnRegisterEvent<T>(Action<T> onEvent)
        {
            mTypeEventSystem.UnRegister<T>(onEvent);
        }

        public void SendEvent<T>() where T : new()
        {
            mTypeEventSystem.Send<T>();
        }

        public void SendEvent<T>(T e)
        {
            mTypeEventSystem.Send<T>(e);
        }
        #endregion
        #endregion
    }

}
