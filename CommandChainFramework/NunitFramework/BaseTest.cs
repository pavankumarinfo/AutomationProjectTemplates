using System;

namespace CommandChainFramework.Nunit
{
    public class BaseTest
    {
        public T GetInstance<T>()
        {
            return (T)Activator.CreateInstance(typeof(T));
        }
    }
}
