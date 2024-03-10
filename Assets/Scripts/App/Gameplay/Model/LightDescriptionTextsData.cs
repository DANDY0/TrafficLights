using System;
using System.Collections.Generic;
using App.Settings;
using UnityEngine;

namespace App.Gameplay.Model
{
    [CreateAssetMenu(fileName = "LightDescriptionTexts", menuName = "LightDescriptionTexts")]
    public class LightDescriptionTextsData : ScriptableObject
    {
        public List<LightDescriptionText> lightDescriptionTexts;
    }

    [Serializable]
    public class LightDescriptionText
    {
        public Enumerators.TrafficLightState TrafficLightState;
        public string Description;
    }
}