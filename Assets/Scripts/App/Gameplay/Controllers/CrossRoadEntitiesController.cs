using System;
using System.Collections.Generic;
using App.Core;
using App.Gameplay.GameEntities;
using App.Managers.Interfaces;
using App.Settings;
using UnityEngine;
using Random = UnityEngine.Random;

namespace App.Gameplay.Controllers
{
    public class CrossRoadEntitiesController : IController
    {
        public Character Character { get; private set; }
        public List<TrafficControlPoint> AllTrafficPoints { get; } = new List<TrafficControlPoint>();

        private IGameplayManager _gameplayManager;
        private IGameFactoryManager _gameFactoryManager;
        private ILoadDataManager _loadDataManager;
        
        private List<Road> _roads = new List<Road>();

        public CrossRoadEntitiesController()
        {
            _gameplayManager = GameClient.Get<IGameplayManager>();
            _gameFactoryManager = GameClient.Get<IGameFactoryManager>();
        }

        public void Init()
        {
            var levelObj = GameObject.FindWithTag("Level");
            var crossRoadObj = levelObj.transform.Find("CrossRoad").gameObject;
            
            foreach (Transform child in crossRoadObj.transform)
                if (child.CompareTag("Road"))
                    _roads.Add(new Road(child.gameObject));

            AddControlPoints(levelObj.transform);
            GetAllTrafficPoints();
            SetupCharacter(levelObj.transform);
        }

        public void Update()
        {
        }

        public void ResetAll()
        {
        }

        public void Dispose()
        {
        }

        private void SetupCharacter(Transform levelParent)
        {
            var randomTrafficLightIndex = Random.Range(0, AllTrafficPoints.Count);
            Character = _gameFactoryManager.CreateCharacter(AllTrafficPoints[randomTrafficLightIndex], levelParent);
        }

        private void GetAllTrafficPoints()
        {
            foreach (var road in _roads)
                foreach (var controlPoint in road.TrafficControlPoints) 
                     AllTrafficPoints.Add(controlPoint);
        }

        private void AddControlPoints(Transform levelParent)
        {
            for (int i = 0; i < (_gameplayManager.GameplayData.controlPointsCount); i++)
            {
                var indexToAdd = i % 2;

                Enumerators.RoadDirection direction =
                    (Enumerators.RoadDirection)Enum.Parse(typeof(Enumerators.RoadDirection), i.ToString());
                _roads[indexToAdd].AddControlPoint(direction, levelParent);
            }
        }
    }
}