
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using UiTest.Common;

namespace UiTest.Functions
{
    public abstract class CoverManagement<T>
    {
        protected readonly ConcurrentDictionary<string, BaseCover<T>> covers;
        protected CancellationTokenSource cts;

        protected CoverManagement()
        {
            covers = new ConcurrentDictionary<string, BaseCover<T>>();
        }
        public void Reset()
        {
            if (cts?.IsCancellationRequested == false)
            {
                cts?.Cancel();
            }
            cts = new CancellationTokenSource();
            IsHaveCancelled = false;
            IsHaveFailled = false;
        }

        public event Action CancelRunEvent;
        public bool IsHaveCancelled {  get; protected set; }
        public bool IsPass => !IsHaveFailled && !IsHaveCancelled;
        public bool IsHaveFailled {  get; protected set; }
        public bool IsRunCancelled => cts?.IsCancellationRequested == true;
        public bool IsEmpty => covers.Count == 0;
        public bool TryAdd(string name, BaseCover<T> functionCover)
        {
            if (string.IsNullOrEmpty(name)) return false;
            return covers.TryAdd(name, functionCover);
        }

        public void SetTestDone(string name)
        {
            if (string.IsNullOrEmpty(name)) return;
            if (covers.TryRemove(name, out var cover))
            {
                switch (cover.Result)
                {
                    case TestResult.FAILED:
                        IsHaveFailled = true;
                        break;
                    case TestResult.CANCEL:
                        IsHaveCancelled = true;
                        break;
                }
            }
        }

        public void CancelAllTask()
        {
            cts?.Cancel();
            CancelRunEvent?.Invoke();
            var list = new List<BaseCover<T>>();
            while (covers.Count > 0)
            {
                list.Clear();
                list.AddRange(covers.Values);
                list.ForEach(x => x.Cancel());
                Thread.Sleep(500);
            }
        }

        public void WaitForAllTaskDone()
        {
            List<BaseCover<T>> list = new List<BaseCover<T>>();
            while (covers.Count > 0)
            {
                list.Clear();
                list.AddRange(covers.Values);
                Thread.Sleep(500);
            }
        }
        public bool TryAdd(BaseCover<T> eventCover)
        {
            if (eventCover == null) return false;
            return TryAdd($"{eventCover.GetHashCode()}", eventCover);
        }
        public void SetTestDone(BaseCover<T> eventCover)
        {
            if (eventCover == null) return;
            SetTestDone($"{eventCover.GetHashCode()}");
        }
    }
}
