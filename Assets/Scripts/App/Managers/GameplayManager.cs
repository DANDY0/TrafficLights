using System;
using System.Collections.Generic;
using App.Core;
using App.Gameplay.Controllers;
using App.Gameplay.Model;
using App.Managers.Interfaces;
using App.Settings;

namespace App.Managers
{
    public class GameplayManager : IService, IGameplayManager
    {
        public event Action GameplayStartedEvent;
        public event Action GameplayEndedEvent;
        private IGameFactoryManager _gameFactoryManager;
        
        public bool IsGameplayStarted { get; private set; }
        public GameplayData GameplayData { get; private set; }
        
        private ILoadDataManager _loadDataManager;
        private List<IController> _controllers = new List<IController>();

        public void Init()
        {
            _loadDataManager = GameClient.Get<ILoadDataManager>();
            
            GameplayData = _loadDataManager.GetObjectByPath<GameplayData>(AssetPath.GameplayData);

            _controllers = new List<IController>()
            {
                new CrossRoadEntitiesController(),
                new TrafficLightsController()
            };

            foreach (var item in _controllers)
                item.Init();
        }
        
        public void Update()
        {
            foreach (var item in _controllers)
                item.Update();
        }

        public T GetController<T>() where T : IController
        {
            foreach (var item in _controllers)
                if (item is T)
                    return (T)item;

            throw new Exception("Controller " + typeof(T).ToString() + " have not implemented");
        }

        public void StartGameplay()
		{
            if (IsGameplayStarted)
                return;

            IsGameplayStarted = true;

            GameplayStartedEvent?.Invoke();
        }

        public void StopGameplay()
		{
            if (!IsGameplayStarted)
                return;

            foreach (var item in _controllers)
                item.ResetAll();

            IsGameplayStarted = false;

            GameplayEndedEvent?.Invoke();
        }

        public void Dispose()
        {
            foreach (var item in _controllers)
                item.Dispose();
        }
    }
}