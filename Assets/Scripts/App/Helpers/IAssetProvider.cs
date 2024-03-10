using UnityEngine;

namespace App.Gameplay.Handlers
{
    public interface IAssetProvider
    {
        GameObject Instantiate(string path, Vector3 at);
        GameObject Instantiate(string path);
    }
}