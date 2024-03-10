using System;
using System.Collections.Generic;
using App.Gameplay.Controllers;
using App.Gameplay.TrafficLightStateMachine.Interfaces;
using App.Helpers;
using App.Managers.Interfaces;
using App.Settings;
using UnityEngine;

namespace App.Gameplay.TrafficLightStateMachine
{
    public class TrafficLightStateMachine : ITrafficLightStateMachine
    {
        public event Action<Enumerators.TrafficLightState> StateChangedEvent;
        public ITrafficLightState CurrentState { get; set; }

        private Timer _timer;
        private IGameplayManager _gameplayManager;
        private TrafficLightsController _trafficLightsController;
        private Dictionary<Enumerators.TrafficLightState, ITrafficLightState> _trafficLightStates = new Dictionary<Enumerators.TrafficLightState, ITrafficLightState>();
        private ITrafficLightState _initialState;

        private bool IsMainRoad { get; set; }

        public TrafficLightStateMachine(ITrafficLightState initialState, Dictionary<Enumerators.TrafficLightState,
            ITrafficLightState> trafficLightStates, bool isMainRoad)
        {
            IsMainRoad = isMainRoad;
            _initialState = initialState;
            _trafficLightStates = trafficLightStates;
            
            _gameplayManager = GameClient.Get<IGameplayManager>();
            _trafficLightsController = _gameplayManager.GetController<TrafficLightsController>();
            _timer = new Timer();

            _gameplayManager.GameplayStartedEvent += GameplayStartedHandler;
            _timer.TimerEndedEvent += TimerEndedEvent;
        }

        public void Update()
        {
            _timer.Update();
        }

        public void Dispose()
        {
            _gameplayManager.GameplayStartedEvent -= GameplayStartedHandler;
            _timer.TimerEndedEvent -= TimerEndedEvent;
        }

        private void GameplayStartedHandler()
        {
            TransitionToState(_initialState);
        }

        private void TimerEndedEvent()
        {
            TransitionToState(_trafficLightStates[CurrentState.NextState]);
        }

        private void TransitionToState(ITrafficLightState newState)
        {
            CurrentState = newState;
            newState.EnterState(_trafficLightsController, IsMainRoad);
            
            _timer.StartTimer(CurrentState.Duration);
            StateChangedEvent?.Invoke(newState.State);
        }
    }
}