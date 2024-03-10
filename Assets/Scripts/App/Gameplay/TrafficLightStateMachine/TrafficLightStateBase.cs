using App.Settings;

namespace App.Gameplay.Controllers
{
    public class TrafficLightStateBase : ITrafficLightState
    {
        public float Duration { get; }
        public Enumerators.TrafficLightState State { get; }
        public Enumerators.TrafficLightState NextState { get; }

        public TrafficLightStateBase(float duration, Enumerators.TrafficLightState state, Enumerators.TrafficLightState nextState)
        {
            Duration = duration;
            State = state;
            NextState = nextState;
        }

        public void EnterState(TrafficLightsController controller, bool isMainRoad)
        {
            controller.SetTrafficLights(State, isMainRoad);
        }
    }
}