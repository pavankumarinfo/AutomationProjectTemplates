﻿using System;

namespace CommandChainFramework.BusinessModel
{
    public class BusinessBase
    {
        public T GetInstance<T>()
        {
            return (T)Activator.CreateInstance(typeof(T));
        }
    }
}
