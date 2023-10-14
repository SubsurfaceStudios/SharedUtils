using System.Collections;
using SubsurfaceStudios.Attributes;
using SubsurfaceStudios.Utilities.Async;
using UnityEngine;
using UnityEngine.Events;

namespace SubsurfaceStudios.Utilities.Time
{
    [System.Serializable]
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
            onTimerStart?.Invoke(currentTime);

            StaticCoroutineHandler.StartCoroutineStatic(Timer());
        }

        public IEnumerator Timer()
        {
            while (currentTime < duration && isRunning)
            {
                currentTime += Time.deltaTime;
                onTimerTick?.Invoke(currentTime);
                yield return null;
            }
            
            isRunning = false;
            onTimerStop?.Invoke(currentTime);
        }

        public void Reset()
        {
            currentTime = 0f;
        }

        public void Stop()
        {
            isRunning = false;
            onTimerStop?.Invoke(currentTime);
        }
    }
}