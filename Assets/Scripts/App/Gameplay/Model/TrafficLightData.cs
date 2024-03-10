using System;
using System.Collections.Generic;
using App.Settings;
using UnityEngine;

namespace App.Gameplay.Model
{
    [CreateAssetMenu(fileName = "TrafficLightData", menuName = "TrafficSystem/TrafficLightData")]
    public class TrafficLightData : ScriptableObject
    {
        public List<TrafficStateData> trafficStatesData;
    }

    [Serializable]
    public class TrafficStateData
    {
        public float Duration;
        public Enumerators.TrafficLightState TrafficLightState;
    }
}