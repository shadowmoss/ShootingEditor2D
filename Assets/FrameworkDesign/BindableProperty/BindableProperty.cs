using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrameworkDesign
{
    // 一个用于数据绑定的专用泛型类.
    // 数据+数据变更事件的集合体。
    public class BindableProperty<T> where T :IEquatable<T>
    {
        private T mValue;

        // 在数据变化时,调用外部委托代码。
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

