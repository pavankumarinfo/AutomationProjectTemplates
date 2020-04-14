using System;

namespace CirclesSeleniumTestScripts
{
    public class UIBaseTest
    {
        public T GetInstance<T>()
        {
            return (T) Activator.CreateInstance(typeof(T));
        }
    }
}
