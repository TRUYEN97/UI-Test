using System;
using System.Collections.Generic;
using System.Threading;
using UiTest.Config.Events;
using UiTest.Functions.Interface;
using UiTest.Service.Factory;

namespace UiTest.Functions.ActionEvents
{
    public class ActionEventRunner : BaseRunner<ActionEventCoverManagent, object>
    {
        private readonly List<ActionEventSetting> actionEvents;

        public ActionEventRunner()
        {
            coverManagement = new ActionEventCoverManagent();
            actionEvents = new List<ActionEventSetting>();
        }
        protected override bool IsRunnable()
        {
            return !IsRunning && ActionEvents?.Count > 0;
        }
        public bool IsAcceptable => coverManagement.IsAcceptable;
        public List<ActionEventSetting> ActionEvents { get => actionEvents; set { actionEvents.Clear(); if (value != null) actionEvents.AddRange(value); } }
        protected override void RunAction()
        {
            coverManagement.Cts = new CancellationTokenSource();
            coverManagement.IsAcceptable = true;
            foreach (var actionEvent in ActionEvents)
            {
                if (!coverManagement.IsAcceptable)
                {
                    break;
                }
                var functionBody = GetFunctionBody(actionEvent);
                var functionCover = new EventCover(functionBody, coverManagement);
                functionCover.Run();
            }
            coverManagement.WaitForAllTaskDone();
        }

        protected virtual IActionEvent GetFunctionBody(ActionEventSetting actionEvent)
        {
            return ActionEventFatory.Instance.CreateFunctionWith(actionEvent);
        }
    }
}
