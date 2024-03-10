using System.Collections.Generic;
using App.Gameplay.Controllers;
using App.Gameplay.Model;
using App.Settings;
using UnityEngine;

namespace App.Gameplay.GameEntities
{
    public class TrafficControlPoint: GameObjectBase
    {
        public Enumerators.RoadDirection RoadDirection { get; set; }
        public TrafficLight TrafficLight { get; private set; }
        public Transform SpawnPoint { get; private set; }
        
        public TrafficControlPoint(GameObject selfObject, TrafficLight trafficLight, Transform spawnPoint,
            Enumerators.RoadDirection roadDirection): base(selfObject)
        {
            RoadDirection = roadDirection;
            TrafficLight = trafficLight;
            SpawnPoint = spawnPoint;
        }

        public void SetupPoint(List<ControlPointData> controlPointsData)
        {
            var eulerAnglesValue = controlPointsData
                .Find(p=>p.Direction == RoadDirection).EulerAnglesValue;
            SelfObject.transform.localEulerAngles = eulerAnglesValue;
        }
    }
}