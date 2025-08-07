
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UiTest.Functions.ActionEvents;
using UiTest.Functions.TestFunctions.Config;

namespace UiTest.Functions
{
    public abstract class CoverManagement<T>
    {
        protected readonly ConcurrentDictionary<string, BaseCover<T>> covers;
        protected CancellationTokenSource cts;
        protected bool isAcceptable;

        protected CoverManagement()
        {
            covers = new ConcurrentDictionary<string, BaseCover<T>>();
            isAcceptable = true;
        }
        public CancellationTokenSource Cts { get { return cts; } set { cts = value; } }
        public bool IsAcceptable { get { return isAcceptable && (cts == null || !cts.IsCancellationRequested); } set { isAcceptable = value; } }
        public bool IsEmpty => covers.Count == 0;

        public bool TryAdd(string name, BaseCover<T> functionCover)
        {
            if (string.IsNullOrEmpty(name)) return false;
            return covers.TryAdd(name, functionCover);
        }

        public void SetTestDone(string name)
        {
            if (string.IsNullOrEmpty(name)) return;
            if (covers.TryRemove(name, out var functionCover) && !functionCover.IsAcceptable)
            {
                IsAcceptable = false;
            }
        }
        public void StopAll()
        {
            List<BaseCover<T>> list = new List<BaseCover<T>>();
            while (covers.Count > 0)
            {
                list.Clear();
                list.AddRange(covers.Values);
                list.ForEach(x => x.Stop());
                Thread.Sleep(500);
            }
        }
        public void CancelAll()
        {
            var list = new List<BaseCover<T>>();
            while (covers.Count > 0)
            {
                list.Clear();
                list.AddRange(covers.Values);
                list.ForEach(x =>x.Cancel());
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
