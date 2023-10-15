using System;
using System.Collections;
using SubsurfaceStudios.Attributes;
using SubsurfaceStudios.Utilities.Async;
using UnityEngine;
using UnityEngine.Events;

namespace SubsurfaceStudios.Utilities.Timers
{
    [Serializable]
    public class SimpleTimer
    {
        public float duration;
        
        [Header("Runtime")]
        [InspectorReadOnly] public bool isRunning = false;
        [InspectorReadOnly] public float currentTime = 0f;
        
        public UnityEvent<float> onTimerStart;
        public UnityEvent<float> onTimerTick;
        public UnityEvent<float> onTimerStop;

        public SimpleTimer(float duration)
        {
            this.duration = duration;
        }
        
        public void Start()
        {
            if (isRunning)
            {
                Debug.LogError("Timer already running!");
                return;
            }
            
            isRunning = true;
            Execute(onTimerStart, currentTime);

            StaticCoroutineHandler.StartCoroutineStatic(Timer());
        }

        private void Execute(UnityEvent<float> action, float value) {
            try {
                action?.Invoke(value);
            } catch {
                Debug.LogException(ex);
            }
        }

        public IEnumerator Timer()
        {
            while (currentTime < duration && isRunning)
            {
                currentTime += Time.deltaTime;
                Execute(onTimerTick, currentTime);
                yield return null;
            }
            
            isRunning = false;
            Execute(onTimerStop, currentTime);
        }

        public void Reset()
        {
            currentTime = 0f;
        }

        public void Stop()
        {
            isRunning = false;
            Execute(onTimerStop, currentTime);
        }
    }
}