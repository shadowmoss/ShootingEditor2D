using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrameworkDesign {
    // Ϊ�˽��Modelģ����Architecture֮��ĳ�ʼ�����޵ݹ����⣬
    // ������Model��Architecture֮�以����и��ԵĶ���
    // Architectureͨ��ע�᷽������Model����
    // Modelͨ��IBelongToArchitecture��IArchitecture���Գ���Architecture����
    // ����Model�Ĺ�����������ȡArhictecture�����ʱ������ڣ���Modelע���Architecture����ʱModel�������л�ȡArchitecture��ȡ������
    // ��Ϊ��Arhictecture���󽻸�Model��������Model����Ĺ�������������֮��
    // �������:
    // ��Model�������ȫ��ʼ���Ӻ��Ƚ�Model����ע���Architecture����,����������Architecture����
    public interface IModel : IBelongToArchitecture,ICanSetArchitecture,ICanGetUtility,ICanSendEvent
    {
        // Model���͵ĳ�ʼ���������ڷ����������������ȫ�ĳ�ʼ����
        void Init();
    }
    public abstract class AbstractModel : IModel
    {
        private IArchitecture mArchitecture = null;
        IArchitecture IBelongToArchitecture.GetArchitecture()
        {
            return mArchitecture;
        }
        void ICanSetArchitecture.SetArchitecture(IArchitecture architecture)
        {
            mArchitecture = architecture;
        }
        void IModel.Init()
        {
            OnInit();
        }
        protected abstract void OnInit();
    }
}