namespace CommandChainFramework.BusinessModel
{
    public class Base<T> where T : new()
    {
        public T GetNewInstance()
        {
            return new T();
        }
    }
}
