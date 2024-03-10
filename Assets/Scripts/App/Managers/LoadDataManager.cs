using System.IO;
using App.Core;
using App.Managers.Interfaces;
using UnityEngine;

namespace App.Managers
{
    public class LoadDataManager : IService, ILoadDataManager
    {
        public void Init()
        {

        }

        public void Update()
        {
            
        }

        public T GetObjectByPath<T>(string path) where T : Object
        {
            return LoadFromResources<T>(path);
        }

        private T LoadFromResources<T>(string path) where T : Object
        {
            return Resources.Load<T>(path);
        }

        public void Dispose()
        {
          
        }
    }
}