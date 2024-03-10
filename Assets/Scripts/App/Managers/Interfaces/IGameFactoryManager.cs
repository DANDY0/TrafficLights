using App.Gameplay.Controllers;
using App.Gameplay.GameEntities;
using App.Settings;
using UnityEngine;

namespace App
{
    public interface IGameFactoryManager
    {
        public Character CreateCharacter(TrafficControlPoint trafficControlPoint, Transform levelParent);
        public TrafficControlPoint CreateControlPoint(Transform parent, Enumerators.RoadDirection roadDirection);

    }
}