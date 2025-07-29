
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UiTest.Functions;

namespace UiTest.Model.Cell
{
    public class FucntionCoverManagement
    {
        private readonly ConcurrentDictionary<string, FunctionCover> functionCovers;
        private CancellationTokenSource cts;
        private bool isAcceptable;

        public FucntionCoverManagement()
        {
            functionCovers = new ConcurrentDictionary<string, FunctionCover>();
            isAcceptable  = true;
        }
        public CancellationTokenSource Cts {  get { return cts; } set { cts = value; } }
        public bool IsAcceptable { get { return isAcceptable && (cts == null || !cts.IsCancellationRequested); } set { isAcceptable = value; } }
        public bool IsEmpty => functionCovers.Count == 0;

        public bool TryAdd(string name, FunctionCover functionCover)
        {
            if (string.IsNullOrEmpty(name)) return false;
            return functionCovers.TryAdd(name, functionCover);
        }

        public void SetTestDone(string name)
        {
            if (string.IsNullOrEmpty(name)) return;
            if(functionCovers.TryRemove(name, out var functionCover) && !functionCover.IsAcceptable)
            {
                IsAcceptable = false;
            }
        }

        public async Task StopAll()
        {
            List<FunctionCover> list = new List<FunctionCover>();
            while (functionCovers.Count > 0 && (Cts == null || !Cts.Token.IsCancellationRequested))
            {
                list.Clear();
                list.AddRange(functionCovers.Values);
                list.ForEach(x => x.Stop());
                await Task.Delay(50);
            }
        }

        public async Task WaitForAllTaskDone()
        {
            List<FunctionCover> list = new List<FunctionCover>();
            while (functionCovers.Count > 0 && (Cts == null || !Cts.Token.IsCancellationRequested))
            {
                list.Clear();
                list.AddRange(functionCovers.Values);
                if (list.All(f => f.Config.IsDontWaitForMe))
                {
                    break;
                }
                await Task.Delay(50);
            }
        }
    }
}
