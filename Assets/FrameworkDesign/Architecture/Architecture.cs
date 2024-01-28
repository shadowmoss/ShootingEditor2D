using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
namespace FrameworkDesign {

    public interface IArchitecture {
        #region ϵͳģ����ؽӿ�
        // ע��ϵͳ��API
        void RegisterSystem<T>(T instance) where T : ISystem;
        // �ṩһ����ȡISystem��API
        T GetSystem<T>() where T : class, ISystem;
        #endregion
        #region ģ�Ͷ�����ؽӿ�
        // �ṩһ��ע��IModel��API
        void RegisterModel<T>(T instance) where T : IModel;
        // �ṩһ�����Ի�ȡIModel��APi
        T GetModel<T>() where T : class, IModel;
        #endregion
        #region ������ؽӿ�
        // �ṩһ��ע��Utility��API
        void RegisterUtility<T>(T instance);

        // �ṩһ����ȡUtility��API
        T GetUtility<T>() where T :class;
        #endregion
        #region ������ؽӿ�
        // ����ָ��
        void SendCommand<T>() where T:ICommand,new();

        // ����ָ��
        void SendCommand<T>(T command) where T : ICommand;
        #endregion
        #region �¼��ӿ�
        // �й��¼��Ľӿ�
        // ע��
        IUnRegister RegisterEvent<T>(Action<T> onEvent);
        // ע��
        void UnRegisterEvent<T>(Action<T> onEvent);
        // �����¼����ͷ����¼�
        void SendEvent<T>() where T : new();
        // �û��Զ����¼����ͷ����¼�
        void SendEvent<T>(T e);
        #endregion
    }
    public abstract class Architecture<T> : IArchitecture where T : Architecture<T>,new()
    {
        // �����л�ע���ģ���ί��,�����T�����ܶ�����,Ҳ����һ����Ҫ����Architecutre���ͷ���ֵΪvoid��ί��
        public static Action<T> OnRegisterPatch = architecture => {};
        // ����ȷ�ϵ�ǰ����Ƿ��ʼ�����
        private bool mInited = false;

        // ���ڳ�ʼ��IModel�Ļ������
        List<IModel> mModels = new List<IModel>();

        // ���ڳ�ʼ��ISystem�Ļ������
        List<ISystem> mSystems = new List<ISystem>();

        #region ���Ƶ���ģʽ ���ǽ����ڲ�����ʹ��
        private static T mArchitecture = null;

        // ȷ��Container����ʵ����
        static void MakeSureArchitecture() {
            if (mArchitecture == null) {
                mArchitecture = new T();
                // ����ִֻ��ע�ᣬ��ִ�и���ģ�����ȫ��ʼ����
                mArchitecture.Init();

                OnRegisterPatch?.Invoke(mArchitecture);

                // ��ʼ��IModel
                foreach (IModel architectureModel in mArchitecture.mModels) { 
                    architectureModel.Init();
                }

                // ��ջ����IModel����
                mArchitecture.mModels.Clear();

                // ��ʼ��ISystem
                foreach (ISystem architectureSystem in mArchitecture.mSystems) {
                    architectureSystem.Init();
                }
                mArchitecture.mSystems.Clear();
                // �޸ı�־λ��ʾ��ʼ�����
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

        // ��������ע��ģ��
        protected abstract void Init();
        #region ��̬��ע���ȡAPI
        // �ṩһ��ע��ģ���API,�����Ǿ�̬��
        public static void Register<T>(T instance) { 
            MakeSureArchitecture();
            mArchitecture.mContainer.Register<T>(instance);
        }
        #endregion

        #region �Ǿ�̬��API
        #region Modelģ����ؽӿ�
        // �ṩһ��ע��Model��API
        public void RegisterModel<T>(T instance) where T : IModel { 
            // ��ÿ��ע�����ܵ�Model���е�ǰ��ܶ���
            instance.SetArchitecture(mArchitecture);
            // ��Modelע������
            mContainer.Register<T>(instance);

            // �����ʼ������
            if (mInited)
            {
                instance.Init();
            }
            else {
                // ��ӵ�Model������,���ڳ�ʼ��
                mModels.Add(instance);
            }
        }
        // �ṩһ����ȡIModel��ķ���
        public T GetModel<T>() where T : class ,IModel
        {
            return mContainer.Get<T>();
        }
        #endregion
        #region ����ģ����ؽӿ�
        // ע�Ṥ��ģ���API
        public void RegisterUtility<T>(T instance)
        {
            mContainer.Register<T>(instance);
        }
        // ��ȡ����ģ���API
        public T GetUtility<T>() where T : class
        {
            return mContainer.Get<T>();
        }
        #endregion
        #region ϵͳģ����ؽӿ�
        // ע��ϵͳ��API
        public void RegisterSystem<T>(T instance) where T : ISystem
        {
            // System���Model���࣬�п��ܻ��ȡModel����Utility��.������Ҫ����architecture�����ֹ�ݹ����
            instance.SetArchitecture(this);

            mContainer.Register<T>(instance);

            // �����ǰ��ܳ�ʼ������
            if (mInited)
            {
                instance.Init();
            }
            else { 
                // ����ǰSystem��ӵ������б���
                mSystems.Add(instance);
            }
        }
        // �ṩһ����ȡISystem��API
        public T GetSystem<T>() where T :class,ISystem
        {
            return mContainer.Get<T>();
        }
        #endregion
        #region ����ģ����ؽӿ�
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
        #region �¼�ģ����ؽӿ�

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
