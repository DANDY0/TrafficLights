using UnityEngine;

namespace App.Gameplay.GameEntities
{
    public class Character: GameObjectBase
    {
        public TrafficControlPoint TrafficLightsControlPoint { get; }

        public Character(GameObject selfObject, TrafficControlPoint trafficLightsControlPoint): base(selfObject)
        {
            TrafficLightsControlPoint = trafficLightsControlPoint;
        }

    }
}