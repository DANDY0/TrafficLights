using System;
using App.Gameplay.Controllers;
using App.Settings;

namespace App.Gameplay.TrafficLightStateMachine.Interfaces
{
    public interface ITrafficLightStateMachine
    {
        event Action<Enumerators.TrafficLightState> StateChangedEvent;
        ITrafficLightState CurrentState { get; set; }
        void Update();
        void Dispose();
    }
}