namespace App.Managers.Interfaces
{
    public interface ILoadDataManager
    {
        T GetObjectByPath<T>(string path) where T : UnityEngine.Object;
    }
}