using System.Collections.Generic;
using App.Gameplay.Controllers;
using App.Managers.Interfaces;
using App.Settings;
using UnityEngine;

namespace App.Gameplay.GameEntities
{
    public class Road: GameObjectBase
    {
        public List<TrafficControlPoint> TrafficControlPoints { get; } = new List<TrafficControlPoint>();

        private IGameplayManager _gameplayManager;
        private IGameFactoryManager _gameFactoryManager;

        public Road(GameObject selfObject): base(selfObject)
        {
            _gameFactoryManager = GameClient.Get<IGameFactoryManager>();
            _gameplayManager = GameClient.Get<IGameplayManager>();
        }

        public void AddControlPoint(Enumerators.RoadDirection roadDirection, Transform parent)
        {
            var controlPoint = _gameFactoryManager.CreateControlPoint(parent, roadDirection);
            controlPoint.SetupPoint(_gameplayManager.GameplayData.controlPointsData);
            TrafficControlPoints.Add(controlPoint);
        }
    }
}