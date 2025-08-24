using System;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Progress
{
    
    /// <summary>
    /// The base progress class from which others will inherit. Implements basic functionality to track the current progress level, set a max progress threshold, and publish events for progress change and completion.
    /// </summary>
    /// <typeparam name="T">The numeric type to be implemented.</typeparam>
    public abstract class BaseProgress<T> where T : IComparable<T>
    {

        public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;
        public event EventHandler OnProgressCompleted;

        public class OnProgressChangedEventArgs : EventArgs
        {
            public float ProgressCompletedNormalized;
            public float ProgressRemainingNormalized;
        }

        private T _progress;

        public T Progress
        {
            get => _progress;
            private protected set
            {
                _progress = value;
                PublishProgressUpdate();
            }
        }
        
        public T MaxProgress { get; set; }

        public abstract void IncreaseProgress(T increaseValue);
        
        public void ResetProgress(T resetValue = default(T))
        {
            Progress = resetValue;
        }
        
        public bool IsProgressCompleted()
        {
            if (!IsMaxProgressInitialized())
            {
                return false;
            }
            return CompareProgress(Progress, MaxProgress) >= 0;
        }

        public bool HasProgress()
        {
            return Comparer<T>.Default.Compare(Progress, default) > 0;
        }

        public bool IsProgressCompletedNormalizedPastThreshold(float threshold)
        {
            return GetProgressCompletedNormalized() >= threshold;
        }

        public float GetProgressCompletedNormalized(bool clamp = false)
        {
            
            if (!IsMaxProgressInitialized())
            {
                return 0f;
            }

            float progressValue = Convert.ToSingle(Progress);
            float maxProgressValue = Convert.ToSingle(MaxProgress);
            float progressCompletedNormalized = progressValue / maxProgressValue;
            
            return clamp ? Mathf.Clamp01(progressCompletedNormalized) : progressCompletedNormalized;

        }

        private float GetProgressRemainingNormalized(bool clamp = false)
        {
            return 1 - GetProgressCompletedNormalized(clamp);
        }

        private int CompareProgress(T left, T right)
        {
            return left.CompareTo(right);
        }

        private void PublishProgressUpdate()
        {
            OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs
            {
                ProgressCompletedNormalized = GetProgressCompletedNormalized(),
                ProgressRemainingNormalized = GetProgressRemainingNormalized(),
            });
            if (IsProgressCompleted())
            {
                OnProgressCompleted?.Invoke(this, EventArgs.Empty);
            }
        }

        private bool IsMaxProgressInitialized()
        {
            return Comparer<T>.Default.Compare(MaxProgress, default) > 0;
        }

    }

    /// <summary>
    /// Tracks continuous (non-discrete) progress values, such as percentages.
    /// </summary>
    public class ContinuousProgress : BaseProgress<float>
    {

        public override void IncreaseProgress(float increaseValue)
        {
            Progress += increaseValue;
        }
        
    }

    /// <summary>
    /// Tracks progress related to time, such as a timer or clock.
    /// </summary>
    public class Timer : ContinuousProgress
    {
        
        public Timer(float maxProgress, float initialProgress = 0f)
        {
            MaxProgress = maxProgress;
            Progress = initialProgress;
        }

        public Timer()
        {
            MaxProgress = 0f;
            Progress = 0f;
        }

        public override void IncreaseProgress(float multiplier = 1f)
        {
            Progress += multiplier * Time.deltaTime;
        }
        
    }

    /// <summary>
    /// This class extends the <c>Timer</c> class by providing extended functionality that keeps track of a
    /// separate progress tracker with a "forgiveness tolerance."
    /// </summary>
    public sealed class CoyoteTimer : Timer
    {

        public event EventHandler OnCoyoteProgressCompleted;
        
        public float CoyoteMaxProgress { get; set; }

        public CoyoteTimer()
        {
            OnProgressChanged += ContinuousProgress_OnProgressChanged;
        }

        public bool IsCoyoteMaxProgressCompleted()
        {
            return Progress >= CoyoteMaxProgress;
        }

        private bool IsCoyoteMaxProgressInitialized()
        {
            return CoyoteMaxProgress > 0f;
        }

        private void ContinuousProgress_OnProgressChanged(object sender, OnProgressChangedEventArgs e)
        {
            
            if (!IsCoyoteMaxProgressInitialized())
            {
                return;
            }
            
            if (!IsCoyoteMaxProgressCompleted())
            {
                return;
            }
            
            OnCoyoteProgressCompleted?.Invoke(this, e);
            
        }
        
    }

    /// <summary>
    /// Tracks discrete progress, such as XP or number of steps taken.
    /// </summary>
    public sealed class DiscreteProgress : BaseProgress<int>
    {

        public override void IncreaseProgress(int increaseValue = 1)
        {
            Progress += increaseValue;
        }
        
    }
    
}