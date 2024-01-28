using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrameworkDesign
{
    // һ���������ݰ󶨵�ר�÷�����.
    // ����+���ݱ���¼��ļ����塣
    public class BindableProperty<T> where T :IEquatable<T>
    {
        private T mValue;

        // �����ݱ仯ʱ,�����ⲿί�д��롣
        public T Value { 
            get => mValue;
            set
            {
                if(!mValue.Equals(value))
                {
                    mValue = value;
                    mOnValueChanged?.Invoke(value);
                }
            }
        }

        private Action<T> mOnValueChanged;

        public IUnRegister RegisterOnValueChanged(Action<T> onValueChanged) {
            mOnValueChanged += onValueChanged;
            return new BindablePropertyUnRegister<T>()
            {
                BindableProperty = this,
                OnValueChanged = onValueChanged
            };
        }

        public void UnRegisterOnValueChanged(Action<T> onValueChanged) {
            mOnValueChanged -= onValueChanged;
        }
    }
}

