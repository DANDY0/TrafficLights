using System;
using System.Collections.Generic;
using App.Settings;
using UnityEngine;

namespace App.Gameplay.GameEntities
{
    public class TrafficLight: GameObjectBase
    {
        public Enumerators.TrafficLightState State { get; private set; }
        private Dictionary<Enumerators.TrafficLightColor, GameObject> _colors = new Dictionary<Enumerators.TrafficLightColor, GameObject>();

        public TrafficLight(GameObject selfObject): base(selfObject)
        {
            _colors.Add(Enumerators.TrafficLightColor.Red, SelfObject.transform.Find("RedLight").gameObject);
            _colors.Add(Enumerators.TrafficLightColor.Yellow, SelfObject.transform.Find("YellowLight").gameObject);
            _colors.Add(Enumerators.TrafficLightColor.Green, SelfObject.transform.Find("GreenLight").gameObject);
        }

        public void SetState(Enumerators.TrafficLightState state)
        {
            State = state;
            SetColors();
        }

        private void SetColors()
        {
            foreach (var color in _colors) 
                color.Value.SetActive(false);
            
            switch(State)
            {
                case Enumerators.TrafficLightState.Red:
                    _colors[Enumerators.TrafficLightColor.Red].SetActive(true);
                    break;
                case Enumerators.TrafficLightState.RedAmber:
                    _colors[Enumerators.TrafficLightColor.Red].SetActive(true);
                    _colors[Enumerators.TrafficLightColor.Yellow].SetActive(true);
                    break;
                case Enumerators.TrafficLightState.Amber:
                    _colors[Enumerators.TrafficLightColor.Yellow].SetActive(true);
                    break;
                case Enumerators.TrafficLightState.Green:
                    _colors[Enumerators.TrafficLightColor.Green].SetActive(true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}