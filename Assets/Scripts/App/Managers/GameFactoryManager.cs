using App.Core;
using App.Gameplay.Controllers;
using App.Gameplay.GameEntities;
using App.Settings;
using UnityEngine;

namespace App.Managers
{
    public class GameFactoryManager : IGameFactoryManager, IService
    {
        private AssetsProvider _assetsProvider;

        public GameFactoryManager()
        {
            _assetsProvider = new AssetsProvider();
        }
        public void Init()
        {
            
        }

        public void Update()
        {
        }

        public Character CreateCharacter(TrafficControlPoint trafficControlPoint, Transform levelParent)
        {
            var charObj = _assetsProvider.Instantiate(AssetPath.Character, trafficControlPoint.SpawnPoint.position,levelParent);
            
            Vector3 direction = levelParent.transform.position - charObj.transform.position;
            direction.y = 0;
            Quaternion rotation = Quaternion.LookRotation(direction);
            charObj.transform.rotation = rotation;
            
            Character character = new Character(charObj, trafficControlPoint);    
            return character;
        }

        public TrafficControlPoint CreateControlPoint(Transform parent, Enumerators.RoadDirection roadDirection)
        {
            var levelObj = _assetsProvider.Instantiate(AssetPath.TrafficControlPoint, parent);
            var trafficLight = new TrafficLight(levelObj.transform.Find("TrafficLight").gameObject);
            var spawnPos = levelObj.transform.Find("SpawnPos");
            TrafficControlPoint trafficControlPoint = new TrafficControlPoint(levelObj, trafficLight, spawnPos, roadDirection);
            
            return trafficControlPoint;
        }

        public void Dispose()
        {
        }
    }
}