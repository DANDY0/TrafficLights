using System;
using App.Core;
using App.Gameplay.Model;

namespace App.Managers.Interfaces
{
    public interface IGameplayManager
    {
        event Action GameplayStartedEvent;
        event Action GameplayEndedEvent;

        bool IsGameplayStarted { get; }
        GameplayData GameplayData { get; }

        T GetController<T>() where T : IController;

        void StartGameplay();
        void StopGameplay();
    }
}