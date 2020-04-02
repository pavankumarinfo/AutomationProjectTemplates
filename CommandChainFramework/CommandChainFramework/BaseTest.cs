using System;

namespace CommandChainFramework.UnitTest
{
    public class BaseTest
    {
        public T GetInstance<T>()
        {
            return (T) Activator.CreateInstance(typeof(T));
        }
    }
}
