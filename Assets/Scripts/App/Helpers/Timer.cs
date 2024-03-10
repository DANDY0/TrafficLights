using System;
using UnityEngine;

namespace App.Helpers
{
    public class Timer
    {
        public event Action TimerEndedEvent;
    
        private float _duration;
        private float _timeElapsed;
        private bool _isRunning;
    
        public void StartTimer(float duration)
        {
            _duration = duration;
            _timeElapsed = 0f;
            _isRunning = true;
        }

        public void StopTimer()
        {
            _isRunning = false;
        }
    
        public void Update()
        {
            if (!_isRunning) return;

            _timeElapsed += Time.deltaTime;
            if (_timeElapsed >= _duration)
            {
                StopTimer();
                TimerEndedEvent?.Invoke();
            }
        }
    }}