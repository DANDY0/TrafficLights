using App.Settings;

namespace App.Gameplay.Controllers
{
    public interface ITrafficLightState
    {
        public float Duration { get; }
        Enumerators.TrafficLightState State { get; }
        Enumerators.TrafficLightState NextState { get; }
        public void EnterState(TrafficLightsController controller, bool isMainRoad);
    }
}