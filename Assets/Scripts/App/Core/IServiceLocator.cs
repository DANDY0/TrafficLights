namespace App.Core
{
    public interface IServiceLocator
    {
        T GetService<T>();
        void Update();
    }
}