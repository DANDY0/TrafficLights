using App.Gameplay.Handlers;
using UnityEngine;

namespace App.Managers
{
    public class AssetsProvider: IAssetProvider
    {
        public GameObject Instantiate(string path, Transform parent)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab, parent);
        }
        
        public GameObject Instantiate(string path, Vector3 at)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab, at, Quaternion.identity);
        }
        
        public GameObject Instantiate(string path, Vector3 at, Transform parent)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab, at , Quaternion.identity, parent);
        }

        public GameObject Instantiate(string path)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab);
        }
    }
}