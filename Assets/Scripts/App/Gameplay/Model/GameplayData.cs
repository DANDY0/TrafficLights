using System;
using System.Collections.Generic;
using App.Settings;
using UnityEngine;

namespace App.Gameplay.Model
{
    [CreateAssetMenu(fileName = "GameplayData", menuName = "GameplayData")]
    public class GameplayData : ScriptableObject
    {
        [Range(1,4)]
        public int controlPointsCount = 2;

        public List<ControlPointData> controlPointsData;
    }

    [Serializable]
    public class ControlPointData
    {
        public Enumerators.RoadDirection Direction;
        public Vector3 EulerAnglesValue;
    }
}