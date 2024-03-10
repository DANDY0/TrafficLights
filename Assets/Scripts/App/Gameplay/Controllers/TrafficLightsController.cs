using System;
using System.Collections.Generic;
using System.Linq;
using App.Core;
using App.Gameplay.GameEntities;
using App.Gameplay.Model;
using App.Gameplay.TrafficLightStateMachine.Interfaces;
using App.Managers.Interfaces;
using App.Settings;
using UnityEngine;

namespace App.Gameplay.Controllers
{
    public class TrafficLightsController: IController
    {
        private IGameplayManager _gameplayManager;
        private ILoadDataManager _loadDataManager;
        private CrossRoadEntitiesController _crossRoadEntitiesController;
        
        private ITrafficLightStateMachine _mainTrafficLightStateMachine;
        private ITrafficLightStateMachine _secondaryTrafficLightStateMachine;

        private List<TrafficControlPoint> _trafficControlPoints;
        private Dictionary<Enumerators.TrafficLightState, ITrafficLightState> _trafficLightStates = new Dictionary<Enumerators.TrafficLightState, ITrafficLightState>();

        readonly Enumerators.TrafficLightState[] _stateOrder = new[]
        {
            Enumerators.TrafficLightState.Red,
            Enumerators.TrafficLightState.RedAmber,
            Enumerators.TrafficLightState.Amber,
            Enumerators.TrafficLightState.Green
        };
        
        public TrafficLightsController()
        {
            _loadDataManager = GameClient.Get<ILoadDataManager>();
        }

        public void Init()
        {
            _gameplayManager = GameClient.Get<IGameplayManager>();
            _crossRoadEntitiesController = _gameplayManager.GetController<CrossRoadEntitiesController>();
            _trafficControlPoints = _crossRoadEntitiesController.AllTrafficPoints;

            Dictionary<Enumerators.TrafficLightState, float> lightDurations = LoadTrafficStateData();
    
            SetupTrafficLightStateMachines(lightDurations);
        }


        public void Update()
        {
            if(!_gameplayManager.IsGameplayStarted)
                return;
         
            _mainTrafficLightStateMachine.Update();
            _secondaryTrafficLightStateMachine.Update();
        }

        public void SubscribeOnCharacterState(Enumerators.RoadDirection characterRoadDirection, Action<Enumerators.TrafficLightState> callback)
        {
            switch (characterRoadDirection)
            {
                case Enumerators.RoadDirection.Forward:
                case Enumerators.RoadDirection.Backward:
                    _mainTrafficLightStateMachine.StateChangedEvent += callback;
                    break;
                case Enumerators.RoadDirection.Left:
                case Enumerators.RoadDirection.Right:
                    _secondaryTrafficLightStateMachine.StateChangedEvent += callback;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(characterRoadDirection), characterRoadDirection, null);
            }
        }
        
        public void UnSubscribeOnCharacterState(Enumerators.RoadDirection characterRoadDirection, Action<Enumerators.TrafficLightState> callback)
        {
            switch (characterRoadDirection)
            {
                case Enumerators.RoadDirection.Forward:
                case Enumerators.RoadDirection.Backward:
                    _mainTrafficLightStateMachine.StateChangedEvent -= callback;
                    break;
                case Enumerators.RoadDirection.Left:
                case Enumerators.RoadDirection.Right:
                    _secondaryTrafficLightStateMachine.StateChangedEvent -= callback;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(characterRoadDirection), characterRoadDirection, null);
            }
        }

        public void SetTrafficLights(Enumerators.TrafficLightState roadState, bool isMain)
        {
            var relevantDirections = isMain
                ? new[] { Enumerators.RoadDirection.Forward, Enumerators.RoadDirection.Backward }
                : new[] { Enumerators.RoadDirection.Right, Enumerators.RoadDirection.Left };

            foreach (var tcp in _trafficControlPoints.Where(tcp => relevantDirections.Contains(tcp.RoadDirection)))
                tcp.TrafficLight.SetState(roadState);
        }

        public void Dispose()
        {
        }

        public void ResetAll()
        {
        }

        private Dictionary<Enumerators.TrafficLightState, float> LoadTrafficStateData()
        {
            List<TrafficStateData> trafficStateData = _loadDataManager.GetObjectByPath<TrafficLightData>(AssetPath.TrafficLightConfig).trafficStatesData;
            Dictionary<Enumerators.TrafficLightState, float> lightDurations = new Dictionary<Enumerators.TrafficLightState, float>();
    
            foreach (var data in trafficStateData) 
                lightDurations.Add(data.TrafficLightState, data.Duration);

            return lightDurations;
        }

        private void SetupTrafficLightStateMachines(Dictionary<Enumerators.TrafficLightState, float> lightDurations)
        {
            for (int i = 0; i < _stateOrder.Length; i++)
            {
                Enumerators.TrafficLightState currentState = _stateOrder[i];
                Enumerators.TrafficLightState nextState = _stateOrder[(i + 1) % _stateOrder.Length]; 
        
                _trafficLightStates.Add(currentState, new TrafficLightStateBase(lightDurations[currentState], currentState, nextState));
            }

            _mainTrafficLightStateMachine = new TrafficLightStateMachine.TrafficLightStateMachine(_trafficLightStates[Enumerators.TrafficLightState.Red], _trafficLightStates, true);
            _secondaryTrafficLightStateMachine = new TrafficLightStateMachine.TrafficLightStateMachine(_trafficLightStates[Enumerators.TrafficLightState.Green], _trafficLightStates, false);
        }
    }
}