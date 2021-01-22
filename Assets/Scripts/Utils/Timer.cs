using System;

    [Serializable]
    public class Timer
    {
        public float RemainingSeconds { get; private set; }
        public Action onTimerEnd;
        public bool TimerActive;
        private float duration;
        public Timer(float duration)
        {
            this.duration = duration;
            RemainingSeconds = duration;
            TimerActive = true;
        }
        public Timer(float duration, bool activeOnStart)
        {
            this.duration = duration;
            RemainingSeconds = duration;
            TimerActive = activeOnStart;
        }

        public void Tick(float deltaTime)
        {
            if(RemainingSeconds <= 0f) return;
            RemainingSeconds -= deltaTime;
            if (RemainingSeconds <= 0f)
            {
                TimerActive = false;
                onTimerEnd?.Invoke();
                ResetTimer();
            }
        }
        public void TickManualReset(float deltaTime)
        {
            if(RemainingSeconds <= 0f) return;
            RemainingSeconds -= deltaTime;
            if (RemainingSeconds <= 0f)
            {
                TimerActive = false;
                onTimerEnd?.Invoke();
            }
        }

        public void ResetTimer()
        {
            RemainingSeconds = duration;
            TimerActive = true;
        }
    }
